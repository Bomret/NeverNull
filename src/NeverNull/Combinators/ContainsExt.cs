using System;
using System.Collections.Generic;

namespace NeverNull.Combinators {
    public static class ContainsExt {
        /// <summary>
        ///     Returns a value that indicates if this option contains the desired value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="desiredValue"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool Contains<T>(this Option<T> option, T desiredValue, IEqualityComparer<T> comparer = null) {
            var c = comparer ?? EqualityComparer<T>.Default;

            return Contains(option, desiredValue, c.Equals);
        }

        /// <summary>
        ///     Returns a value that indicates if this option contains the desired value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="desiredValue"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool Contains<T>(this Option<T> option, T desiredValue, Func<T, T, bool> compare) {
            compare.ThrowIfNull(nameof(compare));

            T t;
            return option.TryGet(out t) && compare(t, desiredValue);
        }
    }
}