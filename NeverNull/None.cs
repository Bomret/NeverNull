using System;

namespace NeverNull {
    public class None<T> : IOption<T> {
        public bool HasValue {
            get { return false; }
        }

        public T Value {
            get { throw new NotSupportedException("None does not have a value."); }
        }
    }
}