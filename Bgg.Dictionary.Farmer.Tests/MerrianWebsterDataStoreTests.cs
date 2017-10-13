using System;
using System.Threading.Tasks;
using Xunit;

namespace Bgg.Dictionary.Farmer.Tests
{
    public class MerrianWebsterDataStoreTests
    {
        [Theory]
        [InlineData("World")]
        [InlineData("something")]
        [InlineData("light")]
        [InlineData("   endeavor")]
        public async Task It_can_get_english_words(string word)
        {
            var result = await new MerrianWebsterDataStore().Search("something  ", "en");
            Assert.True(result.HasBeenFound);
        }

        public async Task Given_a_non_exixtent_word_results_in_a_notfound()
        {
            var result = await new MerrianWebsterDataStore().Search("somethingfsdfweqrwqrfs<szafc  ", "en");
            Assert.False(result.HasBeenFound);
        }
    }
}
