namespace NeverNull {
    public delegate bool TryPattern<in A, B>(A a, out B b);

    public delegate bool TryPattern<in A, in B, C>(A a, B b, out C c);

    public delegate bool TryPattern<in A, in B, in C, D>(A a, B b, C c, out D d);

    public delegate bool TryPattern<in A, in B, in C, in D, E>(A a, B b, C c, D d, out E e);

    public delegate bool TryPattern<in A, in B, in C, in D, in E, F>(A a, B b, C c, D d, E e, out F f);

    public delegate bool TryPattern<in A, in B, in C, in D, in E, in F, G>(A a, B b, C c, D d, E e, F f, out G g);

    public delegate bool TryPattern<in A, in B, in C, in D, in E, in F, in G, H>(
        A a, B b, C c, D d, E e, F f, G g, out H h);

    public delegate bool TryPattern<in A, in B, in C, in D, in E, in F, in G, in H, I>(
        A a, B b, C c, D d, E e, F f, G g, H h, out I i);

    public delegate bool TryPattern<in A, in B, in C, in D, in E, in F, in G, in H, in I, J>(
        A a, B b, C c, D d, E e, F f, G g, H h, I i, out J j);

    public delegate bool TryPattern<in A, in B, in C, in D, in E, in F, in G, in H, in I, in J, K>(
        A a, B b, C c, D d, E e, F f, G g, H h, I i, J j, out K k);

    public delegate bool TryPattern<in A, in B, in C, in D, in E, in F, in G, in H, in I, in J, in K, L>(
        A a, B b, C c, D d, E e, F f, G g, H h, I i, J j, K k, out L l);

    public delegate bool TryPattern<in A, in B, in C, in D, in E, in F, in G, in H, in I, in J, in K, in L, M>(
        A a, B b, C c, D d, E e, F f, G g, H h, I i, J j, K k, L l, out M m);

    public delegate bool TryPattern<in A, in B, in C, in D, in E, in F, in G, in H, in I, in J, in K, in L, in M, N>(
        A a, B b, C c, D d, E e, F f, G g, H h, I i, J j, K k, L l, M m, out N n);

    public delegate bool TryPattern
        <in A, in B, in C, in D, in E, in F, in G, in H, in I, in J, in K, in L, in M, in N, O>(
        A a, B b, C c, D d, E e, F f, G g, H h, I i, J j, K k, L l, M m, N n, out O o);

    public delegate bool TryPattern
        <in A, in B, in C, in D, in E, in F, in G, in H, in I, in J, in K, in L, in M, in N, in O, P>(
        A a, B b, C c, D d, E e, F f, G g, H h, I i, J j, K k, L l, M m, N n, O o, out P p);
}