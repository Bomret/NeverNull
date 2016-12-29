using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods to check if a specified option contains a specific value.
    /// </summary>
    public static class ContainsExt {
        /// <summary>
        ///     Returns a value that indicates if the specified option contains the desired value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="desiredValue"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static bool Contains<T>(this Option<T> option, [CanBeNull] T desiredValue, [CanBeNull] IEqualityComparer<T> comparer = null) {
            var c = comparer ?? EqualityComparer<T>.Default;

            return Contains(option, desiredValue, c.Equals);
        }

        /// <summary>
        ///    Returns a value that indicates if the specified option contains the desired value.
        ///    The specified compare function is used to check for equality.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="desiredValue"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     The compare argument is null.
        /// </exception>
        public static bool Contains<T>(this Option<T> option, [CanBeNull] T desiredValue, [NotNull] Func<T, T, bool> compare) {
            compare.ThrowIfNull(nameof(compare));

            return option.Match(
                None: () => false,
                Some: x => compare(x, desiredValue));
        }
    }
}
