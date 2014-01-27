using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "Recover")]
    public class When_I_have_a_some_with_value_two_and_would_recover_with_zero {
        static Option<int> _two;
        static Option<int> _result;

        Establish context = () => _two = Option.Some(2);

        Because of =
            () => _result = _two.Recover(0);

        It should_contain_two_in_the_some =
            () => _result.Value.ShouldEqual(2);

        It should_return_a_some =
            () => _result.HasValue.ShouldBeTrue();
    }
}