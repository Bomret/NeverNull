using FluentAssertions;
using Machine.Specifications;
using NeverNull.Combinators;

namespace NeverNull.Tests.Combinators
{
    [Subject(typeof(GetOrElseExt), "GetOrElse")]
    class When_I_get_the_value_of_a_none_and_get_zero_instead
    {
        static Option<int> _none;
        static int _two;

        Establish context = () => _none = Option.None;

        Because of = () => _two = _none.GetOrElse(0);

        It should_return_zero = () => _two.Should().Be(0);
    }
}