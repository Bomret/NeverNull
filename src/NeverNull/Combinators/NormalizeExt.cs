namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods to convert a <see cref="Option{T}" /> with a nullable type into the value representation
    ///     of that type.
    /// </summary>
    public static class NormalizeExt {
        /// <summary>
        ///     Normalizes the specified <paramref name="option" /> of a nullable type into the value representation of that type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <returns></returns>
        public static Option<T> Normalize<T>(this Option<T?> option) where T : struct =>
            option.Match(
                None: () => Option.None,
                Some: Option.From);
    }
}