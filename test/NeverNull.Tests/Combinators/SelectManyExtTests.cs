using System.Linq;
using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class SelectManyExtTests {
        [Test]
        public void Only_the_values_of_options_inside_a_Some_should_be_selected() =>
            Prop.ForAll<string>(x => 
                Option.From(x)
                    .SelectMany(v => Option.From(v + "1"))
                    .Equals(x == null ? Option<string>.None : x + "1"))
            .QuickCheckThrowOnFailure();
    }
}
