using System;

namespace NeverNull {
    public static class Combinators {
        public static IOption<TNewValue> Map<T, TNewValue>(this IOption<T> option,
                                                           Func<T, TNewValue> func) {
            return option.HasValue
                       ? Option.Create(func(option.Value))
                       : new None<TNewValue>();
        }

        public static IOption<TNextValue> FlatMap<T, TNextValue>(this IOption<T> option,
                                                                 Func<T, IOption<TNextValue>> func) {
            return option.HasValue
                       ? func(option.Value)
                       : new None<TNextValue>();
        }

        public static IOption<T> Filter<T>(this IOption<T> option,
                                           Func<T, bool> predicate) {
            if (!option.HasValue) {
                return option;
            }

            return predicate(option.Value)
                       ? option
                       : new None<T>();
        }

        public static IOption<T> OrElse<T>(this IOption<T> option,
                                           Func<IOption<T>> elseFunc) {
            return option.HasValue
                       ? option
                       : elseFunc();
        }

        public static IOption<T> OrElse<T>(this IOption<T> option,
                                           IOption<T> elseOption) {
            return option.HasValue
                       ? option
                       : elseOption;
        }
    }
}