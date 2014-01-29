using System;
using System.Diagnostics;

namespace NeverNull {
    [DebuggerDisplay("None")]
    public sealed class None {
        public override string ToString() {
            return "None";
        }

        public bool Equals<T>(Option<T> obj) {
            return obj != null && obj.IsEmpty;
        }

        public override int GetHashCode() {
            return 0;
        }
    }

    [DebuggerDisplay("None")]
    sealed class None<T> : Option<T> {
        public override bool HasValue {
            get { return false; }
        }

        public override bool IsEmpty {
            get { return true; }
        }

        public override T Value {
            get { throw new InvalidOperationException("None does not have a value."); }
        }

        public override int GetHashCode() {
            return 0;
        }

        public override string ToString() {
            return "None";
        }
    }
}