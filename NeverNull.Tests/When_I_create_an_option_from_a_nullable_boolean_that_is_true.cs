using Machine.Specifications;

namespace NeverNull.Tests {
    [Subject(typeof (Option), "From")]
    public class When_I_create_an_option_from_a_nullable_boolean_that_is_true {
        static Option<bool?> _sut;
        static bool? _true;

        Establish context =
            () => _true = true;

        Because of =
            () => _sut = Option.From(_true);

        It should_contain_true_in_the_some =
            () => _sut.Value.Value.ShouldBeTrue();

        It should_return_an_option_that_has_a_value =
            () => _sut.HasValue.ShouldBeTrue();

        It should_return_an_option_that_is_not_empty =
            () => _sut.IsEmpty.ShouldBeFalse();
    }
}