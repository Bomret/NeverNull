using System;

namespace NeverNull.Combinators
{
    public static class MapExt
    {
        public static Option<B> Map<A, B>(this Option<A> option, Func<A, B> f)
        {
            if (option.IsEmpty)
                return Option<B>.None;

            var result = f(option.Value);
            return Option.From(result);
        }
    }
}