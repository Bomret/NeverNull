using System;

namespace NeverNull.Combinators {
    public static class ZipWithExt {
        public static Option<C> ZipWith<A, B, C>(this Option<A> optionA, Option<B> optionB, Func<A, B, Option<C>> f) {
            if (optionA.IsEmpty || optionB.IsEmpty)
                return Option<C>.None;

            return f(optionA.Value, optionB.Value);
        }
    }
}