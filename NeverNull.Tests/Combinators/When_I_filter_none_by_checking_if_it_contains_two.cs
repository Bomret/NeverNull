using FluentAssertions;
using Machine.Specifications;
using NeverNull.Combinators;

namespace NeverNull.Tests.Combinators
{
    [Subject(typeof (FilterExt), "Filter")]
    public class When_I_filter_none_by_checking_if_it_contains_two
    {
        static Option<int> _none;
        static Option<int> _result;

        Establish context = () => _none = Option.None;

        Because of =
            () => _result = _none.Filter(i => i == 3);

        It should_return_none =
            () => _result.IsEmpty.Should().BeTrue();
    }
}