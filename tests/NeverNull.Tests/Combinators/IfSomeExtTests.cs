using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    internal class IfSomeExtTests {
        [Test]
        public void IfSome_should_only_be_called_on_options_containing_a_value() =>
            Prop.ForAll<string>(x => {
                var modifiedVal = "-1";
                Option.From(x).IfSome(v => modifiedVal = v + "1");

                return modifiedVal.Equals(x == null ? "-1" : x + "1");
            }).QuickCheckThrowOnFailure();
    }
}