using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Api
{
    public class AppSettings
    {
        public string Secret { get; set; }
    }
    public class ServerSettings
    {
        public static string OnlineFileServerDomain { get; set; } = "";
        public string OnlineFileServerBaseRoot { get; set; }
        public string FileSystemSeparator { get; set; }
    }
    public class FolderSettings
    {
        public static string ClientsFolder = "Users";
    }
    public class SMSGatewaySettings
    {
        public string SMSGatewayDomin { get; set; }
        public string GatewayUser { get; set; }
        public string GatewayPassword { get; set; }
        public string GatewaySender { get; set; }
    }
    public class RecaptchaSettings
    {
        public string RecaptchaDomin { get; set; }
        public string RecaptchaSecretKey { get; set; }
    }
    public class FireBaseSettings
    {
        public string FireBaseDomin { get; set; }
        public string FireBaseKey { get; set; }
        public string Sender { get; set; }
        public string FireBaseTopic_All { get; set; }
        public string FireBaseTopic_Clients { get; set; }
    }
    public class DecorationSettings
    {
        public string StarSymbol { get; set; }
    }
}
