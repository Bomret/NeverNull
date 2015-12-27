using System;
using JetBrains.Annotations;

namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods to filter instances of <see cref="Option{T}" />.
    /// </summary>
    public static class WhereExt {
        /// <summary>
        ///     Returns the specified <paramref name="option" /> if the specified <paramref name="predicate" /> holds, otherwise
        ///     None.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="predicate" /> is <see langword="null" />.
        /// </exception>
        public static Option<T> Where<T>(this Option<T> option, [NotNull] Func<T, bool> predicate) {
            predicate.ThrowIfNull(nameof(predicate));

            return option.Match(
                None: () => option,
                Some: x => predicate(x) ? x : Option<T>.None);
        }
    }
}