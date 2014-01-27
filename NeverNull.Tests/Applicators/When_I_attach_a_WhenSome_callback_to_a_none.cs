using Machine.Specifications;

namespace NeverNull.Tests.Applicators {
    [Subject(typeof (NeverNull.Applicators), "WhenSome")]
    class When_I_attach_a_WhenSome_callback_to_a_none {
        static Option<int> _none;
        static bool _callbackExecuted;

        Establish context = () => _none = Option.None;

        Because of = () => _none.IfSome(i => _callbackExecuted = true);

        It should_not_execute_the_callback = () => _callbackExecuted.ShouldBeFalse();
    }
}