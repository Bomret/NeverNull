namespace NeverNull {
    public static partial class Option {
        public static Option<B> FromTryPattern<A, B>(TryPattern<A, B> pattern, A val) {
            B b;
            return pattern(val, out b) ? Some(b) : Option<B>.None;
        }

        public static Option<C> FromTryPattern<A, B, C>(TryPattern<A, B, C> pattern, A val, B arg) {
            C c;
            return pattern(val, arg, out c) ? Some(c) : Option<C>.None;
        }

        public static Option<D> FromTryPattern<A, B, C, D>(TryPattern<A, B, C, D> pattern, A val, B arg, C arg2) {
            D d;
            return pattern(val, arg, arg2, out d) ? Some(d) : Option<D>.None;
        }

        public static Option<E> FromTryPattern<A, B, C, D, E>(TryPattern<A, B, C, D, E> pattern, A val, B arg, C arg2,
                                                              D arg3) {
            E e;
            return pattern(val, arg, arg2, arg3, out e) ? Some(e) : Option<E>.None;
        }

        public static Option<F> FromTryPattern<A, B, C, D, E, F>(TryPattern<A, B, C, D, E, F> pattern, A val, B arg,
                                                                 C arg2, D arg3, E arg4) {
            F f;
            return pattern(val, arg, arg2, arg3, arg4, out f) ? Some(f) : Option<F>.None;
        }
    }
}