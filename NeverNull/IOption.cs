namespace NeverNull {
    public interface IOption<out T> {
        bool HasValue { get; }
        T Value { get; }
    }
}