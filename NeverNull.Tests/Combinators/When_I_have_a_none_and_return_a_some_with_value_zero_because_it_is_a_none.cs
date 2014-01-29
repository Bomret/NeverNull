using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "OrElse")]
    public class When_I_have_a_none_and_return_a_some_with_value_zero_because_it_is_a_none {
        static Option<int> _none;
        static Option<int> _result;

        Establish context = () => _none = Option.None;

        Because of =
            () => _result = _none.OrElse(0);

        It should_contain_zero_in_the_some =
            () => _result.Value.ShouldEqual(0);

        It should_return_a_some =
            () => _result.HasValue.ShouldBeTrue();
    }
}