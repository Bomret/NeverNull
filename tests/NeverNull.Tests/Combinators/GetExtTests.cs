using System;
using FsCheck;
using NeverNull.Combinators;
using NUnit.Framework;

namespace NeverNull.Tests.Combinators
{
    [TestFixture]
    class GetExtTests {
        [Test]
        public void Getting_the_value_from_an_option_should_throw_for_None_and_return_the_value_if_the_option_contains_one() =>
            Prop.ForAll<string>(x => {
                string val = null;
                Exception err = null;
                try {
                    val = Option.From(x).Get();
                }
                catch (Exception e) {
                    err = e;
                }

                return x == null 
                    ? (err != null && val == null) 
                    : (err == null && val == x);
            }).QuickCheckThrowOnFailure();

        [Test]
        public void Getting_the_value_from_an_option_from_a_nullable_should_throw_for_None_and_return_the_value_if_the_option_contains_one() =>
            Prop.ForAll<int?>(x => {
                int val = 0;
                Exception err = null;
                try {
                    val = Option.From(x).Get();
                }
                catch (Exception e) {
                    err = e;
                }

                return x == null
                    ? (err != null && val == 0)
                    : (err == null && val == x);
            }).QuickCheckThrowOnFailure();

        [Test]
        public void Getting_the_value_from_an_option_should_return_its_default_for_None_and_return_the_value_if_the_option_contains_one() =>
            Prop.ForAll<string>(x => 
                Option.From(x).GetOrDefault() == (x == null ? default(string) : x))
            .QuickCheckThrowOnFailure();

        [Test]
        public void Getting_the_value_from_an_option_should_return_the_fallback_for_None_and_return_the_value_if_the_option_contains_one() =>
            Prop.ForAll<string, string>((a, b) =>
                Option.From(a).GetOrElse(() => b) == (a == null ? b : a))
            .QuickCheckThrowOnFailure();
    }
}
