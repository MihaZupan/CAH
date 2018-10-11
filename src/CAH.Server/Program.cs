using System;
using System.IO;
using System.Threading;
using WebSocketSharp.Server;

namespace CAH.Server
{
    class Program
    {
        public static readonly WebSocketServer Server = new WebSocketServer(6500, false);

        static void Main(string[] args)
        {
            //Server.AddWebSocketService<>();
            //Server.Start();
            foreach (var deck in JsonAgainstHumanity.DeckParser.Parse(File.ReadAllText("raw.json")))
            {
                Console.WriteLine("{0} B: {1} \tW: {2} ",
                    (deck.Name + ':').PadRight(50, ' '),
                    deck.BlackCards.Length,
                    deck.WhiteCards.Length);
            }
            while (true) Thread.Sleep(10000);
        }
    }
}
