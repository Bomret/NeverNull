using FluentAssertions;
using Machine.Specifications;
using NeverNull.Combinators;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (ThenWithExt), "ThenWith")]
    public class When_I_create_an_option_from_three_and_then_check_if_it_is_empty_with_a_some {
        static Option<bool> _result;

        Because of = () => _result = Option.Some(3)
                                           .ThenWith(o => Option.Some(o.IsEmpty));

        It should_contain_false_in_the_result =
            () => _result.Value.Should().BeFalse();

        It should_return_a_some =
            () => _result.HasValue.Should().BeTrue();
    }
}