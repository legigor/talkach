using System;
using System.Threading.Tasks;
using Orleans;
using Talkach.Client;

namespace Talkach.Grains
{
    public class Chat : Grain, IChat
    {
        public Task Register(ITalker talker)
        {
            throw new System.NotImplementedException();
        }
    }
}