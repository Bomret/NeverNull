namespace NeverNull.Combinators
{
    public static class GetOrElseExt
    {
        public static T GetOrElse<T>(this Option<T> option, T elseValue)
        {
            return option.HasValue ? option.Value : elseValue;
        }
    }
}