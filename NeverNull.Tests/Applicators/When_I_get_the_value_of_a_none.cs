using System;
using FluentAssertions;
using Machine.Specifications;
using NeverNull.Combinators;

namespace NeverNull.Tests.Applicators
{
    [Subject(typeof(GetExt), "Get")]
    internal class When_I_get_the_value_of_a_none
    {
        private static Option<int> _none;
        private static Exception _error;

        private Establish context = () => _none = Option.None;

        private Because of = () => _error = Catch.Exception(() => _none.Get());

        private It should_throw_a_NotSupportedException = () => _error.Should().BeOfType<InvalidOperationException>();
    }
}