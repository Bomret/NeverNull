using System;

namespace NeverNull {
    static class _ {
        public static void ThrowIfNull(this object obj, string name) {
            if (obj.IsNull())
                throw new ArgumentNullException(name);
        }

        public static bool IsNull(this object obj) => ReferenceEquals(obj, null);
        public static bool IsNotNull(this object obj) => !ReferenceEquals(obj, null);
    }
}