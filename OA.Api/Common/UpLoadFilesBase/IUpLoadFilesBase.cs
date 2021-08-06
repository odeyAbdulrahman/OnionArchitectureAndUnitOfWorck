using OA.Base.Helpers;

namespace OA.Api.Common.UpLoadFilesBase
{
    public interface IUpLoadFileBase
    {
        /// <summary>
        /// This mathod upload file from user
        /// </summary>
        /// <param name="Base64"></param>
        /// <param name="Folder"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        (string, FeedBack) UpLoadFile(string Base64, string Folder, EnumFilesType Type);
        /// <summary>
        /// This mathod delete file form a server 
        /// </summary>
        /// <param name="OldFile"></param>
        /// <param name="Folder"></param>
        /// <param name="Type"></param>
        void DeleteFile(string OldFile, string Folder, EnumFilesType Type);
    }
}