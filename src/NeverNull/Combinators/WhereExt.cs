﻿using System;
using JetBrains.Annotations;

namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods to filter instances of Option.
    /// </summary>
    public static class WhereExt {
        /// <summary>
        ///     Returns the specified option if the specified predicate holds, otherwise None.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     The predicate argument is null.
        /// </exception>
        public static Option<T> Where<T>(this Option<T> option, [NotNull] Func<T, bool> predicate) {
            predicate.ThrowIfNull(nameof(predicate));

            return option.Match(
                None: () => option,
                Some: x => predicate(x) ? x : Option<T>.None);
        }
    }
}
