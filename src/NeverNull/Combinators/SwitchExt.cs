using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods to select the first option in a collection that has a value.
    /// </summary>
    public static class SwitchExt {
        /// <summary>
        ///     Returns the first option that contains a value or None, if no option contains one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Option<T> Switch<T>(this Option<T> option, params Option<T>[] options) =>
            Switch(option, options.AsEnumerable());

        /// <summary>
        ///     Returns the first option that contains a value or None, if no option contains one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="options" /> is null.
        /// </exception>
        public static Option<T> Switch<T>(this Option<T> option, [NotNull] IEnumerable<Option<T>> options) {
            options.ThrowIfNull(nameof(options));

            return option.IsSome ? option : options.FirstOrDefault(o => o.IsSome);
        }
    }
}
