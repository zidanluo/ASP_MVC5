using log4net;
using PersonalWebApp.Attributes;
using PersonalWebApp.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PersonalWebApp.Controllers.Api
{
    public class UploadController : ApiController
    {
        private ILog log = LogManager.GetLogger(typeof(UploadController));
        //// GET: api/Upload
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Upload/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Upload
        public HttpResponseMessage Post(string uid, int roomid, string timestamp)
        {
            HttpResponseMessage res = null;
            log.Debug(string.Format("uid={0}, roomid={1}, timestamp={2}", uid, roomid, timestamp));
            DateTime capturetime;
            if (DateTime.TryParseExact(timestamp, "yyMMdd_HHmmss", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out capturetime))
                log.Debug(capturetime);
            //IEnumerable<string> headers;
            //if(Request.Headers.TryGetValues("Authorization",out headers))
            //    log.Info(headers.FirstOrDefault());

            try
            {
                //var content = Request.Content;
                var path = HttpContext.Current.Server.MapPath("~/App_Data/Img");
                var fulluploadpath = "";

                MultipartMemoryStreamProvider sp = new MultipartMemoryStreamProvider();
                Task.Run(async () => await Request.Content.ReadAsMultipartAsync(sp)).Wait();
                foreach (HttpContent item in sp.Contents)
                {
                    if (item.Headers.ContentDisposition.FileName != null)
                    {
                        //string filename = item.Headers.ContentDisposition.FileName.Replace("\"", "");
                        string filename = Guid.NewGuid().ToString();
                        log.Info(filename);
                        Stream ms = item.ReadAsStreamAsync().Result;
                        log.Info(ms.Length);
                        if (ms.Length <= 2000000)
                        {
                            using (BinaryReader br = new BinaryReader(ms))
                            {
                                fulluploadpath = path + "\\" + filename;
                                byte[] data = br.ReadBytes((int)ms.Length);
                                File.WriteAllBytes(fulluploadpath, data);
                            }

                            using (WebAppDbContext db = new WebAppDbContext())
                            {
                                db.RoomImages.Add(new RoomImage
                                {
                                    UserId = uid,
                                    RoomId = roomid,
                                    CaptureTime = capturetime,
                                    FileName = filename,
                                    CreateDt = DateTime.Now,
                                });
                                db.SaveChanges();
                            }
                        }
                    }
                }
                res = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception e)
            {
                log.Error(e.Message, e);
                res = Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return res;
        }

        //// PUT: api/Upload/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Upload/5
        //public void Delete(int id)
        //{
        //}
    }
}
