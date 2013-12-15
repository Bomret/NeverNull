using System;
using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "Map")]
    public class When_I_convert_a_some_with_value_two_to_an_option_of_type_string_with_map {
        static IMaybe<int> _two;
        static Func<int, string> _toString;
        static IMaybe<string> _twoAsString;

        Establish context = () => {
            _two = Maybe.From(2);

            _toString = i => i.ToString();
        };

        Because of = () => _twoAsString = _two.Map(_toString);

        It should_contain_two_as_string_in_the_some =
            () => _twoAsString.Value.ShouldEqual("2");

        It should_return_a_some =
            () => _twoAsString.HasValue.ShouldBeTrue();
    }
}