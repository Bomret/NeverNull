using System;
using System.Collections.Generic;

namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods to join the values of multiple instances of <see cref="Option{T}" />.
    /// </summary>
    public static class JoinExt {
        /// <summary>
        ///     Joins the specified <paramref name="firstOption" /> and <paramref name="secondOption" /> if they have values, based
        ///     on the equality of selected keys into a new <see cref="Option{T}" />.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="firstOption"></param>
        /// <param name="secondOption"></param>
        /// <param name="firstKeySelector"></param>
        /// <param name="secondKeySelector"></param>
        /// <param name="resultSelector"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="firstKeySelector" />, <paramref name="secondKeySelector" /> or <paramref name="resultSelector" />
        ///     is <see langword="null" />.
        /// </exception>
        public static Option<C> Join<A, B, C, TKey>(this Option<A> firstOption, Option<B> secondOption,
            Func<A, TKey> firstKeySelector, Func<B, TKey> secondKeySelector, Func<A, B, C> resultSelector,
            IEqualityComparer<TKey> comparer = null) {
            firstKeySelector.ThrowIfNull(nameof(firstKeySelector));
            secondKeySelector.ThrowIfNull(nameof(secondKeySelector));
            resultSelector.ThrowIfNull(nameof(resultSelector));

            return firstOption.Match(
                None: () => Option<C>.None,
                Some: a => secondOption.Match(
                    None: () => Option<C>.None,
                    Some: b => Option.From(firstKeySelector(a)).Match(
                        None: () => Option<C>.None,
                        Some: keyA => Option.From(secondKeySelector(b)).Match(
                            None: () => Option<C>.None,
                            Some: keyB => {
                                var equalityComparer = comparer ?? EqualityComparer<TKey>.Default;

                                return equalityComparer.Equals(keyA, keyB)
                                    ? resultSelector(a, b)
                                    : Option<C>.None;
                            }))));
        }
    }
}