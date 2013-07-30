using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators))]
    public class When_I_have_a_none_and_return_a_some_with_value_zero_because_it_is_a_none {
        private static IOption<int> _none;
        private static IOption<int> _result;

        private Establish context =
            () => _none = new None<int>();

        private Because of =
            () => _result = _none.OrElse(new Some<int>(0));

        private It should_contain_zero_in_the_some =
            () => _result.Value.ShouldEqual(0);

        private It should_return_a_some =
            () => _result.HasValue.ShouldBeTrue();
    }
}