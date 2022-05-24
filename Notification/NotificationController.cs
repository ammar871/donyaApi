using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Commander.Notification
{




    [Route("api/notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [Route("send")]
        [HttpPost]
        public async Task<IActionResult> SendNotification([FromForm] NotificationModel notificationModel)
        {
            var result = await _notificationService.SendNotification(notificationModel);
            // SendNotificationFromFirebaseCloud();
            return Ok(result);
        }


        // FirebaseMessaging.getInstance().subscribeToTopic("news");
        [Route("send/topic")]
        [HttpPost]
        public ActionResult SendNotificationFromFirebaseCloud([FromForm] NotificationData data)
        {
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAAYUEguiA:APA91bESvNOne5I_3Ccn9qMbWbfTIWP0GZGRQ_spH_-Py5csWb6yUIcZzoazEDb1V17B4WixcWtpb7N5gYz018jMv8PvkU_FfNfMCwiqxIFiHwlWSdWiL2l9B4zaGzSwfowUrepHUBPK"));
            //Sender Id - From firebase project setting  
            tRequest.Headers.Add(string.Format("Sender: id={0}", "417704491552"));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = "/topics/admin",
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = data.Body,
                    title = data.Title,
                    badge = 1
                },
                data = new
                {
                    subject = data.Subject,
                    imageUrl = data.ImageUrl,
                    desc = data.Desc,
                    data = data.createAt
                }

            };

            string postbody = JsonConvert.SerializeObject(payload).ToString();
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                //result.Response = sResponseFromServer;
                            }
                    }
                }
            }

            return Ok(data);
        }



    }
}