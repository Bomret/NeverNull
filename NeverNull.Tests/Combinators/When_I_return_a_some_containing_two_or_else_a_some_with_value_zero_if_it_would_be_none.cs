using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "OrElse")]
    public class When_I_return_a_some_containing_two_or_else_a_some_with_value_zero_if_it_would_be_none {
        static Option<int> _two;
        static Option<int> _result;

        Establish context = () => _two = 2;

        Because of = () => _result = _two.OrElse(0);

        It should_contain_two_in_the_some =
            () => _result.Value.ShouldEqual(2);

        It should_return_a_some =
            () => _result.HasValue.ShouldBeTrue();
    }
}