using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Parser.Html;
using Bgg.Dictionary.Farmer.Dictionary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bgg.Dictionary.Farmer
{
    public class MerrianWebsterDataStore : IDictionaryDataStore
    {
        readonly string _wordUrl = "https://www.merriam-webster.com/dictionary/{0}";
        readonly CultureInfo _dictionaryCulture = new CultureInfo("en-US");

        public MerrianWebsterDataStore()
        {

        }

        public bool HasDictionary(string language)
        {
            return language == "es";
        }

        static Task<string> download(string url)
        {            
            WebProxy proxy = new WebProxy("194.114.63.23:8080",true);
            proxy.Credentials = new NetworkCredential("DQ18GR4", "67PaNn8$U9"); 
            WebRequest.DefaultWebProxy = proxy;
            WebClient client = new WebClient();
            client.Proxy = proxy;
            return client.DownloadStringTaskAsync(new Uri(url));
        }
        public async Task<WordSearch> Search(string word, string language)
        {
            var searchedWord = new Word(word);
            WordSearch result; 
            var address = string.Format(_wordUrl, word);
            var page = await download(address);
            var document = new HtmlParser().Parse(page);
            if (document.QuerySelector(".words_fail_us_cont") == null)
            {
                result = WordSearch.Found(searchedWord);
                searchedWord.Definitions.AddRange(ParseDefinitions(document));
            }
            else
            {
                result = WordSearch.NotFound(searchedWord);
            }

            return result;
        }

        IEnumerable<WordDefinition> ParseDefinitions(IDocument document)
        {
            return document.QuerySelectorAll(".fl").Select((item,index) =>
            {
                var result = new WordDefinition
                {
                    PartOfSpeech = (WordPartOfSpeech)Enum.Parse(typeof(WordPartOfSpeech), item.FirstChild.TextContent, true)
                };
                var speechAnchors = document.QuerySelectorAll($".hw-play-pron").OfType<IHtmlAnchorElement>();
                if(speechAnchors.Count() > index) result.PronunciationUrl.Add("en_us", speechAnchors.ElementAt(index).Href);
                result.Definitions.AddRange(document.QuerySelectorAll($"#entry-{index + 1} .dt").Select(i => CleanDefinition(i.Text())));
                
                var examplesBoxes = document.QuerySelectorAll(".examples-box");
                if (examplesBoxes.Count() > index)
                {
                    var exampleBox = examplesBoxes.ElementAt(index);
                    result.Examples.AddRange(exampleBox.QuerySelectorAll(".definition-inner-item").Select(i => CleanDefinition(i.TextContent)));
                }
                return result;
            });
        }
        string CleanDefinition(string definition)
        {
            definition = Regex.Replace(definition, "/n", "");
            definition = Regex.Replace(definition, "\\n", "");
            definition = Regex.Replace(definition, @"[ ]{2,}", "");
            return _dictionaryCulture.TextInfo.ToTitleCase( definition.Replace(":","").Trim());
        }
    }
}
