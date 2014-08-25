namespace NeverNull.Combinators
{
    public static class FlattenExt
    {
        public static Option<T> Flatten<T>(this Option<Option<T>> nestedOption)
        {
            return nestedOption.IsEmpty ? Option<T>.None : nestedOption.Value;
        }
    }
}