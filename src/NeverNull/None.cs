using System;

namespace NeverNull {
    /// <summary>
    ///     Represents the absence of a value.
    /// </summary>
    public struct None : IEquatable<None> {
        public bool Equals(None _) => true;

        public override bool Equals(object obj) =>
            obj.IsNotNull()
            && (obj is None || typeof(Option<>).IsAssignableFrom(obj.GetType()));

        public override int GetHashCode() => 0;

        public override string ToString() => "None";
    }
}
