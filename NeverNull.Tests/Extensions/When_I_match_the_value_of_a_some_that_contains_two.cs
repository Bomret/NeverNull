using System;
using Machine.Specifications;

namespace NeverNull.Tests.Extensions {
    [Subject(typeof (NeverNull.Extensions))]
    internal class When_I_match_the_value_of_a_some_that_contains_two {
        private static IOption<int> _some;
        private static int _two;

        private static Action<int> _whenSome;
        private static bool _isNone;
        private static Action _whenNone;

        private Establish context = () => {
            _some = new Some<int>(2);

            _whenSome = i => _two = i;
            _whenNone = () => _isNone = true;
        };

        private Because of = () => _some.Match(
            _whenSome,
            _whenNone);

        private It should_execute_the_some_callback_and_return_two = () => _two.ShouldEqual(2);

        private It should_not_execute_the_none_callback = () => _isNone.ShouldBeFalse();
    }
}