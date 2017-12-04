using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MegaChallengeWar
{
    public class War
    {
        public static void DealWar(Deck deck, Player play1, Player play2, Label actionLabel)
        {
            //deals the deck to the two players, displays cards as they are dealt
            Random random = new Random();
            actionLabel.Text += "<h2>Dealing Cards</h2><br />";
            while (deck.Cards.Count > 0)
            {
                play1.Hand.Cards.Enqueue(deck.Cards.Dequeue());
                actionLabel.Text += string.Format("{0} is dealt the {1} of {2}<br />", 
                    play1.Name, 
                    play1.Hand.Cards.ElementAt(play1.Hand.Cards.Count - 1).Value, 
                    play1.Hand.Cards.ElementAt(play1.Hand.Cards.Count - 1).Suit);
                play2.Hand.Cards.Enqueue(deck.Cards.Dequeue());
                actionLabel.Text += string.Format("{0} is dealt the {1} of {2}<br />",
                    play2.Name,
                    play2.Hand.Cards.ElementAt(play2.Hand.Cards.Count - 1).Value,
                    play2.Hand.Cards.ElementAt(play2.Hand.Cards.Count - 1).Suit);
            }
            actionLabel.Text += "<br />";
        }

        public static void PlayGameOfWar(Player play1, Player play2, Label actionLabel)
        {
            //plays 20 rounds of war
            for (int r = 1; r < 21; r++)
            {
                reportRound(r, actionLabel);
                if (playerHasLost(play1, play2, 1))
                {
                    reportloss(play1, play2, actionLabel);
                    break;
                }
                List<Card> bounty = new List<Card> {
                    play1.Hand.Cards.Dequeue(),
                    play2.Hand.Cards.Dequeue()
                };
                compareCards(bounty, play1, play2, actionLabel);
            }
            endGame(play1, play2, actionLabel);
        }

        private static bool playerHasLost(Player play1, Player play2, int reqCards)
        {
            //checks to make sure both players have enough cards to play the round
            if (play1.Hand.Cards.Count < reqCards || play2.Hand.Cards.Count < reqCards) return true;
            return false;
        }

        private static void compareCards (List<Card> bounty, Player play1, Player play2, Label actionLabel)
        {
            //finds the winner of the round and calls the awardWinner method, 
            //unless it's a tie, then calls the goToWar method
            reportBattleCards(bounty, actionLabel);
            Card card1 = bounty.ElementAt(bounty.Count-2);
            Card card2 = bounty.ElementAt(bounty.Count-1);
            if (card1.IntValue > card2.IntValue) awardWinner(bounty, play1, actionLabel);
            else if (card2.IntValue > card1.IntValue) awardWinner(bounty, play2, actionLabel);
            else goToWar(bounty, play1, play2, actionLabel);
        }

        private static void goToWar(List<Card> bounty, Player play1, Player play2, Label actionLabel)
        {
            //adds six cards to the bounty, reports that we're at war, then calls compareCards method
            if (playerHasLost(play1, play2, 3)) lossAtWar(play1, play2, bounty, actionLabel);
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    bounty.Add(play1.Hand.Cards.Dequeue());
                    bounty.Add(play2.Hand.Cards.Dequeue());
                }
                reportWar(actionLabel, bounty.Count);
                compareCards(bounty, play1, play2, actionLabel);
            }

        }

        private static void lossAtWar(Player play1, Player play2, List<Card> bounty, Label actionLabel)
        {
            //if player does not have enough cards for war, forfeits round to other player
            Player winner = (play1.Hand.Cards.Count > play2.Hand.Cards.Count) ? play1 : play2;
            Player loser = (play1.Hand.Cards.Count > play2.Hand.Cards.Count) ? play2 : play1;
            while (bounty.Count > 0)
            {
                winner.Hand.Cards.Enqueue(bounty.ElementAt(0));
                bounty.RemoveAt(0);
            }
            reportForfeirWar(loser, actionLabel, bounty);
        }

        private static void awardWinner(List<Card> bounty, Player winner, Label actionLabel)
        {
            //awards the winner of the round the bounty
            
            /* While testing, I found something interesting: player 1 won slightly more often.
             * After racking my brain, I realized that if the bounty is enqueued in the 
             * order in which it appears in the bounty list, player 2 is at a slight disadvantage
             * because the winning card is enqueued second, and thus is slightly less likely to see
             * play again than the winning card. To level the playing field, both players must
             * enqueue the cards in a neutral order (here they are enqueued by strength) */
            
            reportWinner(winner, bounty, actionLabel);

            while (bounty.Count > 0)
            {
                int highcard = 0;
                for (int i = 1; i < bounty.Count; i++)
                {
                    highcard = (bounty.ElementAt(highcard).IntValue > bounty.ElementAt(i).IntValue) ? highcard : i;
                }
                winner.Hand.Cards.Enqueue(bounty.ElementAt(highcard));
                bounty.RemoveAt(highcard);
            }
        }

        private static void reportForfeirWar(Player loser, Label actionLabel, List<Card> bounty)
        {
            //reports that a player does not have enough cards and thus forfeits war
            actionLabel.Text += "Bounty ...<br />";
            for (int i = 0; i < bounty.Count; i++)
            {
                actionLabel.Text += string.Format("&nbsp;&nbsp;{0} of {1}<br />", bounty.ElementAt(i).Value, bounty.ElementAt(i).Suit);
            }
            actionLabel.Text += string.Format("{0} does not have enough cards for war, and thus forfeits the round.<br /><br />", loser.Name);
        }

        private static void reportWinner(Player winner, List<Card> bounty, Label actionLabel)
        {
            //shows bounty, declares winner
            {
                actionLabel.Text += "Bounty ...<br />";
                for (int i = 0; i < bounty.Count; i++)
                {
                    actionLabel.Text += string.Format("&nbsp;&nbsp;{0} of {1}<br />", bounty.ElementAt(i).Value, bounty.ElementAt(i).Suit);
                }
                actionLabel.Text += string.Format("<b>{0}</b> wins!<br /> <br />", winner.Name);
            }
        }

        private static void reportBattleCards(List<Card> bounty, Label actionLabel)
        {
            //displays battle cards
            actionLabel.Text += string.Format(
                "Battle Cards: {0} of {1} vs {2} of {3}<br />",
                bounty.ElementAt(bounty.Count - 2).Value,
                bounty.ElementAt(bounty.Count - 2).Suit,
                bounty.ElementAt(bounty.Count - 1).Value,
                bounty.ElementAt(bounty.Count - 1).Suit);
        }

        private static void reportWar(Label actionLabel, int defcon)
        {
            //Formal Declaration of War
            if (defcon == 8) actionLabel.Text += "**********WAR**********<br /><br />";
            if (defcon == 14) actionLabel.Text += "*******WORLD WAR*******<br /><br />";
            if (defcon >= 20) actionLabel.Text += "***THERMONUCLEAR WAR***<br /><br />";
        }

        private static void reportRound(int round, Label actionLabel)
        {
            //print round number
            actionLabel.Text += string.Format("<b>Round {0}</b><br />", round);
        }

        private static void reportloss(Player play1, Player play2, Label actionLabel)
        {
            //prints which player is out of cards
            Player loser = (play1.Hand.Cards.Count > play2.Hand.Cards.Count) ? play2 : play1;
            actionLabel.Text += string.Format("{0} is out of cards.<br /><br />", loser.Name);
        }

        private static void endGame(Player play1, Player play2, Label actionLabel)
        {
            //declares winner of the game and prints each players card total
            if (play1.Hand.Cards.Count > play2.Hand.Cards.Count)
                actionLabel.Text += string.Format("<b style=\"color: blue;\">{0} WINS!</b><br />", play1.Name);
            else if(play2.Hand.Cards.Count > play1.Hand.Cards.Count)
                actionLabel.Text += string.Format("<b style=\"color: red;\">{0} WINS!</b><br />", play2.Name);
            else
                actionLabel.Text += "TIE GAME!<br />";

            actionLabel.Text += string.Format("<b style=\"color: blue;\">{0}: {1}</b><br />", play1.Name, play1.Hand.Cards.Count);
            actionLabel.Text += string.Format("<b style=\"color: red;\">{0}: {1}</b><br />", play2.Name, play2.Hand.Cards.Count);
        }

    }
}