/*
    Copyright (c) Miha Zupan. All rights reserved.
    This file is a part of the CAH project by Miha Zupan and Juš Mirtič.
    It is licensed under the Simplified BSD License (BSD 2-clause).
    For more information visit:
    https://github.com/MihaZupan/CAH/blob/master/LICENSE
*/
using CAH.Server.Requests.Lobby;
using CAH.Server.Responses;
using CAH.Server.Responses.Lobby;
using CAH.Server.ServerUpdates;
using CAH.Server.Types;
using System.Collections.Generic;

namespace CAH.Server
{
    class Lobby
    {
        public readonly Deck[] Decks;
        public readonly string[] DeckNames;
        public List<GameRoom> GameRooms;
        public List<LobbyGameRoomInfo> LobbyGameRoomInfos;
        public LinkedList<Session> SessionsInLobby;
        public int LastGameRoomId = 0;

        public Lobby(Deck[] decks)
        {
            Decks = decks;

            DeckNames = new string[Decks.Length];
            for (int i = 0; i < DeckNames.Length; i++)
                DeckNames[i] = Decks[i].Name;
        }

        public IResponse API_Login(Session session, LoginRequest request)
        {
            session.LoggedIn = true;
            session.Username = request.Username;
            session.InLobby = true;
            SessionsInLobby.AddLast(session);
            return new LoginResponse(DeckNames, LobbyGameRoomInfos);
        }
        public IResponse API_CreateGameRoom(Session session, CreateGameRoomRequest request)
        {
            if (!TryGetDecks(request.SelectedDecks, out Deck[] decks))
                return "Invalid deck selection".AsError();

            GameRoom newRoom = new GameRoom(++LastGameRoomId, session, request.Name, request.MaxPlayers, decks, request.Password);
            session.InGameRoom = true;
            session.InLobby = false;
            session.GameRoom = newRoom;
            GameRooms.Add(newRoom);

            return new CreateGameRoomResponse(newRoom.Id);
        }
        public IResponse API_JoinGameRoom(Session session, JoinGameRoomRequest request)
        {
            var gameRoom = GameRooms.Find(gr => gr.Id == request.RoomId);
            if (gameRoom != null)
            {
                if (!gameRoom.IsFull)
                {
                    if (gameRoom.PasswordRequired && (request.Password == null))
                    {
                        return new JoinGameRoomResponse("Game room is protected by a password");
                    }
                    else if (gameRoom.PasswordRequired && gameRoom.Password.OrdinalEquals(request.Password))
                    {
                        return gameRoom.OnJoin(session);
                    }
                    else return new JoinGameRoomResponse("Wrong password");
                }
                else return new JoinGameRoomResponse("Game room is full");
            }
            else return "Game room does not exist on the server".AsError();
        }

        private void Broadcast_GameRoomUpdated(LobbyGameRoomInfo gameRoom)
            => Broadcast(new GameRoomUpdatedUpdate(gameRoom));
        private void Broadcast_GameRoomClosed(int gameRoomId)
            => Broadcast(new GameRoomClosedUpdate(gameRoomId));
        private void Broadcast(IServerUpdate update)
        {
            var message = update.ToJson().Utf8();
            foreach (var session in SessionsInLobby)
                session.Send(message);
        }

        public void OnClose(Session session)
        {
            if (session.InLobby)
            {
                SessionsInLobby.Remove(session);
                session.InLobby = false;
            }
        }
        public void OnRoomClosed(GameRoom gameRoom)
        {
            foreach (var player in gameRoom.Players)
            {
                player.InGameRoom = false;
                player.GameRoom = null;
                player.InLobby = true;
                SessionsInLobby.AddLast(player);
            }
            GameRooms.Remove(gameRoom);
            Broadcast_GameRoomClosed(gameRoom.Id);
        }

        private bool TryGetDecks(List<int> selectedDecks, out Deck[] decks)
        {
            if (selectedDecks.Count == Decks.Length)
            {
                decks = Decks; // Avoid re-allocating if all decks are selected
                return true;
            }
            else
            {
                decks = new Deck[selectedDecks.Count];
                for (int i = 0; i < decks.Length; i++)
                {
                    int index = selectedDecks[i];
                    if (index < 0 || index >= Decks.Length)
                    {
                        return false;
                    }
                    decks[i] = Decks[index];
                }
                return true;
            }
        }
    }
}
