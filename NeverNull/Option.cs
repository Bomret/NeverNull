using System;

namespace NeverNull
{
    public struct Option<T>
    {
        public static Option<T> None = new Option<T>();
        private readonly bool _hasValue;
        private readonly T _value;

        public Option(T value)
        {
            _value = value;
            _hasValue = true;
        }

        public bool HasValue
        {
            get { return _hasValue; }
        }

        public bool IsEmpty
        {
            get { return _hasValue == false; }
        }

        public T Value
        {
            get
            {
                if (IsEmpty)
                    throw new NotSupportedException("None does not have a value");

                return _value;
            }
        }

        public static implicit operator Option<T>(None none)
        {
            return None;
        }

        public static implicit operator Option<T>(T value)
        {
            return Option.From(value);
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
            return new Option<T>(value);
        }

        public static Option<T> From<T>(T value)
        {
            if (typeof (T).IsValueType)
                return Some(value);

            return value == null ? Option<T>.None : Some(value);
        }

        public static Option<T> FromNullable<T>(T? nullable) where T : struct
        {
            return nullable.HasValue ? Some(nullable.Value) : Option<T>.None;
        }

        public static Option<B> FromTryPattern<A, B>(TryPattern<A, B> pattern, A val)
        {
            B b;
            return pattern(val, out b) ? Some(b) : Option<B>.None;
        }

        public static Option<C> FromTryPattern<A, B, C>(TryPattern<A, B, C> pattern, A val, B arg)
        {
            C c;
            return pattern(val, arg, out c) ? Some(c) : Option<C>.None;
        }

        public static Option<D> FromTryPattern<A, B, C, D>(TryPattern<A, B, C, D> pattern, A val, B arg, C arg2)
        {
            D d;
            return pattern(val, arg, arg2, out d) ? Some(d) : Option<D>.None;
        }

        public static Option<E> FromTryPattern<A, B, C, D, E>(TryPattern<A, B, C, D, E> pattern, A val, B arg, C arg2,
            D arg3)
        {
            E e;
            return pattern(val, arg, arg2, arg3, out e) ? Some(e) : Option<E>.None;
        }

        public static Option<F> FromTryPattern<A, B, C, D, E, F>(TryPattern<A, B, C, D, E, F> pattern, A val, B arg,
            C arg2, D arg3, E arg4)
        {
            F f;
            return pattern(val, arg, arg2, arg3, arg4, out f) ? Some(f) : Option<F>.None;
        }
    }
}