using System;
using System.Threading.Tasks;
using Orleans;
using Talkach.Client;

namespace Talkach.Grains
{
    public class Chat : Grain, IChat
    {
        ObserverSubscriptionManager<IChatObserver> _subscribers;

        public override Task OnActivateAsync()
        {
            _subscribers = new ObserverSubscriptionManager<IChatObserver>();
            return TaskDone.Done;
        }

        public override Task OnDeactivateAsync()
        {
            _subscribers.Clear();
            return TaskDone.Done;
        }

        public Task AppendMessage(ITalker talker, string msg)
        {
            var name = talker.GetPrimaryKeyString();
            _subscribers.Notify(x => x.AppendMessage(name, msg));
            return TaskDone.Done;
        }

        public Task SubscribeForMessages(IChatObserver subscriber)
        {
            _subscribers.Subscribe(subscriber);
            return TaskDone.Done;
        }

        public Task UnsubscribeFromMessages(IChatObserver subscriber)
        {
            _subscribers.Unsubscribe(subscriber);
            return TaskDone.Done;
        }
    }
}