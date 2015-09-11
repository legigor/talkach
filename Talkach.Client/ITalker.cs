using System.Threading.Tasks;
using Orleans;

namespace Talkach.Client
{
    public interface ITalker : IGrainWithStringKey
    {
        Task JoinChat(IChat chat);
        Task StartTheGame();
    }
}