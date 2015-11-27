using System;

namespace NeverNull.Combinators {
    public static class IfSomeExt {
        /// <summary>
        ///     Executes the given <paramref name="sideEffect" /> if this option contains a value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="sideEffect"></param>
        /// <exception cref="ArgumentNullException"><paramref name="sideEffect"/> is null.</exception>
        public static void IfSome<T>(this Option<T> option, Action<T> sideEffect) {
            sideEffect.ThrowIfNull(nameof(sideEffect));

            T value;
            if (option.TryGet(out value))
                sideEffect(value);
        }
    }
}