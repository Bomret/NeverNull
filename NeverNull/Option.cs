using System;

namespace NeverNull {
    public static class Option {
        public static IOption<TValue> Create<TValue>(TValue value) {
            // only reference and Nullable types can be null.

            // ReSharper disable CompareNonConstrainedGenericWithNull
            if (value == null) {
                return new None<TValue>();
            }

            return new Some<TValue>(value);
            // ReSharper restore CompareNonConstrainedGenericWithNull
        }

        public static IOption<TValue> Create<TValue>(Func<TValue> func) {
            return Create(func());
        }
    }
}