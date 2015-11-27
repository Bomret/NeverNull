using System;

namespace NeverNull.Combinators {
    public static class OrElseExt {
        /// <summary>
        ///     If this option contains no value, the given <paramref name="fallback" /> value is returned, wrapped in an option.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="fallback"/> is null.</exception>
        public static Option<T> OrElse<T>(this Option<T> option, Func<T> fallback) =>
            OrElseWith(option, () => Option.From(fallback()));

        /// <summary>
        ///     If this option contains no value, the given <paramref name="fallback" /> option is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="fallback"/> is null.</exception>
        public static Option<T> OrElseWith<T>(this Option<T> option, Func<Option<T>> fallback) {
            fallback.ThrowIfNull(nameof(fallback));

            return option.HasValue
                ? option
                : fallback();
        }
    }
}