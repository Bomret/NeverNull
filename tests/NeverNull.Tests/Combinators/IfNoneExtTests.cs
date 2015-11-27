using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class IfNoneExtTests {
        [Test]
        public void IfNone_should_only_be_called_on_None() =>
            Prop.ForAll<string>(x => {
                var option = Option.From(x);

                var modfiedVal = "";
                option.IfNone(() => modfiedVal = "1");

                return x == null ? modfiedVal.Equals("1") : string.IsNullOrEmpty(modfiedVal);

            }).QuickCheckThrowOnFailure();
    }
}