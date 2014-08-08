using FluentAssertions;
using Machine.Specifications;

namespace NeverNull.Tests.Combinators
{
    [Subject(typeof (NeverNull.Combinators), "OrElseWith")]
    public class When_I_return_none_or_else_with_a_some_containing_zero_on_none
    {
        private static Option<int> _none;
        private static Option<int> _result;

        private Establish context = () => _none = Option.None;

        private Because of =
            () => _result = _none.OrElseWith(Option.Some(0));

        private It should_contain_zero_in_the_some =
            () => _result.Value.Should().Be(0);

        private It should_return_a_some =
            () => _result.HasValue.Should().BeTrue();
    }
}