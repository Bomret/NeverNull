using System;

namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods to wrap values into instances of <see cref="Option{T}" />.
    /// </summary>
    public static class ToOptionExt {
        /// <summary>
        ///     Wraps this value in a <see cref="Option{T}" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Option<T> ToOption<T>(this T value) => 
            Option.From(value);

        /// <summary>
        ///     Wraps the value of this <see cref="Nullable{T}" /> in a <see cref="Option{T}" /> or returns None.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public static Option<T> ToOption<T>(this T? nullable) where T : struct => 
            Option.From(nullable);
    }
}