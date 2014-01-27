using System;
using Machine.Specifications;

namespace NeverNull.Tests {
    [Subject(typeof (Option), "Create")]
    public class When_I_create_an_option_from_null {
        static Option<object> _sut;
        static object _null;
        static object _value;

        Establish context =
            () => _null = null;

        Because of =
            () => _sut = Option.From(_null);

        It should_return_an_option_that_has_no_value =
            () => _sut.HasValue.ShouldBeFalse();

        It should_return_an_option_that_is_empty =
            () => _sut.IsEmpty.ShouldBeTrue();

        It should_throw_a_NotSupportedException_when_trying_to_access_the_value =
            () => Catch.Exception(() => _value = _sut.Value).ShouldBeOfType<InvalidOperationException>();
    }
}