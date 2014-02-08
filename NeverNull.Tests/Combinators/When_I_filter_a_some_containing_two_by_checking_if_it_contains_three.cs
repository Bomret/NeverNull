using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "Filter")]
    public class When_I_filter_a_some_containing_two_by_checking_if_it_contains_three {
        static Option<int> _two;
        static Option<int> _result;

        Establish context = () => _two = 2;

        Because of =
            () => _result = _two.Filter(i => i == 3);

        It should_return_none =
            () => _result.HasValue.ShouldBeFalse();
    }
}