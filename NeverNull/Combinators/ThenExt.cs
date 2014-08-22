using System;

namespace NeverNull.Combinators
{
    public static class ThenExt
    {
        public static Option<B> Then<A, B>(this Option<A> option, Func<Option<A>, B> f)
        {
            if (option.IsEmpty)
                return Option<B>.None;

            var result = f(option);
            return Option.From(result);
        }
    }
}