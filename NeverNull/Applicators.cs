using System;

namespace NeverNull {
    public static class Applicators {
        public static T Get<T>(this IMaybe<T> maybe) {
            return maybe.Value;
        }

        public static T GetOrElse<T>(this IMaybe<T> maybe, T elseValue) {
            return maybe.HasValue ? maybe.Value : elseValue;
        }

        public static T GetOrElse<T>(this IMaybe<T> maybe, Func<T> elseFunc) {
            return GetOrElse(maybe, elseFunc());
        }

        public static void WhenSome<T>(this IMaybe<T> maybe, Action<T> action) {
            if (maybe.HasValue) action(maybe.Value);
        }

        public static void WhenNone<T>(this IMaybe<T> maybe, Action action) {
            if (maybe.IsEmpty) action();
        }

        public static void Match<T>(this IMaybe<T> maybe, Action<T> whenSome, Action whenNone) {
            if (maybe.HasValue) whenSome(maybe.Value);
            else whenNone();
        }
    }
}