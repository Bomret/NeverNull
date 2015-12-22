using FsCheck;
using NUnit.Framework;

namespace NeverNull.Tests {
    [TestFixture]
    class FromTests {
        [Test]
        public void Only_the_appropiate_handler_should_be_called_and_return_a_value_for_an_option_that_is_matched() =>
            Prop.ForAll<string>(x => {
                return Option.From(x)
                    .Match(
                        Some: v => 1,
                        None: () => -1)
                    .Equals(x == null ? -1 : 1);
            }).QuickCheckThrowOnFailure();

        [Test]
        public void Creating_an_option_from_reference_types_should_yield_None_for_null_and_a_Some_containing_the_value_otherwise()
            => Prop.ForAll<string>(x => {
                var option = Option.From(x);

                return option.Match(
                    None: () => option.Equals(Option.None) && !option.HasValue,
                    Some: val => option.HasValue && val.Equals(x));
            }).QuickCheckThrowOnFailure();

        [Test]
        public void Creating_an_option_from_value_types_should_always_yield_a_Some_containing_the_value()
            => Prop.ForAll<double>(x => {
                var option = Option.From(x);

                return option.Match(
                    None: () => { throw new AssertionException("Must never happen"); },
                    Some: val => option.HasValue && val.Equals(x));
            }).QuickCheckThrowOnFailure();

        [Test]
        public void Creating_an_option_from_nullable_types_should_yield_None_for_null_and_a_Some_containing_the_value_otherwise()
            => Prop.ForAll<int?>(x => {
                var option = Option.From(x);

                // ReSharper disable once PossibleInvalidOperationException
                option.Match(
                    None: () => option.Equals(Option.None) && !option.HasValue,
                    Some: val => option.HasValue && val.Equals(x.Value));
            }).QuickCheckThrowOnFailure();
    }
}