using System.Collections.Generic;
using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class ContainsExtTests {
        [Test]
        public void Options_containing_a_value_should_return_true_for_Contains() =>
            Prop.ForAll<string>(x => 
                Option.From(x).Contains(x) == (x != null))
            .QuickCheckThrowOnFailure();

        [Test]
        public void Options_containing_a_value_should_return_true_for_Contains_if_the_EqualityComparer_returns_true() =>
            Prop.ForAll<int?, int>((x, y) =>
                Option.From(x).Contains(y, EqualityComparer<int>.Default).Equals(x == y))
            .QuickCheckThrowOnFailure();

        [Test]
        public void Options_containing_a_value_should_return_true_for_Contains_if_the_compare_func_returns_true() =>
            Prop.ForAll<int?, int>((x, y) =>
                Option.From(x).Contains(y, (l, r) => l == r).Equals(x == y))
            .QuickCheckThrowOnFailure();
    }
}