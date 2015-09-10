using System.Threading.Tasks;
using Talkach.Client;

namespace Talkach.Grains
{
    public class Talker : ITalker
    {
        public Task<string> GetName()
        {
            throw new System.NotImplementedException();
        }
    }
}