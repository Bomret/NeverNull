using Machine.Specifications;

namespace NeverNull.Tests.Applicators {
    [Subject(typeof (NeverNull.Applicators), "Get")]
    internal class When_I_get_the_value_of_a_some_that_contains_two {
        static IMaybe<int> _some;
        static int _two;

        Establish context = () => _some = new Some<int>(2);

        Because of = () => _two = _some.Get();

        It should_return_two = () => _two.ShouldEqual(2);
    }
}