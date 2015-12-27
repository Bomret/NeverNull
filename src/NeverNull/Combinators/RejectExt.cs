using System;
using JetBrains.Annotations;

namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods to reject instances of <see cref="Option{T}" />.
    /// </summary>
    public static class RejectExt {
        /// <summary>
        ///     Returns None if the given <paramref name="predicate" /> holds for this option, otherwise this option.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="predicate" /> is null.
        /// </exception>
        public static Option<T> Reject<T>(this Option<T> option, [NotNull] Func<T, bool> predicate) {
            predicate.ThrowIfNull(nameof(predicate));

            return option.Match(
                None: () => Option<T>.None,
                Some: x => predicate(x) ? Option<T>.None : option);
        }
    }
}