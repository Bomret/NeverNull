using System;

namespace NeverNull
{
    public sealed class None
    {
        public override string ToString()
        {
            return "None";
        }

        public bool Equals<T>(Option<T> obj)
        {
            return obj is None<T>;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }

    internal sealed class None<T> : Option<T>
    {
        public override bool HasValue
        {
            get { return false; }
        }

        public override bool IsEmpty
        {
            get { return true; }
        }

        public override T Value
        {
            get { throw new InvalidOperationException("None does not have a value."); }
        }

        public override bool Equals(object obj)
        {
            return obj is None<T> || obj is None;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return "None";
        }
    }
}