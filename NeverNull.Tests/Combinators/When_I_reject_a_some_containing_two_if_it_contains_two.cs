using FluentAssertions;
using Machine.Specifications;
using NeverNull.Combinators;

namespace NeverNull.Tests.Combinators
{
    [Subject(typeof (RejectExt), "Reject")]
    public class When_I_reject_a_some_containing_two_if_it_contains_two
    {
        static Option<int> _two;
        static Option<int> _result;

        Establish context = () => _two = 2;

        Because of = () => _result = _two.Reject(i => i == 2);

        It should_return_none =
            () => _result.IsEmpty.Should().BeTrue();
    }
}