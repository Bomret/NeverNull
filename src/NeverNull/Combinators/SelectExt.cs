using System;
using JetBrains.Annotations;

namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods to transform the values of option into new forms.
    /// </summary>
    public static class SelectExt {
        /// <summary>
        ///     Applies the specified <paramref name="selector" /> function to the value of the specified <see cref="Option{T}"/> if it contains one and wraps the result in a new <see cref="Option{T}"/>. Otherwise None is returned.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="option"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="selector" /> is null.
        /// </exception>
        public static Option<B> Select<A, B>(this Option<A> option, [NotNull] Func<A, B> selector) {
            selector.ThrowIfNull(nameof(selector));

            return option.Match(
                None: () => Option<B>.None,
                Some: x => selector(x));
        }
    }
}
