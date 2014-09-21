namespace NeverNull {
    public interface IOption<out T> {
        bool HasValue { get; }
        bool IsEmpty { get; }
        T Value { get; }
    }
}