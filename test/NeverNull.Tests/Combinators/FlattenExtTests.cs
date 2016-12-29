using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class FlattenExtTests {
        [Test]
        public void Nested_options_should_be_flattened_correctly() =>
            Prop.ForAll<string>(x => {
                var nestedOption = Option.From(x);

                return Option.From(nestedOption).Flatten().Equals(nestedOption);
            }).QuickCheckThrowOnFailure();
    }
}