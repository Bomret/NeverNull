using System;

namespace NeverNull.Combinators {
    public static class WhereExt {
        /// <summary>
        ///     Returns this option if the given <paramref name="predicate" /> holds, otherwise None.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null.</exception>
        public static Option<T> Where<T>(this Option<T> option, Func<T, bool> predicate) {
            predicate.ThrowIfNull(nameof(predicate));

            T value;
            return option.TryGet(out value) && predicate(value)
                ? option 
                : Option.None;
        }
    }
}