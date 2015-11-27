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
                var option = Option.From(x);

                string val = null;
                Exception err = null;
                try {
                    val = option.Get();
                }
                catch (Exception e) {
                    err = e;
                }

                return x == null 
                    ? (err != null && val == null) 
                    : (err == null && val == x);
            }).QuickCheckThrowOnFailure();

        [Test]
        public void Getting_the_value_from_an_option_should_return_its_default_for_None_and_return_the_value_if_the_option_contains_one() =>
            Prop.ForAll<string>(x => {
                var sut = Option.From(x);
                var val = "some value";
                val = sut.GetOrDefault();

                return val == (x ?? default(string));
            }).QuickCheckThrowOnFailure();

        [Test]
        public void Getting_the_value_from_an_option_should_return_the_fallback_for_None_and_return_the_value_if_the_option_contains_one() =>
            Prop.ForAll<string, string>((a, b) => {
                var sut = Option.From(a);
                var val = sut.GetOrElse(() => b);

                return val == (a ?? b);
            }).QuickCheckThrowOnFailure();
    }
}
