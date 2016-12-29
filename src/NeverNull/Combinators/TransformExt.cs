using System;
using JetBrains.Annotations;

// ReSharper disable InconsistentNaming

namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods that allow to transform instances of option depending on their type.
    /// </summary>
    public static class TransformExt {
        /// <summary>
        ///     Executes the appropriate callback depending on the type of the specified option.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="option"></param>
        /// <param name="Some"></param>
        /// <param name="None"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     One of the side effect functions is null.
        /// </exception>
        public static Option<B> Transform<A, B>(this Option<A> option, [NotNull] Func<A, B> Some, [NotNull] Func<B> None) {
            Some.ThrowIfNull(nameof(Some));
            None.ThrowIfNull(nameof(None));

            return option.Match(
                None: None,
                Some: Some);
        }
    }
}
