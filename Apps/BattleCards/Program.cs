using BattleCards;
using SUS.MvcFramework;
using System;
using System.Threading.Tasks;

namespace BattleCard
{
    public static class Program
    {
        public static async Task Main()
        {
            await Host.CreateHostAsync(new Startup());
        }
    }
}
