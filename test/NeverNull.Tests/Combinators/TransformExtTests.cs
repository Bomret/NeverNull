using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {

    [TestFixture]
    class TransformExtTests {
        [Test]
        public void Only_the_appropiate_handler_should_be_called_and_return_a_value_for_an_option_that_is_transformed() =>
            Prop.ForAll<string>(x => {
                return Option.From(x)
                    .Transform(
                        Some: v => 1,
                        None: () => -1)
                    .Equals(x == null ? -1 : 1);
            }).QuickCheckThrowOnFailure();
    }
}