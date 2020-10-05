using System;
using System.Threading.Tasks;

namespace TextAdventureJimmySkinnari
{
    public class LastRoom : Room
    {
        


        public LastRoom(string name, string description) : base(name, description)
        {


        }

        public static async Task DoSomethingEveryTenSeconds()
        {
            int time = 30;
            while (true)
            {
                var delayTask = Task.Delay(1000);
                Console.WriteLine("Time before you die: " + time);
                time--;
                await delayTask; // wait until at least 10s elapsed since delayTask created

            }
        }
    }
}
