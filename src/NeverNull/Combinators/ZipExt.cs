using System;

namespace NeverNull.Combinators {
    public static class ZipExt {
        /// <summary>
        ///     Combines the values of this and the given option using the given <paramref name="selector" />.
        ///     Returns None if one of the options is None or the <paramref name="selector" /> returns NULL.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static Option<C> Zip<A, B, C>(this Option<A> first, Option<B> second, Func<A, B, C> selector) =>
            ZipWith(first, second, (f, s) => Option.From(selector(f, s)));

        /// <summary>
        ///     Combines the values of this and the given option using the given <paramref name="selector" />.
        ///     Returns None if one of the options is None or the <paramref name="selector" /> returns None.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static Option<C> ZipWith<A, B, C>(this Option<A> first, Option<B> second, Func<A, B, Option<C>> selector) {
            selector.ThrowIfNull(nameof(selector));

            A firstValue;
            B secondValue;
            return !first.TryGet(out firstValue) || !second.TryGet(out secondValue)
                ? Option<C>.None 
                : selector(firstValue, secondValue);
        }
    }
}