using FsCheck;
using NUnit.Framework;

namespace NeverNull.Tests {
    [TestFixture]
    class FromTests {
        [Test]
        public void Creating_an_option_from_reference_types_should_yield_None_for_null_and_a_Some_containing_the_value_otherwise()
            => Prop.ForAll<string>(x => {
                var option = Option.From(x);
                if (ReferenceEquals(x, null))
                    return option.Equals(Option<string>.None) && !option.HasValue;

                string val;
                return option.HasValue && option.TryGet(out val) && val.Equals(x);
            }).QuickCheckThrowOnFailure();

        [Test]
        public void Creating_an_option_from_value_types_should_always_yield_a_Some_containing_the_value()
            => Prop.ForAll<double>(x => {
                double val;
                var option = Option.From(x);

                return option.HasValue && option.TryGet(out val) && val.Equals(x);
            }).QuickCheckThrowOnFailure();

        [Test]
        public void Creating_an_option_from_nullable_types_should_yield_None_for_null_and_a_Some_containing_the_value_otherwise()
            => Prop.ForAll<int?>(x => {
                var option = Option.From(x);
                if (!x.HasValue)
                    return option.Equals(Option<int>.None) && !option.HasValue;;

                int val;
                return option.HasValue && option.TryGet(out val) && val.Equals(x);
            }).QuickCheckThrowOnFailure();
    }
}