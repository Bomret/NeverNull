using System;

namespace NeverNull.Combinators
{
    public static class DoExt
    {
        public static Option<T> Do<T>(this Option<T> option, Action action)
        {
            if (option.HasValue)
                action();

            return option;
        }
    }
}