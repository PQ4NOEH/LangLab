using Bgg.Framework;
using System;
using System.Collections.Generic;

namespace Bgg.Dictionary
{

    public enum WordPartOfSpeech
    {
        Noun,
        Adverb,
        Adjetive,
        Verb,
        Pronoun,
        Preposition,
        conjunction,
        interjection
    }
    public class Word
    {
        public string Id { get; set; }
        public string Literal { get; private set; }
        public List<WordDefinition> Definitions { get; private set; } = new List<WordDefinition>();

        public Word() { }
        public Word(string literal)
        {
            if (string.IsNullOrWhiteSpace(literal)) throw new ArgumentNullException();

            Literal = literal.Trim();
            Id = MD5Tools.Generate(Literal);
        }

    }

    public class WordDefinition
    {
        public WordPartOfSpeech PartOfSpeech { get; set; }
        public List<string> Definitions { get; private set; } = new List<string>();
        public List<string> Synonyms { get; private set; } = new List<string>();
        public List<string> Antonyms { get; private set; } = new List<string>();
        public List<string> Examples { get; private set; } = new List<string>();
        public Dictionary<string, string> PronunciationUrl { get; private set; } = new Dictionary<string, string>();
    }

}
