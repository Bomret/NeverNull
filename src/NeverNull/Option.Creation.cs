namespace NeverNull {
    /// <summary>
    ///     Provides methods to create new Option types.
    /// </summary>
    public static partial class Option {
        public static None None => default(None);

        /// <summary>
        ///     Creates a new Option type from the given value.
        ///     If NULL is provided None is returned, in every other case an option containing the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Option<T> From<T>(T value) => value.IsNull() ? Option<T>.None : new Option<T>(value);

        /// <summary>
        ///     Creates a new Option from the given Nullable.
        ///     If the Nullable has no value, None is returned. Otherwise an option containing the value of the Nullable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public static Option<T> From<T>(T? nullable) where T : struct
            => nullable.HasValue ? new Option<T>(nullable.Value) : Option<T>.None;
    }
}