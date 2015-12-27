using System.Collections;
using FsCheck;
using NUnit.Framework;

namespace NeverNull.Tests {
    [TestFixture]
    class OptionTests {
        [Test]
        public void Options_containing_higher_values_should_be_greater_than_ones_containing_lower_values_or_None() =>
            Prop.ForAll<int>(x => {
                var option = (IStructuralComparable)Option.From(x);
                return option.CompareTo(Option.From(x - 1), Comparer.Default) > 0 
                    && option.CompareTo(Option.None, Comparer.Default) > 0;
            });

        [Test]
        public void Options_containing_lower_values_should_be_lower_than_ones_containing_higher_values_but_greater_than_None() =>
            Prop.ForAll<int>(x => {
                var option = (IStructuralComparable)Option.From(x);
                return option.CompareTo(Option.From(x + 1), Comparer.Default) < 0 
                    && option.CompareTo(Option.None, Comparer.Default) > 0;
            });

        [Test]
        public void Options_containing_the_same_values_or_are_None_should_be_equal_to_same_ones() =>
            Prop.ForAll<int?>(x => {
                var option = (IStructuralComparable)Option.From(x);
                return option.CompareTo(Option.From(x), Comparer.Default) == 0
                    && option.Equals(Option.From(x));
            });

        [Test]
        public void IfSome_should_only_be_called_on_options_containing_a_value() =>
            Prop.ForAll<string>(x => {
                var modifiedVal = "-1";
                Option.From(x).IfSome(v => modifiedVal = v + "1");

                return modifiedVal.Equals(x == null ? "-1" : x + "1");
            }).QuickCheckThrowOnFailure();

        [TestFixture]
        class IfNoneExtTests {
            [Test]
            public void IfNone_should_only_be_called_on_None() =>
                Prop.ForAll<string>(x => {
                    var modfiedVal = "-1";
                    Option.From(x).IfNone(() => modfiedVal = "1");

                    return modfiedVal.Equals(x == null ? "1" : "-1");
                }).QuickCheckThrowOnFailure();
        }
    }
}