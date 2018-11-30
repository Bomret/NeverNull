using System;

namespace NeverNull.Combinators
{
  /// <summary>
  ///   Provides extension methods to wrap values into instances of Option.
  /// </summary>
  public static class ToOptionExt
  {
    /// <summary>
    ///   Wraps this value in a Option.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Option<T> ToOption<T>(this T value)
    {
      return Option.From(value);
    }

    /// <summary>
    ///   Wraps the value of this Nullable in a option or returns None.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="nullable"></param>
    /// <returns></returns>
    public static Option<T> ToOption<T>(this T? nullable) where T : struct
    {
      return Option.From(nullable);
    }


    public static Option<TR> ToOptionMapped<T, TR>(this T? item, Func<T, TR> mapFn) where T : struct
    {
      return item.ToOption().Select(mapFn);
    }

    public static Option<TR> ToOptionMappedOrNoneIf<T, TR>(this T item, T nullValue, Func<T, TR> mapFn) where T : struct
    {
      return item.Equals(nullValue) ? Option<TR>.None : mapFn(item);
    }
  }
}
