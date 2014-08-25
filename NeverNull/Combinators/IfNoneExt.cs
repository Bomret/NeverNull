using System;

namespace NeverNull.Combinators
{
    public static class IfNoneExt
    {
        public static void IfNone<T>(this Option<T> option, Action action)
        {
            if (option.IsEmpty)
                action();
        }
    }
}