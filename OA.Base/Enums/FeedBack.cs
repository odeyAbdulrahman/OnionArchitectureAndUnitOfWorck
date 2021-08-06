using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Base.Helpers
{
    public enum FeedBack : int
    {
        //Info

        //Success
        AddedSuccess = 200,
        EditedSuccess = 201,
        DeletedSuccess = 202,
        LoginSuccess = 203,
        OTPGenerated = 204,
        AccountVerified = 205,
        FreezedSuccess = 208,
        ImageUploaded = 209,
        RestPasswordSuccess = 210,

        //Error
        Unauthorized = 401,
        NotFound = 404,
        AddedFail = 405,
        EditedFail = 406,
        DeletedFail = 407,
        LoginFail = 408,
        ImageUploadedFail = 409,
        OTPGeneratedFail = 410,
        AccountNotVerified = 411,
        ConfirmedFail = 412,
        FreezedFail = 413,
        RestPasswordFail = 414,

        //Error
        ServeErrorFail = 500,
        NullOrEmpty = 501,
        largeSize = 502,
        IsNotImage = 503,
        UnUsedCode = 504,
        CodeNotUsed = 506,
        CodeInCorrect = 507,
        IsExist = 508,
        NotAllowed = 509,
        PhoneNumberIsExist = 512,
        AccountIsBlocked = 513,
        couldNotBeDeleted = 514,
        UserAccountNotAllowed = 515,
        UnConfirmedORFreezed = 516,
        WithoutSpace = 517,
        MaxmimQty = 524,
    }
}
