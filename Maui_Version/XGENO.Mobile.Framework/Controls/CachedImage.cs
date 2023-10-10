namespace XGENO.Mobile.Framework.Controls;

public class CachedImage : Image
{
    public static readonly BindableProperty CachedSourceUriProperty =
        BindableProperty.Create(
            nameof(CachedSourceUri),
            typeof(string),
            typeof(CachedImage),
            string.Empty,
            BindingMode.OneWay,
            validateValue: (_, value) => value != null,
            propertyChanged: OnCachedSourceChanged);

    public string CachedSourceUri
    {
        get => (string)GetValue(CachedSourceUriProperty);
        set => SetValue(CachedSourceUriProperty, value);
    }

    private static void OnCachedSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        var control = (CachedImage) bindable;

        if (oldvalue != newvalue && !string.IsNullOrEmpty(control.CachedSourceUri))
            control.Source = new UriImageSource
            {
                Uri = new Uri(control.CachedSourceUri),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(30, 0, 0, 0)
                //Cache for 30 days
                ////TODO: Define Caching duration as a property
            };
    }
}

