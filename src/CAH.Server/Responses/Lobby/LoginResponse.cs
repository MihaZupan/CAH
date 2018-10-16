/*
    Copyright (c) Miha Zupan. All rights reserved.
    This file is a part of the CAH project by Miha Zupan and Juš Mirtič.
    It is licensed under the Simplified BSD License (BSD 2-clause).
    For more information visit:
    https://github.com/MihaZupan/CAH/blob/master/LICENSE
*/
using CAH.Server.Types;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CAH.Server.Responses.Lobby
{
    class LoginResponse : IResponse
    {
        public string Method => Methods.Lobby_Login;
        public bool Success;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FailureReason;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string[] AvailableDecks;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<LobbyGameRoomInfo> GameRooms;

        public LoginResponse(string failureReason)
        {
            Success = false;
            FailureReason = failureReason;
        }
        public LoginResponse(string[] deckNames, List<LobbyGameRoomInfo> gameRooms)
        {
            Success = true;
            AvailableDecks = deckNames;
            GameRooms = gameRooms;
        }
    }
}
