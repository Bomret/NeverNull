using System;
using JetBrains.Annotations;

namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods to get the value from an <see cref="Option{T}" /> or react to None.
    /// </summary>
    public static class GetExt {
        /// <summary>
        ///     Returns the value of the specified <paramref name="option" /> if it has one or throws a
        ///     <see cref="InvalidOperationException" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"><paramref name="option" /> contains no value</exception>
        [NotNull]
        public static T Get<T>(this Option<T> option) =>
            option.Match(
                None: () => { throw new InvalidOperationException("None does not contain a value."); },
                Some: x => x);

        /// <summary>
        ///     Returns the value of the specified <paramref name="option" /> if it has one or the default of
        ///     <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <returns></returns>
        [Obsolete(
            "This method is obsolete and will be removed in 2 releases. Use GetOrElse(default(T)) if need this behavior."
            )]
        [CanBeNull]
        public static T GetOrDefault<T>(this Option<T> option) =>
            GetOrElse(option, default(T));

        /// <summary>
        ///     Returns the value of the specified <paramref name="option" /> if it has one or the given
        ///     <paramref name="fallback" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        [CanBeNull]
        public static T GetOrElse<T>(this Option<T> option, [CanBeNull] T fallback) =>
            GetOrElse(option, () => fallback);

        /// <summary>
        ///     Returns the value of the specified <paramref name="option" /> if it has one or executes the given
        ///     <paramref name="fallback" /> func and returns the produced value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="fallback" /> is <see langword="null" />.
        /// </exception>
        [CanBeNull]
        public static T GetOrElse<T>(this Option<T> option, [NotNull] Func<T> fallback) {
            fallback.ThrowIfNull(nameof(fallback));

            return option.Match(
                None: fallback,
                Some: x => x);
        }
    }
}