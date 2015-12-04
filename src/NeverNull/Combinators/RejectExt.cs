using System;

namespace NeverNull.Combinators {
    public static class RejectExt {
        /// <summary>
        ///     Returns None if the given <paramref name="predicate" /> holds for this option, otherwise this option.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null.</exception>
        public static Option<T> Reject<T>(this Option<T> option, Func<T, bool> predicate) {
            predicate.ThrowIfNull(nameof(predicate));

            T value;
            return option.TryGet(out value) && predicate(value)
                ? Option<T>.None
                : option;
        }
    }
}