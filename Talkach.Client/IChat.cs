using System.Threading.Tasks;
using Orleans;

namespace Talkach.Client
{
    public interface IChat : IGrainWithIntegerKey
    {
        Task Register(ITalker talker);
    }
}
