using Machine.Specifications;

namespace NeverNull.Tests {
    [Subject(typeof (Option))]
    public class When_I_create_an_option_from_hello {
        private static IOption<string> _sut;

        private Because of =
            () => _sut = Option.Create("hello");

        private It should_return_an_option_that_has_a_value =
            () => _sut.HasValue.ShouldBeTrue();

        private It should_return_an_option_that_is_not_empty =
            () => _sut.IsEmpty.ShouldBeFalse();

        private It should_return_zero_when_trying_to_access_the_value =
            () => _sut.Value.ShouldEqual("hello");
    }
}