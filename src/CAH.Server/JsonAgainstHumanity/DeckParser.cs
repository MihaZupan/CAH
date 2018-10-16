/*
    Copyright (c) Miha Zupan. All rights reserved.
    This file is a part of the CAH project by Miha Zupan and Juš Mirtič.
    It is licensed under the Simplified BSD License (BSD 2-clause).
    For more information visit:
    https://github.com/MihaZupan/CAH/blob/master/LICENSE
*/
using CAH.Server.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace CAH.Server.JsonAgainstHumanity
{
    /// <summary>
    /// Parser for the format provided by https://crhallberg.com/cah/
    /// </summary>
    static class DeckParser
    {
        public static Deck[] Parse(string json)
        {
            JToken raw = JsonConvert.DeserializeObject(json) as JToken;
            JArray allBlackCards = raw["blackCards"] as JArray;
            string[] allWhiteCards = raw["whiteCards"].ToObject<string[]>();
            string[] order = raw["order"].ToObject<string[]>();

            Deck[] decks = new Deck[order.Length];
            for (int i = 0; i < order.Length; i++)
            {
                JToken jsonDeck = raw[order[i]];
                string deckName = jsonDeck["name"].ToObject<string>();
                int[] whiteCardIndexes = jsonDeck["white"].ToObject<int[]>();
                int[] blackCardIndexes = jsonDeck["black"].ToObject<int[]>();

                string[] whiteCards = new string[whiteCardIndexes.Length];
                if (whiteCardIndexes.Length != 0)
                {
                    int startIndex = whiteCardIndexes[0];
                    for (int j = 0; j < whiteCards.Length; j++)
                    {
                        whiteCards[j] = Format(allWhiteCards[startIndex + j]);
                    }
                }

                BlackCard[] blackCards = new BlackCard[blackCardIndexes.Length];
                if (blackCardIndexes.Length != 0)
                {
                    int startIndex = blackCardIndexes[0];
                    for (int j = 0; j < blackCards.Length; j++)
                    {
                        blackCards[j] = new BlackCard()
                        {
                            Text = Format(allBlackCards[startIndex + j]["text"].ToObject<string>()),
                            Pick = allBlackCards[startIndex + j]["pick"].ToObject<int>()
                        };
                    }
                }

                decks[i] = new Deck()
                {
                    Name = deckName,
                    WhiteCards = whiteCards,
                    BlackCards = blackCards
                };
            }

            return decks;
        }

        private static string Format(string text)
        {
            text = Regex.Replace(text, "<.*?>", " ");
            text = text.Replace('\r', ' ').Replace('\n', ' ').Replace('\t', ' ');
            text = text.RemoveDoubleSpaces();
            text = text.Trim();
            return text;
        }
    }
}
