using System;
using JetBrains.Annotations;

namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods
    /// </summary>
    public static class ZipExt {
        /// <summary>
        ///     Combines the values of the specified <paramref name="first" /> and <paramref name="second" />
        ///     <see cref="Option{T}" /> using the specified <paramref name="selector" />.
        ///     Returns None if <paramref name="first" /> or <paramref name="second" /> is None or the <paramref name="selector" />
        ///     returns <see langword="null" />.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static Option<C> Zip<A, B, C>(this Option<A> first, Option<B> second, [NotNull] Func<A, B, C> selector) =>
            ZipWith(first, second, (f, s) => Option.From(selector(f, s)));

        /// <summary>
        ///     Combines the values of the specified <paramref name="first" /> and <paramref name="second" />
        ///     <see cref="Option{T}" /> using the specified <paramref name="selector" />.
        ///     Returns None if <paramref name="first" /> or <paramref name="second" /> is None or the <paramref name="selector" />
        ///     returns None.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static Option<C> ZipWith<A, B, C>(this Option<A> first, Option<B> second, [NotNull] Func<A, B, Option<C>> selector) {
            selector.ThrowIfNull(nameof(selector));

            return first.Match(
                None: () => Option<C>.None,
                Some: a => second.Match(
                    None: () => Option<C>.None,
                    Some: b => selector(a, b)));
        }
    }
}