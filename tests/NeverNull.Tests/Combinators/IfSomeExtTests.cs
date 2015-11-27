using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    internal class IfSomeExtTests {
        [Test]
        public void IfSome_should_only_be_called_on_options_containing_a_value() =>
            Prop.ForAll<string>(x => {
                var option = Option.From(x);

                var modfiedVal = "";
                option.IfSome(v => modfiedVal = v + "1");

                return x == null ? string.IsNullOrEmpty(modfiedVal) : modfiedVal.Equals(x + "1");
            }).QuickCheckThrowOnFailure();
    }
}