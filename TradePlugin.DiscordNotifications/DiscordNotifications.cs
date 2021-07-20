using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using OpenMod.API.Plugins;
using OpenMod.Unturned.Plugins;
using static TradePlugin.DiscordNotifications.Models.DiscordMessage;

[assembly: PluginMetadata("TradePlugin.DiscordNotifications", DisplayName = "TradePlugin.DiscordNotifications")]
namespace TradePlugin.DiscordNotifications
{
    public class DiscordNotifications : OpenModUnturnedPlugin
    {
        public DiscordNotifications(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override async UniTask OnLoadAsync()
        {
            if (await DataStore.ExistsAsync("messages"))
                return;
           
            var messages = new Dictionary<string, Message>();

            messages.Add("start", new Message
            {
                Username = "Trade Alerts",
                Embeds = new List<Embed>()
                {
                    new Embed
                    {
                        Title = "Trade Started",
                        Fields = new List<Field>()
                        {
                            new Field
                            {
                                Key = "Sender Name",
                                Value = "{Event.Sender.Player.channel.owner.playerID.playerName}"
                            },
                            new Field
                            {
                                Key = "Receiver Name",
                                Value = "{Event.Receiver.Player.channel.owner.playerID.playerName}"
                            }
                        }
                    }
                }
            });

            messages.Add("end", new Message
            {
                Username = "Trade Alerts",
                Embeds = new List<Embed>()
                {
                    new Embed
                    {
                        Title = "Trade End",
                        Fields = new List<Field>()
                        {
                            new Field
                            {
                                Key = "Sender Name",
                                Value = "{Event.Trade.Sender.Owner.Player.channel.owner.playerID.playerName}"
                            },
                            new Field
                            {
                                Key = "Receiver Name",
                                Value = "{Event.Trade.Receiver.Owner.Player.channel.owner.playerID.playerName}"
                            }
                        }
                    }
                }
            });

            await DataStore.SaveAsync("messages", messages);
        }
    }
}
