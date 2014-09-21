using FluentAssertions;
using Machine.Specifications;
using NeverNull.Combinators;

namespace NeverNull.Tests.Combinators {
    [Subject(typeof (IfSomeExt), "IfSome")]
    class When_I_attach_a_WhenSome_callback_to_a_some_that_contains_two {
        static Option<int> _some;
        static int _two;

        Establish context = () => _some = Option.Some(2);

        Because of = () => _some.IfSome(i => _two = i);

        It should_execute_the_callback_and_return_two = () => _two.Should().Be(2);
    }
}