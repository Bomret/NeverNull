using System;

namespace NeverNull.Combinators
{
    public static class FilterExt
    {
        public static Option<T> Filter<T>(this Option<T> option, Func<T, bool> predicate)
        {
            if (option.IsEmpty)
                return Option.None;

            return predicate(option.Value) ? option : Option<T>.None;
        }
    }
}