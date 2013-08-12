using System;

namespace NeverNull {
    public static class Combinators {
        public static IOption<TNextValue> AndThen<TValue, TNextValue>(this IOption<TValue> option,
                                                                      Func<IOption<TValue>, IOption<TNextValue>>
                                                                          continueWith) {
            return option.IsEmpty
                       ? new None<TNextValue>()
                       : continueWith(option);
        }

        public static IOption<TNextValue> Map<TValue, TNextValue>(this IOption<TValue> option,
                                                                  Func<TValue, TNextValue> applyTo) {
            return option.HasValue
                       ? Option.Create(applyTo(option.Value))
                       : new None<TNextValue>();
        }

        public static IOption<TNextValue> FlatMap<TValue, TNextValue>(this IOption<TValue> option,
                                                                      Func<TValue, IOption<TNextValue>> applyTo) {
            return option.HasValue
                       ? applyTo(option.Value)
                       : new None<TNextValue>();
        }

        public static IOption<TValue> Filter<TValue>(this IOption<TValue> option,
                                                     Func<TValue, bool> predicate) {
            if (option.IsEmpty) {
                return option;
            }

            return predicate(option.Value)
                       ? option
                       : new None<TValue>();
        }

        public static IOption<TValue> OrElse<TValue>(this IOption<TValue> option,
                                                     Func<IOption<TValue>> orElse) {
            return option.HasValue
                       ? option
                       : orElse();
        }

        public static IOption<TValue> OrElse<TValue>(this IOption<TValue> option,
                                                     IOption<TValue> orElse) {
            return option.HasValue
                       ? option
                       : orElse;
        }

        public static IOption<T> Recover<T>(this IOption<T> option,
                                            T recoverValue) {
            return option.HasValue
                       ? option
                       : new Some<T>(recoverValue);
        }

        public static IOption<T> Recover<T>(this IOption<T> option,
                                            Func<T> recoverFunc) {
            return option.HasValue
                       ? option
                       : new Some<T>(recoverFunc());
        }
    }
}