using System;
using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators))]
    public class When_I_filter_a_some_with_value_two_by_checking_if_it_contains_three {
        private static IOption<int> _two;
        private static IOption<int> _result;
        private static Func<int, bool> _isThree;

        private Establish context = () => {
            _two = Option.Create(2);

            _isThree = i => i == 3;
        };

        private Because of =
            () => _result = _two.Filter(_isThree);

        private It should_return_a_none =
            () => _result.HasValue.ShouldBeFalse();
    }
}