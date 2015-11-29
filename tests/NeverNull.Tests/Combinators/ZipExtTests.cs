using System.Linq;
using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class ZipExtTests {
        [Test]
        public void Options_should_only_be_zipped_when_both_options_contain_values() =>
            Prop.ForAll<string, string>((a, b) =>
                Option.From(a)
                    .Zip(Option.From(b), (va, vb) => va + vb)
                    .Equals(a == null || b == null ? Option<string>.None : (a + b)))
            .QuickCheckThrowOnFailure();
    }
}
