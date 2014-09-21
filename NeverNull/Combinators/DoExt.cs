using System;

namespace NeverNull.Combinators {
    public static class DoExt {
        public static Option<T> Do<T>(this Option<T> option, Action action) {
            action();

            return option;
        }
    }
}