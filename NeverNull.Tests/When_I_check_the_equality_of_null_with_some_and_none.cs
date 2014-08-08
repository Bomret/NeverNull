using FluentAssertions;
using Machine.Specifications;

namespace NeverNull.Tests
{
    [Subject(typeof (Option), "Equals")]
    public class When_I_check_the_equality_of_null_with_some_and_none
    {
        private static Option<string> _sut;
        private static string _null;

        private Establish ctx = () => _null = null;

        private Because of = () => _sut = _null;

        private It should_return_an_option_that_None_is_equal_to =
            () => Option.None.Equals(_sut).Should().BeTrue();

        private It should_return_an_option_that_equals_None =
            () => _sut.Equals(Option.None).Should().BeTrue();

        private It should_return_an_option_that_is_not_equal_to_null =
            () => _sut.Equals(null).Should().BeFalse();
    }
}