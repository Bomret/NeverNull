using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class OrElseExtTests {
        [Test]
        public void The_fallback_value_should_only_be_used_for_None() =>
            Prop.ForAll<string, string>((a, b) =>
                Option.From(a).OrElse(b) == Option.From(a ?? b))
            .QuickCheckThrowOnFailure();

        [Test]
        public void The_value_produced_by_the_fallback_func_should_only_be_used_for_None() =>
            Prop.ForAll<string, string>((a, b) => 
                Option.From(a).OrElse(() => b) == Option.From(a ?? b))
            .QuickCheckThrowOnFailure();

        [Test]
        public void The_fallback_option_should_only_be_used_for_None() =>
            Prop.ForAll<string, string>((a, b) =>
                Option.From(a).OrElseWith(Option.From(b)) == Option.From(a ?? b))
            .QuickCheckThrowOnFailure();

        [Test]
        public void The_option_produced_by_the_fallback_func_should_only_be_used_for_None() =>
            Prop.ForAll<string, string>((a, b) => 
                Option.From(a).OrElseWith(() => Option.From(b)) == Option.From(a ?? b))
            .QuickCheckThrowOnFailure();
    }
}