/*
 * Phillip Lyon
 * CSCI-294
 * Final Project - Hearts
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

//disable start new game button until the game is over
//allow the dealing of a new hand when cards remaining == 0
//Cheat = showAllCards
namespace FinalProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool heartsIsBroken = false;
        bool notFound = true;
        Card[] DeckOfCards = new Card[52];
        Card[] playerCards = new Card[13];
        Card[] sharkCards = new Card[13];
        Card[] tigerCards = new Card[13];
        Card[] hawkCards = new Card[13];
        Card[][] game = new Card[4][];
        Card[] trick = new Card[4];
        int totalPlayerScore = 0;
        int totalSharkScore = 0;
        int totalTigerScore = 0;
        int totalHawkScore = 0;
        int currentRoundPlayerScore = 0;
        int currentRoundSharkScore = 0;
        int currentRoundTigerScore = 0;
        int currentRoundHawkScore = 0;
        int cardsRemaining = 13;
        int leader = -1;
        String leadingSuit = "";


        private void reset()
        {
            currentRoundPlayerScore = 0;
            currentRoundSharkScore = 0;
            currentRoundTigerScore = 0;
            currentRoundHawkScore = 0;
            heartsIsBroken = false;
            leadingSuit = "";
            notFound = true;
            cardsRemaining = 13;
            leader = -1;
            leadingSuit = "Clubs";
            DeckOfCards = new Card[52];
            playerCards = new Card[13];
            sharkCards = new Card[13];
            tigerCards = new Card[13];
            hawkCards = new Card[13];
            trick = new Card[4];
            leadingSuitTextBox.Text = "Click the continue button.";

            playerCard0.Visible = true;
            playerCard1.Visible = true;
            playerCard2.Visible = true;
            playerCard3.Visible = true;
            playerCard4.Visible = true;
            playerCard5.Visible = true;
            playerCard6.Visible = true;
            playerCard7.Visible = true;
            playerCard8.Visible = true;
            playerCard9.Visible = true;
            playerCard10.Visible = true;
            playerCard11.Visible = true;
            playerCard12.Visible = true;

            sharkCard1.Visible = true;
            sharkCard2.Visible = true;
            sharkCard3.Visible = true;
            sharkCard4.Visible = true;
            sharkCard5.Visible = true;
            sharkCard6.Visible = true;
            sharkCard7.Visible = true;
            sharkCard8.Visible = true;
            sharkCard9.Visible = true;
            sharkCard10.Visible = true;
            sharkCard11.Visible = true;
            sharkCard12.Visible = true;
            sharkCard13.Visible = true;

            tigerCard1.Visible = true;
            tigerCard2.Visible = true;
            tigerCard3.Visible = true;
            tigerCard4.Visible = true;
            tigerCard5.Visible = true;
            tigerCard6.Visible = true;
            tigerCard7.Visible = true;
            tigerCard8.Visible = true;
            tigerCard9.Visible = true;
            tigerCard10.Visible = true;
            tigerCard11.Visible = true;
            tigerCard12.Visible = true;
            tigerCard13.Visible = true;

            hawkCard1.Visible = true;
            hawkCard2.Visible = true;
            hawkCard3.Visible = true;
            hawkCard4.Visible = true;
            hawkCard5.Visible = true;
            hawkCard6.Visible = true;
            hawkCard7.Visible = true;
            hawkCard8.Visible = true;
            hawkCard9.Visible = true;
            hawkCard10.Visible = true;
            hawkCard11.Visible = true;
            hawkCard12.Visible = true;
            hawkCard13.Visible = true;

            playerPlayedCard.Image = null;
            sharkPlayedCard.Image = null;
            tigerPlayedCard.Image = null;
            hawkPlayedCard.Image = null;

        }
        private void determineLeader(Card[] s)
        {
            int maxValue = -1;
            for (int x = 0; x < 4; x++)
            {
                if (s[x] != null && maxValue < s[x].value && String.Equals(s[x].suit, leadingSuit))
                {
                    maxValue = s[x].value;
                    leader = x;
                }
            }
        }

        public void insertionSort(Card[] arr)
        {
            int j;
            for (int i = 1; i < arr.Length; i++)
            {

                j = i;
                while (j > 0 && arr[j - 1].value > arr[j].value)
                {
                    swap(arr, j - 1, j);
                    j--;
                }
            }
        }

        public void insertionSortBySuit(Card[] arr)
        {
            int j;
            for (int i = 1; i < arr.Length; i++)
            {

                j = i;
                while (j > 0 && arr[j - 1].suitValue > arr[j].suitValue)
                {
                    swap(arr, j - 1, j);
                    j--;
                }
            }
        }
        public void sortPlayerCards()
        {
            insertionSort(playerCards);
            insertionSortBySuit(playerCards);
        }
        public void sortAllCards()
        {
            
            insertionSort(sharkCards);
            insertionSort(tigerCards);
            insertionSort(hawkCards);
            
            insertionSortBySuit(sharkCards);
            insertionSortBySuit(tigerCards);
            insertionSortBySuit(hawkCards);
            
        }

        public void swap(Card[] arr, int i, int j)
        {
            Card temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        private void dealButton_Click(object sender, EventArgs e)
        {
            reset();
            leadingSuitTextBox.Text = "Click The continue button.";
            continueButton.Enabled = true;
            continueButton.BackColor = Color.Green;
            int i = 0;

            //CREATES THE DECK

            for (int val = 2; val <= 14; val++)
            {
                Card toAddClubs = new Card(val, "Clubs");
                Card toAddHearts = new Card(val, "Hearts");
                Card toAddSpades = new Card(val, "Spades");
                Card toAddDiamonds = new Card(val, "Diamonds");

                DeckOfCards[i] = toAddClubs;
                i++;
                DeckOfCards[i] = toAddHearts;
                i++;
                DeckOfCards[i] = toAddSpades;
                i++;
                DeckOfCards[i] = toAddDiamonds;
                i++;

            }
            //SHUFFLES THE DECK

            Random rnd = new Random();
            for (int cardIndex = 0; cardIndex < 52; cardIndex++)
            {
                int randomIndex = rnd.Next(0, 52);
                Card temp = DeckOfCards[randomIndex];
                DeckOfCards[randomIndex] = DeckOfCards[cardIndex];
                DeckOfCards[cardIndex] = temp;
            }

            
            // UNCOMMENT TO SEE THE ENTIRE DECK

            // for (int y = 0; y < 52; y++)
            //      {
            //          System.Diagnostics.Debug.WriteLine(DeckOfCards[y].tS());
            //      }

            //ASSIGNS CARDS TO PLAYER'S CARD[] FOR THE GAME


            for (int assignCards = 0; assignCards < 52; assignCards++)
            {
                if (assignCards < 13)
                {
                    playerCards[assignCards] = DeckOfCards[assignCards];
                    
                }
                else if (assignCards >= 13 && assignCards < 26)
                {
                    sharkCards[assignCards - 13] = DeckOfCards[assignCards];
                }
                else if (assignCards >= 26 && assignCards < 39)
                {
                    tigerCards[assignCards - 26] = DeckOfCards[assignCards];
                }
                else if (assignCards >= 39)
                {
                    hawkCards[assignCards - 39] = DeckOfCards[assignCards];
                }
            }
            sortPlayerCards();


            // UNCOMMENT TO SEE all player's cards in DEBUG -> WINDOWS -> OUTPUT
            /*
            for (int y = 0; y < 13; y++)
                  {
                      System.Diagnostics.Debug.WriteLine(playerCards[y].tS());
                  }

            System.Diagnostics.Debug.WriteLine("");

            for (int y = 0; y < 13; y++)
            {
                System.Diagnostics.Debug.WriteLine(sharkCards[y].tS());
            }
            System.Diagnostics.Debug.WriteLine("");

            for (int y = 0; y < 13; y++)
            {
                System.Diagnostics.Debug.WriteLine(tigerCards[y].tS());
            }
            System.Diagnostics.Debug.WriteLine("");

            for (int y = 0; y < 13; y++)
            {
                System.Diagnostics.Debug.WriteLine(hawkCards[y].tS());
            }
           */

            
            //Makes player cards appear in picture boxes

            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string resourceName = asm.GetName().Name + ".Properties.Resources";
            var rm = new System.Resources.ResourceManager(resourceName, asm);

            playerCard0.Image = (Bitmap)rm.GetObject(playerCards[0].tS());
            playerCard1.Image = (Bitmap)rm.GetObject(playerCards[1].tS());
            playerCard2.Image = (Bitmap)rm.GetObject(playerCards[2].tS());
            playerCard3.Image = (Bitmap)rm.GetObject(playerCards[3].tS());
            playerCard4.Image = (Bitmap)rm.GetObject(playerCards[4].tS());
            playerCard5.Image = (Bitmap)rm.GetObject(playerCards[5].tS());
            playerCard6.Image = (Bitmap)rm.GetObject(playerCards[6].tS());
            playerCard7.Image = (Bitmap)rm.GetObject(playerCards[7].tS());
            playerCard8.Image = (Bitmap)rm.GetObject(playerCards[8].tS());
            playerCard9.Image = (Bitmap)rm.GetObject(playerCards[9].tS());
            playerCard10.Image = (Bitmap)rm.GetObject(playerCards[10].tS());
            playerCard11.Image = (Bitmap)rm.GetObject(playerCards[11].tS());
            playerCard12.Image = (Bitmap)rm.GetObject(playerCards[12].tS());

            game[0] = playerCards;
            game[1] = sharkCards;
            game[2] = tigerCards;
            game[3] = hawkCards;
            dealButton.Enabled = false;
        }

        //Sets card played to the correct center picture
        private void idToPicture(int p, Card y)
        {
            /*
            playerId = 0;
            sharkId = 1;
            tigerId = 2;
            hawkId = 3;
            */

            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string resourceName = asm.GetName().Name + ".Properties.Resources";
            var rm = new System.Resources.ResourceManager(resourceName, asm);

            if (p == 0)
            {

                playerPlayedCard.Image = (Bitmap)rm.GetObject(y.tS());
            }
            if (p == 1)
            {
                sharkPlayedCard.Image = (Bitmap)rm.GetObject(y.tS());
            }
            if (p == 2)
            {
                tigerPlayedCard.Image = (Bitmap)rm.GetObject(y.tS());
            }
            if (p == 3)
            {
                hawkPlayedCard.Image = (Bitmap)rm.GetObject(y.tS());
            }
        }
        private void showScoreButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Player Current Round Score = " + currentRoundPlayerScore + "\n" +
                "Shark Current Round Score = " + currentRoundSharkScore + "\n" +
                "Tiger Current Round Score = " + currentRoundTigerScore + "\n" +
                "Hawk Current Round Score = " + currentRoundHawkScore + "\n\n" +
                "Player Total Score = " + totalPlayerScore + "\n" +
                "Shark Total Score = " + totalSharkScore + "\n" +
                "Tiger Total Score = " + totalTigerScore + "\n" +
                "Hawk Total Score = " + totalHawkScore);
        }
        private void scoreTrick()
        {
            playerPlayedCard.Image = null;
            sharkPlayedCard.Image = null;
            tigerPlayedCard.Image = null;
            hawkPlayedCard.Image = null;

            int maxValueOfLeadingSuit = -1;
            int trickWinner = -1;
            int score = 0;

           // System.Diagnostics.Debug.WriteLine("Leading Suit = " + leadingSuit);
            for (int i = 0; i < 4; i++)
            {
                if (trick[i].suit == leadingSuit && trick[i].value > maxValueOfLeadingSuit)
                {
                    maxValueOfLeadingSuit = trick[i].value;
                    trickWinner = i;
                }
                if (trick[i].suit == "Hearts")
                {
                    score++;
                }
                if (trick[i].suit == "Spades" && trick[i].value == 12)
                {
                    score += 13;
                }
            }
            if(score > 0 && score != 13)
            {
                heartsIsBroken = true;
            }
            if (trickWinner == 0)
            {
                totalPlayerScore += score;
                currentRoundPlayerScore += score;
            }
            else if (trickWinner == 1)
            {
                totalSharkScore += score;
                currentRoundSharkScore += score;
            }
            else if (trickWinner == 2)
            {
                totalTigerScore += score;
                currentRoundTigerScore += score;
            }
            else if (trickWinner == 3)
            {
                totalHawkScore += score;
                currentRoundHawkScore += score;
            }

            //MessageBox.Show("Scores Updated");

            leadingSuitTextBox.Text = "Scores Updated";
            determineLeader(trick);
            leadingSuit = "";
            trick = new Card[4];
            bool outOfCards = true;
            for (int i = 0; i < 13; i++)
            {
                if (playerCards[i] != null)
                {
                    outOfCards = false;
                }
            }
            if (outOfCards)
            {
                if (currentRoundPlayerScore == 26)
                {
                    totalHawkScore += 26;
                    totalSharkScore += 26;
                    totalTigerScore += 26;
                    totalPlayerScore -= 26;
                }
                else if (currentRoundHawkScore == 26) // there is no code to make the computer try to go to get all 26 points so it is very unlikely that it will.
                {
                    totalHawkScore -= 26;
                    totalSharkScore += 26;
                    totalTigerScore += 26;
                    totalPlayerScore += 26;
                }
                else if(currentRoundSharkScore == 26)
                {
                    totalHawkScore += 26;
                    totalSharkScore -= 26;
                    totalTigerScore += 26;
                    totalPlayerScore += 26;
                }
                else if(currentRoundTigerScore == 26)
                {
                    totalHawkScore += 26;
                    totalSharkScore += 26;
                    totalTigerScore -= 26;
                    totalPlayerScore += 26;
                }
                continueButton.Enabled = false;
                continueButton.BackColor = Color.Red;
                dealButton.Enabled = true;
                if (totalPlayerScore > 100 || totalTigerScore > 100 || totalSharkScore > 100 || totalHawkScore > 100)
                {
                    leadingSuitTextBox.Text = "";
                    MessageBox.Show("Game Over");
                    continueButton.Enabled = false;
                    continueButton.BackColor = Color.Red;
                    dealButton.Enabled = false;
                }
            }
        }
        private bool hasLeadingSuit(Card[] j)
        {
            bool rVal = false;
            for (int i = 0; i < j.Length; i++)
            {
                if (j[i] != null && j[i].suit == leadingSuit)
                {
                    rVal = true;
                }
            }
            return rVal;
        }
        private void removeFromHand(int player, int index)
        {
            if (player == 0)
            {
                if (index == 0)
                {
                    playerCard0.Visible = false;
                }
                else if (index == 1)
                {
                    playerCard1.Visible = false;
                }
                else if (index == 2)
                {
                    playerCard2.Visible = false;
                }
                else if (index == 3)
                {
                    playerCard3.Visible = false;
                }
                else if (index == 4)
                {
                    playerCard4.Visible = false;
                }
                else if (index == 5)
                {
                    playerCard5.Visible = false;
                }
                else if (index == 6)
                {
                    playerCard6.Visible = false;
                }
                else if (index == 7)
                {
                    playerCard7.Visible = false;
                }
                else if (index == 8)
                {
                    playerCard8.Visible = false;
                }
                else if (index == 9)
                {
                    playerCard9.Visible = false;
                }
                else if (index == 10)
                {
                    playerCard10.Visible = false;
                }
                else if (index == 11)
                {
                    playerCard11.Visible = false;
                }
                else if (index == 12)
                {
                    playerCard12.Visible = false;
                }
            }
            else if (player == 1)
            {
                if (index == 0)
                {
                    sharkCard1.Visible = false;
                }
                else if (index == 1)
                {
                    sharkCard2.Visible = false;
                }
                else if (index == 2)
                {
                    sharkCard3.Visible = false;
                }
                else if (index == 3)
                {
                    sharkCard4.Visible = false;
                }
                else if (index == 4)
                {
                    sharkCard5.Visible = false;
                }
                else if (index == 5)
                {
                    sharkCard6.Visible = false;
                }
                else if (index == 6)
                {
                    sharkCard7.Visible = false;
                }
                else if (index == 7)
                {
                    sharkCard8.Visible = false;
                }
                else if (index == 8)
                {
                    sharkCard9.Visible = false;
                }
                else if (index == 9)
                {
                    sharkCard10.Visible = false;
                }
                else if (index == 10)
                {
                    sharkCard11.Visible = false;
                }
                else if (index == 11)
                {
                    sharkCard12.Visible = false;
                }
                else if (index == 12)
                {
                    sharkCard13.Visible = false;
                }
            }
            else if (player == 2)
            {
                if (index == 0)
                {
                    tigerCard1.Visible = false;
                }
                else if (index == 1)
                {
                    tigerCard2.Visible = false;
                }
                else if (index == 2)
                {
                    tigerCard3.Visible = false;
                }
                else if (index == 3)
                {
                    tigerCard4.Visible = false;
                }
                else if (index == 4)
                {
                    tigerCard5.Visible = false;
                }
                else if (index == 5)
                {
                    tigerCard6.Visible = false;
                }
                else if (index == 6)
                {
                    tigerCard7.Visible = false;
                }
                else if (index == 7)
                {
                    tigerCard8.Visible = false;
                }
                else if (index == 8)
                {
                    tigerCard9.Visible = false;
                }
                else if (index == 9)
                {
                    tigerCard10.Visible = false;
                }
                else if (index == 10)
                {
                    tigerCard11.Visible = false;
                }
                else if (index == 11)
                {
                    tigerCard12.Visible = false;
                }
                else if (index == 12)
                {
                    tigerCard13.Visible = false;
                }
            }
            else if (player == 3)
            {
                if (index == 0)
                {
                    hawkCard1.Visible = false;
                }
                else if (index == 1)
                {
                    hawkCard2.Visible = false;
                }
                else if (index == 2)
                {
                    hawkCard3.Visible = false;
                }
                else if (index == 3)
                {
                    hawkCard4.Visible = false;
                }
                else if (index == 4)
                {
                    hawkCard5.Visible = false;
                }
                else if (index == 5)
                {
                    hawkCard6.Visible = false;
                }
                else if (index == 6)
                {
                    hawkCard7.Visible = false;
                }
                else if (index == 7)
                {
                    hawkCard8.Visible = false;
                }
                else if (index == 8)
                {
                    hawkCard9.Visible = false;
                }
                else if (index == 9)
                {
                    hawkCard10.Visible = false;
                }
                else if (index == 10)
                {
                    hawkCard11.Visible = false;
                }
                else if (index == 11)
                {
                    hawkCard12.Visible = false;
                }
                else if (index == 12)
                {
                    hawkCard13.Visible = false;
                }
            }
        }
        private void play(int player, int card)
        {
            trick[player] = game[player][card];
            game[player][card] = null;
            idToPicture(player, trick[player]);
            removeFromHand(player, card);
        }
        private void playLowest(Card[] playersHand, int player, string suitToBePlayed)
        {
            int lowestIndex = -1;
            for (int i = 0; i < playersHand.Length; i++)
            {
                if (playersHand[i] != null && playersHand[i].suit == suitToBePlayed)
                {
                    lowestIndex = i;
                    break;
                }
            }
            for (int j = 0; j < playersHand.Length; j++)
            {
                if (playersHand[j] != null && playersHand[j].suit == suitToBePlayed && playersHand[j].value < playersHand[lowestIndex].value)
                {
                    lowestIndex = j;
                }
            }
            trick[player] = game[player][lowestIndex];
            game[player][lowestIndex] = null;
            idToPicture(player, trick[player]);
            removeFromHand(player, lowestIndex);
        }

        private void begin()
        {
            leadingSuit = "Clubs";
            while (notFound)
            {
                for (int player = 0; player < 4; player++)
                {
                    for (int c = 0; c < 13; c++)
                    {
                        //finds the player who has the 2 of clubs and forces the play of it, as is the rules
                        if (game[player][c].value == 2 && game[player][c].suit == leadingSuit)
                        {
                            if (player == 0)
                            {
                                leadingSuitTextBox.Text = "Please select the two of clubs to begin the game.";
                                notFound = false;
                                leader = player;
                                continueButton.Enabled = false;
                                continueButton.BackColor = Color.Red;
                                enableCards();
                            }
                            else
                            {
                                leader = player;
                                notFound = false;
                                play(player, c);
                                leadingSuitTextBox.Text = "The leading suit is: Clubs";
                                continueButton.Enabled = false;
                                continueButton.BackColor = Color.Red;
                                enableCards();
                            }
                        }
                    }
                }
            }
            if (leader != 0)
            {
                playRemaining();
            }
        }
        private void firstRoundContinue()
        {
            if (notFound)
            {
                begin();
            }
            else // happens after the person with the two of clubs plays up to the player
            {
                for (int cPlayer = 1; cPlayer < 4; cPlayer++)
                {
                    if (trick[cPlayer] != null)
                    {
                        continue;
                    }
                    for (int c = 0; c < 13; c++)
                    {
                        if (cPlayer != leader && trick[cPlayer] == null && game[cPlayer][c].suit == leadingSuit)
                        {
                            play(cPlayer, c);
                        }
                        else if (game[cPlayer][c] != null && trick[cPlayer] == null && !hasLeadingSuit(game[cPlayer]) && game[cPlayer][c].suit != "Hearts" && game[cPlayer][c].suit != "Spades")
                        {
                            play(cPlayer, c);
                        }
                    }
                }
                cardsRemaining--;
            }
        }

        private void continueButton_Click(object sender, EventArgs e) //Main method of game
        {
            
            bool trickIsFull = (trick[0] != null && trick[1] != null && trick[2] != null && trick[3] != null);
            //already have called determine leader previously in the first round
            if (cardsRemaining == 13)
            {
                firstRoundContinue();
            }
            else if (trickIsFull) //if everyone has played a card score it.
            {
                scoreTrick();
            }
            else if (!trickIsFull) // if 1 or more players hasn't yet played continue playing
            {
                if (leader == 0) // if player is the leader
                {
                    leadingSuitTextBox.Text = "Select a card to lead.";
                    continueButton.BackColor = Color.Red; 
                    enableCards();


                    if (trick[0] != null) // if player has played
                    {
                        playRemaining();
                        continueButton.BackColor = Color.Green;
                        disableCards();
                    }
                }
                else
                {
                    for (int cPlayer = leader; cPlayer < 4; cPlayer++)
                    {
                        for (int c = 0; c < 13; c++)
                        {
                            if (cPlayer == leader && game[cPlayer][c] != null && trick[leader] == null) // Handles a computer player playing a leading card up to player @ index 0
                            {
                                if (!heartsIsBroken)
                                {
                                    if(game[cPlayer][c].suit == "Hearts" && !onlyHasHearts(game[cPlayer]))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                            leadingSuit = game[cPlayer][c].suit;
                                            continueButton.Enabled = false;
                                        continueButton.BackColor = Color.Red;
                                        leadingSuitTextBox.Text = "Leading suit = " + game[cPlayer][c].suit;
                                            play(cPlayer, c);
                                            enableCards();
                                            break;
                                    }
                                }
                                else // if hearts is broken
                                {
                                   
                                        leadingSuit = game[cPlayer][c].suit;
                                        continueButton.Enabled = false;
                                    continueButton.BackColor = Color.Red;
                                    leadingSuitTextBox.Text = "Leading suit = " + game[cPlayer][c].suit;
                                        play(cPlayer, c);
                                        enableCards();
                                        break;
                                    
                                }
                            }
                            else if (trick[0] == null)
                            {
                                if (game[cPlayer][c] != null && hasLeadingSuit(game[cPlayer]) && trick[cPlayer] == null && game[cPlayer][c].suit == leadingSuit) // computer players that need to play before the player does 
                                {
                                    Random r = new Random();
                                    double chance = r.NextDouble();
                                    if (0.2 < chance) //80% of the time it will play the lowest card of the leading suit
                                    {
                                        playLowest(game[cPlayer], cPlayer, leadingSuit);
                                    }
                                    else //20 % of the time it will play the first card it finds that meets the requirments
                                    {
                                        play(cPlayer, c);
                                    }

                                }
                                else if (game[cPlayer][c] != null && trick[cPlayer] == null && !hasLeadingSuit(game[cPlayer])) //only plays card if the player doesn't have a card that matches the leading suit in their hand
                                {
                                    play(cPlayer, c);
                                }
                            }
                        }
                    }
                    if (trick[0] != null) // player has played
                    {
                        playRemaining(); // players who need to play after the player has
                    }

                }

            }
        }
        private void playRemaining()
        {
            //bool trickIsFull = (trick[0] != null && trick[1] != null && trick[2] != null && trick[3] != null);
            for (int cPlayer = 1; cPlayer < 4; cPlayer++)
            {
                if (trick[cPlayer] != null) // if computer has played
                {
                    continue;
                }
                for (int c = 0; c < 13; c++)
                {
                    if (game[cPlayer][c] != null && hasLeadingSuit(game[cPlayer]) && trick[cPlayer] == null && game[cPlayer][c].suit == leadingSuit) // those remaining who need to play, after the player has played.
                    {
                        Random r = new Random();
                        double chance = r.NextDouble();
                        if (0.2 < chance) //80% of the time it will play the lowest card of the leading suit if the above requirements arent met
                        {
                            playLowest(game[cPlayer], cPlayer, leadingSuit);
                        }
                        else //20 % of the time it will play the first card it finds that meets the requirments
                        {
                            play(cPlayer, c);
                        }

                    }
                    else if (game[cPlayer][c] != null && trick[cPlayer] == null && !hasLeadingSuit(game[cPlayer])) //only plays card if the player doesn't have a card that matches the leading suit in their hand
                    {
                        play(cPlayer, c);

                    }
                }  
            }
        }

        private bool onlyHasHearts(Card[] hand)
        {
            for (int i = 0; i < 13; i++)
            {
                if (hand[i] != null && hand[i].suit != "Hearts")
                {
                    return false;
                }
            }
            return true;
        }
        private void newGameButton_Click(object sender, EventArgs e)
        {

            trick = new Card[4];
            dealButton.Enabled = true;
            continueButton.Enabled = false;
            continueButton.BackColor = Color.Red;
            totalHawkScore = 0;
            totalSharkScore = 0;
            totalPlayerScore = 0;
            totalTigerScore = 0;

            cardsRemaining = 13;
            leader = -1;
            leadingSuit = "Clubs";
            DeckOfCards = new Card[52];
            leadingSuitTextBox.Text = "Click the Deal Cards Button.";
            notFound = true;

            playerCard0.Visible = true;
            playerCard1.Visible = true;
            playerCard2.Visible = true;
            playerCard3.Visible = true;
            playerCard4.Visible = true;
            playerCard5.Visible = true;
            playerCard6.Visible = true;
            playerCard7.Visible = true;
            playerCard8.Visible = true;
            playerCard9.Visible = true;
            playerCard10.Visible = true;
            playerCard11.Visible = true;
            playerCard12.Visible = true;

            playerCard0.Image = null;
            playerCard1.Image = null;
            playerCard2.Image = null;
            playerCard3.Image = null;
            playerCard4.Image = null;
            playerCard5.Image = null;
            playerCard6.Image = null;
            playerCard7.Image = null;
            playerCard8.Image = null;
            playerCard9.Image = null;
            playerCard10.Image = null;
            playerCard11.Image = null;
            playerCard12.Image = null;

            disableCards();

            sharkCard1.Visible = true;
            sharkCard2.Visible = true;
            sharkCard3.Visible = true;
            sharkCard4.Visible = true;
            sharkCard5.Visible = true;
            sharkCard6.Visible = true;
            sharkCard7.Visible = true;
            sharkCard8.Visible = true;
            sharkCard9.Visible = true;
            sharkCard10.Visible = true;
            sharkCard11.Visible = true;
            sharkCard12.Visible = true;
            sharkCard13.Visible = true;

            tigerCard1.Visible = true;
            tigerCard2.Visible = true;
            tigerCard3.Visible = true;
            tigerCard4.Visible = true;
            tigerCard5.Visible = true;
            tigerCard6.Visible = true;
            tigerCard7.Visible = true;
            tigerCard8.Visible = true;
            tigerCard9.Visible = true;
            tigerCard10.Visible = true;
            tigerCard11.Visible = true;
            tigerCard12.Visible = true;
            tigerCard13.Visible = true;

            hawkCard1.Visible = true;
            hawkCard2.Visible = true;
            hawkCard3.Visible = true;
            hawkCard4.Visible = true;
            hawkCard5.Visible = true;
            hawkCard6.Visible = true;
            hawkCard7.Visible = true;
            hawkCard8.Visible = true;
            hawkCard9.Visible = true;
            hawkCard10.Visible = true;
            hawkCard11.Visible = true;
            hawkCard12.Visible = true;
            hawkCard13.Visible = true;


            playerPlayedCard.Image = null;
            sharkPlayedCard.Image = null;
            tigerPlayedCard.Image = null;
            hawkPlayedCard.Image = null;
        }
        private void playPlayerInput(int cardIndex)
        {
            if (0 == leader)
            {
                if (!heartsIsBroken && game[0][cardIndex].suit == "Hearts" && !onlyHasHearts(game[0]))
                {
                    MessageBox.Show("Hearts is not broken yet, you cannot lead a heart until it is.");
                }
                else
                {
                    play(0, cardIndex);
                    leadingSuit = trick[0].suit;
                    disableCards();
                }
            }
            else
            {
                if (hasLeadingSuit(game[0]) && game[0][cardIndex].suit != leadingSuit)
                {
                    MessageBox.Show("You must play the suit that was lead.");
                }
                else
                {
                    play(0, cardIndex);
                    disableCards();
                }
            }
            continueButton.Enabled = true;
            continueButton.BackColor = Color.Green;
        }

        private void playerClick(object sender, EventArgs e)
        {
            PictureBox cardSelected = (PictureBox)sender;
            playPlayerInput(getIndexOfSelectedCard(cardSelected.Name));
        }
        private int getIndexOfSelectedCard(String cName)
        {
            if (cName == "playerCard0")
            {
                return 0;
            }
            else if (cName == "playerCard1")
            {
                return 1;
            }
            else if (cName == "playerCard2")
            {
                return 2;
            }
            else if (cName == "playerCard3")
            {
                return 3;
            }
            else if (cName == "playerCard4")
            {
                return 4;
            }
            else if (cName == "playerCard5")
            {
                return 5;
            }
            else if (cName == "playerCard6")
            {
                return 6;
            }
            else if (cName == "playerCard7")
            {
                return 7;
            }
            else if (cName == "playerCard8")
            {
                return 8;
            }
            else if (cName == "playerCard9")
            {
                return 9;
            }
            else if (cName == "playerCard10")
            {
                return 10;
            }
            else if (cName == "playerCard11")
            {
                return 11;
            }
            else if (cName == "playerCard12")
            {
                return 12;
            }
            return -1;
        }

        private void enableCards()
        {
            playerCard0.Enabled = true;
            playerCard1.Enabled = true;
            playerCard2.Enabled = true;
            playerCard3.Enabled = true;
            playerCard4.Enabled = true;
            playerCard5.Enabled = true;
            playerCard6.Enabled = true;
            playerCard7.Enabled = true;
            playerCard8.Enabled = true;
            playerCard9.Enabled = true;
            playerCard10.Enabled = true;
            playerCard11.Enabled = true;
            playerCard12.Enabled = true;
        }

        private void disableCards()
        {
            playerCard0.Enabled = false;
            playerCard1.Enabled = false;
            playerCard2.Enabled = false;
            playerCard3.Enabled = false;
            playerCard4.Enabled = false;
            playerCard5.Enabled = false;
            playerCard6.Enabled = false;
            playerCard7.Enabled = false;
            playerCard8.Enabled = false;
            playerCard9.Enabled = false;
            playerCard10.Enabled = false;
            playerCard11.Enabled = false;
            playerCard12.Enabled = false;
        }

        private void rulesButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. The goal in the game of Hearts is to have the lowest score.\n" +
                "\n2. the game ends whenever any player reaches 100 points.\n" +
                "\n3. Each card of the hearts suit is worth 1 point, and the Queen of Spades is worth 13 points. Whoever wins the 'trick' gets the points that were played.\n" +
                "\n4. If you cannot play a card click the continue button until you can, the text in the text box will change.\n" +
                "\n5. You cannot lead a hearts card until hearts is broken. Meaning that on a given hand a player doesn't have the leading suit and then plays a heart instead. Playing the Queen of Spades does not count as breaking hearts.\n" +
                "\n6. If any player manages to get all 26 points in a given round, they will actually get 0 points and the other 3 players will recieve 26 points.");
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cheatButton_Click(object sender, EventArgs e)
        {
            sortAllCards();
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string resourceName = asm.GetName().Name + ".Properties.Resources";
            var rm = new System.Resources.ResourceManager(resourceName, asm);
            if (leadingSuitTextBox.Text == "showAllCards") //ONLY WORKS WHEN REMAINING CARDS == 13 (after clicking only the deal cards button)
            {
                sharkCard1.Image = (Bitmap)rm.GetObject(sharkCards[0].tS());
                sharkCard2.Image = (Bitmap)rm.GetObject(sharkCards[1].tS());
                sharkCard3.Image = (Bitmap)rm.GetObject(sharkCards[2].tS());
                sharkCard4.Image = (Bitmap)rm.GetObject(sharkCards[3].tS());
                sharkCard5.Image = (Bitmap)rm.GetObject(sharkCards[4].tS());
                sharkCard6.Image = (Bitmap)rm.GetObject(sharkCards[5].tS());
                sharkCard7.Image = (Bitmap)rm.GetObject(sharkCards[6].tS());
                sharkCard8.Image = (Bitmap)rm.GetObject(sharkCards[7].tS());
                sharkCard9.Image = (Bitmap)rm.GetObject(sharkCards[8].tS());
                sharkCard10.Image = (Bitmap)rm.GetObject(sharkCards[9].tS());
                sharkCard11.Image = (Bitmap)rm.GetObject(sharkCards[10].tS());
                sharkCard12.Image = (Bitmap)rm.GetObject(sharkCards[11].tS());
                sharkCard13.Image = (Bitmap)rm.GetObject(sharkCards[12].tS());


                tigerCard1.Image = (Bitmap)rm.GetObject(tigerCards[0].tS());
                tigerCard2.Image = (Bitmap)rm.GetObject(tigerCards[1].tS());
                tigerCard3.Image = (Bitmap)rm.GetObject(tigerCards[2].tS());
                tigerCard4.Image = (Bitmap)rm.GetObject(tigerCards[3].tS());
                tigerCard5.Image = (Bitmap)rm.GetObject(tigerCards[4].tS());
                tigerCard6.Image = (Bitmap)rm.GetObject(tigerCards[5].tS());
                tigerCard7.Image = (Bitmap)rm.GetObject(tigerCards[6].tS());
                tigerCard8.Image = (Bitmap)rm.GetObject(tigerCards[7].tS());
                tigerCard9.Image = (Bitmap)rm.GetObject(tigerCards[8].tS());
                tigerCard10.Image = (Bitmap)rm.GetObject(tigerCards[9].tS());
                tigerCard11.Image = (Bitmap)rm.GetObject(tigerCards[10].tS());
                tigerCard12.Image = (Bitmap)rm.GetObject(tigerCards[11].tS());
                tigerCard13.Image = (Bitmap)rm.GetObject(tigerCards[12].tS());

                hawkCard1.Image = (Bitmap)rm.GetObject(hawkCards[0].tS());
                hawkCard2.Image = (Bitmap)rm.GetObject(hawkCards[1].tS());
                hawkCard3.Image = (Bitmap)rm.GetObject(hawkCards[2].tS());
                hawkCard4.Image = (Bitmap)rm.GetObject(hawkCards[3].tS());
                hawkCard5.Image = (Bitmap)rm.GetObject(hawkCards[4].tS());
                hawkCard6.Image = (Bitmap)rm.GetObject(hawkCards[5].tS());
                hawkCard7.Image = (Bitmap)rm.GetObject(hawkCards[6].tS());
                hawkCard8.Image = (Bitmap)rm.GetObject(hawkCards[7].tS());
                hawkCard9.Image = (Bitmap)rm.GetObject(hawkCards[8].tS());
                hawkCard10.Image = (Bitmap)rm.GetObject(hawkCards[9].tS());
                hawkCard11.Image = (Bitmap)rm.GetObject(hawkCards[10].tS());
                hawkCard12.Image = (Bitmap)rm.GetObject(hawkCards[11].tS());
                hawkCard13.Image = (Bitmap)rm.GetObject(hawkCards[12].tS());
            }
        }
    }
}