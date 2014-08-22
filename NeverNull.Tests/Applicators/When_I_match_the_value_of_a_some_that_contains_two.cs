using System;
using FluentAssertions;
using Machine.Specifications;
using NeverNull.Combinators;

namespace NeverNull.Tests.Applicators
{
    [Subject(typeof(MatchExt), "Match")]
    class When_I_match_the_value_of_a_some_that_contains_two
    {
        static Option<int> _some;
        static int _two;

        static Action<int> _whenSome;
        static bool _isNone;
        static Action _whenNone;

        Establish context = () =>
        {
            _some = Option.Some(2);

            _whenSome = i => _two = i;
            _whenNone = () => _isNone = true;
        };

        Because of = () => _some.Match(
                         _whenSome,
                         _whenNone);

        It should_execute_the_some_callback_and_return_two = () => _two.Should().Be(2);

        It should_not_execute_the_none_callback = () => _isNone.Should().BeFalse();
    }
}