using Microsoft.Extensions.Configuration;
using OpenMod.API.Eventing;
using OpenMod.API.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradePlugin.API.Events;
using static TradePlugin.DiscordNotifications.Models.DiscordMessage;

namespace TradePlugin.DiscordNotifications.Events
{
    public class TradeStartingEventListener : IEventListener<TradeStartingEvent>
    {
        private readonly IConfiguration configuration;
        private readonly IDataStore dataStore;

        public TradeStartingEventListener(IConfiguration configuration, IDataStore dataStore)
        {
            this.configuration = configuration;
            this.dataStore = dataStore;
        }

        public async Task HandleEventAsync(object sender, TradeStartingEvent @event)
        {
            var webhook = (await dataStore.LoadAsync<Dictionary<string, Message>>("messages"))["start"];

            foreach(var embed in webhook.Embeds)
            {
                foreach(var field in embed.Fields)
                {
                    field.Key = SmartFormat.Smart.Format(field.Key, new
                    {
                        Event = @event
                    });

                    field.Value = SmartFormat.Smart.Format(field.Value, new
                    {
                        Event = @event
                    });
                }
            }

            await webhook.SendMessageAsync(configuration.GetSection("webhookConfiguration:webhookUrl").Get<string>());
        }
    }
}
