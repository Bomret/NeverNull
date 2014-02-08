using Machine.Specifications;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (NeverNull.Combinators), "ZipWith")]
    public class When_I_zip_none_and_some_containing_two_with_the_addition_of_their_values {
        static Option<int> _result;
        static Option<int> _two;
        static Option<int> _none;

        Establish ctx = () => {
            _two = 2;
            _none = Option.None;
        };

        Because of = () => _result = _none.ZipWith(_two, (a, b) => Option.From(a + b));

        It should_return_none =
            () => _result.IsEmpty.ShouldBeTrue();
    }
}