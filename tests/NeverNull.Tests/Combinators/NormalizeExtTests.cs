using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class NormalizeExtTests {
        [Test]
        public void Normalizing_options_of_nullables_should_return_the_correct_value_representation() =>
            Prop.ForAll<int?>(x => 
                Option.From<int?>(x)
                .Normalize()
                .Equals(Option.From(x)))
            .QuickCheckThrowOnFailure();
    }
}
