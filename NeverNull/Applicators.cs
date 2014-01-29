using System;

namespace NeverNull {
    public static class Applicators {
        public static T Get<T>(this Option<T> option) {
            return option.Value;
        }

        public static T GetOrElse<T>(this Option<T> option, T elseValue) {
            return option.HasValue ? option.Value : elseValue;
        }

        public static T GetOrDefault<T>(this Option<T> option) {
            return option.HasValue ? option.Value : default(T);
        }

        public static void IfSome<T>(this Option<T> option, Action<T> action) {
            if (option.HasValue) action(option.Value);
        }

        public static void IfNone<T>(this Option<T> option, Action action) {
            if (option.IsEmpty) action();
        }

        public static void Match<T>(this Option<T> option, Action<T> ifSome, Action ifNone) {
            if (option.HasValue)
                ifSome(option.Value);
            else
                ifNone();
        }

        public static B Match<A, B>(this Option<A> option, Func<A, B> ifSome, Func<B> ifNone) {
            return option.HasValue ? ifSome(option.Value) : ifNone();
        }
    }
}