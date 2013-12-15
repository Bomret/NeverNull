using System;
using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "AndThen")]
    public class When_I_have_a_none_and_then_check_if_it_is_empty {
        static Func<IMaybe<int>, IMaybe<bool>> _checkIfEmpty;
        static IMaybe<bool> _result;
        static None<int> _none;

        Establish context = () => {
            _none = new None<int>();

            _checkIfEmpty = option => Maybe.From(option.IsEmpty);
        };

        Because of =
            () => _result = _none.Then(_checkIfEmpty);

        It should_return_a_none =
            () => _result.IsEmpty.ShouldBeTrue();
    }
}