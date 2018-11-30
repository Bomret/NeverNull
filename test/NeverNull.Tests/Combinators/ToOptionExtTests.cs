using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators
{
  [TestFixture]
  internal class ToOptionExtTests
  {
    [Test]
    public void Converting_nullable_values_to_options_should_yield_None_for_null_otherwise_a_Some_containing_the_value()
    {
      Prop.ForAll<int?>(x =>
          x.ToOption().Equals(Option.From(x)))
        .QuickCheckThrowOnFailure();
    }

    [Test]
    public void Converting_nullable_values_to_options_with_custom_mapping_should_yield_None_for_null_otherwise_a_Some_containing_the_value()
    {
      Prop.ForAll<int?>(x => x.ToOptionMapped(CustomType.Create).Equals(x.ToOption().Select(CustomType.Create)))
        .QuickCheckThrowOnFailure();
    }

    [Test]
    public void Converting_nullable_values_to_options_with_custom_mapping_and_magic_null_value_should_yield_None_for_null_otherwise_a_Some_containing_the_value()
    {
      Prop.ForAll<int>(x => x.ToOptionMappedOrNoneIf(1, CustomType.Create).Equals(x == 1 ? Option.None : x.ToOption().Select(CustomType.Create)))
        .QuickCheckThrowOnFailure();
    }

    [Test]
    public void
      Converting_reference_values_to_options_should_yield_None_for_null_otherwise_a_Some_containing_the_value()
    {
      Prop.ForAll<string>(x =>
          x.ToOption().Equals(Option.From(x)))
        .QuickCheckThrowOnFailure();
    }

    [Test]
    public void Converting_values_to_options_should_yield_None_for_null_otherwise_a_Some_containing_the_value()
    {
      Prop.ForAll<int>(x =>
          x.ToOption().Equals(x))
        .QuickCheckThrowOnFailure();
    }

    private class CustomType
    {
      public int Value { get; private set; }

      public static CustomType Create(int i)
      {
        return new CustomType {Value = i};
      }

      protected bool Equals(CustomType other)
      {
        return Value == other.Value;
      }

      public override bool Equals(object obj)
      {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CustomType) obj);
      }

      public override int GetHashCode()
      {
        return Value;
      }
    }
  }
}
