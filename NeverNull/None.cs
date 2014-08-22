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

    }
}