using Machine.Specifications;

namespace NeverNull.Tests.Applicators {
    [Subject(typeof (NeverNull.Applicators), "WhenSome")]
    internal class When_I_attach_a_WhenSome_callback_to_a_none {
        static IMaybe<int> _none;
        static bool _callbackExecuted;

        Establish context = () => _none = new None<int>();

        Because of = () => _none.WhenSome(i => _callbackExecuted = true);

        It should_not_execute_the_callback = () => _callbackExecuted.ShouldBeFalse();
    }
}