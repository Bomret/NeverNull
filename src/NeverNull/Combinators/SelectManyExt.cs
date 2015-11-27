using System;

namespace NeverNull.Combinators {
    public static class SelectManyExt {
        /// <summary>
        ///     Applies the given <paramref name="selector" /> on the value of this option, if it has one, and returns the
        ///     resulting option. Otherwise None is returned.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="option"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static Option<B> SelectMany<A, B>(this Option<A> option, Func<A, Option<B>> selector) {
            selector.ThrowIfNull(nameof(selector));

            A value;
            return !option.TryGet(out value)
                ? Option<B>.None
                : selector(value);
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

            A value;
            if (!option.TryGet(out value))
                return Option<C>.None;

            var selected = optionSelector(value);

            B selectedValue;
            return selected.TryGet(out selectedValue)
                ? resultSelector(value, selectedValue)
                : Option<C>.None;
        }
    }
}