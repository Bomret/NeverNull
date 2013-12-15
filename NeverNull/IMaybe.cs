namespace NeverNull {
    public interface IMaybe<out TValue> {
        bool HasValue { get; }
        bool IsEmpty { get; }
        TValue Value { get; }
    }
}