namespace NeverNull
{
    public abstract class Option<T>
    {
        private static readonly Option<T> _none = new None<T>();

        public abstract bool HasValue { get; }
        public abstract bool IsEmpty { get; }
        public abstract T Value { get; }

        public static Option<T> None
        {
            get { return _none; }
        }

        public static implicit operator Option<T>(T value)
        {
            return Option.From(value);
        }

        public static implicit operator Option<T>(None none)
        {
            return None;
        }
    }

    public static class Option
    {
        private static readonly None _none = new None();

        public static None None
        {
            get { return _none; }
        }

        public static Option<T> Some<T>(T value)
        {
            return new Some<T>(value);
        }

        public static Option<T> From<T>(T value)
        {
            if (typeof (T).IsValueType)
                return Some(value);

            return value == null ? None : Some(value);
        }

        public static Option<T> FromNullable<T>(T? nullable) where T : struct
        {
            return nullable.HasValue ? Some(nullable.Value) : None;
        }

        public static Option<B> FromTryPattern<A, B>(TryPattern<A, B> pattern, A val)
        {
            B b;
            return pattern(val, out b) ? Some(b) : None;
        }

        public static Option<C> FromTryPattern<A, B, C>(TryPattern<A, B, C> pattern, A val, B arg)
        {
            C c;
            return pattern(val, arg, out c) ? Some(c) : None;
        }

        public static Option<D> FromTryPattern<A, B, C, D>(TryPattern<A, B, C, D> pattern, A val, B arg, C arg2)
        {
            D d;
            return pattern(val, arg, arg2, out d) ? Some(d) : None;
        }

        public static Option<E> FromTryPattern<A, B, C, D, E>(TryPattern<A, B, C, D, E> pattern, A val, B arg, C arg2,
                                                              D arg3)
        {
            E e;
            return pattern(val, arg, arg2, arg3, out e) ? Some(e) : None;
        }

        public static Option<F> FromTryPattern<A, B, C, D, E, F>(TryPattern<A, B, C, D, E, F> pattern, A val, B arg,
                                                                 C arg2, D arg3, E arg4)
        {
            F f;
            return pattern(val, arg, arg2, arg3, arg4, out f) ? Some(f) : None;
        }
    }
}