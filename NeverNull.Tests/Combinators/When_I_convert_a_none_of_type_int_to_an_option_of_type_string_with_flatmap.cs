using System;
using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "FlatMap")]
    public class When_I_convert_a_none_of_type_int_to_an_option_of_type_string_with_flatmap {
        static IMaybe<int> _none;
        static Func<int, IMaybe<string>> _toString;
        static IMaybe<string> _anotherNone;

        Establish context = () => {
            _none = new None<int>();

            _toString = i => Maybe.From(i.ToString());
        };

        Because of = () => _anotherNone = _none.FlatMap(_toString);

        It should_return_a_none =
            () => _anotherNone.HasValue.ShouldBeFalse();
    }
}