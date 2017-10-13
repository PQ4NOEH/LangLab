using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bgg.Dictionary.Farmer
{
    public class LangLabEnglishDictionary
    {

        public async Task InsertWord(Word word)
        {
            WebProxy proxy = new WebProxy("194.114.63.23:8080", true);
            proxy.Credentials = new NetworkCredential("DQ18GR4", "67PaNn8$U9");
            WebRequest.DefaultWebProxy = proxy;
            //WebClient client = new WebClient();
            //client.Proxy = proxy;
            //return client.DownloadStringTaskAsync(new Uri(url));
            FirebaseOptions opt = new FirebaseOptions();
            var client = new FirebaseClient("https://langlab-1753c.firebaseio.com/");
            var result = await client
                .Child("englishDictionary")
                .PostAsync(Newtonsoft.Json.JsonConvert.SerializeObject(word));
            Console.WriteLine("stop here");

        }
    }
}
