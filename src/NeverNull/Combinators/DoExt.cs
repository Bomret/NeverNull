using System;

namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods to execute side effects on the value of an <see cref="Option{T}" />.
    /// </summary>
    public static class DoExt {
        /// <summary>
        ///     Executes the specified <paramref name="sideEffect" /> on the value of the specified <paramref name="option" />, if
        ///     it has one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="sideEffect"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="sideEffect" /> is <see langword="null" />.
        /// </exception>
        public static Option<T> Do<T>(this Option<T> option, Action<T> sideEffect) {
            sideEffect.ThrowIfNull(nameof(sideEffect));

            option.Match(
                None: () => { },
                Some: sideEffect);

            return option;
        }
    }
}