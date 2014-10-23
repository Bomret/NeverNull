using FluentAssertions;
using Machine.Specifications;

namespace NeverNull.Tests {
    [Subject(typeof (Option), "From")]
    public class When_I_create_an_option_from_hello {
        private static Option<string> _sut;

        private Because of =
            () => _sut = Option.From("hello");

        private It should_return_an_option_that_has_a_value =
            () => _sut.HasValue.Should().BeTrue();

        private It should_return_an_option_that_is_not_empty =
            () => _sut.IsEmpty.Should().BeFalse();

        private It should_return_that_contains_hello_as_the_value =
            () => _sut.Value.Should().Be("hello");
    }
}