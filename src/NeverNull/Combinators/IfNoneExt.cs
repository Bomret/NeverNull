using System;

namespace NeverNull.Combinators {
    public static class IfNoneExt {
        /// <summary>
        ///     Executes the given <paramref name="sideEffect" /> if this option is None.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="sideEffect"></param>
        /// <exception cref="ArgumentNullException"><paramref name="sideEffect"/> is null.</exception>
        public static void IfNone<T>(this Option<T> option, Action sideEffect) {
            sideEffect.ThrowIfNull(nameof(sideEffect));

            if (!option.HasValue)
                sideEffect();
        }
    }
}