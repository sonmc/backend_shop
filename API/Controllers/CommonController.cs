using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System; 
using System.IO; 
using System.Net.Http.Headers; 

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/common")]
    public class CommonController : Controller
    {
        [AllowAnonymous]
        [Route("upload")]
        [HttpPost, DisableRequestSizeLimit]
        public Response Upload()
        {
            Response res = new Response();
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("assets", "images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    res.Status = 1;
                    res.Data = dbPath;
                    res.Message = "Upload Success!";
                }
                else
                {
                    res.Status = 2;
                    res.Data = null;
                    res.Message = "Error!";
                }
            }
            catch (Exception ex)
            {
                res.Status = 2;
                res.Data = null;
                res.Message = "Error!";
            }
            return res;
        }
    }
}
