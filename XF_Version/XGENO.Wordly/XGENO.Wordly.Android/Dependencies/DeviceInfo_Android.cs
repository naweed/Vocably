using XGENO.Mobile.Framework.UI;
using XGENO.Wordly.Droid.Dependencies;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceInfo_Android))]
namespace XGENO.Wordly.Droid.Dependencies
{
    public class DeviceInfo_Android : IDeviceInfo
    {
        public float StatusbarHeight => 36f;
    }
}
