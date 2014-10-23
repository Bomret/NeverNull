using FluentAssertions;
using Machine.Specifications;
using NeverNull.Combinators;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (FilterExt), "Filter")]
    public class When_I_filter_a_some_containing_two_by_checking_if_it_contains_two {
        static Option<int> _two;
        static Option<int> _result;

        Establish context = () => _two = Option.From(2);

        Because of = () => _result = _two.Filter(i => i == 2);

        It should_contain_two_in_the_some =
            () => _result.Value.Should().Be(2);

        It should_return_a_some =
            () => _result.HasValue.Should().BeTrue();
    }
}