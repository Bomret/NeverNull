using FluentAssertions;
using Machine.Specifications;
using NeverNull.Combinators;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (MapExt), "Map")]
    public class When_I_convert_a_some_with_value_two_to_an_option_of_type_string_with_map {
        private static Option<string> _twoAsString;

        private Because of = () => _twoAsString = Option.Some(2)
                                                        .Map(i => i.ToString());

        private It should_contain_two_as_string_in_the_some =
            () => _twoAsString.Value.Should().Be("2");

        private It should_return_a_some =
            () => _twoAsString.HasValue.Should().BeTrue();
    }
}