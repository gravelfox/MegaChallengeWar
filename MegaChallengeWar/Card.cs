using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaChallengeWar
{
    public class Card
    {

        public string Value { get; set; }
        public string Suit { get; set; }
        public int IntValue { get; set; }

        public Card(string value, string suit, int intval)
        {
            this.Value = value;
            this.Suit = suit;
            this.IntValue = intval;
        }
    }
}