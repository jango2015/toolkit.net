using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace MacroSource.Toolkit.Uwp
{
    public class DeviceHelper
    {
        public static async Task<Tuple<double, double>> GetCurrentCoordinatesAsync()
        {
            if (await Geolocator.RequestAccessAsync() == GeolocationAccessStatus.Allowed)
            {
                var geoposition = await new Geolocator().GetGeopositionAsync();
                var position = geoposition.Coordinate.Point.Position;
                return new Tuple<double, double>(position.Longitude, position.Latitude);
            }
            return null;
        }
    }
}
