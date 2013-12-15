using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "Recover")]
    public class When_I_have_a_none_and_recover_with_zero {
        static IMaybe<int> _none;
        static IMaybe<int> _result;

        Establish context = () => _none = new None<int>();

        Because of =
            () => _result = _none.Recover(0);

        It should_contain_zero_in_the_some =
            () => _result.Value.ShouldEqual(0);

        It should_return_a_some =
            () => _result.HasValue.ShouldBeTrue();
    }
}