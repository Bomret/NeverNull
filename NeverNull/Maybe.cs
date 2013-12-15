using System;

namespace NeverNull {
    public static class Maybe {
        public static IMaybe<T> From<T>(T value) {
            // only reference and Nullable types can be null.

            // ReSharper disable CompareNonConstrainedGenericWithNull
            if (value == null) return new None<T>();

            return new Some<T>(value);
            // ReSharper restore CompareNonConstrainedGenericWithNull
        }

        public static IMaybe<TValue> From<TValue>(Func<TValue> func) {
            return From(func());
        }
    }
}