using System;
using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "AndThen")]
    public class When_I_have_a_none_and_then_check_if_it_is_empty {
        private static Func<IOption<int>, IOption<bool>> _checkIfEmpty;
        private static IOption<bool> _result;
        private static None<int> _none;

        private Establish context = () => {
            _none = new None<int>();

            _checkIfEmpty = option => Option.Create(option.IsEmpty);
        };

        private Because of =
            () => _result = _none.AndThen(_checkIfEmpty);

        private It should_return_a_none =
            () => _result.IsEmpty.ShouldBeTrue();
    }
}