using System;
using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "Then")]
    public class When_I_have_a_none_and_then_check_if_it_is_empty {
        static Func<Option<int>, Option<bool>> _checkIfEmpty;
        static Option<bool> _result;
        static Option<int> _none;

        Establish context = () => {
            _none = Option.None;

            _checkIfEmpty = option => Option.From(option.IsEmpty);
        };

        Because of =
            () => _result = _none.Then(_checkIfEmpty);

        It should_return_a_none =
            () => _result.IsEmpty.ShouldBeTrue();
    }
}