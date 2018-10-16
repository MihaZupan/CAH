/*
    Copyright (c) Miha Zupan. All rights reserved.
    This file is a part of the CAH project by Miha Zupan and Juš Mirtič.
    It is licensed under the Simplified BSD License (BSD 2-clause).
    For more information visit:
    https://github.com/MihaZupan/CAH/blob/master/LICENSE
*/
using CAH.Server.Types;

namespace CAH.Server.ServerUpdates
{
    class GameRoomUpdatedUpdate : IServerUpdate
    {
        public string Method => Methods.Lobby_GameRoomUpdated;
        public LobbyGameRoomInfo Info;

        public GameRoomUpdatedUpdate(LobbyGameRoomInfo gameRoomInfo)
        {
            Info = gameRoomInfo;
        }
    }
}
