using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bgg.Dictionary.Farmer.Tests
{
    public class LangLabEnglishDictionarySpec
    {
        [Fact]
        public async Task Can_insert_a_new_word()
        {
            await new LangLabEnglishDictionary().InsertWord(new Word("Hope"));

        }
    }
}
