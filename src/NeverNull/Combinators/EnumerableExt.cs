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
        ///     Returns all values in the enumerable of this option as options, if it contains an enumerable.
        ///     if the contained enumerable is empty or None, an empty enumerable is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="optionalEnumerable"></param>
        /// <returns></returns>
        public static IEnumerable<Option<T>> Exchange<T>(this Option<IEnumerable<T?>> optionalEnumerable) where T : struct {
            IEnumerable<T?> xs;
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
        ///     Returns all values in the array of this option as options, if it contains an array.
        ///     if the contained array is empty or None, an empty array is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="optionalArray"></param>
        /// <returns></returns>
        public static Option<T>[] Exchange<T>(this Option<T?[]> optionalArray) where T : struct {
            T?[] xs;
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
        ///     Selects all values from the options in this enumerable that contain nullables with values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable"/> is null.</exception>
        public static IEnumerable<T> SelectValues<T>(this IEnumerable<Option<T?>> enumerable) where T : struct{
            enumerable.ThrowIfNull(nameof(enumerable));

            foreach (var option in enumerable) {
                T? nullable;
                if (option.TryGet(out nullable) && nullable.HasValue)
                    yield return nullable.Value;
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
        ///     Aggregates the values of this enumerable using the given <paramref name="fold" />
        ///     function. If this enumerable is empty or all values are NULL, None is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="fold"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable"/> or <paramref name="fold"/> is null.</exception>
        public static Option<T> AggregateOptionalNullable<T>(this IEnumerable<T?> enumerable, Func<T, T, T> fold) where T : struct {
            enumerable.ThrowIfNull(nameof(enumerable));
            fold.ThrowIfNull(nameof(fold));

            return enumerable.Aggregate(Option<T>.None, (accu, current) => {
                T previousValue;
                return accu.TryGet(out previousValue) && current.HasValue ? fold(previousValue, current.Value) : Option.From(current);
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
        ///     Returns an option containing all values or None, if any of the options in this enumerable
        ///     does not contain a value or the enumerable is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable"/> is null.</exception>
        public static Option<IEnumerable<T>> AllOrNone<T>(this IEnumerable<Option<T?>> enumerable) where T : struct{
            enumerable.ThrowIfNull(nameof(enumerable));

            var results = new List<T>();
            foreach (var option in enumerable) {
                T? nullable;
                if (option.TryGet(out nullable) && nullable.HasValue) results.Add(nullable.Value);
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
        ///     Returns the first value of this enumerable wrapped in an option.
        ///     If this enumerable is empty or the first value is NULL, None is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable"/> is null.</exception>
        public static Option<T> FirstOptional<T>(this IEnumerable<T?> enumerable) where T : struct {
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
        ///     Returns the last value of this enumerable wrapped in an option.
        ///     If this enumerable is empty or the last value is NULL, None is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable"/> is null.</exception>
        public static Option<T> LastOptional<T>(this IEnumerable<T?> enumerable) where T : struct {
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
        public static Option<T> SingleOptional<T>(this IEnumerable<T> enumerable) {
            enumerable.ThrowIfNull(nameof(enumerable));

            var xs = enumerable.ToList();
            return xs.Count == 0
                ? Option.None
                : Option.From(xs.SingleOrDefault());
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
        public static Option<T> SingleOptional<T>(this IEnumerable<T?> enumerable) where T : struct {
            enumerable.ThrowIfNull(nameof(enumerable));

            var xs = enumerable.ToList();
            return xs.Count == 0
                ? Option.None
                : Option.From(xs.SingleOrDefault());
        }

        /// <summary>
        ///     Returns the only element in this enumerable that matches a predicate, wrapped in an option.
        ///     If this enumerable is empty or the single matching element is NULL, None is returned.
        ///     Throws an exception if this enumerable contains more than one matching element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable"/> or <paramref name="predicate"/> is null.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="enumerable"/> contains more than one element.</exception>
        public static Option<T> SingleOptional<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) {
            enumerable.ThrowIfNull(nameof(enumerable));
            predicate.ThrowIfNull(nameof(predicate));

            var xs = enumerable.ToList();
            return xs.Count == 0
                ? Option.None
                : Option.From(xs.SingleOrDefault(predicate));
        }

        /// <summary>
        ///     Returns the only element in this enumerable that matches a predicate, wrapped in an option.
        ///     If this enumerable is empty or the single matching element is NULL, None is returned.
        ///     Throws an exception if this enumerable contains more than one matching element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumerable"/> or <paramref name="predicate"/> is null.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="enumerable"/> contains more than one element.</exception>
        public static Option<T> SingleOptionalNullable<T>(this IEnumerable<T?> enumerable, Func<T, bool> predicate) where T : struct {
            enumerable.ThrowIfNull(nameof(enumerable));
            predicate.ThrowIfNull(nameof(predicate));

            var xs = enumerable.ToList();
            return xs.Count == 0
                ? Option.None
                : Option.From(xs.SingleOrDefault(x => x.HasValue && predicate(x.Value)));
        }
    }
}