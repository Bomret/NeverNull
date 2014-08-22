using System;

namespace NeverNull
{
    public struct None
    {
        internal static readonly None Instance = new None();

        public bool HasValue
        {
            get { return false; }
        }

        public bool IsEmpty
        {
            get { return true; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj,null)) return false;
            if (obj is None) return true;

            return false;
        }

        public bool Equals<T>(Option<T> option)
        {
            return option.HasValue;
        }
    }
}