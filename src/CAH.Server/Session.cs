/*
    Copyright (c) Miha Zupan. All rights reserved.
    This file is a part of the CAH project by Miha Zupan and Juš Mirtič.
    It is licensed under the Simplified BSD License (BSD 2-clause).
    For more information visit:
    https://github.com/MihaZupan/CAH/blob/master/LICENSE
*/
using System;
using CAH.Server.Requests.Lobby;
using CAH.Server.Responses;
using WebSocketSharp;

namespace CAH.Server
{
    class Session : ConnectionBehavior
    {
        public readonly Lobby Lobby;
        public Session(Lobby lobby)
        {
            Lobby = lobby;
        }

        public bool Left = false;
        public bool InLobby = false;
        public bool LoggedIn = false;
        public bool InGameRoom = false;
        public string Username;
        public GameRoom GameRoom;

        protected override void OnClose(CloseEventArgs e) => Close();
        protected override void OnError(ErrorEventArgs e)
        {
            Console.WriteLine("Exception: " + e.ToString());
            Close();
        }
        private void Close()
        {
            if (Left) return;
            Left = true;
            if (InLobby)
            {
                Lobby.OnClose(this);
            }
            else
            {

            }
        }

        // Lobby
        protected override IResponse On(LoginRequest request)
        {
            if (Left) return null;
            if (LoggedIn)
            {
                return "Already logged in".AsError();
            }
            else
            {
                return Lobby.API_Login(this, request);
            }
        }
        protected override IResponse On(CreateGameRoomRequest request)
        {
            if (Left) return null;
            if (LoggedIn)
            {
                if (!InGameRoom)
                {
                    if (request.MaxPlayers > 1 && request.Name.Length > 0)
                    {
                        return Lobby.API_CreateGameRoom(this, request);
                    }
                    else return "Invalid request parameters".AsError();
                }
                else return "In a game room already".AsError();
            }
            else return "Not logged in".AsError();
        }
        protected override IResponse On(JoinGameRoomRequest request)
        {
            if (Left) return null;
            if (LoggedIn)
            {
                if (!InGameRoom)
                {
                    if (request.RoomId >= 0)
                    {
                        return Lobby.API_JoinGameRoom(this, request);
                    }
                    else return "Invalid room Id".AsError();
                }
                else return "In a game room already".AsError();
            }
            else return "Not logged in".AsError();
        }
    }
}
