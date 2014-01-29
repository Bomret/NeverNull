using Machine.Specifications;

namespace NeverNull.Tests {
    [Subject(typeof (Option), "Equals")]
    public class When_I_check_the_equality_of_null_with_some_and_none {
        static Option<string> _sut;
        static string _null;

        Establish ctx = () => _null = null;

        Because of = () => _sut = _null;

        It should_return_an_option_that_None_is_equal_to =
            () => Option.None.Equals(_sut).ShouldBeTrue();

        It should_return_an_option_that_equals_None =
            () => _sut.Equals(Option.None).ShouldBeTrue();

        It should_return_an_option_that_equals_null =
            () => _sut.Equals(null).ShouldBeTrue();
    }
}