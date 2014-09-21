using System;

namespace NeverNull.Combinators {
    public static class IfSomeExt {
        public static void IfSome<T>(this Option<T> option, Action<T> action) {
            if (option.HasValue)
                action(option.Value);
        }
    }
}