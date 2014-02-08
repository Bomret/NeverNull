using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "Transform")]
    public class When_I_transform_a_some_containing_three_to_a_some_of_string {
        static Option<string> _result;

        Because of = () => _result = Option.Some(3)
                                           .Transform(i => i.ToString(), () => "nothing");

        It should_contain_three_as_string_in_the_result =
            () => _result.Value.ShouldEqual("3");

        It should_return_a_some =
            () => _result.HasValue.ShouldBeTrue();
    }
}