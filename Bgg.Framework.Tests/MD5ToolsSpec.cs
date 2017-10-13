using System;
using Xunit;

namespace Bgg.Framework.Tests
{
    public class MD5ToolsSpec
    {
        [Theory]
        [InlineData("something", "437b930db84b8079c2dd804a71936b5f")]
        public void It_can_generate_correct_MD5_of_a_string(string input, string expected)
        {
            var actual = MD5Tools.Generate(input);
            Assert.Equal(expected, actual,true);
        }
    }
}
