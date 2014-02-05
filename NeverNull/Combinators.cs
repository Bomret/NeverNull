using System;

namespace NeverNull {
    public static class Combinators {
        public static Option<B> Then<A, B>(this Option<A> option, Func<Option<A>, B> f) {
            return option.Map(_ => f(option));
        }

        public static Option<B> ThenWith<A, B>(this Option<A> option, Func<Option<A>, Option<B>> f) {
            return option.FlatMap(_ => f(option));
        }

        public static Option<B> Map<A, B>(this Option<A> option, Func<A, B> f) {
            return option.Match(a => Option.From(f(a)), () => Option.None);
        }

        public static Option<B> FlatMap<A, B>(this Option<A> option, Func<A, Option<B>> f) {
            return option.Match(f, () => Option.None);
        }

        public static Option<T> Flatten<T>(this Option<Option<T>> nestedOption) {
            return nestedOption.Value;
        }

        public static Option<B> Transform<A, B>(this Option<A> option, Func<A, B> ifSome, Func<B> ifNone) {
            return option.Match(ifSome, ifNone);
        }

        public static Option<B> TransformWith<A, B>(this Option<A> option, Func<A, Option<B>> ifSome,
                                                    Func<Option<B>> ifNone) {
            return option.Match(ifSome, ifNone);
        }

        public static Option<C> Zip<A, B, C>(this Option<A> optionA, Option<B> optionB, Func<A, B, C> f) {
            return optionA.FlatMap(a => optionB.Map(b => f(a, b)));
        }

        public static Option<C> ZipWith<A, B, C>(this Option<A> optionA, Option<B> optionB, Func<A, B, Option<C>> f) {
            return optionA.FlatMap(a => optionB.FlatMap(b => f(a, b)));
        }

        public static Option<T> Filter<T>(this Option<T> option, Func<T, bool> predicate) {
            return option.FlatMap(a => predicate(a) ? option : Option.None);
        }

        public static Option<T> Reject<T>(this Option<T> option, Func<T, bool> rejectCondition) {
            return option.Filter(t => !rejectCondition(t));
        }

        public static Option<T> OrElse<T>(this Option<T> option, T orElse) {
            return option.Match(a => a, () => orElse);
        }

        public static Option<T> OrElseWith<T>(this Option<T> option, Option<T> orElse) {
            return option.Match(a => a, () => orElse);
        }

        public static Option<T> Tap<T>(this Option<T> option, Action<T> tap) {
            option.IfSome(tap);

            return option;
        }

        public static Option<T> Do<T>(this Option<T> option, Action action) {
            action();

            return option;
        }
    }
}