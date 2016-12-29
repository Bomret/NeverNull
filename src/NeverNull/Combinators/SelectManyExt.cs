using System;
using JetBrains.Annotations;

namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods to transform the value of option into new forms of
    ///     Option.    ///
    /// </summary>
    public static class SelectManyExt {
        /// <summary>
        ///     Applies the specified <paramref name="select" /> function to the value of the specified <paramref name="option" />,
        ///     if it has one, and returns the produced Option. Otherwise None is returned.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="option"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="select" /> is null.
        /// </exception>
        public static Option<B> SelectMany<A, B>(this Option<A> option, [NotNull] Func<A, Option<B>> @select) {
            @select.ThrowIfNull(nameof(@select));

            return option.Match(
                None: () => Option<B>.None,
                Some: @select);
        }

        /// <summary>
        ///     Applies the specified <paramref name="optionSelector" /> and <paramref name="resultSelector" /> functions to the
        ///     value of the specified <paramref name="option" />, if it contains one. Otherwise None is returned.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="option"></param>
        /// <param name="optionSelector"></param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="optionSelector" /> or <paramref name="resultSelector" /> is null.
        /// </exception>
        public static Option<C> SelectMany<A, B, C>(this Option<A> option, [NotNull] Func<A, Option<B>> optionSelector,
            Func<A, B, C> resultSelector) {
            optionSelector.ThrowIfNull(nameof(optionSelector));
            resultSelector.ThrowIfNull(nameof(resultSelector));

            return option.Match(
                None: () => Option<C>.None,
                Some: a => optionSelector(a).Match(
                    None: () => Option<C>.None,
                    Some: b => resultSelector(a, b)));
        }
    }
}