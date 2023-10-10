
namespace XGENO.Mobile.Framework.Controls;

public class RatingView : GraphicsView
{
    public static readonly BindableProperty ItemCountProperty = BindableProperty.Create(nameof(ItemCount), typeof(int),
    typeof(RatingView), 5, BindingMode.OneWay,
    validateValue: (_, value) => value != null,
    propertyChanged:
        (bindableObject, oldValue, newValue) =>
        {
            if (newValue is not null && bindableObject is RatingView rating && newValue != oldValue)
                rating.UpdateItemCount();
        }
        );

    public int ItemCount
    {
        get => (int)GetValue(ItemCountProperty);
        set => SetValue(ItemCountProperty, value);
    }

    private void UpdateItemCount()
    {
        _drawableCanvas.ItemCount = ItemCount;
        SetSize();
        Invalidate();
    }

    public static readonly BindableProperty ItemSizeProperty = BindableProperty.Create(nameof(ItemSize), typeof(float),
    typeof(RatingView), 24f, BindingMode.OneWay,
    validateValue: (_, value) => value != null,
    propertyChanged:
        (bindableObject, oldValue, newValue) =>
        {
            if (newValue is not null && bindableObject is RatingView rating && newValue != oldValue)
                rating.UpdateItemSize();
        }
        );

    public float ItemSize
    {
        get => (float)GetValue(ItemSizeProperty);
        set => SetValue(ItemSizeProperty, value);
    }

    private void UpdateItemSize()
    {
        _drawableCanvas.ItemSize = ItemSize;
        SetSize();
        Invalidate();
    }

    public static readonly BindableProperty ItemSpacingProperty = BindableProperty.Create(nameof(ItemSpacing), typeof(float),
    typeof(RatingView), 6f, BindingMode.OneWay,
    validateValue: (_, value) => value != null,
    propertyChanged:
        (bindableObject, oldValue, newValue) =>
        {
            if (newValue is not null && bindableObject is RatingView rating && newValue != oldValue)
                rating.UpdateItemSpacing();
        }
        );

    public float ItemSpacing
    {
        get => (float)GetValue(ItemSpacingProperty);
        set => SetValue(ItemSpacingProperty, value);
    }

    private void UpdateItemSpacing()
    {
        _drawableCanvas.ItemSpacing = ItemSpacing;
        SetSize();
        Invalidate();
    }

    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(double),
    typeof(RatingView), 2.5d, BindingMode.OneWay,
    validateValue: (_, value) => value != null,
    propertyChanged:
        (bindableObject, oldValue, newValue) =>
        {
            if (newValue is not null && bindableObject is RatingView rating && newValue != oldValue)
                rating.UpdateValue();
        }
        );

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    private void UpdateValue()
    {
        _drawableCanvas.Value = Value;
        SetSize();
        Invalidate();
    }

    public static readonly BindableProperty RatedFillColorProperty = BindableProperty.Create(nameof(RatedFillColor), typeof(Color),
    typeof(RatingView), Color.FromArgb("#FFFF00"), BindingMode.OneWay,
    validateValue: (_, value) => value != null,
    propertyChanged:
        (bindableObject, oldValue, newValue) =>
        {
            if (newValue is not null && bindableObject is RatingView rating && newValue != oldValue)
                rating.UpdateRatedFillColor();
        }
        );

    public Color RatedFillColor
    {
        get => (Color)GetValue(RatedFillColorProperty);
        set => SetValue(RatedFillColorProperty, value);
    }

    private void UpdateRatedFillColor()
    {
        _drawableCanvas.RatedFillColor = RatedFillColor;
        SetSize();
        Invalidate();
    }

    public static readonly BindableProperty UnRatedFillColorProperty = BindableProperty.Create(nameof(UnRatedFillColor), typeof(Color),
    typeof(RatingView), Color.FromArgb("#D3D3D3"), BindingMode.OneWay,
    validateValue: (_, value) => value != null,
    propertyChanged:
        (bindableObject, oldValue, newValue) =>
        {
            if (newValue is not null && bindableObject is RatingView rating && newValue != oldValue)
                rating.UpdateUnRatedFillColor();
        }
        );

    public Color UnRatedFillColor
    {
        get => (Color)GetValue(UnRatedFillColorProperty);
        set => SetValue(UnRatedFillColorProperty, value);
    }

    private void UpdateUnRatedFillColor()
    {
        _drawableCanvas.UnRatedFillColor = UnRatedFillColor;
        SetSize();
        Invalidate();
    }

    public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create(nameof(StrokeColor), typeof(Color),
    typeof(RatingView), Color.FromArgb("#FFFFE0"), BindingMode.OneWay,
    validateValue: (_, value) => value != null,
    propertyChanged:
        (bindableObject, oldValue, newValue) =>
        {
            if (newValue is not null && bindableObject is RatingView rating && newValue != oldValue)
                rating.UpdateStrokeColor();
        }
        );

    public Color StrokeColor
    {
        get => (Color)GetValue(StrokeColorProperty);
        set => SetValue(StrokeColorProperty, value);
    }

    private void UpdateStrokeColor()
    {
        _drawableCanvas.StrokeColor = StrokeColor;
        SetSize();
        Invalidate();
    }

    public static readonly BindableProperty StrokeWidthProperty = BindableProperty.Create(nameof(StrokeWidth), typeof(float),
    typeof(RatingView), 1f, BindingMode.OneWay,
    validateValue: (_, value) => value != null,
    propertyChanged:
        (bindableObject, oldValue, newValue) =>
        {
            if (newValue is not null && bindableObject is RatingView rating && newValue != oldValue)
                rating.UpdateStrokeWidth();
        }
        );

    public float StrokeWidth
    {
        get => (float)GetValue(StrokeWidthProperty);
        set => SetValue(StrokeWidthProperty, value);
    }

    private void UpdateStrokeWidth()
    {
        _drawableCanvas.StrokeWidth = StrokeWidth;
        SetSize();
        Invalidate();
    }

    public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool),
    typeof(RatingView), true, BindingMode.OneWay,
    validateValue: (_, value) => value != null,
    propertyChanged:
        (bindableObject, oldValue, newValue) =>
        {
            if (newValue is not null && bindableObject is RatingView rating && newValue != oldValue)
                rating.UpdateIsReadOnly();
        }
        );

    public bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    private void UpdateIsReadOnly()
    {
        ////TODO: Implement Interactions
    }

    public static readonly BindableProperty ShapePathProperty = BindableProperty.Create(nameof(ShapePath), typeof(string),
    typeof(RatingView), "M16.001007,0L20.944,10.533997 32,12.223022 23.998993,20.421997 25.889008,32 16.001007,26.533997 6.1109924,32 8,20.421997 0,12.223022 11.057007,10.533997z", 
    BindingMode.OneWay,
    validateValue: (_, value) => value != null,
    propertyChanged:
        (bindableObject, oldValue, newValue) =>
        {
            if (newValue is not null && bindableObject is RatingView rating && newValue != oldValue)
                rating.UpdateShapePath();
        }
        );

    //"M16.001007,0L20.944,10.533997 32,12.223022 23.998993,20.421997 25.889008,32 16.001007,26.533997 6.1109924,32 8,20.421997 0,12.223022 11.057007,10.533997z"

    public string ShapePath
    {
        get => (string)GetValue(ShapePathProperty);
        set => SetValue(ShapePathProperty, value);
    }

    private void UpdateShapePath()
    {
        _drawableCanvas.ShapePath = ShapePath;
        SetSize();
        Invalidate();
    }


    private void SetSize()
    {
        HeightRequest = ItemSize;
        WidthRequest = ItemSize * ItemCount + ItemSpacing * (ItemCount - 1);
    }

    protected override void OnParentSet()
    {
        base.OnParentSet();

        if (Parent is not null)
        {
            UpdateItemCount();
            UpdateItemSize();
            UpdateItemSpacing();
            UpdateRatedFillColor();
            UpdateStrokeColor();
            UpdateStrokeWidth();
            UpdateUnRatedFillColor();
            UpdateShapePath();
            UpdateValue();
        }
    }

    //Rating Drawable
    private RatingCanvas _drawableCanvas;

    public RatingView()
    {
        Drawable = _drawableCanvas = new RatingCanvas();
    }
}

internal class RatingCanvas : IDrawable
{
    public int ItemCount { get; set; }
    public float ItemSize { get; set; }
    public float ItemSpacing { get; set; }
    public double Value { get; set; }
    public Color RatedFillColor { get; set; }
    public Color UnRatedFillColor { get; set; }
    public Color StrokeColor { get; set; }
    public float StrokeWidth { get; set; }
    public string ShapePath { get; set; }


    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.Antialias = true;

        //Draw Background Color
        DrawBackground(canvas, dirtyRect);

        //Draw Stars
        for (int i = 0; i < ItemCount; i++)
        {
            DrawRatingItem(canvas, dirtyRect, i);
        }
    }

    private void DrawBackground(ICanvas canvas, RectF dirtyRect)
    {
        canvas.SaveState();

        canvas.FillColor = Color.FromArgb("#00000000");

        canvas.FillRectangle(dirtyRect);

        canvas.RestoreState();
    }

    private void DrawRatingItem(ICanvas canvas, RectF dirtyRect, int itemIndex)
    {
        canvas.SaveState();

        //Position the Shape in the Canvas
        canvas.Translate(itemIndex * ItemSize + itemIndex * ItemSpacing, 0);

        var pathBuilder = new PathBuilder();
        var shapePath = pathBuilder.BuildPath(ShapePath);
        var scaledShapePath = shapePath.AsScaledPath(ItemSize / shapePath.Bounds.Height);

        //Draw Empty Star as background
        DrawShape(canvas, scaledShapePath, StrokeColor, UnRatedFillColor, StrokeWidth, null);

        //Empty Star
        if (itemIndex < Value)
        {
            var clippedPath = new PathF();
            clippedPath.AppendRectangle(0, 0, Convert.ToSingle(scaledShapePath.Bounds.Width * (Value - itemIndex)), scaledShapePath.Bounds.Height);
                        
            DrawShape(canvas, scaledShapePath, StrokeColor, RatedFillColor, StrokeWidth, clippedPath);
        }

        canvas.RestoreState();
    }

    private void DrawShape(ICanvas canvas, PathF shapePath, Color strokeColor, Color fillColor, float strokeWidth, PathF clippedPath)
    {
        canvas.StrokeColor = strokeColor;
        canvas.StrokeSize = strokeWidth;
        canvas.FillColor = fillColor;

        if (clippedPath is not null)
            canvas.ClipPath(clippedPath);

        canvas.DrawPath(shapePath);
        canvas.FillPath(shapePath);

    }
}
