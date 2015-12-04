namespace NeverNull.Combinators {
    public static class FlattenExt {
        /// <summary>
        ///     Returns the nested option or None, if nothing is contained.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nestedOption"></param>
        /// <returns></returns>
        public static Option<T> Flatten<T>(this Option<Option<T>> nestedOption) {
            Option<T> option;

            return nestedOption.TryGet(out option)
                ? option
                : Option.None;
        }
    }
}