using Machine.Specifications;

namespace NeverNull.Tests {
    [Subject(typeof (Option), "Equals")]
    public class When_I_check_the_equality_of_hello_with_some_and_none {
        static Option<string> _sut;

        Because of =
            () => _sut = "hello";

        It should_return_an_option_that_None_is_not_equal_to =
            () => Option.None.Equals(_sut).ShouldBeFalse();

        It should_return_an_option_that_does_not_equal_None =
            () => _sut.Equals(Option.None).ShouldBeFalse();

        It should_return_an_option_that_equals_hello =
            () => _sut.Equals("hello").ShouldBeTrue();

        It should_return_an_option_that_hello_is_not_equal_to =
            () => "hello".Equals(_sut).ShouldBeFalse();
    }
}