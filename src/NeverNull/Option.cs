using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

// ReSharper disable InconsistentNaming

namespace NeverNull
{
    /// <summary>
    ///     Represents the presence or absence of a value of a specific type.
    /// </summary>
    /// <typeparam name="T">The type of the value</typeparam>
    [DebuggerDisplay("{ToString(),nq}")]
    public struct Option<T> : IEquatable<Option<T>>, IStructuralEquatable, IStructuralComparable, IComparable<Option<T>>,
        IComparable
    {
        readonly T _value;

        /// <summary>
        ///     Returns an option that represents the absence of a value.
        /// </summary>
        public static Option<T> None => default(Option<T>);

        /// <summary>
        ///     Indicates if this option contains a value.
        ///     Is true if a value is present, false otherwise.
        /// </summary>
        [Obsolete("This property is deprecated and will be removed in the next release. Use IsSome instead.", true)]
        public bool HasValue { get; }

        /// <summary>
        ///     Indicates if this option contains a value, i.e. is Some of T.
        ///     Is true if a value is present, false otherwise.
        /// </summary>
        public bool IsSome { get; }

        /// <summary>
        ///     Indicates if this option is empty.
        ///     Is true if no value is present, false otherwise.
        /// </summary>
        [Obsolete("This property is deprecated and will be removed in the next release. Use IsNone instead.", true)]
        public bool IsEmpty => !HasValue;

        /// <summary>
        ///     Indicates if this option is empty, i.e. is None.
        ///     Is true if no value is present, false otherwise.
        /// </summary>
        public bool IsNone => !IsSome;

        internal Option(T value) : this()
        {
            _value = value;
            IsSome = true;
        }

        /// <summary>
        ///     Executes the specified side effect on the value of this if it contains one.
        /// </summary>
        /// <param name="sideEffect"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="sideEffect" /> is null.
        /// </exception>
        public void IfSome(Action<T> sideEffect)
        {
            sideEffect.ThrowIfNull(nameof(sideEffect));

            if (IsSome)
                sideEffect(_value);
        }

        /// <summary>
        ///     Executes the specified side effect if this is None.
        /// </summary>
        /// <param name="sideEffect"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="sideEffect" /> is null.
        /// </exception>
        public void IfNone(Action sideEffect)
        {
            sideEffect.ThrowIfNull(nameof(sideEffect));

            if (IsNone)
                sideEffect();
        }

        /// <summary>
        ///     Executes a given side effect if this option contains a value, otherwise a different side effect.
        /// </summary>
        /// <param name="Some"></param>
        /// <param name="None"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="Some" /> or <paramref name="None" /> is null.
        /// </exception>
        // ReSharper disable once ParameterHidesMember
        public void Match(Action<T> Some, Action None)
        {
            Some.ThrowIfNull(nameof(Some));
            None.ThrowIfNull(nameof(None));

            if (IsSome) Some(_value);
            else None();
        }

        /// <summary>
        ///     Applies the first selector to the value of this option if it contains one, otherwise executes the second selector.
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="Some"></param>
        /// <param name="None"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="Some" /> or <paramref name="None" /> is null.
        /// </exception>
        // ReSharper disable once ParameterHidesMember
        public R Match<R>(Func<T, R> Some, Func<R> None)
        {
            Some.ThrowIfNull(nameof(Some));
            None.ThrowIfNull(nameof(None));

            return IsSome
                ? Some(_value)
                : None();
        }

        #region Formatting

        /// <summary>
        ///     Returns the string representation of this option.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            IsSome ? $"Some({_value})" : "None";

        #endregion

        #region Equality

        static int CombineHashCodes(int h1, int h2) => ((h1 << 5) + h1) ^ h2;

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
        {
            if (!(other is Option<T>)) return false;

            var option = (Option<T>)other;
            return (IsSome && option.IsSome && comparer.Equals(_value, option._value))
                   || IsSome == false && option.IsSome == false;
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer) =>
            CombineHashCodes(comparer.GetHashCode(IsSome), comparer.GetHashCode(_value));

        /// <summary>
        ///     Compares the specified option with this one for equality.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Option<T> other) =>
            ((IStructuralEquatable)this).Equals(other, EqualityComparer<object>.Default);

        /// <summary>
        ///     Indicates if this instance is equal to the specified object.
        /// </summary>
        public override bool Equals(object obj) =>
            ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);

        /// <summary>
        ///     Returns the calculated hash code for this Option.
        /// </summary>
        public override int GetHashCode() =>
            ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);

        /// <summary>
        ///     Compares the specified Options for equality.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Option<T> left, Option<T> right) =>
            ((IStructuralEquatable)left).Equals(right, EqualityComparer<object>.Default);

        /// <summary>
        ///     Compares the specified Options for inequality.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Option<T> left, Option<T> right) =>
            ((IStructuralEquatable)left).Equals(right, EqualityComparer<object>.Default);

        #endregion

        #region Comparability

        int IStructuralComparable.CompareTo(object other, IComparer comparer)
        {
            if (!(other is Option<T>))
                throw new ArgumentException("Provided object is not of type Option<T>", nameof(other));

            var otherOption = (Option<T>)other;
            if (IsSome && !otherOption.IsSome)
                return 1;
            if (!IsSome && otherOption.IsSome)
                return -1;

            return comparer.Compare(_value, otherOption._value);
        }

        int IComparable<Option<T>>.CompareTo(Option<T> other) =>
            ((IStructuralComparable)this).CompareTo(other, Comparer<object>.Default);

        int IComparable.CompareTo(object obj) =>
            ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);

        #endregion

        #region Implicits

        /// <summary>
        ///     Implicitly converts the specified value into an Option.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator Option<T>(T value) => Option.From(value);

        /// <summary>
        ///     Implicitly converts the specified None into its generic representation.
        /// </summary>
        /// <param name="_"></param>
        public static implicit operator Option<T>(None _) => None;

        #endregion
    }
}
