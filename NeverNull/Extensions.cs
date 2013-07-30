using System;

namespace NeverNull {
    public static class Extensions {
        public static T Get<T>(this IOption<T> option) {
            if (option.HasValue) {
                return option.Value;
            }

            throw new NotSupportedException("The given option does not have a value.");
        }

        public static T GetOrElse<T>(this IOption<T> option,
                                     T elseValue) {
            return option.HasValue
                       ? option.Value
                       : elseValue;
        }

        public static T GetOrElse<T>(this IOption<T> option,
                                     Func<T> elseFunc) {
            return option.HasValue
                       ? option.Value
                       : elseFunc();
        }

        public static void WhenSome<T>(this IOption<T> option,
                                       Action<T> action) {
            if (option.HasValue) {
                action(option.Value);
            }
        }

        public static void WhenNone<T>(this IOption<T> option,
                                       Action action) {
            if (option.HasValue) {
                action();
            }
        }

        public static void Match<T>(this IOption<T> option,
                                    Action<T> whenSome,
                                    Action whenNone) {
            if (option.HasValue) {
                whenSome(option.Value);
            } else {
                whenNone();
            }
        }
    }
}