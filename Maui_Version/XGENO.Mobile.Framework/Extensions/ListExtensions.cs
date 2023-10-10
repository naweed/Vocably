namespace XGENO.Mobile.Framework.Extensions;

public static partial class ListExtensions
{
    public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> newItems, bool clearFirst = false)
    {
        if (clearFirst)
            collection.Clear();

        foreach (var newItem in newItems)
        {
            collection.Add(newItem);
        }
    }

    public static void Refresh<T>(this ObservableCollection<T> collection)
    {
        var original = collection.ToArray();

        collection.Clear();

        foreach (var item in original)
            collection.Add(item);
    }

    public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
    {
        Random rnd = new Random();
        return source.OrderBy<T, int>((item) => rnd.Next());
    }

    public static List<T> RandomizeList<T>(this IEnumerable<T> source)
    {
        Random rnd = new Random();
        return source.OrderBy<T, int>((item) => rnd.Next()).ToList();
    }


    public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
    {
        foreach (T item in enumeration)
        {
            action(item);
        }
    }

    public static void Sort<TSource, TKey>(this ObservableCollection<TSource> source, Func<TSource, TKey> keySelector, bool isAscending)
    {
        if (isAscending)
        {
            List<TSource> sortedList = source.OrderBy(keySelector).ToList();

            source.AddRange(sortedList, true);
        }
        else
        {
            List<TSource> sortedList = source.OrderByDescending(keySelector).ToList();

            source.AddRange(sortedList, true);
        }
    }
}