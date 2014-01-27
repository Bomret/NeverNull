using System;

namespace NeverNull {
    public static class Combinators {
        public static Option<B> Then<A, B>(this Option<A> option, Func<Option<A>, Option<B>> f) {
            return option.FlatMap(_ => f(option));
        }

        public static Option<B> Map<A, B>(this Option<A> option, Func<A, B> f) {
            return option.FlatMap(a => Option.From(f(a)));
        }

        public static Option<B> FlatMap<A, B>(this Option<A> option, Func<A, Option<B>> f) {
            return option.HasValue ? f(option) : Option.None;
        }

        public static Option<T> Filter<T>(this Option<T> option, Func<T, bool> predicate) {
            return option.FlatMap(a => predicate(a) ? option : Option.None);
        }

        public static Option<T> OrElse<T>(this Option<T> option, Func<T> orElse) {
            return OrElse(option, orElse());
        }

        public static Option<T> OrElse<T>(this Option<T> option, T orElse) {
            return option.HasValue ? option.Value : orElse;
        }

        public static Option<T> Recover<T>(this Option<T> option, T recoverValue) {
            return option.OrElse(recoverValue);
        }

        public static Option<T> Recover<T>(this Option<T> option, Func<T> recoverFunc) {
            return option.OrElse(recoverFunc());
        }
    }
}