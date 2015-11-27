using System;
// ReSharper disable InconsistentNaming

namespace NeverNull.Combinators {
    public static class TransformExt {
        /// <summary>
        ///     Executes the <paramref name="Some" /> on the value of this option, if it has one, or the
        ///     <paramref name="None" /> otherwise and wraps the result in an option.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="option"></param>
        /// <param name="Some"></param>
        /// <param name="None"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="Some"/> or <paramref name="None"/> is null.</exception>
        public static Option<B> Transform<A, B>(this Option<A> option, Func<A, B> Some, Func<B> None) =>
            TransformWith(option, a => Option.From(Some(a)), () => Option.From(None()));

        /// <summary>
        ///     Executes the <paramref name="Some" /> on the value of this option, if it has one, or the
        ///     <paramref name="None" /> otherwise.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="option"></param>
        /// <param name="Some"></param>
        /// <param name="None"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="Some"/> or <paramref name="None"/> is null.</exception>
        public static Option<B> TransformWith<A, B>(this Option<A> option, Func<A, Option<B>> Some, Func<Option<B>> None) {
            Some.ThrowIfNull(nameof(Some));
            None.ThrowIfNull(nameof(None));

            A value;
            return option.TryGet(out value)
                ? Some(value)
                : None();
        }
    }
}