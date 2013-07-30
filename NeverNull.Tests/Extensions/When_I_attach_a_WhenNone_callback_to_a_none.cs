using Machine.Specifications;

namespace NeverNull.Tests.Extensions {
    [Subject(typeof (NeverNull.Extensions))]
    internal class When_I_attach_a_WhenNone_callback_to_a_none {
        private static IOption<int> _none;
        private static bool _callbackExecuted;

        private Establish context = () => _none = new None<int>();

        private Because of = () => _none.WhenNone(() => _callbackExecuted = true);

        private It should_execute_the_callback = () => _callbackExecuted.ShouldBeTrue();
    }
}