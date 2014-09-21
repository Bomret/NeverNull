namespace NeverNull {
    public struct None {
        static readonly None OnlyInstance = new None();

        public static None Instance {
            get { return OnlyInstance; }
        }

        public bool HasValue {
            get { return false; }
        }

        public bool IsEmpty {
            get { return true; }
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(obj, null)) return false;
            if (obj is None) return true;

            return false;
        }

        public bool Equals<T>(Option<T> option) {
            return option.IsEmpty;
        }

        public override string ToString() {
            return "None";
        }
    }
}