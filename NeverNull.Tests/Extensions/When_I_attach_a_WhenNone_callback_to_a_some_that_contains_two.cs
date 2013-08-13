using Machine.Specifications;

namespace NeverNull.Tests.Extensions {
    [Subject(typeof (NeverNull.Extensions),"WhenNone")]
    internal class When_I_attach_a_WhenNone_callback_to_a_some_that_contains_two {
        private static IOption<int> _some;
        private static bool _callbackExecuted;

        private Establish context = () => _some = new Some<int>(2);

        private Because of = () => _some.WhenNone(() => _callbackExecuted = true);

        private It should_not_execute_the_callback = () => _callbackExecuted.ShouldBeFalse();
    }
}