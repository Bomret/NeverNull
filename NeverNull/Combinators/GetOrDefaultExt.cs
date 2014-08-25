namespace NeverNull.Combinators
{
    public static class GetOrDefaultExt
    {
        public static T GetOrDefault<T>(this Option<T> option)
        {
            return option.HasValue ? option.Value : default(T);
        }
    }
}