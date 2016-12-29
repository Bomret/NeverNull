using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class RejectExtTests {
        [Test]
        public void Rejected_options_and_None_should_result_in_None() {
            Prop.ForAll<string>(x => {
                var option = Option.From(x);
                var result = option.Reject(v => v.Length < 10);

                return result.Equals(x?.Length < 10 ? Option.None : option);
            }).QuickCheckThrowOnFailure();
        }
    }
}