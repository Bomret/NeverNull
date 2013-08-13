using Machine.Specifications;

namespace NeverNull.Tests.Extensions {
    [Subject(typeof (NeverNull.Extensions),"WhenSome")]
    internal class When_I_attach_a_WhenSome_callback_to_a_none {
        private static IOption<int> _none;
        private static bool _callbackExecuted;

        private Establish context = () => _none = new None<int>();

        private Because of = () => _none.WhenSome(i => _callbackExecuted = true);

        private It should_not_execute_the_callback = () => _callbackExecuted.ShouldBeFalse();
    }
}