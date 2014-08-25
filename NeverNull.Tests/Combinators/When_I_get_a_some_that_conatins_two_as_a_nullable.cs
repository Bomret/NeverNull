using FluentAssertions;
using Machine.Specifications;
using NeverNull.Combinators;

namespace NeverNull.Tests.Combinators
{
    [Subject(typeof (ToNullableExt), "ToNullable")]
    class When_I_get_a_some_that_conatins_two_as_a_nullable
    {
        static Option<int> _none;
        static int? _nullable;

        Establish context = () => _none = Option.Some(2);

        Because of = () => _nullable = _none.ToNullable();

        It should_return_a_nullable_that_contains_two = () => _nullable.Value.Should().Be(2);
    }
}