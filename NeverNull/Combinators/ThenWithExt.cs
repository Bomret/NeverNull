using System;

namespace NeverNull.Combinators {
    public static class ThenWithExt {
        public static Option<B> ThenWith<A, B>(this Option<A> option, Func<Option<A>, Option<B>> f) {
            return option.IsEmpty ? Option<B>.None : f(option);
        }
    }
}