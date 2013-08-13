using Machine.Specifications;

namespace NeverNull.Tests.Extensions {
    [Subject(typeof (NeverNull.Extensions),"GetOrElse")]
    internal class When_I_get_the_value_of_a_none_and_get_zero_instead {
        private static IOption<int> _none;
        private static int _two;

        private Establish context = () => _none = new None<int>();

        private Because of = () => _two = _none.GetOrElse(0);

        private It should_return_zero = () => _two.ShouldEqual(0);
    }
}