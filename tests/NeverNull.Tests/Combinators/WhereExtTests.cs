using System;
using System.Linq;
using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    internal class WhereExtTests {
        [Test]
        public void Options_that_not_adhere_to_a_predicate_and_None_should_result_in_None() {
            var xs = Arb
                .From<int>()
                .Convert(i => new string(Enumerable.Repeat('a', Math.Abs(i) + 1).ToArray()), s => s.Length);

            Prop.ForAll(xs, x => {
                var option = Option.From(x);

                return option.Where(v => v.Length < 10)
                    .Equals(x.Length < 10 ? option : Option<string>.None);
            }).QuickCheckThrowOnFailure();
        }
    }
}