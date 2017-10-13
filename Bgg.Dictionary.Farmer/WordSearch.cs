namespace Bgg.Dictionary.Farmer
{
    public class WordSearch
    {
        public Word SearchedWord { get; private set; }
        public bool HasBeenFound { get; private set; } = true;


        public static WordSearch NotFound(Word searchedWord)
        {
            return new WordSearch
            {
                HasBeenFound = false,
                SearchedWord = searchedWord
            };
        }

        public static WordSearch Found(Word searchedWord)
        {
            return new WordSearch
            {
                HasBeenFound = true,
                SearchedWord = searchedWord
            };
        }
    }
}
