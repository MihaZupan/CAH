/*
    Copyright (c) Miha Zupan. All rights reserved.
    This file is a part of the CAH project by Miha Zupan and Juš Mirtič.
    It is licensed under the Simplified BSD License (BSD 2-clause).
    For more information visit:
    https://github.com/MihaZupan/CAH/blob/master/LICENSE
*/
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CAH.Server.Requests.Lobby
{
    class CreateGameRoomRequest : RequestBase
    {
        [JsonProperty(Required = Required.Always)]
        public string Name;

        [JsonProperty(Required = Required.Always)]
        public int MaxPlayers;

        [JsonProperty(Required = Required.Always)]
        public List<int> SelectedDecks;

        public string Password;
    }
}
