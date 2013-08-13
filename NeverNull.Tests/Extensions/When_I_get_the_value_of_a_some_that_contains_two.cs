using Machine.Specifications;

namespace NeverNull.Tests.Extensions {
    [Subject(typeof (NeverNull.Extensions), "Get")]
    internal class When_I_get_the_value_of_a_some_that_contains_two {
        private static IOption<int> _some;
        private static int _two;

        private Establish context = () => _some = new Some<int>(2);

        private Because of = () => _two = _some.Get();

        private It should_return_two = () => _two.ShouldEqual(2);
    }
}