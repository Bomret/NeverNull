using Machine.Specifications;

namespace NeverNull.Tests.Applicators {
    [Subject(typeof (NeverNull.Applicators), "GetOrElse")]
    internal class When_I_get_the_value_of_a_none_and_get_zero_instead {
        static IMaybe<int> _none;
        static int _two;

        Establish context = () => _none = new None<int>();

        Because of = () => _two = _none.GetOrElse(0);

        It should_return_zero = () => _two.ShouldEqual(0);
    }
}