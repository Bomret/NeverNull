using System;
using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators))]
    public class When_I_filter_a_some_with_value_two_by_checking_if_it_contains_two {
        private static IOption<int> _two;
        private static IOption<int> _result;
        private static Func<int, bool> _isTwo;

        private Establish context = () => {
            _two = Option.Create(2);

            _isTwo = i => i == 2;
        };

        private Because of =
            () => _result = _two.Filter(_isTwo);

        private It should_contain_two_in_the_some =
            () => _result.Value.ShouldEqual(2);

        private It should_return_a_some =
            () => _result.HasValue.ShouldBeTrue();
    }
}