/*
    Copyright (c) Miha Zupan. All rights reserved.
    This file is a part of the CAH project by Miha Zupan and Juš Mirtič.
    It is licensed under the Simplified BSD License (BSD 2-clause).
    For more information visit:
    https://github.com/MihaZupan/CAH/blob/master/LICENSE
*/
namespace CAH.Server
{
    static class Methods
    {
        public const string Error = "error";

        public const string Lobby_Login = "lobby/login";
        public const string Lobby_JoinGameRoom = "lobby/joinGameRoom";
        public const string Lobby_CreateGameRoom = "lobby/createGameRoom";

        // Server updates
        public const string Lobby_GameRoomUpdated = "lobby/gameRoomUpdated";
        public const string Lobby_GameRoomClosed = "lobby/gameRoomClosed";


    }
}
