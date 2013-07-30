using Machine.Specifications;

namespace NeverNull.Tests.Extensions {
    [Subject(typeof (NeverNull.Extensions))]
    internal class When_I_attach_a_WhenSome_callback_to_a_some_that_contains_two {
        private static IOption<int> _some;
        private static int _two;

        private Establish context = () => _some = new Some<int>(2);

        private Because of = () => _some.WhenSome(i => _two = i);

        private It should_execute_the_callback_and_return_two = () => _two.ShouldEqual(2);
    }
}