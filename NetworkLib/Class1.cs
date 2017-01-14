using System;
using System.Collections.Generic;
using System.Net.Http;
//using System.Threading.Tasks;

namespace NetworkLib
{
    public class NetworkManager
    {
        private string MatchmakerUrl { get; set; }

        public event Func<string> MessageReceived;

        public bool BroadcastMessage(string message)
        {
            //var a = Task.Run(() =>
            //{

            //});
            return false;
        }

        //async Task f()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var values = new Dictionary<string, string>
        //        {
        //            { "thing1", "hello" },
        //            { "thing2", "world" }
        //        };

        //        var content = new FormUrlEncodedContent(values);

        //        var response = await client.PostAsync("http://localhost:1337/", content);

        //        var responseString = await response.Content.ReadAsStringAsync();
        //    }
        //}

        private void OnMessageReceived(string message)
        {
            MessageReceived?.Invoke();
        }

        public NetworkManager(string matchmakerUrl)
        {
            MatchmakerUrl = matchmakerUrl;
        }
    }
}
