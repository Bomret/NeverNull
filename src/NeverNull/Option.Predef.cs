using JetBrains.Annotations;

namespace NeverNull {
    /// <summary>
    ///     Provides static methods to create and work with instances of Option.
    ///     This module is meant to be used as static import (C# 6 feature).
    /// </summary>
    public static class Predef {
        /// <summary>
        ///     Represents the absence of a value.
        /// </summary>
        public static None None => default(None);

        /// <summary>
        ///     Creates an option from the specified value.
        ///     Alias for Option.From.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Option<T> Option<T>([CanBeNull] T value) =>
            NeverNull.Option.From(value);

        /// <summary>
        ///     Creates an option from the specified Nullable.
        ///     Alias for Option.From.
        /// </summary>
        /// <param name="nullable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Option<T> Option<T>([CanBeNull] T? nullable) where T : struct =>
            NeverNull.Option.From(nullable);

        /// <summary>
        ///     Creates an option from the specified value.
        ///     Alias for Option.From.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Option<T> Some<T>([NotNull] T value) =>
            NeverNull.Option.From(value);

        /// <summary>
        ///     Creates an option from the specified Nullable.
        ///     Alias for Option.From.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public static Option<T> Some<T>([NotNull] T? nullable) where T : struct =>
            NeverNull.Option.From(nullable);
    }
}
