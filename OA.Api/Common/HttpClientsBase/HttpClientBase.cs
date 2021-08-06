
using Microsoft.Extensions.Configuration;
using Nancy.Json;
using OA.Base;
using OA.Base.Helpers;
using OA.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OA.Api.Common.HttpClientsBase
{
    class HttpClientBase : IHttpClientBase
    {
        public HttpClientBase(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly IConfiguration Configuration;
        readonly HttpClient Http = new HttpClient();

        public bool Valid { get; private set; } = false;

        public bool GoogleRecaptcha(string Response)
        {
            var RecaptchaSettingsSection = Configuration.GetSection("RecaptchaSettings");
            var ServerSetting = RecaptchaSettingsSection.Get<RecaptchaSettings>();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create($"{ServerSetting.RecaptchaDomin}?secret={ServerSetting.RecaptchaSecretKey}&response={Response}");

            //Google recaptcha Response
            using (WebResponse wResponse = req.GetResponse())
            {

                using StreamReader readStream = new StreamReader(wResponse.GetResponseStream());
                string jsonResponse = readStream.ReadToEnd();

                JavaScriptSerializer js = new JavaScriptSerializer();
                GoogleRecaptchaViewModel data = js.Deserialize<GoogleRecaptchaViewModel>(jsonResponse);// Deserialize Json
                Valid = Convert.ToBoolean(data.success);
            }
            return Valid;
        }

        public FeedBack PushNotification(string DeviceId, string Title, string Body, dynamic Data = null, string AndroidChannelId = null)
        {
            var FireBaseSettingsSection = Configuration.GetSection("FireBaseSettings");
            var FireBaseSetting = FireBaseSettingsSection.Get<FireBaseSettings>();
            WebRequest tRequest = WebRequest.Create(FireBaseSetting.FireBaseDomin);
            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            tRequest.Headers.Add(string.Format("Authorization: key={0}", FireBaseSetting.FireBaseKey));
            //Sender Id - From firebase project setting  
            tRequest.Headers.Add(string.Format("Sender: id={0}", FireBaseSetting.Sender));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = DeviceId,
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = Body,
                    title = Title,
                    android_channel_id = AndroidChannelId,
                    badge = 1
                },
                data = new
                {
                    Order = Data,
                    click_action = "FLUTTER_NOTIFICATION_CLICK"
                }
            };

            string postbody = JsonHelper.Serialize(payload); 
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            using WebResponse tResponse = tRequest.GetResponse();
            using Stream dataStreamResponse = tResponse.GetResponseStream();
            if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                {
                    String sResponseFromServer = tReader.ReadToEnd();
                    FCMResponse response = JsonHelper.Deserialize<FCMResponse>(sResponseFromServer);
                    tReader.Close();
                    dataStream.Close();
                    tResponse.Close();
                    return FeedBack.SendSuccessfully;
                }
            else
            {
                return FeedBack.ServeErrorFail;
            }
        }

        public async Task<string> SendSMSCode(string Phone, string Lable, string body)
        {
            var SMSGatewaySettingsSection = Configuration.GetSection("SMSGatewaySettings");
            var SMSGatewaySetting = SMSGatewaySettingsSection.Get<SMSGatewaySettings>();
            string smsUrl = $"{SMSGatewaySetting.SMSGatewayDomin}user={SMSGatewaySetting.GatewayUser}&pwd={SMSGatewaySetting.GatewayPassword}&smstext={Lable}: {body}&Sender={SMSGatewaySetting.GatewaySender}&Nums=249{Phone}";
            HttpResponseMessage res = await Http.GetAsync(smsUrl);
            if (res.IsSuccessStatusCode)
            {
                // Get the response
                var customerJsonString = res.Content.ReadAsStringAsync();
                return customerJsonString.Result;
            }
            else
            {
                return "The message has not been sent. Try again";
            }
        }
    }
}
