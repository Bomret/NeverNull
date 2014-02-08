using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "Do")]
    public class When_I_do_set_a_bool_before_mapping_toString_to_a_some_containing_three {
        static Option<string> _result;
        static bool _doExecuted;

        Because of = () => _result = Option.Some(3)
                                           .Do(() => _doExecuted = true)
                                           .Map(i => i.ToString());

        It should_contain_three_as_string_in_the_result =
            () => _result.Value.ShouldEqual("3");

        It should_return_a_some =
            () => _result.HasValue.ShouldBeTrue();

        It should_set_the_bool_to_true =
            () => _doExecuted.ShouldBeTrue();
    }
}