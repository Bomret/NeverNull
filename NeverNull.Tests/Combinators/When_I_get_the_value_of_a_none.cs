using System;
using FluentAssertions;
using Machine.Specifications;
using NeverNull.Combinators;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (GetExt), "Get")]
    class When_I_get_the_value_of_a_none {
        static Option<int> _none;
        static Exception _error;

        Establish context = () => _none = Option.None;

        Because of = () => _error = Catch.Exception(() => _none.Get());

        It should_throw_a_NotSupportedException = () => _error.Should().BeOfType<NotSupportedException>();
    }
}