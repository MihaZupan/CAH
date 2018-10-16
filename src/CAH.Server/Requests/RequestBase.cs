/*
    Copyright (c) Miha Zupan. All rights reserved.
    This file is a part of the CAH project by Miha Zupan and Juš Mirtič.
    It is licensed under the Simplified BSD License (BSD 2-clause).
    For more information visit:
    https://github.com/MihaZupan/CAH/blob/master/LICENSE
*/
using Newtonsoft.Json;

namespace CAH.Server.Requests
{
    abstract class RequestBase
    {
        [JsonProperty(Required = Required.Always)]
        public string Method;
    }
}
