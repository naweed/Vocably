using System;
using Android.Content;
using Color = Xamarin.Forms.Color;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using XGENO.Wordly.ViewControls;
using XGENO.Wordly.Droid.Renderers;

[assembly: Xamarin.Forms.ExportRenderer(typeof(XFCircleProgress), typeof(XFCircleProgressRenderer))]
namespace XGENO.Wordly.Droid.Renderers
{
    public class XFCircleProgressRenderer : ViewRenderer<XFCircleProgress, Android.Widget.ProgressBar>
    {
        private Paint _paintForeground;
        private Paint _paintBackground;
        private RectF _ringDrawArea;
        private bool _sizeChanged = false;

        public XFCircleProgressRenderer(Context context) : base(context)
        {
            SetWillNotDraw(false);
        }

        protected override void OnDraw(Canvas canvas)
        {
            var progressRing = (XFCircleProgress)Element;

            if (_paintForeground == null)
            {
                var displayDensity = Context.Resources.DisplayMetrics.Density;
                var strokeWidth = (float)Math.Ceiling(progressRing.RingThickness * displayDensity);

                _paintForeground = new Paint();
                _paintForeground.StrokeWidth = strokeWidth;
                _paintForeground.SetStyle(Paint.Style.Stroke);
                _paintForeground.Flags = PaintFlags.AntiAlias;
            }

            if (_paintBackground == null)
            {
                var displayDensity = Context.Resources.DisplayMetrics.Density;
                var strokeWidth = (float)Math.Ceiling(progressRing.RingThickness * displayDensity);

                _paintBackground = new Paint();
                _paintBackground.StrokeWidth = strokeWidth / 3;
                _paintBackground.SetStyle(Paint.Style.Stroke);
                _paintBackground.Flags = PaintFlags.AntiAlias;
            }


            if (_ringDrawArea == null || _sizeChanged)
            {
                _sizeChanged = false;

                var ringAreaSize = Math.Min(canvas.ClipBounds.Width(), canvas.ClipBounds.Height());

                var ringDiameter = ringAreaSize - _paintForeground.StrokeWidth;

                var left = canvas.ClipBounds.CenterX() - ringDiameter / 2;
                var top = canvas.ClipBounds.CenterY() - ringDiameter / 2;

                _ringDrawArea = new RectF(left, top, left + ringDiameter, top + ringDiameter);
            }

            var backColor = progressRing.RingBaseColor;
            var frontColor = progressRing.RingProgressColor;
            var progress = (float)progressRing.Progress;
            DrawProgressRing(canvas, progress, backColor, frontColor);
        }

        private void DrawProgressRing(Canvas canvas, float progress,
                                      Color ringBaseColor,
                                      Color ringProgressColor)
        {
            _paintBackground.Color = ringBaseColor.ToAndroid();
            canvas.DrawArc(_ringDrawArea, 270, 360, false, _paintBackground);

            _paintForeground.Color = ringProgressColor.ToAndroid();
            canvas.DrawArc(_ringDrawArea, 270, 360 * progress, false, _paintForeground);
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Xamarin.Forms.ProgressBar.ProgressProperty.PropertyName ||
                e.PropertyName == XFCircleProgress.RingThicknessProperty.PropertyName ||
                e.PropertyName == XFCircleProgress.RingBaseColorProperty.PropertyName ||
                e.PropertyName == XFCircleProgress.RingProgressColorProperty.PropertyName ||
                e.PropertyName == XFCircleProgress.ProgressProperty.PropertyName)
            {
                Invalidate();
            }

            if (e.PropertyName == VisualElement.WidthProperty.PropertyName ||
                e.PropertyName == VisualElement.HeightProperty.PropertyName)
            {
                _sizeChanged = true;
                Invalidate();
            }
        }

    }
}
