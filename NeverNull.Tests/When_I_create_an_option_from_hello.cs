using Machine.Specifications;

namespace NeverNull.Tests {
    [Subject(typeof (Maybe), "Create")]
    public class When_I_create_an_option_from_hello {
        static IMaybe<string> _sut;

        Because of =
            () => _sut = Maybe.From("hello");

        It should_return_an_option_that_has_a_value =
            () => _sut.HasValue.ShouldBeTrue();

        It should_return_an_option_that_is_not_empty =
            () => _sut.IsEmpty.ShouldBeFalse();

        It should_return_zero_when_trying_to_access_the_value =
            () => _sut.Value.ShouldEqual("hello");
    }
}