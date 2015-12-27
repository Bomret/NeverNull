using FsCheck;
using NUnit.Framework;

namespace NeverNull.Tests {
    [TestFixture]
    class FromTests {
        [Test]
        public void Only_the_appropiate_handler_should_be_called_and_return_a_value_for_an_option_that_is_matched() =>
            Prop.ForAll<string>(x => 
                Option.From(x)
                    .Match(
                        None: () => -1,
                        Some: v => 1)
                    .Equals(x == null ? -1 : 1))
            .QuickCheckThrowOnFailure();

        [Test]
        public void Creating_an_option_from_reference_types_should_yield_None_for_null_and_a_Some_containing_the_value_otherwise()
            => Prop.ForAll<string>(x => 
                Option.From(x)
                    .Match(
                        None: () => Option.From(x).Equals(Option.None) && !Option.From(x).HasValue,
                        Some: val => Option.From(x).HasValue && val.Equals(x)))
            .QuickCheckThrowOnFailure();

        [Test]
        public void Creating_an_option_from_value_types_should_always_yield_a_Some_containing_the_value()
            => Prop.ForAll<double>(x => 
                Option.From(x)
                    .Match(
                        None: () => { throw new AssertionException("Must never happen"); },
                        Some: val => Option.From(x).HasValue && val.Equals(x)))
            .QuickCheckThrowOnFailure();

        [Test]
        public void Creating_an_option_from_nullable_types_should_yield_None_for_null_and_a_Some_containing_the_value_otherwise()
            => Prop.ForAll<int?>(x => 
                Option.From(x)
                    .Match(
                        None: () => Option.From(x).Equals(Option.None) && !Option.From(x).HasValue,
                        Some: val => Option.From(x).HasValue && val.Equals(x.Value))).QuickCheckThrowOnFailure();
    }
}