using System;
using JetBrains.Annotations;

namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods to execute side effects on the value of an option.
    /// </summary>
    public static class DoExt {
        /// <summary>
        ///     Executes the specified side effect on the value of the specified option, if it has one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="sideEffect"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     The sideEffect argument is null.
        /// </exception>
        public static Option<T> Do<T>(this Option<T> option, [NotNull] Action<T> sideEffect) {
            sideEffect.ThrowIfNull(nameof(sideEffect));

            option.IfSome(sideEffect);

            return option;
        }
    }
}
