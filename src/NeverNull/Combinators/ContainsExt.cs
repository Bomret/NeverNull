using System;
using System.Collections.Generic;

namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods to check if a specified <see cref="Option{T}" /> contains a specific value.
    /// </summary>
    public static class ContainsExt {
        /// <summary>
        ///     Returns a value that indicates if the specified <paramref name="option" /> contains the
        ///     <paramref name="desiredValue" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="desiredValue"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static bool Contains<T>(this Option<T> option, T desiredValue, IEqualityComparer<T> comparer = null) {
            var c = comparer ?? EqualityComparer<T>.Default;

            return Contains(option, desiredValue, c.Equals);
        }

        /// <summary>
        ///     Returns a value that indicates if the specified <paramref name="option" /> contains the
        ///     <paramref name="desiredValue" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="desiredValue"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="compare" /> is <see langword="null" />
        /// </exception>
        public static bool Contains<T>(this Option<T> option, T desiredValue, Func<T, T, bool> compare) {
            compare.ThrowIfNull(nameof(compare));

            return option.Match(
                None: () => false,
                Some: x => compare(x, desiredValue));
        }
    }
}