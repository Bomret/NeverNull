using System;
using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "FlatMap")]
    public class When_I_convert_a_some_with_value_two_to_an_option_of_type_string_with_flatmap {
        private static IOption<int> _two;
        private static Func<int, IOption<string>> _toString;
        private static IOption<string> _twoAsString;

        private Establish context = () => {
            _two = Option.Create(2);

            _toString = i => Option.Create(i.ToString());
        };

        private Because of = () => _twoAsString = _two.FlatMap(_toString);

        private It should_contain_two_as_string_in_the_some =
            () => _twoAsString.Value.ShouldEqual("2");

        private It should_return_a_some =
            () => _twoAsString.HasValue.ShouldBeTrue();
    }
}