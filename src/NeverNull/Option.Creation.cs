using JetBrains.Annotations;

namespace NeverNull {
    /// <summary>
    ///     Provides methods to create new instances of Options.
    /// </summary>
    public static partial class Option {
        public static None None => default(None);

        /// <summary>
        ///     Creates a new option from the specified value.
        ///     If 'null' is provided None is returned, in every other case an option containing
        ///     the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Option<T> From<T>([CanBeNull] T value) =>
            value.IsNull() ? Option<T>.None : new Option<T>(value);

        /// <summary>
        ///     Creates a new option from the specified Nullable.
        ///     If 'null' is provided None is returned, in every other case an option containing
        ///     the value of the Nullable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public static Option<T> From<T>([CanBeNull] T? nullable) where T : struct
            => nullable.HasValue ? new Option<T>(nullable.Value) : Option<T>.None;
  }
}
