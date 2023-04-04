using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace pacman2
{
    class Program
    {
        //ma to nejake ty brouky (haha) ale myslenka tam je

        //kdyz se potkaji 2 duchove tak se zacnou navzajem printit a vznikne lajna klonu duchu. Moje supr cupr reseni by bylo
        //at si kazdy policko pamatuje hodnotu, ale na to abych to implementovala by nemela byt pulnoc v utery vecer zejo...

        //taky je neskutecne tezky ty duchy chytit, ale pevne doufam, ze kdyz se dotknou, tak hra skonci

        //jsou inteligentni tak, ze chodi alespon trochu smerem dopredu, ale pacman jim je ukradeny, tak mam duchy stoiky.

        static void Main(string[] args)
        {
            Game game = new Game(28,31);
            Ghost ghost = new Ghost(13, 15, game.gameMap);
            Ghost blinky = new Ghost(10, 12, game.gameMap);
            game.gameMap.Duchove.Add(ghost);
            game.gameMap.Duchove.Add(blinky);

            Console.WriteLine(game.PrintMap());

            async Task RunInBackground(TimeSpan timeSpan, Action action)
            {
                var periodicTimer = new PeriodicTimer(timeSpan);
                while (await periodicTimer.WaitForNextTickAsync())
                {
                    action();
                }
            }

            game.Play();
        }
    }
}
