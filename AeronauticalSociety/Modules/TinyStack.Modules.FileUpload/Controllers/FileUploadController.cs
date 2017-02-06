using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using TinyStack.Modules.FileUpload.DataModels;




namespace TinyStack.Modules.FileUpload
{
    public class FileUploadApiController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage FileUpload()
        {
            HttpResponseMessage response = null;
            FileInfoResult _FileInfoResult = new FileInfoResult();
            // 检查是否是 multipart/form-data
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            try
            {

                string temp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp");
                //string temp = System.Web.Hosting.HostingEnvironment.MapPath(ConfigSetting.GetValue("KEY4001"));

                for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    FileDetail _FileDetail = new FileDetail();
                    HttpPostedFile file = HttpContext.Current.Request.Files[0];
                    Guid FileGuid = Guid.NewGuid();
                    var filePath = Path.Combine(temp, FileGuid.ToString(), Path.GetFileName(file.FileName));
                    Directory.CreateDirectory(Path.Combine(temp, FileGuid.ToString()));
                    file.SaveAs(filePath);
                    _FileDetail.FileID = FileGuid.ToString("N");
                    _FileDetail.FileName = Path.GetFileName(file.FileName);
                    _FileDetail.FileSize = file.ContentLength.ToString();
                    _FileDetail.UploadDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    _FileDetail.URL = FileGuid.ToString() + '/' + Path.GetFileName(file.FileName);
                    _FileInfoResult.FilesInfo.Add(_FileDetail);
                }
                _FileInfoResult.returnCode = 0;
                //return Request.CreateResponse(HttpStatusCode.OK);
                StringContent content = new StringContent(JsonConvert.SerializeObject(_FileInfoResult));
                response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = content;
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
