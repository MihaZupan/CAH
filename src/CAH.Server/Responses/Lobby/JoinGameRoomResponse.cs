/*
    Copyright (c) Miha Zupan. All rights reserved.
    This file is a part of the CAH project by Miha Zupan and Juš Mirtič.
    It is licensed under the Simplified BSD License (BSD 2-clause).
    For more information visit:
    https://github.com/MihaZupan/CAH/blob/master/LICENSE
*/
using CAH.Server.Types;
using Newtonsoft.Json;

namespace CAH.Server.Responses.Lobby
{
    class JoinGameRoomResponse : IResponse
    {
        public string Method => Methods.Lobby_JoinGameRoom;
        public bool Success;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FailureReason;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public PrivateGameRoomInfo RoomInfo;

        public JoinGameRoomResponse(string failureReason)
        {
            Success = false;
            FailureReason = failureReason;
        }
    }
}
