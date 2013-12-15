using System;

namespace NeverNull {
    public struct None<T> : IMaybe<T> {
        public bool HasValue {
            get { return false; }
        }

        public bool IsEmpty {
            get { return true; }
        }

        public T Value {
            get { throw new NotSupportedException("None does not have a value."); }
        }
    }
}