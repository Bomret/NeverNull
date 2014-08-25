namespace NeverNull
{
    public delegate bool TryPattern<in A, B>(A a, out B b);

    public delegate bool TryPattern<in A, in B, C>(A a, B b, out C c);

    public delegate bool TryPattern<in A, in B, in C, D>(A a, B b, C c, out D d);

    public delegate bool TryPattern<in A, in B, in C, in D, E>(A a, B b, C c, D d, out E e);

    public delegate bool TryPattern<in A, in B, in C, in D, in E, F>(A a, B b, C c, D d, E e, out F f);
}