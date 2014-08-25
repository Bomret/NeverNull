using System;

namespace NeverNull.Combinators
{
    public static class FlatMapExt
    {
        public static Option<B> FlatMap<A, B>(this Option<A> option, Func<A, Option<B>> f)
        {
            return option.IsEmpty ? Option<B>.None : f(option.Value);
        }
    }
}