using System;
using System.Linq;
using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

using static FsCheck.Prop;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class EnumerableExtTests {
        [Test]
        public void Exchanged_options_of_enumerables_should_yield_empty_enumerables_for_None_or_the_enumerables_for_Some() =>
            ForAll<string[]>(xs =>
                Option.From(xs.AsEnumerable()).Exchange()
                .SequenceEqual(xs?.Select(Option.From) ?? Enumerable.Empty<Option<string>>()))
            .QuickCheckThrowOnFailure();

        [Test]
        public void Exchanged_options_of_enumerables_of_nullables_should_yield_empty_enumerables_for_None_or_the_enumerables_for_Some() =>
            ForAll<int?[]>(xs =>
                Option.From(xs.AsEnumerable()).Exchange()
                .SequenceEqual(xs?.Select(Option.From) ?? Enumerable.Empty<Option<int>>()))
            .QuickCheckThrowOnFailure();

        [Test]
        public void Exchanged_options_of_arrays_should_yield_empty_arrays_for_None_or_the_arrays_for_Some() =>
            ForAll<string[]>(xs =>
                Option.From(xs).Exchange()
                .SequenceEqual(xs?.Select(Option.From) ?? Enumerable.Empty<Option<string>>()))
            .QuickCheckThrowOnFailure();

        [Test]
        public void Exchanged_options_of_arrays_of_nullables_should_yield_empty_arrays_for_None_or_the_arrays_for_Some() =>
            ForAll<int?[]>(xs =>
                Option.From(xs).Exchange()
                .SequenceEqual(xs?.Select(Option.From) ?? Enumerable.Empty<Option<int>>()))
            .QuickCheckThrowOnFailure();

        [Test]
        public void Selecting_the_values_from_an_enumerable_of_options_should_only_yield_the_values() =>
            ForAll<string[]>(xs =>
                xs.Select(Option.From)
                    .SelectValues()
                    .SequenceEqual(xs.Where(x => x != null)))
            .QuickCheckThrowOnFailure();

        [Test]
        public void Selecting_the_values_from_an_enumerable_of_options_of_nullables_should_only_yield_the_values() =>
            ForAll<int?[]>(xs =>
                xs.Select(x => Option.From<int?>(x))
                    .SelectValues()
                    .SequenceEqual(xs.Where(x => x.HasValue).Select(x => x.Value)))
            .QuickCheckThrowOnFailure();

        [Test]
        public void Aggregating_the_values_from_an_enumerable_of_options_should_yield_None_for_an_empty_one_or_if_all_values_are_null() =>
            ForAll<string[]>(xs => {
                var check = xs.Where(x => x != null)
                    .Aggregate(default(string), (a, c) => a == null ? c : a + c);

                xs.AggregateOptional((a, c) => a + c).Equals(check);
            })
            .QuickCheckThrowOnFailure();

        [Test]
        public void Aggregating_the_values_from_an_enumerable_of_options_of_nullables_should_yield_None_for_an_empty_one_or_if_all_values_are_null() =>
            ForAll<int?[]>(xs => {
                var check = xs.Where(x => x.HasValue)
                    .Aggregate(0, (a, c) => c.HasValue ? a + c.Value : a);

                xs.AggregateOptionalNullable((a, c) => a + c).Equals(check);
            })
            .QuickCheckThrowOnFailure();

        [Test]
        public void Getting_all_values_or_none_from_an_enumerable_should_yield_None_for_an_empty_one_or_if_any_value_is_null() {
            var options = Arb.From<string[]>()
                .Convert(
                    xs => xs.Select(Option.From),
                    ys => ys.Select(o => o.GetOrDefault()).ToArray());

            ForAll(options, xs => {
                var sut = xs.AllOrNone();

                return xs.Count() == 0 || xs.Any(x => !x.HasValue)
                    ? sut.Equals(Option.None)
                    : sut.Get().SequenceEqual(xs.Select(o => o.Get()));
            })
            .QuickCheckThrowOnFailure();
        }

        [Test]
        public void Getting_all_values_or_none_from_an_enumerable_of_nullables_should_yield_None_for_an_empty_one_or_if_any_value_is_null() {
            var options = Arb.From<int?[]>()
                .Convert(
                    xs => xs.Select(x => Option.From<int?>(x)),
                    ys => ys.Select(o => o.GetOrDefault()).ToArray());

            ForAll(options, xs => {
                var sut = xs.AllOrNone();

                return xs.Count() == 0 || xs.Any(x => !x.HasValue)
                    ? sut.Equals(Option.None)
                    : sut.Get().SequenceEqual(xs.Select(o => o.Where(x => x.HasValue).Select(x => x.Value).Get()));
            })
            .QuickCheckThrowOnFailure();
        }

        [Test]
        public void Selecting_the_first_value_from_an_enumerable_should_yield_None_for_an_empty_enumerable() =>
            ForAll<string[]>(xs => xs.FirstOptional().Equals(Option.From(xs.FirstOrDefault())))
            .QuickCheckThrowOnFailure();

        [Test]
        public void Selecting_the_first_value_from_an_enumerable_of_nullables_should_yield_None_for_an_empty_enumerable() =>
            ForAll<int?[]>(xs => xs.FirstOptional().Equals(Option.From(xs.FirstOrDefault())))
            .QuickCheckThrowOnFailure();

        [Test]
        public void Selecting_the_last_value_from_an_enumerable_should_yield_None_for_an_empty_enumerable() =>
            ForAll<string[]>(xs => xs.LastOptional().Equals(xs.LastOrDefault()))
            .QuickCheckThrowOnFailure();

        [Test]
        public void Selecting_the_last_value_from_an_enumerable_of_nullables_should_yield_None_for_an_empty_enumerable() =>
            ForAll<int?[]>(xs => xs.LastOptional().Equals(Option.From(xs.LastOrDefault())))
            .QuickCheckThrowOnFailure();
        
        [Test]
        public void Selecting_a_single_value_from_an_enumerable_should_yield_None_for_an_empty_enumerable_and_throw_if_the_enumerable_contains_more_than_one_element() =>
            ForAll<string[]>(xs => {
                Option<string> val = Option.None;
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

        [Test]
        public void Selecting_a_single_value_from_an_enumerable_of_nullables_should_yield_None_for_an_empty_enumerable_and_throw_if_the_enumerable_contains_more_than_one_element() =>
            ForAll<int?[]>(xs => {
                Option<int> val = Option.None;
                Exception err = null;
                try {
                    val = xs.SingleOptional();
                }
                catch (Exception e) {
                    err = e;
                }

                return err != null || val.Equals(Option.From(xs.SingleOrDefault()));
            })
            .QuickCheckThrowOnFailure();

        [Test]
        public void Selecting_a_single_value_from_an_enumerable_should_yield_None_for_an_empty_enumerable_or_if_the_predicate_does_not_hold_and_throw_if_the_enumerable_contains_more_than_one_element() =>
            ForAll<string[]>(xs => {
                Option<string> val = Option.None;
                Exception err = null;
                try {
                    val = xs.SingleOptional(x => x.Contains('a'));
                }
                catch (Exception e) {
                    err = e;
                }

                return err != null || val.Equals(xs.SingleOrDefault(x => x != null && x.Contains('a')));
            })
            .QuickCheckThrowOnFailure();

        [Test]
        public void Selecting_a_single_value_from_an_enumerable_of_nullables_should_yield_None_for_an_empty_enumerable_or_if_the_predicate_does_not_hold_and_throw_if_the_enumerable_contains_more_than_one_element() =>
            ForAll<int?[]>(xs => {
                Option<int> val = Option.None;
                Exception err = null;
                try {
                    val = xs.SingleOptionalNullable(x => x > 5);
                }
                catch (Exception e) {
                    err = e;
                }

                return err != null || val.Equals(Option.From(xs.SingleOrDefault(x => x.HasValue && x.Value > 5)));
            })
            .QuickCheckThrowOnFailure();
    }
}
