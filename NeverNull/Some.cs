using System.Diagnostics;

namespace NeverNull {
    [DebuggerDisplay("Some({Value})")]
    sealed class Some<T> : Option<T> {
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

        public override string ToString() {
            return string.Format("Some({0})", Value);
        }

        public override int GetHashCode() {
            return Value.GetHashCode();
        }
    }
}