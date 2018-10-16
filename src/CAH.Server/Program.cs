/*
    Copyright (c) Miha Zupan. All rights reserved.
    This file is a part of the CAH project by Miha Zupan and Juš Mirtič.
    It is licensed under the Simplified BSD License (BSD 2-clause).
    For more information visit:
    https://github.com/MihaZupan/CAH/blob/master/LICENSE
*/
using CAH.Server.Types;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using WebSocketSharp.Server;

namespace CAH.Server
{
    class Program
    {
        public static readonly WebSocketServer Server = new WebSocketServer(6500, false);
        public static Lobby Lobby;

        static void Main(string[] args)
        {
            var decks = JsonAgainstHumanity.DeckParser.Parse(File.ReadAllText("raw.json"));
            DumpDecks(decks);
            Lobby = new Lobby(decks);

            Server.AddWebSocketService("/", () => new Session(Lobby));
            Server.Start();
            while (true) Thread.Sleep(10000);
        }

        static void DumpDecks(Deck[] decks)
        {
            foreach (var deck in decks)
            {
                Console.WriteLine("{0} B: {1} \tW: {2} ",
                    (deck.Name + ':').PadRight(50, ' '),
                    deck.BlackCards.Length,
                    deck.WhiteCards.Length);
            }
            File.WriteAllText("decks.json", JsonConvert.SerializeObject(decks, Formatting.Indented));
        }
    }
}
