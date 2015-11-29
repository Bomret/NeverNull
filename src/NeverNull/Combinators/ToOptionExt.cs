namespace NeverNull.Combinators {
    public static class ToOptionExt {
        /// <summary>
        ///     Wraps this value in an option.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Option<T> ToOption<T>(this T value) => Option.From(value);

        /// <summary>
        ///     Wraps the value of this nullable in an option or returns None.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public static Option<T> ToOption<T>(this T? nullable) where T : struct => Option.From(nullable);
    }
}