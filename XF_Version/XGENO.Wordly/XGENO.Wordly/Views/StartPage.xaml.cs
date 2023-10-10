using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XGENO.Wordly.ViewModels;

namespace XGENO.Wordly.Views
{
    public partial class StartPage : ViewBase<StartPageViewModel>
    {
        bool isGradientRunning = false;

        public StartPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            //Force VM Event
            _isLoaded = false;

            base.OnAppearing();

            if (!isGradientRunning)
                _ = Device.InvokeOnMainThreadAsync(() => Task.Run(AnimateBorder));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            isGradientRunning = false;
        }

        private async void AnimateBorder()
        {
            isGradientRunning = true;


            _ = Device.InvokeOnMainThreadAsync(async () =>
                        {
                            Action<double> borderThicknessOuter = t => borderGradientOuter.StrokeThickness = (float)t;
                            Action<double> borderThicknessMiddle = t => borderGradientMiddle.StrokeThickness = (float)t;
                            Action<double> borderThicknessInner = t => borderGradientInner.StrokeThickness = (float)t;

                            while (isGradientRunning)
                            {
                                borderGradientInner.Animate(name: "forwardInner", callback: borderThicknessInner, start: 0.0, end: 3.0, length: 800, easing: Easing.SinIn);
                                await Task.Delay(1000);
                                borderGradientMiddle.Animate(name: "forwardMiddle", callback: borderThicknessMiddle, start: 0.0, end: 2.0, length: 600, easing: Easing.SinIn);
                                await Task.Delay(800);
                                borderGradientOuter.Animate(name: "forwardOuter", callback: borderThicknessOuter, start: 0.0, end: 1.0, length: 400, easing: Easing.SinIn);
                                await Task.Delay(1000);
                                borderGradientOuter.Animate(name: "backwardOuter", callback: borderThicknessOuter, start: 1.0, end: 0.0, length: 200, easing: Easing.SinIn);
                                //await Task.Delay(400);
                                borderGradientMiddle.Animate(name: "backwardMiddle", callback: borderThicknessMiddle, start: 2.0, end: 0.0, length: 200, easing: Easing.SinIn);
                                //await Task.Delay(300);
                                borderGradientInner.Animate(name: "backwardInner", callback: borderThicknessInner, start: 3.0, end: 0.0, length: 200, easing: Easing.SinIn);
                                await Task.Delay(400);

                            }

                        }
                );

        }

    }
}
