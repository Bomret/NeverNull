namespace NeverNull {
    public static class Option {
        public static IOption<T> Create<T>(T value) {
            // only reference and Nullable types can be null.

// ReSharper disable CompareNonConstrainedGenericWithNull
            if (value == null) {
                return new None<T>();
            }

            return new Some<T>(value);
            // ReSharper restore CompareNonConstrainedGenericWithNull
        }
    }
}