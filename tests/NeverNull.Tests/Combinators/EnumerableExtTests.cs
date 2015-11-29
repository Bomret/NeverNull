using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

using static FsCheck.Prop;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class EnumerableExtTests {
        [Test]
        public void Exchanged_options_of_enumerables_should_yield_empty_enumerables_for_None_or_the_enumerables_for_Some() =>
            ForAll<int[]>(xs =>
                Option.From(xs.AsEnumerable()).Exchange()
                .SequenceEqual(xs?.Select(Option.From) ?? Enumerable.Empty<Option<int>>()))
            .QuickCheckThrowOnFailure();

        [Test]
        public void Exchanged_options_of_arrays_should_yield_empty_arrays_for_None_or_the_arrays_for_Some() =>
            ForAll<int[]>(xs =>
                Option.From(xs).Exchange()
                .SequenceEqual(xs?.Select(Option.From) ?? Enumerable.Empty<Option<int>>()))
            .QuickCheckThrowOnFailure();

        [Test]
        public void Selecting_the_values_from_an_enumerable_of_options_should_only_yield_the_values() {
            var strings = Arb.From<string[]>().Filter(xs => xs != null);

            ForAll(strings, xs => 
                xs.Select(Option.From)
                    .SelectValues()
                    .SequenceEqual(xs.Where(x => x != null)))
            .QuickCheckThrowOnFailure();
        }

        [Test]
        public void Aggregating_the_values_from_an_enumerable_of_options_should_yield_None_for_an_empty_one_or_if_all_values_are_null() {
            var strings = Arb.From<string[]>().Filter(xs => xs != null);

            ForAll(strings, xs => {
                var check = xs.Where(x => x != null)
                    .Aggregate(default(string), (a, c) => a == null ? c : a + c);

                xs.AggregateOptional((a, c) => a + c).Equals(check);
            })
            .QuickCheckThrowOnFailure();
        }

        [Test]
        public void Getting_all_values_or_none_from_an_enumerable_should_yield_None_for_an_empty_one_or_if_any_value_is_null() {
            var options = Arb.From<string[]>()
                .Filter(xs => xs != null)
                .Convert(
                    xs => xs.Select(Option.From),
                    ys => ys.Select(o => o.GetOrDefault()).ToArray());

            ForAll(options, xs => {
                var sut = xs.AllOrNone();

                return xs.Count() == 0 || xs.Any(x => !x.HasValue)
                    ? sut.Equals(Option<IEnumerable<string>>.None)
                    : sut.Get().SequenceEqual(xs.Select(o => o.Get()));
            })
            .QuickCheckThrowOnFailure();
        }

        [Test]
        public void Selecting_the_first_value_from_an_enumerable_should_yield_None_for_an_empty_enumerable() {
            var strings = Arb.From<string[]>().Filter(xs => xs != null);

            ForAll(strings, xs =>
                xs.FirstOptional().Equals(xs.FirstOrDefault()))
            .QuickCheckThrowOnFailure();
        }

        [Test]
        public void Selecting_the_last_value_from_an_enumerable_should_yield_None_for_an_empty_enumerable() {
            var strings = Arb.From<string[]>().Filter(xs => xs != null);

            ForAll(strings, xs =>
                xs.LastOptional().Equals(xs.LastOrDefault()))
            .QuickCheckThrowOnFailure();
        }

        [Test]
        public void Selecting_a_single_value_from_an_enumerable_should_yield_None_for_an_empty_enumerable() {
            var strings = Arb.From<string[]>().Filter(xs => xs != null);

            ForAll(strings, xs => {
                Option<string> val = Option<string>.None;
                Exception err = null;
                try {
                    val = xs.SingleOptional();
                }
                catch (Exception e) {
                    err = e;
                }

                return err != null || val.Equals(xs.SingleOrDefault());
            })
            .QuickCheckThrowOnFailure();
        }
    }
}
