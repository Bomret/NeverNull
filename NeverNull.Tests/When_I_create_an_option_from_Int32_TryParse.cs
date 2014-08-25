using System;
using FluentAssertions;
using Machine.Specifications;

namespace NeverNull.Tests
{
    [Subject(typeof (Option), "FromTryPattern")]
    public class When_I_create_an_option_from_Int32_TryParse
    {
        static Option<int> _sut;

        Because of =
            () => _sut = Option.FromTryPattern<string, int>(Int32.TryParse, "26");

        It should_return_an_option_that_has_a_value =
            () => _sut.HasValue.Should().BeTrue();

        It should_return_an_option_that_is_not_empty =
            () => _sut.IsEmpty.Should().BeFalse();

        It should_return_that_contains_hello_as_the_value =
            () => _sut.Value.Should().Be(26);
    }
}