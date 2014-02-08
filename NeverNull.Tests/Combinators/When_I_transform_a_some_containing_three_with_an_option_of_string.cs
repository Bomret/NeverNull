using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "TransformWith")]
    public class When_I_transform_a_some_containing_three_with_an_option_of_string {
        static Option<string> _result;

        Because of = () => _result = Option.Some(3)
                                           .TransformWith(i => Option.From(i.ToString()), () => Option.Some("nothing"));

        It should_contain_three_as_string_in_the_result =
            () => _result.Value.ShouldEqual("3");

        It should_return_a_some =
            () => _result.HasValue.ShouldBeTrue();
    }
}