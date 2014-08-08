using FluentAssertions;
using Machine.Specifications;

namespace NeverNull.Tests {
    [Subject(typeof (Option), "From")]
    public class When_I_create_an_option_from_zero {
        static Option<int> _sut;

        Because of =
            () => _sut = 0;

        It should_return_an_option_that_has_a_value =
            () => _sut.HasValue.Should().BeTrue();

        It should_return_an_option_that_is_not_empty =
            () => _sut.IsEmpty.Should().BeFalse();

        It should_return_zero_when_trying_to_access_the_value =
            () => _sut.Value.Should().Be(0);
    }
}