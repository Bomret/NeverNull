using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace NeverNull.Combinators {
    /// <summary>
    ///     Provides extension methods for working with <see cref="Option{T}" /> in conjunction with
    ///     <see cref="IEnumerable{T}" />.
    /// </summary>
    public static class EnumerableExt {
        /// <summary>
        ///     Returns all values in the specified <paramref name="optionalEnumerable" /> as
        ///     <see cref="Option{T}" />, if it contains an enumerable.
        ///     If the contained enumerable is empty or <paramref name="optionalEnumerable" /> is None, an empty enumerable is
        ///     returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="optionalEnumerable"></param>
        /// <returns></returns>
        public static IEnumerable<Option<T>> Exchange<T>(this Option<IEnumerable<T>> optionalEnumerable) =>
            optionalEnumerable.Match(
                None: Enumerable.Empty<Option<T>>,
                Some: xs => xs.Select(Option.From));

        /// <summary>
        ///     Returns all values in the specified <paramref name="optionalEnumerable" /> as <see cref="Option{T}" />, if it
        ///     contains an enumerable.
        ///     If the contained enumerable is empty or <paramref name="optionalEnumerable" /> is None, an empty enumerable is
        ///     returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="optionalEnumerable"></param>
        /// <returns></returns>
        public static IEnumerable<Option<T>> Exchange<T>(this Option<IEnumerable<T?>> optionalEnumerable)
            where T : struct =>
                optionalEnumerable.Match(
                    None: Enumerable.Empty<Option<T>>,
                    Some: xs => xs.Select(Option.From));

        /// <summary>
        ///     Returns all values in the specified <paramref name="optionalArray" /> as <see cref="Option{T}" />, if it contains
        ///     an array.
        ///     If the contained array is empty or <paramref name="optionalArray" /> is None, an empty array is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="optionalArray"></param>
        /// <returns></returns>
        public static Option<T>[] Exchange<T>(this Option<T[]> optionalArray) =>
            optionalArray.Match(
                None: () => new Option<T>[0],
                Some: xs => xs.Select(Option.From).ToArray());

        /// <summary>
        ///     Returns all values in the specified <paramref name="optionalArray" /> as <see cref="Option{T}" />, if it contains
        ///     an array.
        ///     If the contained array is empty or <paramref name="optionalArray" /> is None, an empty array is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="optionalArray"></param>
        /// <returns></returns>
        public static Option<T>[] Exchange<T>(this Option<T?[]> optionalArray) where T : struct =>
            optionalArray.Match(
                None: () => new Option<T>[0],
                Some: xs => xs.Select(Option.From).ToArray());

        /// <summary>
        ///     Selects all values from the options inside the specified <paramref name="enumerable" /> that contain values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<T> SelectValues<T>([NotNull] this IEnumerable<Option<T>> enumerable) {
            enumerable.ThrowIfNull(nameof(enumerable));

            return enumerable
                .Select(o => o.Match(
                    None: () => new { hasVal = false, val = default(T) },
                    Some: x => new { hasVal = true, val = x }))
                .Where(o => o.hasVal)
                .Select(o => o.val);
        }

        /// <summary>
        ///     Selects all values from the options inside the specified <paramref name="enumerable" /> that contain values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<T> SelectValues<T>([NotNull] this IEnumerable<Option<T?>> enumerable) where T : struct {
            enumerable.ThrowIfNull(nameof(enumerable));

            return enumerable
                .Select(o => o.Match(
                    None: () => new { hasVal = false, val = default(T?) },
                    Some: x => new { hasVal = x.HasValue, val = x }))
                .Where(o => o.hasVal)
                .Select(o => o.val.Value);
        }

        /// <summary>
        ///     Aggregates the values of the specified <paramref name="enumerable" /> using the given <paramref name="fold" />
        ///     function. If <paramref name="enumerable" /> is empty or all values are <see langword="null" />, None is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="fold"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> or <paramref name="fold" /> is <see langword="null" />.
        /// </exception>
        public static Option<T> AggregateOptional<T>([NotNull] this IEnumerable<T> enumerable, [NotNull] Func<T, T, T> fold) {
            enumerable.ThrowIfNull(nameof(enumerable));
            fold.ThrowIfNull(nameof(fold));

            return enumerable
                .Aggregate(Option<T>.None, (accu, current) => {
                    var currentOption = Option.From(current);

                    return accu.Match(
                        None: () => currentOption,
                        Some: previousValue => currentOption.Match(
                            None: () => previousValue,
                            Some: currentValue => fold(previousValue, currentValue)));
                });
        }

        /// <summary>
        ///     Aggregates the values of the specified <paramref name="enumerable" /> using the given <paramref name="fold" />
        ///     function. If <paramref name="enumerable" /> is empty or all values are <see langword="null" />, None is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="fold"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> or <paramref name="fold" /> is <see langword="null" />.
        /// </exception>
        public static Option<T> AggregateOptionalNullable<T>([NotNull] this IEnumerable<T?> enumerable, [NotNull] Func<T, T, T> fold)
            where T : struct {
            enumerable.ThrowIfNull(nameof(enumerable));
            fold.ThrowIfNull(nameof(fold));

            return enumerable
                .Aggregate(Option<T>.None, (accu, current) => {
                    var currentOption = Option.From(current);

                    return accu.Match(
                        None: () => currentOption,
                        Some: previousValue => currentOption.Match(
                            None: () => previousValue,
                            Some: currentValue => fold(previousValue, currentValue)));
                });
        }

        /// <summary>
        ///     Returns an <see cref="Option{T}" /> containing an <see cref="IEnumerable{T}" /> with all values or None, if any of
        ///     the options in the specified <paramref name="enumerable" /> does not contain a value or the enumerable is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> is <see langword="null" />.
        /// </exception>
        public static Option<IEnumerable<T>> AllOrNone<T>([NotNull] this IEnumerable<Option<T>> enumerable) {
            enumerable.ThrowIfNull(nameof(enumerable));

            var results = new List<T>();
            foreach (var option in enumerable) {
                if (option.IsEmpty)
                    return Option.None;

                option.IfSome(x => results.Add(x));
            }

            return results.Count == 0
                ? Option.None
                : Option.From(results.AsEnumerable());
        }

        /// <summary>
        ///     Returns an <see cref="Option{T}" /> containing an <see cref="IEnumerable{T}" /> with all values or None, if any of
        ///     the options in the specified <paramref name="enumerable" /> does not contain a value or the enumerable is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> is <see langword="null" />.
        /// </exception>
        public static Option<IEnumerable<T>> AllOrNone<T>([NotNull] this IEnumerable<Option<T?>> enumerable) where T : struct {
            enumerable.ThrowIfNull(nameof(enumerable));

            var results = new List<T>();
            foreach (var option in enumerable) {
                if (option.IsEmpty)
                    return Option.None;

                option.IfSome(x => {
                    if (x.HasValue)
                        results.Add(x.Value);
                });
            }

            return results.Count == 0
                ? Option.None
                : Option.From(results.AsEnumerable());
        }

        /// <summary>
        ///     Returns the first value of the specified <paramref name="enumerable" /> wrapped in an <see cref="Option{T}" />.
        ///     If <paramref name="enumerable" /> is empty or the first value is <see langword="null" />, None is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> is <see langword="null" />.
        /// </exception>
        public static Option<T> FirstOptional<T>([NotNull] this IEnumerable<T> enumerable) {
            enumerable.ThrowIfNull(nameof(enumerable));

            foreach (var value in enumerable)
                return Option.From(value);

            return Option.None;
        }

        /// <summary>
        ///     Returns the first value of the specified <paramref name="enumerable" /> wrapped in an <see cref="Option{T}" />.
        ///     If <paramref name="enumerable" /> is empty or the first value is <see langword="null" />, None is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> is <see langword="null" />.
        /// </exception>
        public static Option<T> FirstOptional<T>([NotNull] this IEnumerable<T?> enumerable) where T : struct {
            enumerable.ThrowIfNull(nameof(enumerable));

            foreach (var value in enumerable)
                return Option.From(value);

            return Option.None;
        }

        /// <summary>
        ///     Returns the last value of the specified <paramref name="enumerable" /> wrapped in an <see cref="Option{T}" />.
        ///     If <paramref name="enumerable" /> is empty or the last value is <see langword="null" />, None is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> is <see langword="null" />.
        /// </exception>
        public static Option<T> LastOptional<T>([NotNull] this IEnumerable<T> enumerable) {
            enumerable.ThrowIfNull(nameof(enumerable));

            var xs = enumerable.ToList();
            return xs.Count == 0
                ? Option.None
                : Option.From(xs[xs.Count - 1]);
        }

        /// <summary>
        ///     Returns the last value of the specified <paramref name="enumerable" /> wrapped in an <see cref="Option{T}" />.
        ///     If <paramref name="enumerable" /> is empty or the last value is <see langword="null" />, None is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> is <see langword="null" />.
        /// </exception>
        public static Option<T> LastOptional<T>([NotNull] this IEnumerable<T?> enumerable) where T : struct {
            enumerable.ThrowIfNull(nameof(enumerable));

            var xs = enumerable.ToList();
            return xs.Count == 0
                ? Option.None
                : Option.From(xs[xs.Count - 1]);
        }

        /// <summary>
        ///     Returns the only element in the specified <paramref name="enumerable" /> wrapped in an <see cref="Option{T}" />.
        ///     If <paramref name="enumerable" /> is empty or the single element is <see langword="null" />, None is returned.
        ///     Throws an exception if <paramref name="enumerable" /> contains more than one element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="enumerable" /> contains more than one element.
        /// </exception>
        public static Option<T> SingleOptional<T>([NotNull] this IEnumerable<T> enumerable) {
            enumerable.ThrowIfNull(nameof(enumerable));

            var xs = enumerable.ToList();
            return xs.Count == 0
                ? Option.None
                : Option.From(xs.SingleOrDefault());
        }

        /// <summary>
        ///     Returns the only element in the specified <paramref name="enumerable" /> wrapped in an <see cref="Option{T}" />.
        ///     If <paramref name="enumerable" /> is empty or the single element is <see langword="null" />, None is returned.
        ///     Throws an exception if <paramref name="enumerable" /> contains more than one element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="enumerable" /> contains more than one element.
        /// </exception>
        public static Option<T> SingleOptional<T>([NotNull] this IEnumerable<T?> enumerable) where T : struct {
            enumerable.ThrowIfNull(nameof(enumerable));

            var xs = enumerable.ToList();
            return xs.Count == 0
                ? Option.None
                : Option.From(xs.SingleOrDefault());
        }

        /// <summary>
        ///     Returns the only element in the specified <paramref name="enumerable" /> taht matches a predicate wrapped in an
        ///     <see cref="Option{T}" />.
        ///     If <paramref name="enumerable" /> is empty or the single element is <see langword="null" /> or the predicate does
        ///     not match any element, None is returned.
        ///     Throws an exception if <paramref name="enumerable" /> contains more than one element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> or <paramref name="predicate" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="enumerable" /> contains more than one element.
        /// </exception>
        public static Option<T> SingleOptional<T>([NotNull] this IEnumerable<T> enumerable, [NotNull] Func<T, bool> predicate) {
            enumerable.ThrowIfNull(nameof(enumerable));
            predicate.ThrowIfNull(nameof(predicate));

            var xs = enumerable.ToList();
            return xs.Count == 0
                ? Option.None
                : Option.From(xs.SingleOrDefault(predicate));
        }

        /// <summary>
        ///     Returns the only element in the specified <paramref name="enumerable" /> taht matches a predicate wrapped in an
        ///     <see cref="Option{T}" />.
        ///     If <paramref name="enumerable" /> is empty or the single element is <see langword="null" /> or the predicate does
        ///     not match any element, None is returned.
        ///     Throws an exception if <paramref name="enumerable" /> contains more than one element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> or <paramref name="predicate" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="enumerable" /> contains more than one element.
        /// </exception>
        public static Option<T> SingleOptionalNullable<T>([NotNull] this IEnumerable<T?> enumerable, [NotNull] Func<T, bool> predicate)
            where T : struct {
            enumerable.ThrowIfNull(nameof(enumerable));
            predicate.ThrowIfNull(nameof(predicate));

            var xs = enumerable.ToList();
            return xs.Count == 0
                ? Option.None
                : Option.From(xs.SingleOrDefault(x => x.HasValue && predicate(x.Value)));
        }
    }
}