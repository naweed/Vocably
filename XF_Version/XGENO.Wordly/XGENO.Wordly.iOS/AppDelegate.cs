using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace XGENO.Wordly.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();
            Plugin.Segmented.Control.iOS.SegmentedControlRenderer.Initialize();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
