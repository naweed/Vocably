using System;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XGENO.Wordly.iOS.Renderers;
using XGENO.Wordly.ViewControls;

[assembly: Xamarin.Forms.ExportRenderer(typeof(XFCircleProgress), typeof(XFCircleProgressRenderer))]
namespace XGENO.Wordly.iOS.Renderers
{
    [Preserve(AllMembers = true)]
    public class XFCircleProgressRenderer : ViewRenderer
    {
        float? _radius;
        bool _sizeChanged = false;

        public async static void Init()
        {
            var temp = DateTime.Now;
        }

        private nfloat GetRadius(nfloat lineWidth)
        {
            if (_radius == null || _sizeChanged)
            {
                _sizeChanged = false;

                nfloat width = Bounds.Width;
                nfloat height = Bounds.Height;
                var size = (float)Math.Min(width, height);

                _radius = (size / 2f) - ((float)lineWidth / 2f);
            }

            return _radius.Value;
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            using (CGContext g = UIGraphics.GetCurrentContext())
            {
                var progressRing = (XFCircleProgress)Element;

                var lineWidth = (float)progressRing.RingThickness;
                var radius = (int)(GetRadius(lineWidth));
                var progress = (float)progressRing.Progress;
                var backColor = progressRing.RingBaseColor.ToUIColor();
                var frontColor = progressRing.RingProgressColor.ToUIColor();

                DrawProgressRing(g, Bounds.GetMidX(), Bounds.GetMidY(), progress, lineWidth, radius, backColor, frontColor);
            };
        }

        private void DrawProgressRing(CGContext g, nfloat x0, nfloat y0,
                                     nfloat progress, nfloat lineThickness, nfloat radius,
                                     UIColor backColor, UIColor frontColor)
        {
            g.SetLineWidth(lineThickness / 3);

            // Draw background circle
            CGPath path = new CGPath();

            backColor.SetStroke();

            path.AddArc(x0, y0, radius, 0, 2.0f * (float)Math.PI, true);
            g.AddPath(path);
            g.DrawPath(CGPathDrawingMode.Stroke);

            g.SetLineWidth(lineThickness);

            // Draw progress circle
            var pathStatus = new CGPath();
            frontColor.SetStroke();

            var startingAngle = 1.5f * (float)Math.PI;
            pathStatus.AddArc(x0, y0, radius, startingAngle, startingAngle + progress * 2 * (float)Math.PI, false);

            g.AddPath(pathStatus);
            g.DrawPath(CGPathDrawingMode.Stroke);
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var progressRing = (XFCircleProgress)this.Element;

            if (e.PropertyName == ProgressBar.ProgressProperty.PropertyName ||
                e.PropertyName == XFCircleProgress.RingThicknessProperty.PropertyName ||
                e.PropertyName == XFCircleProgress.RingBaseColorProperty.PropertyName ||
                e.PropertyName == XFCircleProgress.RingProgressColorProperty.PropertyName ||
                e.PropertyName == XFCircleProgress.ProgressProperty.PropertyName ||
                e.PropertyName == XFCircleProgress.AnimatedProgressProperty.PropertyName
                )
            {
                SetNeedsDisplay();
            }

            if (e.PropertyName == VisualElement.WidthProperty.PropertyName ||
               e.PropertyName == VisualElement.HeightProperty.PropertyName)
            {
                _sizeChanged = true;
                SetNeedsDisplay();
            }
        }
    }
}
