using FluentAssertions;
using Machine.Specifications;
using NeverNull.Combinators;

namespace NeverNull.Tests.Combinators
{
    [Subject(typeof (ThenWithExt), "ThenWith")]
    public class When_I_have_a_none_and_then_check_if_it_is_empty_with_a_some
    {
        static Option<bool> _result;
        static Option<int> _none;

        Establish context = () => _none = Option.None;

        Because of = () => _result = _none.ThenWith(o => Option.Some(o.IsEmpty));

        It should_return_a_none =
            () => _result.IsEmpty.Should().BeTrue();
    }
}