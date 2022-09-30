using System.Collections.Generic;
using System.Linq;

public static class BaseExtensions
{
    public static bool IsNullOrDefault<T>(this T obj)
    {
        if (obj == null)
            return true;

        if (typeof(T).IsValueType)
            return System.Collections.Generic.EqualityComparer<T>.Default.Equals(obj, default(T));
        else
            return obj.Equals(null);
    }

    public static T GetRandom<T>(this IEnumerable<T> collection)
    {
        if (collection == null || !collection.Any())
            return default;

        var idx = UnityEngine.Random.Range(0, collection.Count());
        var i = 0;
        var enumerator = collection.GetEnumerator();
        
        while (i++ <= idx)
            enumerator.MoveNext();
     
        return enumerator.Current;
    }
}
