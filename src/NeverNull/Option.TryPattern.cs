using JetBrains.Annotations;

namespace NeverNull {
    public static partial class Option {
		/// <summary>
		///     Executes the specified TryPattern and returns an option that represents the success or failure of the operation.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="pattern"></param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="pattern" /> is null.</exception>
		public static Option<T> FromTryPattern<T>([NotNull] TryPattern<T> pattern)
		{
			pattern.ThrowIfNull(nameof(pattern));

			T t;
			return pattern(out t) ? From(t) : Option<T>.None;
		}

        /// <summary>
        ///     Executes the specified TryPattern and returns an option that represents the success or failure of the operation.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="pattern"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="pattern" /> is null.</exception>
        public static Option<B> FromTryPattern<A, B>([NotNull] TryPattern<A, B> pattern, A arg) {
            pattern.ThrowIfNull(nameof(pattern));

            B b;
            return pattern(arg, out b) ? From(b) : Option<B>.None;
        }

        /// <summary>
        ///     Executes the specified TryPattern and returns an option that represents the success or failure of the operation.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="pattern"></param>
        /// <param name="arg"></param>
        /// <param name="arg1"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="pattern" /> is null.</exception>
        public static Option<C> FromTryPattern<A, B, C>([NotNull] TryPattern<A, B, C> pattern, A arg, B arg1) {
            pattern.ThrowIfNull(nameof(pattern));

            C c;
            return pattern(arg, arg1, out c) ? From(c) : Option<C>.None;
        }

        /// <summary>
        ///     Executes the specified TryPattern and returns an option that represents the success or failure of the operation.
        /// </summary>
        /// <typeparam name="A">The type of the first argument</typeparam>
        /// <typeparam name="B">The type of the second argument</typeparam>
        /// <typeparam name="C">The type of the third argument</typeparam>
        /// <typeparam name="D">The type of the result</typeparam>
        /// <param name="pattern">The delegate to execute</param>
        /// <param name="arg">The first argument</param>
        /// <param name="arg1">The second argument</param>
        /// <param name="arg2">The third argument</param>
        /// <returns>A Some containing the result if the operation was successful, None otherwise</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="pattern" /> is null.</exception>
        public static Option<D> FromTryPattern<A, B, C, D>([NotNull] TryPattern<A, B, C, D> pattern, A arg, B arg1, C arg2) {
            pattern.ThrowIfNull(nameof(pattern));

            D d;
            return pattern(arg, arg1, arg2, out d) ? From(d) : Option<D>.None;
        }

        /// <summary>
        ///     Executes the specified TryPattern and returns an option that represents the success or failure of the operation.
        /// </summary>
        /// <typeparam name="A">The type of the first argument</typeparam>
        /// <typeparam name="B">The type of the second argument</typeparam>
        /// <typeparam name="C">The type of the third argument</typeparam>
        /// <typeparam name="D">The type of the fourth argument</typeparam>
        /// <typeparam name="E">The type of the result</typeparam>
        /// <param name="pattern">The delegate to execute</param>
        /// <param name="arg">The first argument</param>
        /// <param name="arg1">The second argument</param>
        /// <param name="arg2">The third argument</param>
        /// <param name="arg3">The fourth argument</param>
        /// <returns>A Some containing the result if the operation was successful, None otherwise</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="pattern" /> is null.</exception>
        public static Option<E> FromTryPattern<A, B, C, D, E>([NotNull] TryPattern<A, B, C, D, E> pattern, A arg, B arg1, C arg2,
            D arg3) {
            pattern.ThrowIfNull(nameof(pattern));

            E e;
            return pattern(arg, arg1, arg2, arg3, out e) ? From(e) : Option<E>.None;
        }

        /// <summary>
        ///     Executes the specified TryPattern and returns an option that represents the success or failure of the operation.
        /// </summary>
        /// <typeparam name="A">The type of the first argument</typeparam>
        /// <typeparam name="B">The type of the second argument</typeparam>
        /// <typeparam name="C">The type of the third argument</typeparam>
        /// <typeparam name="D">The type of the fourth argument</typeparam>
        /// <typeparam name="E">The type of the fifth argument</typeparam>
        /// <typeparam name="F">The type of the result</typeparam>
        /// <param name="pattern">The delegate to execute</param>
        /// <param name="arg">The first argument</param>
        /// <param name="arg1">The second argument</param>
        /// <param name="arg2">The third argument</param>
        /// <param name="arg3">The fourth argument</param>
        /// <param name="arg4">The fifth argument</param>
        /// <returns>A Some containing the result if the operation was successful, None otherwise</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="pattern" /> is null.</exception>
        public static Option<F> FromTryPattern<A, B, C, D, E, F>([NotNull] TryPattern<A, B, C, D, E, F> pattern, A arg, B arg1,
            C arg2, D arg3, E arg4) {
            pattern.ThrowIfNull(nameof(pattern));

            F f;
            return pattern(arg, arg1, arg2, arg3, arg4, out f) ? From(f) : Option<F>.None;
        }

        /// <summary>
        ///     Executes the specified TryPattern and returns an option that represents the success or failure of the operation.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="D"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <typeparam name="G"></typeparam>
        /// <param name="pattern"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="pattern" /> is null.</exception>
        public static Option<G> FromTryPattern<A, B, C, D, E, F, G>([NotNull] TryPattern<A, B, C, D, E, F, G> pattern, A a, B b,
            C c, D d, E e, F f) {
            pattern.ThrowIfNull(nameof(pattern));

            G g;
            return pattern(a, b, c, d, e, f, out g) ? From(g) : Option<G>.None;
        }

        /// <summary>
        ///     Executes the specified TryPattern and returns an option that represents the success or failure of the operation.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="D"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <typeparam name="G"></typeparam>
        /// <typeparam name="H"></typeparam>
        /// <param name="pattern"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="pattern" /> is null.</exception>
        public static Option<H> FromTryPattern<A, B, C, D, E, F, G, H>([NotNull] TryPattern<A, B, C, D, E, F, G, H> pattern, A a,
            B b, C c, D d, E e, F f, G g) {
            pattern.ThrowIfNull(nameof(pattern));

            H h;
            return pattern(a, b, c, d, e, f, g, out h) ? From(h) : Option<H>.None;
        }

        /// <summary>
        ///     Executes the specified TryPattern and returns an option that represents the success or failure of the operation.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="D"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <typeparam name="G"></typeparam>
        /// <typeparam name="H"></typeparam>
        /// <typeparam name="I"></typeparam>
        /// <param name="pattern"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="pattern" /> is null.</exception>
        public static Option<I> FromTryPattern<A, B, C, D, E, F, G, H, I>([NotNull] TryPattern<A, B, C, D, E, F, G, H, I> pattern,
            A a, B b, C c, D d, E e, F f, G g, H h) {
            pattern.ThrowIfNull(nameof(pattern));

            I i;
            return pattern(a, b, c, d, e, f, g, h, out i) ? From(i) : Option<I>.None;
        }

        /// <summary>
        ///     Executes the specified TryPattern and returns an option that represents the success or failure of the operation.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="D"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <typeparam name="G"></typeparam>
        /// <typeparam name="H"></typeparam>
        /// <typeparam name="I"></typeparam>
        /// <typeparam name="J"></typeparam>
        /// <param name="pattern"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="pattern" /> is null.</exception>
        public static Option<J> FromTryPattern<A, B, C, D, E, F, G, H, I, J>(
            [NotNull] TryPattern<A, B, C, D, E, F, G, H, I, J> pattern, A a, B b, C c, D d, E e, F f, G g, H h, I i) {
            pattern.ThrowIfNull(nameof(pattern));

            J j;
            return pattern(a, b, c, d, e, f, g, h, i, out j) ? From(j) : Option<J>.None;
        }

        /// <summary>
        ///     Executes the specified TryPattern and returns an option that represents the success or failure of the operation.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="D"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <typeparam name="G"></typeparam>
        /// <typeparam name="H"></typeparam>
        /// <typeparam name="I"></typeparam>
        /// <typeparam name="J"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="pattern"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="pattern" /> is null.</exception>
        public static Option<K> FromTryPattern<A, B, C, D, E, F, G, H, I, J, K>(
            [NotNull] TryPattern<A, B, C, D, E, F, G, H, I, J, K> pattern, A a, B b, C c, D d, E e, F f, G g, H h, I i, J j) {
            pattern.ThrowIfNull(nameof(pattern));

            K k;
            return pattern(a, b, c, d, e, f, g, h, i, j, out k) ? From(k) : Option<K>.None;
        }

        /// <summary>
        ///     Executes the specified TryPattern and returns an option that represents the success or failure of the operation.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="D"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <typeparam name="G"></typeparam>
        /// <typeparam name="H"></typeparam>
        /// <typeparam name="I"></typeparam>
        /// <typeparam name="J"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="L"></typeparam>
        /// <param name="pattern"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="pattern" /> is null.</exception>
        public static Option<L> FromTryPattern<A, B, C, D, E, F, G, H, I, J, K, L>(
            [NotNull] TryPattern<A, B, C, D, E, F, G, H, I, J, K, L> pattern, A a, B b, C c, D d, E e, F f, G g, H h, I i, J j,
            K k) {
            pattern.ThrowIfNull(nameof(pattern));

            L l;
            return pattern(a, b, c, d, e, f, g, h, i, j, k, out l) ? From(l) : Option<L>.None;
        }

        /// <summary>
        ///     Executes the specified TryPattern and returns an option that represents the success or failure of the operation.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="D"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <typeparam name="G"></typeparam>
        /// <typeparam name="H"></typeparam>
        /// <typeparam name="I"></typeparam>
        /// <typeparam name="J"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="L"></typeparam>
        /// <typeparam name="M"></typeparam>
        /// <param name="pattern"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="pattern" /> is null.</exception>
        public static Option<M> FromTryPattern<A, B, C, D, E, F, G, H, I, J, K, L, M>(
            [NotNull] TryPattern<A, B, C, D, E, F, G, H, I, J, K, L, M> pattern, A a, B b, C c, D d, E e, F f, G g, H h, I i, J j,
            K k, L l) {
            pattern.ThrowIfNull(nameof(pattern));

            M m;
            return pattern(a, b, c, d, e, f, g, h, i, j, k, l, out m) ? From(m) : Option<M>.None;
        }

        /// <summary>
        ///     Executes the specified TryPattern and returns an option that represents the success or failure of the operation.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="D"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <typeparam name="G"></typeparam>
        /// <typeparam name="H"></typeparam>
        /// <typeparam name="I"></typeparam>
        /// <typeparam name="J"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="L"></typeparam>
        /// <typeparam name="M"></typeparam>
        /// <typeparam name="N"></typeparam>
        /// <param name="pattern"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <param name="l"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="pattern" /> is null.</exception>
        public static Option<N> FromTryPattern<A, B, C, D, E, F, G, H, I, J, K, L, M, N>(
            [NotNull] TryPattern<A, B, C, D, E, F, G, H, I, J, K, L, M, N> pattern, A a, B b, C c, D d, E e, F f, G g, H h, I i,
            J j, K k, L l, M m) {
            pattern.ThrowIfNull(nameof(pattern));

            N n;
            return pattern(a, b, c, d, e, f, g, h, i, j, k, l, m, out n) ? From(n) : Option<N>.None;
        }

        /// <summary>
        ///     Executes the specified TryPattern and returns an option that represents the success or failure of the operation.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="D"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <typeparam name="G"></typeparam>
        /// <typeparam name="H"></typeparam>
        /// <typeparam name="I"></typeparam>
        /// <typeparam name="J"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="L"></typeparam>
        /// <typeparam name="M"></typeparam>
        /// <typeparam name="N"></typeparam>
        /// <typeparam name="O"></typeparam>
        /// <param name="pattern"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <param name="l"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="pattern" /> is null.</exception>
        public static Option<O> FromTryPattern<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O>(
            [NotNull] TryPattern<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O> pattern, A a, B b, C c, D d, E e, F f, G g, H h, I i,
            J j, K k, L l, M m, N n) {
            pattern.ThrowIfNull(nameof(pattern));

            O o;
            return pattern(a, b, c, d, e, f, g, h, i, j, k, l, m, n, out o) ? From(o) : Option<O>.None;
        }

        /// <summary>
        ///     Executes the specified TryPattern and returns an option that represents the success or failure of the operation.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="D"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <typeparam name="G"></typeparam>
        /// <typeparam name="H"></typeparam>
        /// <typeparam name="I"></typeparam>
        /// <typeparam name="J"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="L"></typeparam>
        /// <typeparam name="M"></typeparam>
        /// <typeparam name="N"></typeparam>
        /// <typeparam name="O"></typeparam>
        /// <typeparam name="P"></typeparam>
        /// <param name="pattern"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <param name="l"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="pattern" /> is null.</exception>
        public static Option<P> FromTryPattern<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P>(
            [NotNull] TryPattern<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P> pattern, A a, B b, C c, D d, E e, F f, G g, H h,
            I i, J j, K k, L l, M m, N n, O o) {
            pattern.ThrowIfNull(nameof(pattern));

            P p;
            return pattern(a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, out p) ? From(p) : Option<P>.None;
        }
    }
}
