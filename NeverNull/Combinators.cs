using System;

namespace NeverNull {
    public static class Combinators {
        public static IMaybe<B> Then<A, B>(this IMaybe<A> maybe, Func<IMaybe<A>, IMaybe<B>> f) {
            return maybe.FlatMap(_ => f(maybe));
        }

        public static IMaybe<B> Map<A, B>(this IMaybe<A> maybe, Func<A, B> f) {
            return maybe.FlatMap(a => Maybe.From(f(a)));
        }

        public static IMaybe<B> FlatMap<A, B>(this IMaybe<A> maybe, Func<A, IMaybe<B>> f) {
            return maybe.HasValue ? f(maybe.Value) : new None<B>();
        }

        public static IMaybe<T> Filter<T>(this IMaybe<T> maybe, Func<T, bool> predicate) {
            return maybe.FlatMap(a => predicate(a) ? maybe : new None<T>());
        }

        public static IMaybe<B> OrElse<A, B>(this IMaybe<A> maybe, Func<IMaybe<B>> orElse) where A : B {
            return OrElse(maybe, orElse());
        }

        public static IMaybe<B> OrElse<A, B>(this IMaybe<A> maybe, IMaybe<B> orElse) where A : B {
            return maybe.HasValue ? new Some<B>(maybe.Value) : orElse;
        }

        public static IMaybe<T> Recover<T>(this IMaybe<T> maybe, T recoverValue) {
            return maybe.OrElse(Maybe.From(recoverValue));
        }

        public static IMaybe<T> Recover<T>(this IMaybe<T> maybe, Func<T> recoverFunc) {
            return maybe.OrElse(Maybe.From(recoverFunc()));
        }
    }
}