using FsCheck;
using NUnit.Framework;

namespace NeverNull.Tests {
    [TestFixture]
    internal class FromTryPatternTests {
        [Test]
        public void Creating_an_option_from_a_TryPattern_should_yield_a_Some_containing_the_value_if_the_operation_succeeded_or_None_otherwise()
            => Prop.ForAll<string>(x => {
                var option = Option.FromTryPattern<string, double>(double.TryParse, x);

                return option.Match(
                    None: () => option.IsNone && option.Equals(Option<double>.None),
                    Some: v => option.IsSome && double.Parse(x).Equals(v));
            }).QuickCheckThrowOnFailure();
    }
}