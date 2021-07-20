using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace TradePlugin.DiscordNotifications.Models
{
    public class DiscordMessage
    {
        [Serializable]
        public class Message
        {
            [JsonProperty("username")]
            public string Username { get; set; }

            [JsonProperty("embeds")]
            public List<Embed> Embeds { get; set; } = new List<Embed>();

            public Task SendMessageAsync(string url)
            {
                var webClient = new WebClient();
                webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                webClient.UploadStringAsync(new Uri(url), JsonConvert.SerializeObject(this));
                return Task.CompletedTask;
            }
        }

        public class Embed
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("fields")]
            public List<Field> Fields { get; set; }
        }

        [Serializable]
        public class Field
        {
            [JsonProperty("name")]
            public string Key { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }

            [JsonProperty("inline")]
            public bool Inline { get; set; }
        }
    }
}
