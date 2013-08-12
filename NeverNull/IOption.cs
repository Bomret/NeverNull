namespace NeverNull {
    public interface IOption<out TValue> {
        bool HasValue { get; }
        bool IsEmpty { get; }
        TValue Value { get; }
    }
}