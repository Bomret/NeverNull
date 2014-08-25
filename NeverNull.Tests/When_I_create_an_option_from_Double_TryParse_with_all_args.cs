using System;
using System.Globalization;
using FluentAssertions;
using Machine.Specifications;

namespace NeverNull.Tests
{
    [Subject(typeof (Option), "FromTryPattern")]
    public class When_I_create_an_option_from_Double_TryParse_with_all_args
    {
        static Option<double> _sut;

        Because of =
            () =>
            _sut =
            Option.FromTryPattern<string, NumberStyles, IFormatProvider, double>(Double.TryParse,
                                                                                 "2.6",
                                                                                 NumberStyles.AllowDecimalPoint,
                                                                                 CultureInfo.InvariantCulture);

        It should_return_an_option_that_has_a_value =
            () => _sut.HasValue.Should().BeTrue();

        It should_return_an_option_that_is_not_empty =
            () => _sut.IsEmpty.Should().BeFalse();

        It should_return_that_contains_hello_as_the_value =
            () => _sut.Value.Should().Be(2.6);
    }
}