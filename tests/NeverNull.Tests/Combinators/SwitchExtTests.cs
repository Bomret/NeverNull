using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class SwitchExtTests {
        [Test]
        public void A_collection_of_options_should_switch_to_the_first_Some_otherwise_to_None() =>
            Prop.ForAll<string, string, string>((a, b, c) => 
                Option.From(a)
                    .Switch(Option.From(b), Option.From(c))
                    .Equals(a ?? b ?? c ?? Option<string>.None))
            .QuickCheckThrowOnFailure();
    }
}
