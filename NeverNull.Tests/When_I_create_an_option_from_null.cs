using System;
using Machine.Specifications;

namespace NeverNull.Tests {
    [Subject(typeof (Option))]
    public class When_I_create_an_option_from_null {
        private static IOption<object> _sut;
        private static object _null;
        private static object _value;

        private Establish context =
            () => _null = null;

        private Because of =
            () => _sut = Option.Create(_null);

        private It should_return_an_option_that_has_no_value =
            () => _sut.HasValue.ShouldBeFalse();

        private It should_return_an_option_that_is_empty =
            () => _sut.IsEmpty.ShouldBeTrue();

        private It should_throw_a_NotSupportedException_when_trying_to_access_the_value =
            () => Catch.Exception(() => _value = _sut.Value).ShouldBeOfType<NotSupportedException>();
    }
}