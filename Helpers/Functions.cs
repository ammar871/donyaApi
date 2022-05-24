

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Commander.Data;
using Commander.Models;
using Commander.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Commander.Helpers{



    class Functions {


           public static async Task<User> getCurrentUser(IHttpContextAccessor _httpContextAccessor, CommanderContext _context)
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var user = await _context.Users.FindAsync(userId);
        return user;
    }



      public async Task<bool> SendNotificationAsync(List<string> userIds, string title, string body,bool save, CommanderContext context)
    {

        List<string> tokens = 
             userIds
         .Select(x => context.Users.Where(u=>u.Id == x).First().DeviceToken)
          .ToList();

        using (var client = new HttpClient())
        {
            var firebaseOptionsServerId = "AAAAYUEguiA:APA91bESvNOne5I_3Ccn9qMbWbfTIWP0GZGRQ_spH_-Py5csWb6yUIcZzoazEDb1V17B4WixcWtpb7N5gYz018jMv8PvkU_FfNfMCwiqxIFiHwlWSdWiL2l9B4zaGzSwfowUrepHUBPK";
            var firebaseOptionsSenderId = "417704491552";

            client.BaseAddress = new Uri("https://fcm.googleapis.com");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",
                $"key={firebaseOptionsServerId}");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Sender", $"id={firebaseOptionsSenderId}");
            var data = new
            {
                registration_ids = tokens,
                notification = new
                {
                    body = body,
                    title = title,
                },
                data=new  {
                    orderId =1
                },
                priority = "high"
            };

            var json = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await client.PostAsync("/fcm/send", httpContent);

            if (save) {

                userIds.ForEach((id) => {
                    Alert alert = new Alert()
                    {
                        Body = body,
                        UserId = id
                    };
                    // context.Alerts.AddAsync(alert);

                });

                await context.SaveChangesAsync();
            }
            return result.StatusCode.Equals(HttpStatusCode.OK);
        }
    }


        public static NotificationData SendNotificationFromFirebaseCloud([FromForm] NotificationData data)
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

            return data;
        }


    }
}