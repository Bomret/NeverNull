using System;

namespace NeverNull.Combinators
{
    public static class ZipExt
    {
        public static Option<C> Zip<A, B, C>(this Option<A> optionA, Option<B> optionB, Func<A, B, C> f)
        {
            if (optionA.IsEmpty || optionB.IsEmpty)
                return Option<C>.None;

            var result = f(optionA.Value, optionB.Value);
            return Option.From(result);
        }
    }
}