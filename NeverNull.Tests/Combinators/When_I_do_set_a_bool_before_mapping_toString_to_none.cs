using FluentAssertions;
using Machine.Specifications;
using NeverNull.Combinators;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (DoExt), "Do")]
    public class When_I_do_set_a_bool_before_mapping_toString_to_none {
        static Option<string> _result;
        static bool _doExecuted;
        static Option<int> _none;

        Establish ctx = () => _none = Option.None;

        Because of = () => _result = _none.Do(() => _doExecuted = true)
                                          .Map(i => i.ToString());

        It should_return_none =
            () => _result.IsEmpty.Should().BeTrue();

        It should_set_the_bool_to_true =
            () => _doExecuted.Should().BeTrue();
    }
}