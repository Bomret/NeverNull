namespace NeverNull {
    public static partial class Option {
        public static None None {
            get { return None.Instance; }
        }

        public static Option<T> Some<T>(T value) {
            return new Option<T>(value);
        }

        public static Option<T> From<T>(T value) {
            if (typeof (T).IsValueType)
                return Some(value);

            return ReferenceEquals(value, null) ? Option<T>.None : Some(value);
        }

        public static Option<T> FromNullable<T>(T? nullable) where T : struct {
            return nullable.HasValue ? Some(nullable.Value) : Option<T>.None;
        }
    }
}