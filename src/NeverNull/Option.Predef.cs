// ReSharper disable InconsistentNaming

namespace NeverNull {
    /// <summary>
    ///     Provides static methods to create and work with instances of <see cref="NeverNull.Option{T}" />.
    ///     This module is meant to be used as static import (C# 6 feature).
    /// </summary>
    public static class Predef {
        /// <summary>
        ///     Represents the absence of a value.
        /// </summary>
        public static None None => default(None);

        /// <summary>
        ///     Creates an <see cref="NeverNull.Option{T}" /> from the specified <paramref name="value" />.
        ///     Alias for Option.From.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Option<T> Option<T>(T value) => 
            NeverNull.Option.From(value);

        /// <summary>
        ///     Creates an <see cref="NeverNull.Option{T}" /> from the specified <paramref name="nullable" />.
        ///     Alias for Option.From.
        /// </summary>
        /// <param name="nullable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Option<T> Option<T>(T? nullable) where T : struct => 
            NeverNull.Option.From(nullable);

        /// <summary>
        ///     Creates an <see cref="NeverNull.Option{T}" /> from the specified <paramref name="value" />.
        ///     Alias for Option.From.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Option<T> Some<T>(T value) => 
            NeverNull.Option.From(value);

        /// <summary>
        ///     Creates an <see cref="NeverNull.Option{T}" /> from the specified <paramref name="nullable" />.
        ///     Alias for Option.From.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public static Option<T> Some<T>(T? nullable) where T : struct => 
            NeverNull.Option.From(nullable);
    }
}