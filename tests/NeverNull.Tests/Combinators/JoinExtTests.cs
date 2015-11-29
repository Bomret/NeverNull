using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class JoinExtTests {
        [Test]
        public void Only_options_containing_values_should_be_joined_otherwise_None_is_expected() =>
            Prop.ForAll<string, string>((a, b) => {
                var optionA = Option.From(a);
                var optionB = Option.From(b);

                var joined =
                    from va in optionA
                    join vb in optionB on true equals true
                    select va + vb;

                return a == null || b == null 
                    ? joined.Equals(Option<string>.None) 
                    : joined.Equals(Option.From(a + b));
            }).QuickCheckThrowOnFailure();
    }
}