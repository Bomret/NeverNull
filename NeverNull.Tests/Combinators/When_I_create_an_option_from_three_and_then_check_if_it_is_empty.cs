using System;
using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "AndThen")]
    public class When_I_create_an_option_from_three_and_then_check_if_it_is_empty {
        static Func<IMaybe<int>, IMaybe<bool>> _checkIfEmpty;
        static IMaybe<bool> _result;
        Establish context = () => _checkIfEmpty = option => Maybe.From(option.IsEmpty);

        Because of = () => _result = Maybe.From(3)
                                          .Then(_checkIfEmpty);

        It should_contain_false_in_the_result =
            () => _result.Value.ShouldBeFalse();

        It should_return_a_some =
            () => _result.HasValue.ShouldBeTrue();
    }
}