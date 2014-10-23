using FluentAssertions;
using Machine.Specifications;
using NeverNull.Combinators;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (ZipWithExt), "ZipWith")]
    public class When_I_zip_two_somes_containing_two_and_three_with_the_addition_of_their_values {
        static Option<int> _result;
        static Option<int> _two;
        static Option<int> _three;

        Establish ctx = () => {
            _two = Option.From(2);
            _three = Option.From(3);
        };

        Because of = () => _result = _two.ZipWith(_three, (a, b) => Option.From(a + b));

        It should_contain_five_in_the_result =
            () => _result.Value.Should().Be(5);

        It should_return_a_some =
            () => _result.HasValue.Should().BeTrue();
    }
}