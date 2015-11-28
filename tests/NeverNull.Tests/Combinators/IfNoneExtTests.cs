using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class IfNoneExtTests {
        [Test]
        public void IfNone_should_only_be_called_on_None() =>
            Prop.ForAll<string>(x => {
                var modfiedVal = "-1";
                Option.From(x).IfNone(() => modfiedVal = "1");

                return modfiedVal.Equals(x == null ? "1": "-1");
            }).QuickCheckThrowOnFailure();
    }
}