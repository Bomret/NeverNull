using System;
using System.Collections.Generic;

namespace NeverNull {
    public abstract class Option<T> : IEquatable<Option<T>>,
                                      IEquatable<None> {
        static readonly Option<T> _none = new None<T>();

        public abstract bool HasValue { get; }
        public abstract bool IsEmpty { get; }
        public abstract T Value { get; }

        public bool Equals(None other) {
            return IsEmpty;
        }

        public static Option<T> None
        {
            get { return _none; }
        }

        public bool Equals(Option<T> other) {
            if (other == null) return false;

            if (other.HasValue && HasValue)
                return EqualityComparer<T>.Default.Equals(other.Value, Value);

            return other.IsEmpty == IsEmpty;
        }

        public static implicit operator Option<T>(T value) {
            return Option.From(value);
        }

        public static implicit operator Option<T>(None none) {
            return None;
        }
    }

    public static class Option {
        static readonly None _none = new None();

        public static None None {
            get { return _none; }
        }

        public static Option<T> Some<T>(T value) {
            return new Some<T>(value);
        }

        public static Option<T> From<T>(T value) {
            // None is only returned for references to null (classes and Nullable types)  

            // ReSharper disable CompareNonConstrainedGenericWithNull
            return value == null ? None : Some(value);
            // ReSharper restore CompareNonConstrainedGenericWithNull
        }
    }
}