using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace NeverNull {
    /// <summary>
    ///     Represents the presence or absence of a value of a specific type.
    /// </summary>
    /// <typeparam name="T">The type of the value</typeparam>
    [DebuggerDisplay("{ToString(),nq}")]
    public struct Option<T> : IEquatable<Option<T>>, IStructuralEquatable, IStructuralComparable, IComparable<Option<T>>,
        IComparable {
        readonly T _value;

        /// <summary>
        ///     Returns an option that represents the absence of a value.
        /// </summary>
        public static Option<T> None => default(Option<T>);

        /// <summary>
        ///     Indicates if this option contains a value.
        ///     Is true if a value is present, false otherwise.
        /// </summary>
        public bool HasValue { get; }

        internal Option(T value) : this() {
            _value = value;
            HasValue = true;
        }

        /// <summary>
        ///     Gets the value contained in this option and returns a value that indicates if this has worked.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool TryGet(out T val) {
            if (HasValue) {
                val = _value;
                return true;
            }

            val = default(T);
            return false;
        }

        #region Formatting

        public override string ToString() {
            return HasValue
                ? $"Some({_value})"
                : "None";
        }

        #endregion

        #region Equality

        static int CombineHashCodes(int h1, int h2) {
            return (((h1 << 5) + h1) ^ h2);
        }

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer) {
            if (!(other is Option<T>)) return false;
            var otherOption = (Option<T>) other;
            return comparer.Equals(_value, otherOption._value) && HasValue == otherOption.HasValue;
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer) {
            unchecked {
                return CombineHashCodes(comparer.GetHashCode(HasValue), comparer.GetHashCode(_value));
            }
        }

        public bool Equals(Option<T> other) {
            return ((IStructuralEquatable) this).Equals(other, EqualityComparer<object>.Default);
        }

        public override bool Equals(object obj) {
            return ((IStructuralEquatable) this).Equals(obj, EqualityComparer<object>.Default);
        }

        public override int GetHashCode() {
            return ((IStructuralEquatable) this).GetHashCode(EqualityComparer<object>.Default);
        }

        public static bool operator ==(Option<T> left, Option<T> right) {
            return ((IStructuralEquatable) left).Equals(right, EqualityComparer<object>.Default);
        }

        public static bool operator !=(Option<T> left, Option<T> right) {
            return ((IStructuralEquatable) left).Equals(right, EqualityComparer<object>.Default);
        }

        #endregion

        #region Comparability

        int IStructuralComparable.CompareTo(object other, IComparer comparer) {
            if (!(other is Option<T>))
                throw new ArgumentException("Provided object is not of type Option<T>", nameof(other));

            var otherOption = (Option<T>) other;
            if (HasValue && !otherOption.HasValue)
                return 1;
            if (!HasValue && otherOption.HasValue)
                return -1;

            return comparer.Compare(_value, otherOption._value);
        }

        int IComparable<Option<T>>.CompareTo(Option<T> other) {
            return ((IStructuralComparable) this).CompareTo(other, Comparer<object>.Default);
        }

        int IComparable.CompareTo(object obj) {
            return ((IStructuralComparable) this).CompareTo(obj, Comparer<object>.Default);
        }

        #endregion

        #region Implicits

        public static implicit operator Option<T>(T value) {
            return Option.From(value);
        }

        #endregion
    }
}