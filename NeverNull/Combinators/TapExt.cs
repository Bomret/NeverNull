using System;

namespace NeverNull.Combinators
{
    public static class TapExt
    {
        public static Option<T> Tap<T>(this Option<T> option, Action<T> tap)
        {
            if (option.HasValue)
                tap(option.Value);

            return option;
        }
    }
}