﻿namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods to extract instances of nested Option.
    /// </summary>
    public static class FlattenExt {
        /// <summary>
        ///     Returns the nested option from the specified <paramref name="nestedOption" /> or None, if nothing
        ///     is contained.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nestedOption"></param>
        /// <returns></returns>
        public static Option<T> Flatten<T>(this Option<Option<T>> nestedOption) =>
            nestedOption.Match(
                None: () => Option.None,
                Some: x => x);
    }
}
