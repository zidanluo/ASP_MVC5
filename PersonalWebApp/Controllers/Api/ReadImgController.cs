using log4net;
using PersonalWebApp.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace PersonalWebApp.Controllers.Api
{
    public class ReadImgController : ApiController
    {
        private ILog log = LogManager.GetLogger(typeof(ReadImgController));

        public HttpResponseMessage Get(string uid, int roomid, string start, string end, int feq)
        {
            HttpResponseMessage res = null;
            log.Debug(string.Format("uid={0}, roomid={1}, start={2}, end={3}, feq={4}", uid, roomid, start, end, feq));
            try
            {
                DateTime starttime = DateTime.ParseExact(start, "yyMMdd_HHmmss", System.Globalization.CultureInfo.CurrentCulture);
                DateTime endtime = DateTime.ParseExact(end, "yyMMdd_HHmmss", System.Globalization.CultureInfo.CurrentCulture);

                var path = HttpContext.Current.Server.MapPath("~/App_Data/Img");
                var fulluploadpath = "";



                using (WebAppDbContext db = new WebAppDbContext())
                {
                    var list_images = db.RoomImages.Where(i => i.UserId == uid && i.RoomId == roomid && starttime <= i.CaptureTime && i.CaptureTime <= endtime);
                    fulluploadpath = path + "\\" + list_images.FirstOrDefault().FileName;
                }
                if (!string.IsNullOrEmpty(fulluploadpath))
                {
                    byte[] imgbyte = File.ReadAllBytes(fulluploadpath);
                    //MemoryStream imgstream = new MemoryStream(imgbyte);

                    res = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        //Content = new ByteArrayContent(imgbyte),
                    };
                    var c = new MultipartContent();
                    var m = new ByteArrayContent(imgbyte) ;
                    m.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
                    
                    c.Add(m);
                    c.Add(m);
                    res.Content = c;
                }
                //res = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
                res = Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return res;
        }
    }
}
