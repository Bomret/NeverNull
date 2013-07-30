using Machine.Specifications;

namespace NeverNull.Tests {
    [Subject(typeof (Option))]
    public class When_I_create_an_option_from_a_nullable_boolean_that_is_true {
        private static IOption<bool?> _sut;
        private static bool? _true;

        private Establish context =
            () => _true = true;

        private Because of =
            () => _sut = Option.Create(_true);

        private It should_contain_true_in_the_some =
            () => _sut.Value.Value.ShouldBeTrue();

        private It should_return_a_some =
            () => _sut.HasValue.ShouldBeTrue();
    }
}