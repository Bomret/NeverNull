using Machine.Specifications;

namespace NeverNull.Tests.Applicators {
    [Subject(typeof (NeverNull.Applicators), "WhenNone")]
    internal class When_I_attach_a_WhenNone_callback_to_a_some_that_contains_two {
        static IMaybe<int> _some;
        static bool _callbackExecuted;

        Establish context = () => _some = new Some<int>(2);

        Because of = () => _some.WhenNone(() => _callbackExecuted = true);

        It should_not_execute_the_callback = () => _callbackExecuted.ShouldBeFalse();
    }
}