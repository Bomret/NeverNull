using FluentAssertions;
using Machine.Specifications;
using NeverNull.Combinators;

namespace NeverNull.Tests.Applicators
{
    [Subject(typeof(ToNullableExt), "ToNullable")]
    internal class When_I_get_a_some_that_conatins_two_as_a_nullable
    {
        private static Option<int> _none;
        private static int? _nullable;

        private Establish context = () => _none = Option.Some(2);

        private Because of = () => _nullable = _none.ToNullable();

        private It should_return_a_nullable_that_contains_two = () => _nullable.Value.Should().Be(2);
    }
}