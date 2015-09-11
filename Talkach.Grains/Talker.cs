using System.Threading.Tasks;
using Orleans;
using Talkach.Client;

namespace Talkach.Grains
{
    public class Talker : Grain, ITalker
    {
        public Task<string> GetName()
        {
            throw new System.NotImplementedException();
        }
    }
}