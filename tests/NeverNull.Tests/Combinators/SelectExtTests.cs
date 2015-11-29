using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class SelectExtTests {
        [Test]
        public void Only_values_inside_a_Some_should_be_selected() {
            Prop.ForAll<string>(x => 
                Option.From(x).Select(v => v.Length).Equals(x?.Length ?? Option<int>.None))
            .QuickCheckThrowOnFailure();
        }
    }
}