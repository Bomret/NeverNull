using System;
using Machine.Specifications;

namespace NeverNull.Tests.Extensions {
    [Subject(typeof (NeverNull.Extensions), "Match")]
    internal class When_I_match_the_value_of_a_none {
        private static IOption<int> _none;
        private static Action<int> _whenSome;
        private static bool _noneCallbackExecuted;
        private static Action _whenNone;
        private static bool _someCallbackExecuted;

        private Establish context = () => {
            _none = new None<int>();

            _whenSome = i => _someCallbackExecuted = true;
            _whenNone = () => _noneCallbackExecuted = true;
        };

        private Because of = () => _none.Match(
            _whenSome,
            _whenNone);

        private It should_execute_the_none_callback = () => _noneCallbackExecuted.ShouldBeTrue();

        private It should_not_execute_the_some_callback = () => _someCallbackExecuted.ShouldBeFalse();
    }
}