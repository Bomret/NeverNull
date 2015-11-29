using System;

namespace NeverNull.Combinators {
    public static class GetExt {
        /// <summary>
        ///     Returns the value of this option, if it has one or throws a <see cref="InvalidOperationException" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"><paramref name="option"/> contains no value</exception>
        public static T Get<T>(this Option<T> option) {
            T value;
            if (!option.TryGet(out value))
                throw new InvalidOperationException("None does not contain a value.");

            return value;
        }

        /// <summary>
        ///     Returns the value of this option, if it has one or the default of <typeparamref name="T" />, if not.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <returns></returns>
        public static T GetOrDefault<T>(this Option<T> option) {
            T value;
            return option.TryGet(out value)
                ? value
                : default(T);
        }

        /// <summary>
        ///     Returns the value of this option, if it has one or the given <paramref name="fallback" />, if not.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="fallback"/> is null.</exception>
        public static T GetOrElse<T>(this Option<T> option, Func<T> fallback) {
            fallback.ThrowIfNull(nameof(fallback));

            T value;
            return option.TryGet(out value)
                ? value
                : fallback();
        }
    }
}