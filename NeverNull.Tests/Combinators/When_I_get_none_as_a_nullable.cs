using FluentAssertions;
using Machine.Specifications;
using NeverNull.Combinators;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (ToNullableExt), "ToNullable")]
    class When_I_get_none_as_a_nullable {
        static Option<int> _none;
        static int? _nullable;

        Establish context = () => _none = Option.None;

        Because of = () => _nullable = _none.ToNullable();

        It should_return_an_empty_nullable = () => _nullable.HasValue.Should().BeFalse();
    }
}