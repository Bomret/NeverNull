using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    internal class WhereExtTests {
        [Test]
        public void Options_that_not_adhere_to_a_predicate_and_None_should_result_in_None() {
            Prop.ForAll<string>(x => {
                var option = Option.From(x);

                return option.Where(v => v.Length < 10)
                    .Equals(x?.Length < 10 ? option : Option.None);
            }).QuickCheckThrowOnFailure();
        }
    }
}