using System;
using System.Collections.Generic;
using System.Linq;

namespace NeverNull.Combinators {
    public static class EnumerableExt {
        /// <summary>
        ///     Returns all values in the enumerable of this option as options, if it contains an enumerable.
        ///     if the contained enumerable is empty or None, an empty enumerable is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="optionalEnumerable"></param>
        /// <returns></returns>
        public static IEnumerable<Option<T>> Exchange<T>(this Option<IEnumerable<T>> optionalEnumerable) {
            IEnumerable<T> xs;
            return optionalEnumerable.TryGet(out xs)
                ? xs.Select(Option.From)
                : Enumerable.Empty<Option<T>>();
        }

        /// <summary>
        ///     Returns all values in the array of this option as options, if it contains an array.
        ///     if the contained array is empty or None, an empty array is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="optionalArray"></param>
        /// <returns></returns>
        public static Option<T>[] Exchange<T>(this Option<T[]> optionalArray) {
            T[] xs;
            return optionalArray.TryGet(out xs)
                ? xs.Select(Option.From).ToArray()
                : new Option<T>[0];
        }

        /// <summary>
        ///     Selects all values from the options in this enumerable that contain values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable"/> is null.</exception>
        public static IEnumerable<T> SelectValues<T>(this IEnumerable<Option<T>> enumerable) {
            enumerable.ThrowIfNull(nameof(enumerable));

            foreach (var option in enumerable) {
                T value;
                if (option.TryGet(out value))
                    yield return value;
            }
        }

        /// <summary>
        ///     Aggregates the values of this enumerable using the given <paramref name="fold" />
        ///     function. If this enumerable is empty or all values are NULL, None is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="fold"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable"/> or <paramref name="fold"/> is null.</exception>
        public static Option<T> AggregateOptional<T>(this IEnumerable<T> enumerable, Func<T, T, T> fold) {
            enumerable.ThrowIfNull(nameof(enumerable));
            fold.ThrowIfNull(nameof(fold));

            return enumerable.Aggregate(Option<T>.None, (accu, current) => {
                T previousValue;
                return accu.TryGet(out previousValue) ? fold(previousValue, current) : Option.From(current);
            });
        }

        /// <summary>
        ///     Returns an option containing all values or None, if any of the options in this enumerable
        ///     does not contain a value or the enumerable is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable"/> is null.</exception>
        public static Option<IEnumerable<T>> AllOrNone<T>(this IEnumerable<Option<T>> enumerable) {
            enumerable.ThrowIfNull(nameof(enumerable));

            var results = new List<T>();
            foreach (var option in enumerable) {
                T t;
                if (option.TryGet(out t)) results.Add(t);
                else return Option.None;
            }
            return results.Count == 0
                ? Option.None
                : Option.From(results.AsEnumerable());
        }

        /// <summary>
        ///     Returns the first value of this enumerable wrapped in an option.
        ///     If this enumerable is empty or the first value is NULL, None is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable"/> is null.</exception>
        public static Option<T> FirstOptional<T>(this IEnumerable<T> enumerable) {
            enumerable.ThrowIfNull(nameof(enumerable));

            foreach (var value in enumerable)
                return Option.From(value);

            return Option.None;
        }

        /// <summary>
        ///     Returns the last value of this enumerable wrapped in an option.
        ///     If this enumerable is empty or the last value is NULL, None is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable"/> is null.</exception>
        public static Option<T> LastOptional<T>(this IEnumerable<T> enumerable) {
            enumerable.ThrowIfNull(nameof(enumerable));

            var xs = enumerable.ToList();
            return xs.Count == 0
                ? Option.None
                : Option.From(xs[xs.Count - 1]);
        }

        /// <summary>
        ///     Returns the only element in this enumerable wrapped in an option.
        ///     If this enumerable is empty or the single element is NULL, None is returned.
        ///     Throws an exception if this enumerable contains more than one element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable"/> is null.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="enumerable"/> contains more than one element.</exception>
        public static Option<T> SingleOptional<T>(this IEnumerable<T> enumerable) =>
            SingleOptional(enumerable, _ => true);

        /// <summary>
        ///     Returns the only element in this enumerable that matches a predicate, wrapped in an option.
        ///     If this enumerable is empty or the single matching element is NULL, None is returned.
        ///     Throws an exception if this enumerable contains more than one matching element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Option<T> SingleOptional<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) {
            enumerable.ThrowIfNull(nameof(enumerable));
            predicate.ThrowIfNull(nameof(predicate));

            var xs = enumerable.ToList();
            switch (xs.Count) {
                case 0:
                    return Option.None;
                case 1:
                    return predicate(xs[0]) ? xs[0] : Option<T>.None;
                default:
                    return xs.SingleOrDefault(predicate);
            }
        }
    }
}