using Orleans;

namespace Talkach.Client
{
    public interface IChatObserver : IGrainObserver
    {
        void AppendMessage(string name, string msg);
    }
}