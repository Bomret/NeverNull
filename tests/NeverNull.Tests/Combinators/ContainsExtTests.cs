using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class ContainsExtTests {
        [Test]
        public void Options_containing_a_value_should_return_true_for_Contains() =>
            Prop.ForAll<string>(x => Option.From(x).Contains(x) == (x != null))
            .QuickCheckThrowOnFailure();
    }
}