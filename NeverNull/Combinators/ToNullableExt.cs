namespace NeverNull.Combinators {
    public static class ToNullableExt {
        public static T? ToNullable<T>(this Option<T> option) where T : struct {
            if (option.HasValue)
                return option.Value;

            return null;
        }
    }
}