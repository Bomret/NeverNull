namespace NeverNull {
	/// <summary>
	///     Represents a call to a Try* method.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="t"></param>
	/// <returns></returns>
	public delegate bool TryPattern<T>(out T t);

    /// <summary>
    ///     Represents a call to a Try* method.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="B"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public delegate bool TryPattern<in A, B>(A a, out B b);

    /// <summary>
    ///     Represents a call to a Try* method.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="B"></typeparam>
    /// <typeparam name="C"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public delegate bool TryPattern<in A, in B, C>(A a, B b, out C c);

    /// <summary>
    ///     Represents a call to a Try* method.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="B"></typeparam>
    /// <typeparam name="C"></typeparam>
    /// <typeparam name="D"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <returns></returns>
    public delegate bool TryPattern<in A, in B, in C, D>(A a, B b, C c, out D d);

    /// <summary>
    ///     Represents a call to a Try* method.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="B"></typeparam>
    /// <typeparam name="C"></typeparam>
    /// <typeparam name="D"></typeparam>
    /// <typeparam name="E"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    public delegate bool TryPattern<in A, in B, in C, in D, E>(A a, B b, C c, D d, out E e);

    /// <summary>
    ///     Represents a call to a Try* method.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="B"></typeparam>
    /// <typeparam name="C"></typeparam>
    /// <typeparam name="D"></typeparam>
    /// <typeparam name="E"></typeparam>
    /// <typeparam name="F"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <param name="e"></param>
    /// <param name="f"></param>
    /// <returns></returns>
    public delegate bool TryPattern<in A, in B, in C, in D, in E, F>(A a, B b, C c, D d, E e, out F f);

    /// <summary>
    ///     Represents a call to a Try* method.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="B"></typeparam>
    /// <typeparam name="C"></typeparam>
    /// <typeparam name="D"></typeparam>
    /// <typeparam name="E"></typeparam>
    /// <typeparam name="F"></typeparam>
    /// <typeparam name="G"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <param name="e"></param>
    /// <param name="f"></param>
    /// <param name="g"></param>
    /// <returns></returns>
    public delegate bool TryPattern<in A, in B, in C, in D, in E, in F, G>(A a, B b, C c, D d, E e, F f, out G g);

    /// <summary>
    ///     Represents a call to a Try* method.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="B"></typeparam>
    /// <typeparam name="C"></typeparam>
    /// <typeparam name="D"></typeparam>
    /// <typeparam name="E"></typeparam>
    /// <typeparam name="F"></typeparam>
    /// <typeparam name="G"></typeparam>
    /// <typeparam name="H"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <param name="e"></param>
    /// <param name="f"></param>
    /// <param name="g"></param>
    /// <param name="h"></param>
    /// <returns></returns>
    public delegate bool TryPattern<in A, in B, in C, in D, in E, in F, in G, H>(
        A a, B b, C c, D d, E e, F f, G g, out H h);

    /// <summary>
    ///     Represents a call to a Try* method.
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
    public delegate bool TryPattern<in A, in B, in C, in D, in E, in F, in G, in H, I>(
        A a, B b, C c, D d, E e, F f, G g, H h, out I i);

    /// <summary>
    ///     Represents a call to a Try* method.
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
    public delegate bool TryPattern<in A, in B, in C, in D, in E, in F, in G, in H, in I, J>(
        A a, B b, C c, D d, E e, F f, G g, H h, I i, out J j);

    /// <summary>
    ///     Represents a call to a Try* method.
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
    public delegate bool TryPattern<in A, in B, in C, in D, in E, in F, in G, in H, in I, in J, K>(
        A a, B b, C c, D d, E e, F f, G g, H h, I i, J j, out K k);

    /// <summary>
    ///     Represents a call to a Try* method.
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
    public delegate bool TryPattern<in A, in B, in C, in D, in E, in F, in G, in H, in I, in J, in K, L>(
        A a, B b, C c, D d, E e, F f, G g, H h, I i, J j, K k, out L l);

    /// <summary>
    ///     Represents a call to a Try* method.
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
    public delegate bool TryPattern<in A, in B, in C, in D, in E, in F, in G, in H, in I, in J, in K, in L, M>(
        A a, B b, C c, D d, E e, F f, G g, H h, I i, J j, K k, L l, out M m);

    /// <summary>
    ///     Represents a call to a Try* method.
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
    public delegate bool TryPattern<in A, in B, in C, in D, in E, in F, in G, in H, in I, in J, in K, in L, in M, N>(
        A a, B b, C c, D d, E e, F f, G g, H h, I i, J j, K k, L l, M m, out N n);

    /// <summary>
    ///     Represents a call to a Try* method.
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
    public delegate bool TryPattern
        <in A, in B, in C, in D, in E, in F, in G, in H, in I, in J, in K, in L, in M, in N, O>(
        A a, B b, C c, D d, E e, F f, G g, H h, I i, J j, K k, L l, M m, N n, out O o);

    /// <summary>
    ///     Represents a call to a Try* method.
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
    /// <param name="p"></param>
    /// <returns></returns>
    public delegate bool TryPattern
        <in A, in B, in C, in D, in E, in F, in G, in H, in I, in J, in K, in L, in M, in N, in O, P>(
        A a, B b, C c, D d, E e, F f, G g, H h, I i, J j, K k, L l, M m, N n, O o, out P p);
}
