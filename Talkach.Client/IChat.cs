using System.Threading.Tasks;
using Orleans;

namespace Talkach.Client
{
    public interface IChat : IGrainWithIntegerKey
    {
        Task AppendMessage(ITalker talker, string msg);

        Task SubscribeForMessages(IChatObserver subscriber);
        Task UnsubscribeFromMessages(IChatObserver subscriber);
    }
}
