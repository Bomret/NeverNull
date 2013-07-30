using System;
using Machine.Specifications;

namespace NeverNull.Tests.Extensions {
    [Subject(typeof (NeverNull.Extensions))]
    internal class When_I_get_the_value_of_a_none {
        private static IOption<int> _none;
        private static Exception _error;

        private Establish context = () => _none = new None<int>();

        private Because of = () => _error = Catch.Exception(() => _none.Get());

        private It should_throw_a_NotSupportedException = () => _error.ShouldBeOfType<NotSupportedException>();
    }
}