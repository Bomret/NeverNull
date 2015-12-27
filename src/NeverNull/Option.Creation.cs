using JetBrains.Annotations;

namespace NeverNull {
    /// <summary>
    ///     Provides methods to create new instances of <see cref="Option{T}" />.
    /// </summary>
    public static partial class Option {
        public static None None => default(None);

        /// <summary>
        ///     Creates a new <see cref="Option{T}" /> from the specified <paramref name="value" />.
        ///     If <see langword="null" /> is provided None is returned, in every other case an <see cref="Option{T}" /> containing
        ///     the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Option<T> From<T>([CanBeNull] T value) =>
            value.IsNull() ? Option<T>.None : new Option<T>(value);

        /// <summary>
        ///     Creates a new <see cref="Option{T}" /> from the specified <paramref name="nullable" />.
        ///     If <see langword="null" /> is provided None is returned, in every other case an <see cref="Option{T}" /> containing
        ///     the value of the <paramref name="nullable" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public static Option<T> From<T>([CanBeNull] T? nullable) where T : struct
            => nullable.HasValue ? new Option<T>(nullable.Value) : Option<T>.None;
    }
}