using System;
using JetBrains.Annotations;

namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods to combine the values of several instances of <see cref="Option{T}" />.
    /// </summary>
    public static class ZipExt {
        /// <summary>
        ///     Combines the values of the specified <paramref name="first" /> and <paramref name="second" />
        ///     <see cref="Option{T}" /> using the specified <paramref name="select" /> function.
        ///     Returns None if <paramref name="first" /> or <paramref name="second" /> is None or <paramref name="select" />
        ///     returns <see langword="null" />.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="select" /> is <see langword="null" />.
        /// </exception>
        public static Option<C> Zip<A, B, C>(this Option<A> first, Option<B> second, [NotNull] Func<A, B, C> @select) =>
            ZipWith(first, second, (f, s) => Option.From(@select(f, s)));

        /// <summary>
        ///     Combines the values of the specified <paramref name="first" /> and <paramref name="second" />
        ///     <see cref="Option{T}" /> using the specified <paramref name="select" /> function.
        ///     Returns None if <paramref name="first" /> or <paramref name="second" /> is None or <paramref name="select" />
        ///     returns None.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="select" /> is <see langword="null" />.
        /// </exception>
        public static Option<C> ZipWith<A, B, C>(this Option<A> first, Option<B> second,
            [NotNull] Func<A, B, Option<C>> @select) {
            @select.ThrowIfNull(nameof(@select));

            return first.Match(
                None: () => Option<C>.None,
                Some: a => second.Match(
                    None: () => Option<C>.None,
                    Some: b => @select(a, b)));
        }
    }
}