using FluentAssertions;
using Machine.Specifications;
using NeverNull.Combinators;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (TapExt), "Tap")]
    public class When_I_tap_a_some_containing_three_before_mapping_ToString_to_it {
        static Option<string> _result;
        static int _three;

        Because of = () => _result = Option.Some(3)
                                           .Tap(i => _three = i)
                                           .Map(i => i.ToString());

        It should_contain_three_as_string_in_the_result =
            () => _result.Value.Should().Be("3");

        It should_return_a_some =
            () => _result.HasValue.Should().BeTrue();

        It should_set_the_tapped_result_to_three_as_integer =
            () => _three.Should().Be(3);
    }
}