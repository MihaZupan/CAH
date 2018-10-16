/*
    Copyright (c) Miha Zupan. All rights reserved.
    This file is a part of the CAH project by Miha Zupan and Juš Mirtič.
    It is licensed under the Simplified BSD License (BSD 2-clause).
    For more information visit:
    https://github.com/MihaZupan/CAH/blob/master/LICENSE
*/
using CAH.Server.Requests.Lobby;
using CAH.Server.Responses;
using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace CAH.Server
{
    class ConnectionBehavior : WebSocketBehavior
    {
        private readonly object _lock = new object();
        protected void Lock() => Monitor.Enter(_lock);
        protected void Unlock() => Monitor.Exit(_lock);

        protected override void OnMessage(MessageEventArgs message)
        {
            if (!message.IsText || string.IsNullOrEmpty(message.Data))
            {
                goto invalidRequest;
            }

            JToken messageObject;
            string method;
            try
            {
                messageObject = JToken.Parse(message.Data);
                method = messageObject["Method"].ToObject<string>();
            }
            catch
            {
                goto invalidRequest;
            }

            switch (method)
            {
                case Methods.Lobby_Login:
                    var loginRequest = messageObject.ToObject<LoginRequest>();
                    if (loginRequest != null)
                    {
                        SendResponse(On(loginRequest));
                        return;
                    }
                    goto invalidRequest;

                case Methods.Lobby_JoinGameRoom:
                    var joinGameRoomRequest = messageObject.ToObject<JoinGameRoomRequest>();
                    if (joinGameRoomRequest != null)
                    {
                        SendResponse(On(joinGameRoomRequest));
                        return;
                    }
                    goto invalidRequest;

                case Methods.Lobby_CreateGameRoom:
                    var createGameRoomRequest = messageObject.ToObject<CreateGameRoomRequest>();
                    if (createGameRoomRequest != null)
                    {
                        SendResponse(On(createGameRoomRequest));
                        return;
                    }
                    goto invalidRequest;


                default:
                    SendError("Unknown method");
                    return;
            }

            invalidRequest:
            SendError("Invalid request");
        }

        public void SendResponse(IResponse response)
        {
            if (response != null)
                Send(response.ToJson());
        }
        public new void Send(byte[] message)
            => base.Send(message);
        public void SendError(string message)
            => SendResponse(new ErrorResponse(message));

        protected virtual IResponse On(LoginRequest request) => throw new NotImplementedException(nameof(LoginRequest));
        protected virtual IResponse On(JoinGameRoomRequest request) => throw new NotImplementedException(nameof(JoinGameRoomRequest));
        protected virtual IResponse On(CreateGameRoomRequest request) => throw new NotImplementedException(nameof(CreateGameRoomRequest));
    }
}
