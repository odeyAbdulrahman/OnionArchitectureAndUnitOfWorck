using Microsoft.Extensions.Configuration;
using OA.Base.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OA.Api.Common.UpLoadFilesBase
{
    public class UpLoadFileBase : IUpLoadFileBase
    {

        public UpLoadFileBase(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly IConfiguration Configuration;
        protected const int ImageFileLength = 1024 * 1024 * 5;
        protected string FullPath;
        //UpLoad Files In Domain
        public (string, FeedBack) UpLoadFile(string Base64, string Folder, EnumFilesType Type)
        {
            var ServerSettingsSection = Configuration.GetSection("ServerSettings");
            ServerSettings ServerSetting = ServerSettingsSection.Get<ServerSettings>();
            if (string.IsNullOrEmpty(Base64))
            {
                return ("", FeedBack.NullOrEmpty);
            }
            else
            {
                string FixBase64 = Regex.Replace(Base64, @"^data:.+;base64,", string.Empty);
                byte[] Length = Convert.FromBase64String(FixBase64);
                if (Type == EnumFilesType.Image)
                {
                    if (Length.Length > ImageFileLength)
                    {
                        return ("", FeedBack.largeSize);
                    }
                    else
                    {
                        EnumImageFormat Format = GetImageFormat(Length);
                        if (Format == EnumImageFormat.jpeg || Format == EnumImageFormat.png)
                        {
                            Directory.CreateDirectory(string.Format("{0}{1}", ServerSetting.OnlineFileServerBaseRoot, Folder));
                            string SaveInDataBase = string.Format("{0}{1}{2}{3}", Guid.NewGuid().ToString().Substring(0, 4), DateTime.Now.Ticks.ToString(), '.', EnumImageFormat.jpeg);
                            FullPath = string.Format("{0}{1}{2}{3}", ServerSetting.OnlineFileServerBaseRoot, Folder, ServerSetting.FileSystemSeparator, SaveInDataBase);
                            File.WriteAllBytes(FullPath, Length);
                            return (SaveInDataBase, FeedBack.ImageUploaded);
                        }
                        else
                        {
                            return("", FeedBack.IsNotImage);
                        }
                    }
                }
                else if (Type == EnumFilesType.PDF)
                {
                    return ("", FeedBack.IsNotImage);
                }
                else
                {
                   return ("", FeedBack.IsNotImage);
                }
            }
        }
        //Delete File From File Domain
        public void DeleteFile(string OldFile, string Folder, EnumFilesType Type)
        {
            var ServerSettingsSection = Configuration.GetSection("ServerSettings");
            var ServerSetting = ServerSettingsSection.Get<ServerSettings>();
            if (Type == EnumFilesType.Image)
            {
                if (!string.IsNullOrEmpty(OldFile))
                {
                    var DeletedFile = string.Format("{0}{1}{2}{3}", ServerSetting.OnlineFileServerBaseRoot, Folder, ServerSetting.FileSystemSeparator, OldFile);
                    if (File.Exists(DeletedFile))
                    {
                        File.Delete(DeletedFile);
                    }
                }
            }
            else if (Type == EnumFilesType.PDF)
            {

            }
            else
            {

            }
        }
        //Check Image Extantion
        public static EnumImageFormat GetImageFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            var png = new byte[] { 137, 80, 78, 71 };    // PNG
            var tiff = new byte[] { 73, 73, 42 };         // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };         // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 }; // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 }; // jpeg canon

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return EnumImageFormat.bmp;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return EnumImageFormat.gif;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return EnumImageFormat.png;

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return EnumImageFormat.tiff;

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return EnumImageFormat.tiff;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return EnumImageFormat.jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return EnumImageFormat.jpeg;

            return EnumImageFormat.unknown;
        }
    }
}
