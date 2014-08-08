using FluentAssertions;
using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "Tap")]
    public class When_I_tap_none_before_mapping_ToString_to_it {
        static Option<string> _result;
        static Option<int> _none;
        static int _zero;

        Establish ctx = () => _none = Option.None;

        Because of = () => _result = _none.Tap(i => _zero = i)
                                          .Map(i => i.ToString());

        It should_leave_the_tapped_result_as_default_of_int =
            () => _zero.Should().Be(default(int));

        It should_return_none =
            () => _result.IsEmpty.Should().BeTrue();
    }
}