namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods to wrap values into instances of Option.
    /// </summary>
    public static class ToOptionExt {
        /// <summary>
        ///     Wraps this value in a Option.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Option<T> ToOption<T>(this T value) =>
            Option.From(value);

        /// <summary>
        ///     Wraps the value of this Nullable in a option or returns None.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public static Option<T> ToOption<T>(this T? nullable) where T : struct =>
            Option.From(nullable);
    }
}
