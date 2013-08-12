using System;

namespace NeverNull {
    public class None<TValue> : IOption<TValue> {
        public bool HasValue {
            get { return false; }
        }

        public bool IsEmpty {
            get { return true; }
        }

        public TValue Value {
            get { throw new NotSupportedException("None does not have a value."); }
        }
    }
}