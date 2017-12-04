using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaChallengeWar
{
    public class Deck
    {
        private Random random;
        public Queue<Card> Cards;

        public Deck()
        {
            this.Cards = new Queue<Card> { };
            this.random = new Random();
            List<string> values = new List<string>
                {
                    "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace"
                };

            List<string> suits = new List<string> {
                "Spades", "Diamonds", "Hearts", "Clubs"
            };

            for (int v = 0; v < values.Count; v++)
            {
                for (int s = 0; s < suits.Count; s++)
                {
                    Cards.Enqueue(new Card(values.ElementAt(v), suits.ElementAt(s), v));
                }
            }
        }

        public Deck (int emptyDeck)
        {
            this.Cards = new Queue<Card> { };
            this.random = new Random();
        }

        public void ShuffleDeck()
        {
            if (this.Cards.Count != 52) return;
            List<Card> shuffleDeck = new List<Card> { };

            while (Cards.Count > 0)
            {
                shuffleDeck.Add(Cards.Dequeue());
            }
            
            for (int i = 0; i < 52; i++)
            {
                int randomCard = this.random.Next(0, shuffleDeck.Count);
                this.Cards.Enqueue(shuffleDeck.ElementAt(randomCard));
                shuffleDeck.RemoveAt(randomCard);
            }
        }
    }
}