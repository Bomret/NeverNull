using System;

namespace NeverNull {
    public static class Extensions {
        public static TValue Get<TValue>(this IOption<TValue> option) {
            if (option.HasValue) {
                return option.Value;
            }

            throw new NotSupportedException("The given option does not have a value.");
        }

        public static TValue GetOrElse<TValue>(this IOption<TValue> option,
                                               TValue elseValue) {
            return option.HasValue
                       ? option.Value
                       : elseValue;
        }

        public static TValue GetOrElse<TValue>(this IOption<TValue> option,
                                               Func<TValue> elseFunc) {
            return option.HasValue
                       ? option.Value
                       : elseFunc();
        }

        public static void WhenSome<TValue>(this IOption<TValue> option,
                                            Action<TValue> action) {
            if (option.HasValue) {
                action(option.Value);
            }
        }

        public static void WhenNone<TValue>(this IOption<TValue> option,
                                            Action action) {
            if (option.IsEmpty) {
                action();
            }
        }

        public static void Match<TValue>(this IOption<TValue> option,
                                         Action<TValue> whenSome,
                                         Action whenNone) {
            if (option.HasValue) {
                whenSome(option.Value);
            } else {
                whenNone();
            }
        }
    }
}