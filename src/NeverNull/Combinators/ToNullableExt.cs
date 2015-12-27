using JetBrains.Annotations;

namespace NeverNull.Combinators {
    public static class ToNullableExt {
        /// <summary>
        ///     Returns a nullable containing the value of this option or an empty nullable otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <returns></returns>
        [CanBeNull]
        public static T? ToNullable<T>(this Option<T> option) where T : struct =>
            option.Match(
                None: () => default(T?),
                Some: x => x);
    }
}