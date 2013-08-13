using Machine.Specifications;

namespace NeverNull.Tests {
    [Subject(typeof (Option), "Create")]
    public class When_I_create_an_option_from_zero {
        private static IOption<int> _sut;

        private Because of =
            () => _sut = Option.Create(0);

        private It should_return_an_option_that_has_a_value =
            () => _sut.HasValue.ShouldBeTrue();

        private It should_return_an_option_that_is_not_empty =
            () => _sut.IsEmpty.ShouldBeFalse();

        private It should_return_zero_when_trying_to_access_the_value =
            () => _sut.Value.ShouldEqual(0);
    }
}