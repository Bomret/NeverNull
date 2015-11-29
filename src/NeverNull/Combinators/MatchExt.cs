using System;
// ReSharper disable InconsistentNaming

namespace NeverNull.Combinators {
    public static class MatchExt {
        /// <summary>
        ///     Executes a given side effect if this option contains a value, otherwise a different side effect.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="Some"></param>
        /// <param name="None"></param>
        /// <exception cref="ArgumentNullException"><paramref name="Some"/> or <paramref name="None"/> is null.</exception>
        public static void Match<T>(this Option<T> option, Action<T> Some, Action None) {
           Some.ThrowIfNull(nameof(Some));
           None.ThrowIfNull(nameof(None));

            T value;
            if (option.TryGet(out value))
                Some(value);
            else
                None();
        }

        /// <summary>
        ///     Applies the first selector to the value of this option if it contains one, otherwise executes the second selector.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="option"></param>
        /// <param name="Some"></param>
        /// <param name="None"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="Some"/> or <paramref name="None"/> is null.</exception>
        public static B Match<A, B>(this Option<A> option, Func<A, B> Some, Func<B> None) {
            Some.ThrowIfNull(nameof(Some));
            None.ThrowIfNull(nameof(None));

            A value;
            return option.TryGet(out value)
                ? Some(value)
                : None();
        }
    }
}