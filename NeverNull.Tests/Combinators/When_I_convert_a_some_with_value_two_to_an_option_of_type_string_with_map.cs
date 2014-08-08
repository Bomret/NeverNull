using FluentAssertions;
using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "Map")]
    public class When_I_convert_a_some_with_value_two_to_an_option_of_type_string_with_map {
        static Option<int> _two;
        static Option<string> _twoAsString;

        Establish context = () => _two = 2;

        Because of = () => _twoAsString = _two.Map(i => i.ToString());

        It should_contain_two_as_string_in_the_some =
            () => _twoAsString.Value.Should().Be("2");

        It should_return_a_some =
            () => _twoAsString.HasValue.Should().BeTrue();
    }
}