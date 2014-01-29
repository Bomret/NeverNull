using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "FlatMap")]
    public class When_I_convert_a_some_with_value_two_to_an_option_of_type_string_with_flatmap {
        static Option<int> _two;
        static Option<string> _twoAsString;

        Establish context = () => _two = 2;

        Because of = () => _twoAsString = _two.FlatMap(i => Option.Some(i.ToString()));

        It should_contain_two_as_string_in_the_some =
            () => _twoAsString.Value.ShouldEqual("2");

        It should_return_a_some =
            () => _twoAsString.HasValue.ShouldBeTrue();
    }
}