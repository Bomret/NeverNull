using System;
using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators))]
    public class When_I_create_an_option_from_three_and_then_check_if_it_is_empty {
        private static Func<IOption<int>, IOption<bool>> _checkIfEmpty;
        private static IOption<bool> _result;
        private Establish context = () => _checkIfEmpty = option => Option.Create(option.IsEmpty);

        private Because of = () => _result = Option.Create(3)
                                                   .AndThen(_checkIfEmpty);

        private It should_contain_false_in_the_result =
            () => _result.Value.ShouldBeFalse();

        private It should_return_a_some =
            () => _result.HasValue.ShouldBeTrue();
    }
}