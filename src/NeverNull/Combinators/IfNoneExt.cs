using System;

namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods to execute side effects on <see cref="Option{T}" /> if it does not contain a value.
    /// </summary>
    public static class IfNoneExt {
        /// <summary>
        ///     Executes the specified <paramref name="sideEffect" /> if the specified <paramref name="option" /> is None.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="sideEffect"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="sideEffect" /> is <see langword="null" />.
        /// </exception>
        public static void IfNone<T>(this Option<T> option, Action sideEffect) {
            sideEffect.ThrowIfNull(nameof(sideEffect));

            if (!option.HasValue)
                sideEffect();
        }
    }
}