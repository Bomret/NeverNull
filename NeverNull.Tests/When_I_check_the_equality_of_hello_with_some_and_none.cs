using FluentAssertions;
using Machine.Specifications;

namespace NeverNull.Tests {
    [Subject(typeof (Option), "Equals")]
    public class When_I_check_the_equality_of_hello_with_some_and_none {
        private static Option<string> _sut;

        private Because of =
            () => _sut = Option.From("hello");

        private It should_return_an_option_that_None_is_not_equal_to =
            () => Option.None.Equals(_sut).Should().BeFalse();

        private It should_return_an_option_that_a_some_containing_hello_is_equal_to =
            () => Option.Some("hello").Equals(_sut).Should().BeTrue();

        private It should_return_an_option_that_does_not_equal_None =
            () => _sut.Equals(Option.None).Should().BeFalse();

        private It should_return_an_option_that_hello_is_not_equal_to =
            () => "hello".Equals(_sut).Should().BeFalse();

        private It should_return_an_option_that_is_equal_to_a_some_containing_hello =
            () => _sut.Equals(Option.Some("hello")).Should().BeTrue();

        private It should_return_an_option_that_is_not_equal_to_hello =
            () => _sut.Equals("hello").Should().BeFalse();
    }
}