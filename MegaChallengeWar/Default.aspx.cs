using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MegaChallengeWar
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void playButton_Click(object sender, EventArgs e)
        {
            //balance testing
            //int play1Wins = 0;
            //int play2Wins = 0;
            //for (int i = 0; i < 1000000; i++)
            //{
            Deck redHoyle = new Deck();
            redHoyle.ShuffleDeck();
            gamePlayLabel.Text = "";
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");
            War.DealWar(redHoyle, player1, player2, gamePlayLabel);
            War.PlayGameOfWar(player1, player2, gamePlayLabel);
            //    if (player1.Hand.Cards.Count > player2.Hand.Cards.Count) play1Wins++;
            //    if (player1.Hand.Cards.Count < player2.Hand.Cards.Count) play2Wins++;
            //}
            //gamePlayLabel.Text += string.Format("play1Wins: {0} play2Wins: {1}", play1Wins, play2Wins);
        }
    }
}