using OA.Base.Helpers;
using System;
using System.Linq;
using System.Reflection;

namespace OA.Base.Messages
{
    public class SystemMessagesEn
    {
        // ***** Info [000] ***** //

        // ***** Success [200] ***** //
        //200
        public string AddedSuccess { get; set; } = "Added successfully";
        //201
        public string EditedSuccess { get; set; } = "Edited successfully";
        //202
        public string DeletedSuccess { get; set; } = "Deleted successfully";
        //203
        public string LoginSuccess { get; set; } = "You are logged in successfully";
        //204
        public string OTPGenerated { get; set; } = "Activation code created";
        //205
        public string AccountVerified { get; set; } = "Account Verified";
        //206
        public string ConfirmedSuccess { get; set; } = "Confirmed successfully";


        public string YourMessage(FeedBack feedBack)
        {
            Type Type = Assembly.Load("OA.Base").GetTypes().First(t => t.Name == "SystemMessagesEn");
            object CreateInstance = Activator.CreateInstance(Type.GetType(Type.FullName));
            PropertyInfo prop = CreateInstance.GetType().GetProperty(feedBack.ToString());
            return prop.GetValue(CreateInstance).ToString();
        }
    }
    public class SystemMessagesAr
    {
        // ***** Info [000] ***** //

        // ***** Success [200] ***** //
        //200
        public string AddedSuccess { get; set; } = "تمت الإضافة بنجاح";
        //201
        public string EditedSuccess { get; set; } = "تم التعديل بنجاح";
        //202
        public string DeletedSuccess { get; set; } = "تم الحذف بنجاح";
        //203
        public string LoginSuccess { get; set; } = "تم تسجيل الدخول بنجاح";
        //204
        public string OTPGenerated { get; set; } = "تم إنشاء كود التفعيل";
        //205
        public string AccountVerified { get; set; } = "تم التحقق من الحساب";
        //206
        public string ConfirmedSuccess { get; set; } = "تم التأكيد بنجاح";

        public string YourMessage(FeedBack feedBack)
        {
            Type Type = Assembly.Load("OA.Base").GetTypes().First(t => t.Name == "SystemMessagesAr");
            object CreateInstance = Activator.CreateInstance(Type.GetType(Type.FullName));
            PropertyInfo prop = CreateInstance.GetType().GetProperty(feedBack.ToString());
            return prop.GetValue(CreateInstance).ToString();
        }
    }
}
