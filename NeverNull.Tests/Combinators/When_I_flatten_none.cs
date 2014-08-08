using FluentAssertions;
using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "Flatten")]
    public class When_I_flatten_none {
        static Option<int> _result;
        static Option<Option<int>> _nested;

        Establish ctx = () => _nested = Option.None;

        Because of = () => _result = _nested.Flatten();

        It should_return_none =
            () => _result.HasValue.Should().BeFalse();
    }
}