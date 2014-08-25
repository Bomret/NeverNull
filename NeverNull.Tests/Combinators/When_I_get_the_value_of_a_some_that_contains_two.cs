using FluentAssertions;
using Machine.Specifications;
using NeverNull.Combinators;

namespace NeverNull.Tests.Combinators
{
    [Subject(typeof(GetExt), "Get")]
    class When_I_get_the_value_of_a_some_that_contains_two
    {
        static Option<int> _some;
        static int _two;

        Establish context = () => _some = Option.Some(2);

        Because of = () => _two = _some.Get();

        It should_return_two = () => _two.Should().Be(2);
    }
}