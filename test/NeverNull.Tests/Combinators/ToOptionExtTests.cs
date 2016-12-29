using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class ToOptionExtTests {
        [Test]
        public void Converting_reference_values_to_options_should_yield_None_for_null_otherwise_a_Some_containing_the_value() =>
            Prop.ForAll<string>(x => 
                x.ToOption().Equals(Option.From(x)))
            .QuickCheckThrowOnFailure();

        [Test]
        public void Converting_values_to_options_should_yield_None_for_null_otherwise_a_Some_containing_the_value() =>
            Prop.ForAll<int>(x =>
                x.ToOption().Equals(x))
            .QuickCheckThrowOnFailure();

        [Test]
        public void Converting_nullable_values_to_options_should_yield_None_for_null_otherwise_a_Some_containing_the_value() =>
            Prop.ForAll<int?>(x =>
                x.ToOption().Equals(Option.From(x)))
            .QuickCheckThrowOnFailure();
    }
}
