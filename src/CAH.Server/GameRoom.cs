/*
    Copyright (c) Miha Zupan. All rights reserved.
    This file is a part of the CAH project by Miha Zupan and Juš Mirtič.
    It is licensed under the Simplified BSD License (BSD 2-clause).
    For more information visit:
    https://github.com/MihaZupan/CAH/blob/master/LICENSE
*/
using CAH.Server.Responses;
using CAH.Server.Types;
using System.Collections.Generic;

namespace CAH.Server
{
    class GameRoom
    {
        public int Id;
        public string Name;
        public int MaxPlayers;
        public string Password;
        public List<Session> Players;
        public Session Host;
        public Deck[] AvailableDecks;

        public bool PasswordRequired => Password != null;
        public int CurrentPlayers => Players.Count;
        public bool IsFull => CurrentPlayers == MaxPlayers;

        public GameRoom(int id, Session host, string name, int maxPlayers, Deck[] decks, string password = null)
        {
            Id = id;
            Host = host;
            Name = name;
            MaxPlayers = maxPlayers;
            AvailableDecks = decks;
            Password = password;
        }

        public IResponse OnJoin(Session session)
        {
            Players.Add(session);
            session.InGameRoom = true;
            session.GameRoom = this;
            session.InLobby = false;
        }
    }
}
