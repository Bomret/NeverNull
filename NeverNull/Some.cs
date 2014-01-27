using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NeverNull {
    [DebuggerDisplay("Some({Value})")]
    sealed class Some<T> : Option<T>, IEquatable<Option<T>> {
        readonly T _value;

        public Some(T value) {
            _value = value;
        }

        public override bool HasValue {
            get { return true; }
        }

        public override bool IsEmpty {
            get { return false; }
        }

        public override T Value {
            get { return _value; }
        }

        public bool Equals(Option<T> other) {
            if (other == null || !other.HasValue) return false;

            return EqualityComparer<T>.Default.Equals(other.Value, Value);
        }

        public override string ToString() {
            return string.Format("Some({0})", Value);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Some<T> && Equals((Some<T>) obj);
        }

        public override int GetHashCode() {
            return EqualityComparer<T>.Default.GetHashCode(_value);
        }
    }
}