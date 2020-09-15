using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace Images_ASP_NET_MVC.Controllers
{
    public class ImageController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            const string imagePath = @"C:\Users\Andrew\Проектор\2020-08-02\Проповедь Савчук\1.jpg";
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new FileStream(imagePath, FileMode.Open));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            return response;
        }

        [HttpPost]
        public IHttpActionResult Post()
        {
            const string uploadPath = @"C:\Users\Andrew\Upload\";
            
            HttpRequest request = HttpContext.Current.Request;
            
            if (request.Files.Count == 1)
            {
                HttpPostedFile file = request.Files[0];
                
                string filePath = uploadPath + ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds() + "_" + file.FileName;
                file.SaveAs(filePath);
                
                return Ok();
            }

            return BadRequest("File count is " + request.Files.Count);
        }
    }
}
