namespace NeverNull.Combinators {
    public static class NormalizeExt {
        /// <summary>
        ///     Normalizes an option of a nullable type into the value representation of that type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <returns></returns>
        public static Option<T> Normalize<T>(this Option<T?> option) where T : struct {
            T? nullable;
            return option.TryGet(out nullable) ? Option.From(nullable) : Option<T>.None;
        }
    }
}
