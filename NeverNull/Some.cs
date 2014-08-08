using System.Collections.Generic;

namespace NeverNull
{
    internal sealed class Some<T> : Option<T>
    {
        private readonly T _value;

        public Some(T value)
        {
            _value = value;
        }

        public override bool HasValue
        {
            get { return true; }
        }

        public override bool IsEmpty
        {
            get { return false; }
        }

        public override T Value
        {
            get { return _value; }
        }

        public override string ToString()
        {
            return string.Format("Some({0})", Value);
        }

        private bool Equals(Some<T> other)
        {
            return EqualityComparer<T>.Default.Equals(_value, other._value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return obj is Some<T> && Equals((Some<T>) obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(_value);
        }
    }
}