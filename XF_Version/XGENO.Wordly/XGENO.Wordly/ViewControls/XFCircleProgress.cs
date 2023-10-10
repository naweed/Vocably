using Xamarin.Forms;

namespace XGENO.Wordly.ViewControls
{
    public class XFCircleProgress : ProgressBar
    {
        #region Properties

        public static BindableProperty AnimatedProgressProperty = BindableProperty.Create("AnimatedProgress", typeof(double), typeof(XFCircleProgress), 0.0d, BindingMode.OneWay, null, HandleAnimatedProgressChanged);
        public double AnimatedProgress
        {
            get => (double)base.GetValue(AnimatedProgressProperty); 
            set
            {
                base.SetValue(AnimatedProgressProperty, value);

                //StartProgressToAnimation();
            }
        }

        public static BindableProperty AnimationLengthProperty = BindableProperty.Create("AnimationLength", typeof(uint), typeof(XFCircleProgress), (uint)1000, BindingMode.OneWay, null, HandleAnimationLengthChanged);
        public uint AnimationLength
        {
            get => (uint)base.GetValue(AnimationLengthProperty); 
            set => base.SetValue(AnimationLengthProperty, value); 
        }

        public static BindableProperty AnimationEasingProperty = BindableProperty.Create("AnimationEasing", typeof(uint), typeof(XFCircleProgress), (uint)6, BindingMode.OneWay, null, HandleAnimationEasingChanged);

        public Easing AnimationEasing
        {
            get
            {
                var intValue = (uint)base.GetValue(AnimationEasingProperty);
                var easingMethod = ProgressRingUtils.IntToEasingMethod((int)intValue);

                return easingMethod;
            }
            set
            {
                var easingMethod = ProgressRingUtils.EasingMethodToInt(value);

                base.SetValue(AnimationEasingProperty, easingMethod);
            }
        }

        public static BindableProperty RingProgressColorProperty = BindableProperty.Create("RingProgressColor", typeof(Color), typeof(XFCircleProgress), Color.FromRgb(234, 105, 92), BindingMode.OneWay);
        public Color RingProgressColor
        {
            get => (Color)base.GetValue(RingProgressColorProperty); 
            set => base.SetValue(RingProgressColorProperty, value); 
        }

        public static BindableProperty RingBaseColorProperty = BindableProperty.Create("RingBaseColor", typeof(Color), typeof(XFCircleProgress), Color.FromRgb(46, 60, 76), BindingMode.OneWay);
        public Color RingBaseColor
        {
            get => (Color)base.GetValue(RingBaseColorProperty); 
            set => base.SetValue(RingBaseColorProperty, value); 
        }

        public static BindableProperty RingThicknessProperty = BindableProperty.Create("RingThickness", typeof(double), typeof(XFCircleProgress), 5.0, BindingMode.OneWay);
        public double RingThickness
        {
            get => (double)base.GetValue(RingThicknessProperty); 
            set => base.SetValue(RingThicknessProperty, value); 
        }

        #endregion

        #region Animation

        public void StartProgressToAnimation()
        {
            ViewExtensions.CancelAnimations(this);
            var length = base.GetValue(AnimationLengthProperty);

            ProgressTo(AnimatedProgress, AnimationLength, AnimationEasing);
        }

        #endregion

        #region static handlers

        static void HandleAnimatedProgressChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var progressRing = (XFCircleProgress)bindable;
            //progressRing.AnimatedProgress = (double)newValue;
            progressRing.StartProgressToAnimation();
        }

        static void HandleAnimationLengthChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var progressRing = (XFCircleProgress)bindable;

            var animationIsRunning = progressRing.AnimationIsRunning("Progress");

            // If the progress animation is already running
            // rerun it with the new length value.
            if (animationIsRunning)
                progressRing.StartProgressToAnimation();
        }

        static void HandleAnimationEasingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var progressRing = (XFCircleProgress)bindable;
            var animationIsRunning = progressRing.AnimationIsRunning("Progress");

            // If the progress animation is already running
            // rerun it with the new length value.
            if (animationIsRunning)
                progressRing.StartProgressToAnimation();
        }

        #endregion

    }

    public static class ProgressRingUtils
    {

        public static int EasingMethodToInt(Easing easingMethod)
        {
            int easingMethodInt;

            if (easingMethod == Easing.BounceIn)
                easingMethodInt = 1;
            else if (easingMethod == Easing.BounceOut)
                easingMethodInt = 2;
            else if (easingMethod == Easing.CubicIn)
                easingMethodInt = 3;
            else if (easingMethod == Easing.CubicInOut)
                easingMethodInt = 4;
            else if (easingMethod == Easing.CubicOut)
                easingMethodInt = 5;
            else if (easingMethod == Easing.SinIn)
                easingMethodInt = 6;
            else if (easingMethod == Easing.SinInOut)
                easingMethodInt = 7;
            else if (easingMethod == Easing.SinOut)
                easingMethodInt = 8;
            else if (easingMethod == Easing.SpringIn)
                easingMethodInt = 9;
            else if (easingMethod == Easing.SpringOut)
                easingMethodInt = 10;
            else
                easingMethodInt = 0; // Easing.Linear

            return easingMethodInt;
        }

        public static Easing IntToEasingMethod(int value)
        {
            Easing easingMethod;

            switch (value)
            {
                case 1:
                    easingMethod = Easing.BounceIn;
                    break;
                case 2:
                    easingMethod = Easing.BounceOut;
                    break;
                case 3:
                    easingMethod = Easing.CubicIn;
                    break;
                case 4:
                    easingMethod = Easing.CubicInOut;
                    break;
                case 5:
                    easingMethod = Easing.CubicOut;
                    break;
                case 6:
                    easingMethod = Easing.SinIn;
                    break;
                case 7:
                    easingMethod = Easing.SinInOut;
                    break;
                case 8:
                    easingMethod = Easing.SinOut;
                    break;
                case 9:
                    easingMethod = Easing.SpringIn;
                    break;
                case 10:
                    easingMethod = Easing.SpringOut;
                    break;
                default:
                    easingMethod = Easing.Linear;
                    break;
            }

            return easingMethod;
        }
    }
}
