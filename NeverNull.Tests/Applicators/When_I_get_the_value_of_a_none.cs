using System;
using Machine.Specifications;

namespace NeverNull.Tests.Applicators {
    [Subject(typeof (NeverNull.Applicators), "Get")]
    internal class When_I_get_the_value_of_a_none {
        static IMaybe<int> _none;
        static Exception _error;

        Establish context = () => _none = new None<int>();

        Because of = () => _error = Catch.Exception(() => _none.Get());

        It should_throw_a_NotSupportedException = () => _error.ShouldBeOfType<NotSupportedException>();
    }
}