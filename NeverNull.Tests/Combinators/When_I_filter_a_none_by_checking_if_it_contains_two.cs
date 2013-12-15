using System;
using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "Filter")]
    public class When_I_filter_a_none_by_checking_if_it_contains_two {
        static IMaybe<int> _none;
        static IMaybe<int> _result;
        static Func<int, bool> _isTwo;

        Establish context = () => {
            _none = new None<int>();

            _isTwo = i => i == 3;
        };

        Because of =
            () => _result = _none.Filter(_isTwo);

        It should_return_the_original_none =
            () => _result.ShouldEqual(_none);
    }
}