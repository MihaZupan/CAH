/*
    Copyright (c) Miha Zupan. All rights reserved.
    This file is a part of the CAH project by Miha Zupan and Juš Mirtič.
    It is licensed under the Simplified BSD License (BSD 2-clause).
    For more information visit:
    https://github.com/MihaZupan/CAH/blob/master/LICENSE
*/
using CAH.Server.Responses;
using Newtonsoft.Json;
using System;
using System.Text;

namespace CAH.Server
{
    public static class StringExtensions
    {
        public static string RemoveDoubleSpaces(this string source)
        {
            int length;
            do
            {
                length = source.Length;
                source = source.Replace("  ", " ");
            }
            while (length != source.Length);
            return source;
        }

        public static byte[] Utf8(this string source)
        {
            return Encoding.UTF8.GetBytes(source);
        }

        public static bool IsNullOrEmpty(this string source)
            => string.IsNullOrEmpty(source);

        internal static ErrorResponse AsError(this string message)
            => new ErrorResponse(message);

        public static bool OrdinalEquals(this string source, string other)
            => source.Equals(other, StringComparison.Ordinal);
    }
    public static class JsonHelpers
    {
        public static string ToJson(this object obj)
            => JsonConvert.SerializeObject(obj);

        public static bool TryDeserializeObject<T>(this string json, out T value)
        {
            try
            {
                value = JsonConvert.DeserializeObject<T>(json);
                return true;
            }
            catch
            {
                value = default;
                return false;
            }
        }
    }
}
