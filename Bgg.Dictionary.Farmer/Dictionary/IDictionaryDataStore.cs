using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bgg.Dictionary.Farmer.Dictionary
{
    public interface IDictionaryDataStore
    {
        Task<WordSearch> Search(string word, string language);

        bool HasDictionary(string language);

    }
}
