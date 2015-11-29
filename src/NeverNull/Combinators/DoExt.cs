using System;

namespace NeverNull.Combinators {
    public static class DoExt {
        /// <summary>
        ///     Executes the given <paramref name="sideEffect" /> on the value of this option, if it has one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="sideEffect"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Option<T> Do<T>(this Option<T> option, Action<T> sideEffect) {
            sideEffect.ThrowIfNull(nameof(sideEffect));

            T value;
            if (option.TryGet(out value))
                sideEffect(value);

            return option;
        }
    }
}