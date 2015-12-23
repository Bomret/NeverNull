using System;

namespace NeverNull.Combinators {
    public static class SelectManyExt {
        /// <summary>
        ///     Applies the given <paramref name="select" /> on the value of this option, if it has one, and returns the
        ///     resulting option. Otherwise None is returned.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="option"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="select"/> is null.</exception>
        public static Option<B> SelectMany<A, B>(this Option<A> option, Func<A, Option<B>> @select) {
            @select.ThrowIfNull(nameof(@select));

            return option.Match(
                None: () => Option<B>.None,
                Some: @select);
        }

        /// <summary>
        ///     Applies the given <paramref name="optionSelector" /> and <paramref name="resultSelector" /> on the value of this
        ///     option, if it contains one. Otherwise None is returned.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="option"></param>
        /// <param name="optionSelector"></param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="optionSelector"/> or <paramref name="resultSelector"/> is null.</exception>
        public static Option<C> SelectMany<A, B, C>(this Option<A> option, Func<A, Option<B>> optionSelector, Func<A, B, C> resultSelector) {
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