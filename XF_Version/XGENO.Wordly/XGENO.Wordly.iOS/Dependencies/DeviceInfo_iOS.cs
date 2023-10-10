using UIKit;
using XGENO.Mobile.Framework.UI;
using XGENO.Wordly.iOS.Dependencies;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceInfo_iOS))]
namespace XGENO.Wordly.iOS.Dependencies
{
    public class DeviceInfo_iOS : IDeviceInfo
    {
        public float StatusbarHeight => (float)UIApplication.SharedApplication.StatusBarFrame.Size.Height;
    }
}
