using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class DoExtTests {
        [Test]
        public void Side_effects_should_only_be_executed_for_options_that_contain_values() =>
            Prop.ForAll<string>(x => {
                var modfiedVal = "-1";
                var option = Option.From(x);
                var returnedOption = option.Do(v => modfiedVal = v + "1");

                return option.Equals(returnedOption) && modfiedVal.Equals(x == null ? "-1" : x + "1");
            }).QuickCheckThrowOnFailure();
    }
}