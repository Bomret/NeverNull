﻿namespace NeverNull {
    public class Some<T> : IOption<T> {
        public Some(T value) {
            Value = value;
        }

        public bool HasValue {
            get { return true; }
        }

        public bool IsEmpty {
            get { return false; }
        }

        public T Value { get; private set; }
    }
}