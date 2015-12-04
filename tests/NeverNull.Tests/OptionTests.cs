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
    }
}