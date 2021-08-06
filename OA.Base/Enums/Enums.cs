using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Base.Helpers
{
    public enum EnumDefaultValue
    {
        Organization = 2,
    }
    public enum EnumLang
    {
        Ar = 11,
        En = 22
    }
    public enum EnumServers
    {
        Windows = 1,
        Linux = 2,
    }
    public enum EnumFilesType : int
    {
        Image = 1,
        PDF = 2,
        Excel = 3
    }
    public enum EnumImageFormat
    {
        bmp,
        jpeg,
        gif,
        tiff,
        png,
        unknown
    }

    public enum EnumTimeZones:int
    {
        Sudan = 2,
        SaudiArab = 3
    }
}
