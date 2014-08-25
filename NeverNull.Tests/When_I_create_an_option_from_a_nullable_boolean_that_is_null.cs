using System;
using FluentAssertions;
using Machine.Specifications;

namespace NeverNull.Tests
{
    [Subject(typeof (Option), "From")]
    public class When_I_create_an_option_from_a_nullable_boolean_that_is_null
    {
        static Option<bool> _sut;
        static bool? _null;
        static bool _value;

        Establish context =
            () => _null = null;

        Because of =
            () => _sut = Option.FromNullable(_null);

        It should_return_an_option_that_has_no_value =
            () => _sut.HasValue.Should().BeFalse();

        It should_return_an_option_that_is_empty =
            () => _sut.IsEmpty.Should().BeTrue();

        It should_throw_a_NotSupportedException_when_trying_to_access_the_value =
            () => Catch.Exception(() => _value = _sut.Value).Should().BeOfType<NotSupportedException>();
    }
}