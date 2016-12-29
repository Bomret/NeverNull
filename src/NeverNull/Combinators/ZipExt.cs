using System;
using JetBrains.Annotations;

namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods to combine the values of several instances of Option.
    /// </summary>
    public static class ZipExt {
        /// <summary>
        ///     Combines the values of the specified options using the specified select function.
        ///     Returns None if any of the arguments is None or executing select returns null.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     The select argument is null.
        /// </exception>
        public static Option<C> Zip<A, B, C>(this Option<A> first, Option<B> second, [NotNull] Func<A, B, C> @select) =>
            ZipWith(first, second, (f, s) => Option.From(@select(f, s)));

        /// <summary>
        ///     Combines the values of the specified options using the specified select function.
        ///     Returns None if any of the arguments is None or executing select returns None.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     The select argument is null.
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
