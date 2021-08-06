using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Base.Helpers.Coordinates
{
    public interface ICoordinate
    {
        GeoCoordinate GetGeoCoordinate(double lat, double lon);
    }
}
