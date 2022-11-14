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
        public record BooleanOption([property: Option("-l")] Boolean logging);


        [Test]
        public void should_get_port_as_option_value()
        {
            var option = Arguments.Parse<UInt16Option>("-p", "8080");
            Assert.IsTrue(option.port == 8080);
        }

        public record UInt16Option([property:Option("-p")]UInt16 port);

        [Test]
        public void should_get_directory_as_option_value()
        {
            var option = Arguments.Parse<StringOption>("-d", "/usr/logs");
            Assert.IsTrue(option.directory=="/usr/logs");
        }

        public record StringOption([property: Option("-d")]String directory);

        [Test]
        public void should_parse_multi_option()
        {
            var option = Arguments.Parse<MultiOption>("-l", "-p", "8080", "-d", "/usr/logs");
            Assert.IsTrue(option.logging);
            Assert.IsTrue(option.port == 8080);
            Assert.IsTrue(option.directory == "/usr/logs");
        }

        public record MultiOption([property: Option("-l")] Boolean logging, 
                                    [property: Option("-p")] UInt16 port, 
                                    [property: Option("-d")] String directory);
    }
}