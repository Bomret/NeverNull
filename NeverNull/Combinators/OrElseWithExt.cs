namespace NeverNull.Combinators
{
    public static class OrElseWithExt
    {
        public static Option<T> OrElseWith<T>(this Option<T> option, Option<T> orElse)
        {
            return option.HasValue ? option : orElse;
        }
    }
}