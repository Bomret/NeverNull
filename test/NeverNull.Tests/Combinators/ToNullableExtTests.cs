using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators {
    [TestFixture]
    class ToNullableExtTests {
        [Test]
        public void Converting_options_to_nullable_should_yield_null_for_None_and_the_value_for_Some() =>
            Prop.ForAll<int?>(x => 
                Option.From(x).ToNullable().Equals(x))
            .QuickCheckThrowOnFailure();

      [Test]
      public void Converting_options_to_nullable_on_custom_type_should_yield_null_for_None_and_the_value_for_Some() =>
        Prop.ForAll<int?>(x =>
            (x == null ? Option.None : CustomType.Create(x.Value).ToOption()).ToNullable(o => o.Value).Equals(x))
          .QuickCheckThrowOnFailure();

    private class CustomType
      {
        public int Value { get; private set; }

        public static CustomType Create(int i)
        {
          return new CustomType { Value = i };
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
          return Equals((CustomType)obj);
        }

        public override int GetHashCode()
        {
          return Value;
        }
      }
  }
}
