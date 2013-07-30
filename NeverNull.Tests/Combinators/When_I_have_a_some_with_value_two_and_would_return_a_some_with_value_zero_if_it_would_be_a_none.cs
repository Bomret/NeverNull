using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators))]
    public class When_I_have_a_some_with_value_two_and_would_return_a_some_with_value_zero_if_it_would_be_a_none {
        private static IOption<int> _two;
        private static IOption<int> _result;

        private Establish context = () => _two = new Some<int>(2);

        private Because of =
            () => _result = _two.OrElse(new Some<int>(0));

        private It should_contain_two_in_the_some =
            () => _result.Value.ShouldEqual(2);

        private It should_return_a_some =
            () => _result.HasValue.ShouldBeTrue();
    }
}