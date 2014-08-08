using FluentAssertions;
using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "Flatten")]
    public class When_I_flatten_a_some_containing_three_that_is_nested_in_another_some {
        static Option<int> _result;
        static Option<Option<int>> _nested;

        Establish ctx = () => _nested = Option.Some(Option.Some(3));

        Because of = () => _result = _nested.Flatten();

        It should_contain_three_in_the_result =
            () => _result.Value.Should().Be(3);

        private It should_return_a_some =
            () => _result.HasValue.Should().BeTrue();

    }
}