using System;

using Xamarin.Forms;

namespace XGENO.Mobile.Framework.UI.Controls
{
    public class SquareView : ContentView
    {
        public SquareView()
        {
            this.SizeChanged += SquareView_SizeChanged;
        }

        private void SquareView_SizeChanged(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.HeightRequest = (sender as SquareView).Width;
                this.ForceLayout();
            });
        }
    }
}

