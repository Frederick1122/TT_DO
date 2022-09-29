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
}
