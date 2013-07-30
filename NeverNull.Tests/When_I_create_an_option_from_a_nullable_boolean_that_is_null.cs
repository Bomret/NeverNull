using System;
using Machine.Specifications;

namespace NeverNull.Tests {
    [Subject(typeof (Option))]
    public class When_I_create_an_option_from_a_nullable_boolean_that_is_null {
        private static IOption<bool?> _sut;
        private static bool? _null;
        private static bool? _value;

        private Establish context =
            () => _null = null;

        private Because of =
            () => _sut = Option.Create(_null);

        private It should_return_a_none =
            () => _sut.HasValue.ShouldBeFalse();

        private It should_throw_a_NotSupportedException_when_trying_to_access_the_value =
            () => Catch.Exception(() => _value = _sut.Value).ShouldBeOfType<NotSupportedException>();
    }
}