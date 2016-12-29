using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class ToNullableExtTests {
        [Test]
        public void Converting_options_to_nullable_should_yield_null_for_None_and_the_value_for_Some() =>
            Prop.ForAll<int?>(x => 
                Option.From(x).ToNullable().Equals(x))
            .QuickCheckThrowOnFailure();
    }
}
