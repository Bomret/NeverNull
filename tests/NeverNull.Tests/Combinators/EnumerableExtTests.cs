using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class EnumerableExtTests {
        [Test]
        public void Exchanged_options_of_enumerables_should_yield_empty_enumerables_for_None_or_the_enumerables_for_Some() =>
            Prop.ForAll<int[]>(xs =>
                Option.From(xs.AsEnumerable()).Exchange()
                .SequenceEqual(xs?.Select(Option.From) ?? Enumerable.Empty<Option<int>>()))
            .QuickCheckThrowOnFailure();

        [Test]
        public void Exchanged_options_of_arrays_should_yield_empty_arrays_for_None_or_the_arrays_for_Some() =>
            Prop.ForAll<int[]>(xs =>
                Option.From(xs).Exchange()
                .SequenceEqual(xs?.Select(Option.From) ?? Enumerable.Empty<Option<int>>()))
            .QuickCheckThrowOnFailure();

        [Test]
        public void Selecting_the_values_from_a_enumerable_of_options_should_only_yield_the_values() =>
            Prop.ForAll<string[]>(xs =>
                xs == null 
                    ? true
                    : xs.Select(Option.From)
                    .SelectValues()
                    .Equals(xs.Where(x => x != null)))
            .QuickCheckThrowOnFailure();
    }
}
