using System;

public class Card
{
	public int value;
	public string suit;
	public int suitValue; //used in the suit sorting method

	public Card(int v, string s)
	{
		value = v;
		suit = s;
		if(suit == "Hearts")
        {
			suitValue = 1;
        }
		else if(suit == "Clubs")
        {
			suitValue = 2;
        }
		else if(suit == "Diamonds")
        {
			suitValue = 3;
        }
		else if(suit == "Spades")
        {
			suitValue = 4;
        }

	}
	public string tS()
	{
		string s = "";
		if (this.value == 11)
		{
			return s + "Jack_" + this.suit;
		}
		else if (this.value == 12)
		{
			return s + "Queen_" + this.suit;
		}
		else if (this.value == 13)
		{
			return s + "King_" + this.suit;
		}
		else if (this.value == 14)
		{
			return s + "Ace_" + this.suit;
		}
		else
		{
			return s + "_" + this.value + "_" + this.suit;
		}
	}
}
