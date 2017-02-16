using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Devices.Input;
using Windows.Graphics.Display;
using Windows.Networking.Connectivity;

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

        public static bool HasInternetConnection()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && 
                connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }

        public static int GetScreenHeight()
        {
            var rect = PointerDevice.GetPointerDevices().Last().ScreenRect;
            var scale = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            return (int)(rect.Height * scale);
        }

        public static int GetScreenWidth()
        {
            var rect = PointerDevice.GetPointerDevices().Last().ScreenRect;
            var scale = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            return (int)(rect.Width * scale);
        }

        //public static void MakePhoneCall(string phoneNumber, string displayName)
        //{
        //    Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI(phoneNumber, displayName);
        //}
    }
}
