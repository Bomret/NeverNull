using Machine.Specifications;

namespace NeverNull.Tests {
    [Subject(typeof (Option), "Create")]
    public class When_I_create_an_option_from_a_nullable_boolean_that_is_true {
        private static IOption<bool?> _sut;
        private static bool? _true;

        private Establish context =
            () => _true = true;

        private Because of =
            () => _sut = Option.Create(_true);

        private It should_contain_true_in_the_some =
            () => _sut.Value.Value.ShouldBeTrue();

        private It should_return_an_option_that_has_a_value =
            () => _sut.HasValue.ShouldBeTrue();

        private It should_return_an_option_that_is_not_empty =
            () => _sut.IsEmpty.ShouldBeFalse();
    }
}