using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class OrElseExtTests {
        [Test]
        public void The_fallback_value_should_only_be_used_for_None() =>
            Prop.ForAll<string, string>((a, b) => {
                var sut = Option.From(a);
                var result = sut.OrElse(() => b);

                return result == Option.From(a ?? b);
            }).QuickCheckThrowOnFailure();

        [Test]
        public void The_fallback_option_should_only_be_used_for_None() =>
            Prop.ForAll<string, string>((a, b) => {
                var sut = Option.From(a);
                var result = sut.OrElseWith(() => Option.From(b));

                return result == Option.From(a ?? b);
            }).QuickCheckThrowOnFailure();
    }
}