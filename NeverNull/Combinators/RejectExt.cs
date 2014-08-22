using System;

namespace NeverNull.Combinators
{
    public static class RejectExt
    {
        public static Option<T> Reject<T>(this Option<T> option, Func<T, bool> predicate)
        {
            if (option.IsEmpty)
                return Option<T>.None;

            return predicate(option.Value) ? Option<T>.None : option;
        }
    }
}