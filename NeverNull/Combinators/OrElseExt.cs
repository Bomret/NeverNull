namespace NeverNull.Combinators
{
    public static class OrElseExt
    {
        public static Option<T> OrElse<T>(this Option<T> option, T orElse)
        {
            return option.HasValue ? option : Option.From(orElse);
        }
    }
}