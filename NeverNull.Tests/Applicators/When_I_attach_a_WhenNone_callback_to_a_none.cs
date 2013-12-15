using Machine.Specifications;

namespace NeverNull.Tests.Applicators {
    [Subject(typeof (NeverNull.Applicators), "WhenNone")]
    internal class When_I_attach_a_WhenNone_callback_to_a_none {
        static IMaybe<int> _none;
        static bool _callbackExecuted;

        Establish context = () => _none = new None<int>();

        Because of = () => _none.WhenNone(() => _callbackExecuted = true);

        It should_execute_the_callback = () => _callbackExecuted.ShouldBeTrue();
    }
}