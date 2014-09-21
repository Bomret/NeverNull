using System;

namespace NeverNull.Combinators {
    public static class MatchExt {
        public static void Match<T>(this Option<T> option, Action<T> ifSome, Action ifNone) {
            if (option.HasValue)
                ifSome(option.Value);
            else
                ifNone();
        }

        public static B Match<A, B>(this Option<A> option, Func<A, B> ifSome, Func<B> ifNone) {
            return option.HasValue ? ifSome(option.Value) : ifNone();
        }
    }
}