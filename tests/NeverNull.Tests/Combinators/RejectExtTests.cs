using System;
using System.Linq;
using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class RejectExtTests {
        [Test]
        public void Rejected_options_and_None_should_result_in_None() {
            var xs = Arb
                .From<int>()
                .Convert(i => new string(Enumerable.Repeat('a', Math.Abs(i) + 1).ToArray()), s => s.Length);

            Prop.ForAll(xs, x => {
                var option = Option.From(x);
                var result = option.Reject(v => v.Length < 10);

                return result.Equals(x.Length < 10 ? Option.None : option);
            }).QuickCheckThrowOnFailure();
        }
    }
}