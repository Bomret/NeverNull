using FluentAssertions;
using Machine.Specifications;

namespace NeverNull.Tests {
    [Subject(typeof (Option), "From")]
    public class When_I_create_an_option_from_zero {
        private static Option<int> _sut;

        private Because of =
            () => _sut = Option.From(0);

        private It should_return_an_option_that_has_a_value =
            () => _sut.HasValue.Should().BeTrue();

        private It should_return_an_option_that_is_not_empty =
            () => _sut.IsEmpty.Should().BeFalse();

        private It should_return_zero_when_trying_to_access_the_value =
            () => _sut.Value.Should().Be(0);
    }
}