using System;
using FluentAssertions;
using Machine.Specifications;

namespace NeverNull.Tests
{
    [Subject(typeof (Option), "From")]
    public class When_I_create_an_option_from_a_nullable_boolean_that_is_null
    {
        private static Option<bool> _sut;
        private static bool? _null;
        private static bool _value;

        private Establish context =
            () => _null = null;

        private Because of =
            () => _sut = Option.FromNullable(_null);

        private It should_return_an_option_that_has_no_value =
            () => _sut.HasValue.Should().BeFalse();

        private It should_return_an_option_that_is_empty =
            () => _sut.IsEmpty.Should().BeTrue();

        private It should_throw_a_NotSupportedException_when_trying_to_access_the_value =
            () => Catch.Exception(() => _value = _sut.Value).Should().BeOfType<NotSupportedException>();
    }
}