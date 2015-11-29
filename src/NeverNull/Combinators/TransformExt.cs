using System;

namespace NeverNull.Combinators {
    public static class TransformExt {
        public static Option<B> Transform<A, B>(this Option<A> option, Func<A, B> Some, Func<B> None) {
            Some.ThrowIfNull(nameof(Some));
            None.ThrowIfNull(nameof(None));

            A val;
            return option.TryGet(out val)
                ? Some(val)
                : None(); 
        }
    }
}
