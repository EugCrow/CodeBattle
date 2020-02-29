using System;
using SnakeBattle.Api;

namespace Client
{
    class Program
    {
        //const string SERVER_ADDRESS = "http://codingdojo2020.westeurope.cloudapp.azure.com/codenjoy-contest/board/player/j7koydkba973ovsryolc?code=5169101146561309444";
        const string SERVER_ADDRESS = "http://192.168.1.250:8080/codenjoy-contest/board/player/xcz570c3b2uvpglcvgry?code=2139264015254690273&gameName=snakebattle";

        static void Main(string[] args)
        {
            var client = new SnakeBattleClient(SERVER_ADDRESS);
            client.Run(new Decider());

            Console.ReadKey();
            client.InitiateExit();
        }

    }
}