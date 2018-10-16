/*
    Copyright (c) Miha Zupan. All rights reserved.
    This file is a part of the CAH project by Miha Zupan and Juš Mirtič.
    It is licensed under the Simplified BSD License (BSD 2-clause).
    For more information visit:
    https://github.com/MihaZupan/CAH/blob/master/LICENSE
*/
namespace CAH.Server.ServerUpdates
{
    class GameRoomClosedUpdate : IServerUpdate
    {
        public string Method => Methods.Lobby_GameRoomClosed;

        public int RoomId;

        public GameRoomClosedUpdate(int id)
        {
            RoomId = id;
        }
    }
}
