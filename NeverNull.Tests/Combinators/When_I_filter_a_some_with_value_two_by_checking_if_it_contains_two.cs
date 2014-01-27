using System;
using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "Filter")]
    public class When_I_filter_a_some_with_value_two_by_checking_if_it_contains_two {
        static Option<int> _two;
        static Option<int> _result;
        static Func<int, bool> _isTwo;

        Establish context = () => {
            _two = Option.From(2);

            _isTwo = i => i == 2;
        };

        Because of =
            () => _result = _two.Filter(_isTwo);

        It should_contain_two_in_the_some =
            () => _result.Value.ShouldEqual(2);

        It should_return_a_some =
            () => _result.HasValue.ShouldBeTrue();
    }
}