using System;

namespace NeverNull.Combinators
{
    public static class TransformExt
    {
        public static Option<B> Transform<A, B>(this Option<A> option, Func<A, B> ifSome, Func<B> ifNone)
        {
            if (option.IsEmpty)
            {
                var noneValue = ifNone();
                return Option.From(noneValue);
            }

            var someValue = ifSome(option.Value);
            return Option.From(someValue);
        }
    }
}