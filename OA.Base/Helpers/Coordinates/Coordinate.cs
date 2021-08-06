using Nest;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("OA.Api"), InternalsVisibleTo("XUnitTest.Base")]
namespace OA.Base.Helpers.Coordinates
{
    internal class Coordinate : ICoordinate
    {
        /// <summary>
        /// This mathod Get Geo Coordinate
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
        public GeoCoordinate GetGeoCoordinate(double lat, double lon)
        {
            return new GeoCoordinate(lat, lon);
        }
    }
}
