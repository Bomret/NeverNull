using System;
using System.Collections.Generic;

namespace NeverNull.Combinators {
    public static class JoinExt {
        /// <summary>
        ///     Joins two options if they have values, based on the equality of selected keys into a new option.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="firstKeySelector"></param>
        /// <param name="secondKeySelector"></param>
        /// <param name="resultSelector"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="firstKeySelector"/> or <paramref name="secondKeySelector"/> or <paramref name="resultSelector"/> is null.</exception>
        public static Option<C> Join<A, B, C, TKey>(this Option<A> first, Option<B> second, Func<A, TKey> firstKeySelector, Func<B, TKey> secondKeySelector, Func<A, B, C> resultSelector, IEqualityComparer<TKey> comparer = null) {
            firstKeySelector.ThrowIfNull(nameof(firstKeySelector));
            secondKeySelector.ThrowIfNull(nameof(secondKeySelector));
            resultSelector.ThrowIfNull(nameof(resultSelector));

            A a;
            B b;
            if (!first.TryGet(out a) || !second.TryGet(out b))
                return Option.None;

            var keyA = firstKeySelector(a);
            var keyB = secondKeySelector(b);

            var c = comparer ?? EqualityComparer<TKey>.Default;

            return c.Equals(keyA, keyB)
                ? resultSelector(a, b)
                : Option<C>.None;
        }
    }
}