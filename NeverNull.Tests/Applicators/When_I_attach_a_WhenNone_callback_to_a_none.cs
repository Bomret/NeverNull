using Machine.Specifications;

namespace NeverNull.Tests.Applicators {
    [Subject(typeof (NeverNull.Applicators), "WhenNone")]
    class When_I_attach_a_WhenNone_callback_to_a_none {
        static Option<int> _none;
        static bool _callbackExecuted;

        Establish context = () => _none = Option.None;

        Because of = () => _none.IfNone(() => _callbackExecuted = true);

        It should_execute_the_callback = () => _callbackExecuted.ShouldBeTrue();
    }
}