using Args;
using Newtonsoft.Json;
using System;
using System.Runtime.InteropServices;

namespace ArgsTest
{
    /// <summary>
    /// -l -p 8080 -d /usr/logs
    /// </summary>
    public class ArgsTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void should_set_boolean_option_to_true_if_flag_present()
        {
            BooleanOption option = Arguments.Parse<BooleanOption>("-l");
            Assert.IsTrue(option.logging);
        }

        [Test]
        public void should_set_boolean_option_to_false_if_flag_not_present()
        {
            var option = Arguments.Parse<BooleanOption>(String.Empty);
            Assert.IsFalse(option.logging);
        }

        public record BooleanOption([property:Option("-l")] Boolean logging);

    }
}