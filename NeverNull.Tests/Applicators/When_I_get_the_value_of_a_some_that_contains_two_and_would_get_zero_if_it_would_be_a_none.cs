using Machine.Specifications;

namespace NeverNull.Tests.Applicators {
    [Subject(typeof (NeverNull.Applicators), "GetOrElse")]
    internal class When_I_get_the_value_of_a_some_that_contains_two_and_would_get_zero_if_it_would_be_a_none {
        static IMaybe<int> _some;
        static int _two;

        Establish context = () => _some = new Some<int>(2);

        Because of = () => _two = _some.GetOrElse(0);

        It should_return_two = () => _two.ShouldEqual(2);
    }
}