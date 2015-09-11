using System.Threading.Tasks;
using Orleans;
using Talkach.Client;

namespace Talkach.Grains
{
    public class Talker : Grain, ITalker, IChatObserver
    {
        IChat _chat;

        public override Task OnDeactivateAsync()
        {
            return _chat.UnsubscribeFromMessages(this);
        }

        public async Task JoinChat(IChat chat)
        {
            _chat = chat;
            await chat.AppendMessage(this, "Hello");
            await _chat.SubscribeForMessages(this);
        }

        public async Task StartTheGame()
        {
            await _chat.AppendMessage(this, "I'm ready to start The Game");
        }

        public void AppendMessage(string name, string msg)
        {
            // Listen only for others
            if (name == this.GetPrimaryKeyString()) return;

            if (msg.Contains("start"))
            {
                var chat = GrainFactory.GetGrain<IChat>(0);
                chat.AppendMessage(this, "Ok, " + name + ", go ahead");
            }
        }
    }
}