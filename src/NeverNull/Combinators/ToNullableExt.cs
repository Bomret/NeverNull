using System;
using JetBrains.Annotations;

namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods to transform instances of <see cref="Option{T}" /> into their <see cref="Nullable{T}" />
    ///     representation.
    /// </summary>
    public static class ToNullableExt {
        /// <summary>
        ///     Returns a <see cref="Nullable{T}" /> containing the value of the specified <see cref="Option{T}" /> or an empty
        ///     <see cref="Nullable{T}" /> otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <returns></returns>
        [CanBeNull]
        public static T? ToNullable<T>(this Option<T> option) where T : struct =>
            option.Match(
                None: () => default(T?),
                Some: x => x);
    }
}