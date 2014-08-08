using FluentAssertions;
using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "Map")]
    public class When_I_convert_a_none_of_type_int_to_an_option_of_type_string_with_map {
        static Option<int> _none;
        static Option<string> _anotherNone;

        Establish context = () => _none = Option.None;

        Because of = () => _anotherNone = _none.Map(i => i.ToString());

        It should_return_a_none =
            () => _anotherNone.HasValue.Should().BeFalse();
    }
}