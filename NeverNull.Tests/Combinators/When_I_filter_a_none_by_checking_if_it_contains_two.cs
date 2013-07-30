using System;
using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators))]
    public class When_I_filter_a_none_by_checking_if_it_contains_two {
        private static IOption<int> _none;
        private static IOption<int> _result;
        private static Func<int, bool> _isTwo;

        private Establish context = () => {
            _none = new None<int>();

            _isTwo = i => i == 3;
        };

        private Because of =
            () => _result = _none.Filter(_isTwo);

        private It should_return_the_original_none =
            () => _result.ShouldEqual(_none);
    }
}