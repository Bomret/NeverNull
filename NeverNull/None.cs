using System;
using System.Diagnostics;

namespace NeverNull {
    [DebuggerDisplay("None")]
    public sealed class None : IEquatable<None> {
        public bool Equals(None other) {
            return true;
        }

        public override string ToString() {
            return "None";
        }
    }

    [DebuggerDisplay("None")]
    sealed class None<T> : Option<T>, IEquatable<None<T>> {
        public override bool HasValue {
            get { return false; }
        }

        public override bool IsEmpty {
            get { return true; }
        }

        public override T Value {
            get { throw new InvalidOperationException("None does not have a value."); }
        }

        public bool Equals(None<T> other) {
            return true;
        }

        public override string ToString() {
            return "None";
        }
    }
}