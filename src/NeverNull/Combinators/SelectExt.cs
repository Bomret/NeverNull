using System;
using JetBrains.Annotations;

namespace NeverNull.Combinators {
    public static class SelectExt {
        /// <summary>
        ///     Applies the given <paramref name="selector" /> to the value of this option if it contains
        ///     one and wraps the result in an option. Otherwise None is returned.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="option"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector" /> is null.</exception>
        public static Option<B> Select<A, B>(this Option<A> option, [NotNull] Func<A, B> selector) {
            selector.ThrowIfNull(nameof(selector));

            return option.Match(
                None: () => Option<B>.None,
                Some: x => selector(x));
        }
    }
}