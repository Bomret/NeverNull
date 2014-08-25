using System;

namespace NeverNull.Combinators
{
    public static class TransformWithExt
    {
        public static Option<B> TransformWith<A, B>(this Option<A> option, Func<A, Option<B>> ifSome,
                                                    Func<Option<B>> ifNone)
        {
            return option.IsEmpty ? ifNone() : ifSome(option.Value);
        }
    }
}