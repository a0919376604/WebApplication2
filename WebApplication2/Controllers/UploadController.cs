using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApplication2.Controllers
{
    public class UploadController : ApiController
    {
        [Route("api/Files/Upload")]
        public async Task<string> Post()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                if(httpRequest.Files.Count > 0)
                {
                    foreach(string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];

                        var folderName = postedFile.FileName.Split('\\').FirstOrDefault();
                        var fileName = postedFile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();

                        var folderPath = HttpContext.Current.Server.MapPath("~/Uploads/" + folderName);
                        var filePath = HttpContext.Current.Server.MapPath("~/Uploads/" + folderName+"/" + fileName);

                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        postedFile.SaveAs(filePath);

                        return "/Uploads/" + folderName + "/" + fileName;
                    }
                }
            }
            catch(Exception exception)
            {
                return exception.Message;
            }

            return "No files";
        }
    }
}