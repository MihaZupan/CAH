/*
    Copyright (c) Miha Zupan. All rights reserved.
    This file is a part of the CAH project by Miha Zupan and Juš Mirtič.
    It is licensed under the Simplified BSD License (BSD 2-clause).
    For more information visit:
    https://github.com/MihaZupan/CAH/blob/master/LICENSE
*/
namespace CAH.Server.Responses
{
    class ErrorResponse : IResponse
    {
        public string Method => Methods.Error;
        public string Message;

        public ErrorResponse(string message)
        {
            Message = message;
        }
    }
}
