using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "Reject")]
    public class When_I_reject_a_some_containing_two_if_it_contains_three {
        static Option<int> _two;
        static Option<int> _result;

        Establish context = () => _two = 2;

        Because of =
            () => _result = _two.Reject(i => i == 3);

        It should_contain_two_in_the_some =
            () => _result.Value.ShouldEqual(2);

        It should_return_a_some =
            () => _result.HasValue.ShouldBeTrue();
    }
}