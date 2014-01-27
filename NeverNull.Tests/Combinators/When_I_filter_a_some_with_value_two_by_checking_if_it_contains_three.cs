using System;
using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "Filter")]
    public class When_I_filter_a_some_with_value_two_by_checking_if_it_contains_three {
        static Option<int> _two;
        static Option<int> _result;
        static Func<int, bool> _isThree;

        Establish context = () => {
            _two = Option.From(2);

            _isThree = i => i == 3;
        };

        Because of =
            () => _result = _two.Filter(_isThree);

        It should_return_a_none =
            () => _result.HasValue.ShouldBeFalse();
    }
}