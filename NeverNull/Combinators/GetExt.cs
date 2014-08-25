namespace NeverNull.Combinators
{
    public static class GetExt
    {
        public static T Get<T>(this Option<T> option)
        {
            return option.Value;
        }
    }
}