using System;

namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods to execute side effects on the value of <see cref="Option{T}" /> if it contains one.
    /// </summary>
    public static class IfSomeExt {
        /// <summary>
        ///     Executes the specified <paramref name="sideEffect" /> if the value of the specified <paramref name="option"/> if it contains one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="sideEffect"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="sideEffect"/> is <see langword="null"/>.
        /// </exception>
        public static void IfSome<T>(this Option<T> option, Action<T> sideEffect) {
            sideEffect.ThrowIfNull(nameof(sideEffect));

            option.Match(
                None: () => { },
                Some: sideEffect);
        }
    }
}