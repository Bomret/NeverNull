using FluentAssertions;
using Machine.Specifications;
using NeverNull.Combinators;

namespace NeverNull.Tests.Combinators
{
    [Subject(typeof (TransformExt), "Transform")]
    public class When_I_transform_none_to_a_some_of_string
    {
        static Option<string> _result;
        static Option<int> _none;

        Establish ctx = () => _none = Option.None;

        Because of = () => _result = _none.Transform(i => i.ToString(), () => "nothing");

        It should_contain_nothing_as_string_in_the_result =
            () => _result.Value.Should().Be("nothing");

        It should_return_a_some =
            () => _result.HasValue.Should().BeTrue();
    }
}