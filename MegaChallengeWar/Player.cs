using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaChallengeWar
{
    public class Player
    {
        public Deck Hand { get; set; }
        public string Name { get; set; }

        public Player(string name)
        {
            this.Hand = new Deck(0);
            this.Name = name;
        }
    }
}