using System;

namespace NeverNull {
    public struct Option<T> : IOption<T>, IEquatable<None> {
        private static readonly Option<T> NoneInstance = new Option<T>();

        private readonly bool _hasValue;
        private readonly T _value;

        public Option(T value) {
            _value = value;
            _hasValue = true;
        }

        public static Option<T> None {
            get { return NoneInstance; }
        }

        public bool Equals(None other) {
            return IsEmpty;
        }

        public bool HasValue {
            get { return _hasValue; }
        }

        public bool IsEmpty {
            get { return _hasValue == false; }
        }

        public T Value {
            get {
                if (IsEmpty)
                    throw new NotSupportedException("None does not have a value");

                return _value;
            }
        }

        public override string ToString() {
            return HasValue ? string.Format("Some({0})", Value) : "None";
        }

        public static implicit operator Option<T>(None _) {
            return NoneInstance;
        }

        public static implicit operator Option<T>(T value) {
            return Option.From(value);
        }
    }
}