using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class DoExtTests {
        [Test]
        public void Side_effects_should_only_be_executed_for_options_that_contain_values() =>
            Prop.ForAll<string>(x => {
                var option = Option.From(x);

                var modfiedVal = "";
                option.Do(v => modfiedVal = x + "1");

                return x == null ? string.IsNullOrEmpty(modfiedVal) : modfiedVal.Equals(x + "1");
            }).QuickCheckThrowOnFailure();
    }
}