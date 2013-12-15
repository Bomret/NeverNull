using Machine.Specifications;

namespace NeverNull.Tests.Applicators {
    [Subject(typeof (NeverNull.Applicators), "WhenSome")]
    internal class When_I_attach_a_WhenSome_callback_to_a_some_that_contains_two {
        static IMaybe<int> _some;
        static int _two;

        Establish context = () => _some = new Some<int>(2);

        Because of = () => _some.WhenSome(i => _two = i);

        It should_execute_the_callback_and_return_two = () => _two.ShouldEqual(2);
    }
}