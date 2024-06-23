
using Poker;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

public class RoyalFlushHandRule : IHandRule
{
    public bool isTrue(List<Card> hand)
    {
        var suit = hand[0].Suit;

        var isSingleSuit = hand.All(card => card.Suit == suit);

        if (!isSingleSuit)
        {
            return false;
        }

        var hasFiveValidCards = hand.Count(card => card.Name == 'T'
        || card.Name == 'J'
        || card.Name == 'Q'
        || card.Name == 'K'
        || card.Name == 'A'
        ) == 5;

        if (!hasFiveValidCards)
        {
            return false;
        }

        return true;
    }
};

public class StraightFlushHandRule : IHandRule
{
    public bool isTrue(List<Card> hand)
    {
        var suit = hand[0].Suit;

        var isSingleSuit = hand.All(card => card.Suit == suit);

        if (!isSingleSuit)
        {
            return false;
        }

        var hasAnAce = hand.Any(it => it.Name == 'A');
        var hasATwo = hand.Any(it => it.Name == '2');
        if (hasAnAce && hasATwo)
        {
            var ace = hand.First(it => it.Name == 'A');
            ace.Value = 1;
        }


        var gapBetweenHighestAndLowest = hand.Max(it => it.Value) - hand.Min(it => it.Value);
        if (gapBetweenHighestAndLowest > 4)
        {
            return false;
        }

        return true;
    }
};

public class FourOfAKindHandRule : IHandRule
{
    public bool isTrue(List<Card> hand)
    {
        List<int> cardValues = new List<int>();
        foreach (var card in hand) 
        {
            cardValues.Add(card.Value);
        }

        cardValues.Sort();

        if (cardValues[0] == cardValues[3] || cardValues[1] == cardValues[4]) 
        { return true; }
        else { return false; }
       
    }
};

public class FullHouseHandRule : IHandRule
{
    public bool isTrue(List<Card> hand)
    {
        var hasThreeOfAKind = false;
        var hasAPair = false;

        var threeOfAKindValue = 0;

            foreach (var card in hand)
            {
                int duplicates = hand.Where(it => it.Value == card.Value).Count();            
               
                if (duplicates >= 3) 
                {
                    hasThreeOfAKind = true;
                    threeOfAKindValue = card.Value;
                    break;
                }
            
            }

     

        

        if (!hasThreeOfAKind) { return false; }
        
        var handCopy = new List<Card>();
        
        foreach (var card in hand)
        { 
            if(card.Value != threeOfAKindValue) 
            { handCopy.Add(card); } 
        }

        foreach (var card in handCopy)
        {

            var duplicates = handCopy.Where(it => it.Value == card.Value).Count();


            if (duplicates >= 2)
            {
                hasAPair = true;
                break;
            }

        }
        if (!hasAPair) { return false; }
        return true;

       

       // Full House: Three of a kind and a pair
    }
};

public class FlushHandRule : IHandRule
{
    public bool isTrue(List<Card> hand)
    {
        var suit = hand[0].Suit;

        var isSingleSuit = hand.All(card => card.Suit == suit);

        if (!isSingleSuit)
        {
            return false;
        }
        else { return true; }
        
    }
};

public class StraightHandRule : IHandRule
{
    public bool isTrue(List<Card> hand)
    {
        var hasAnAce = hand.Any(it => it.Value == 14);

        if(hasAnAce) 
        { 
            var hasLowAce = hand.Any(it => it.Value == 2);
            if (hasLowAce) 
            {
                var ace = hand.First(it => it.Name == 'A');
                ace.Value = 1;
            }
        }

        var isAStraight = hand.Max(it => it.Value) - hand.Min(it => it.Value) == hand.Count() - 1;

        if (!isAStraight) { return false; }
        else { return true; }
       
    }
};

public class ThreeOfAKindHandRule : IHandRule
{
    public bool isTrue(List<Card> hand)
    {
        for(int i = 0; i < hand.Count; i++ )
        {
            var duplicates = 0;

            var currentCardValue = hand[i].Value;

            foreach (var card in hand)
            { if (card.Value == currentCardValue) { duplicates++; } }

            if(duplicates >= 3) { return true; }

        }

        return false;
    }
};

public class TwoPairsHandRule : IHandRule
{
    public bool isTrue(List<Card> hand)
    {
        var pairCount = 0;

        List<int> cardValues = new List<int>();

        foreach (Card card in hand)
        { 
         cardValues.Add(card.Value);
        }

        cardValues.Sort();

        for (int i = 1; i < cardValues.Count; i++)
        {
            if(cardValues[(i-1)] == cardValues[i])
            { pairCount++; }
        }

        if(pairCount >= 2)
        { return true; }
        else { return false; }
        
    }
};

public class OnePairHandRule : IHandRule
{
    public bool isTrue(List<Card> hand)
    {
        List<int> cardValues = new List<int>();

        foreach (Card card in hand)
        {
            cardValues.Add(card.Value);
        }

        cardValues.Sort();

        for (int i = 1; i < cardValues.Count -1; i++)
        {
            if (cardValues[(i - 1)] == cardValues[i])
            { return true; }
        }

        return false;
    }
};


public class TieBreak
{
    public Hand PlayerOne;
    public Hand PlayerTwo;

    public string Resolution;

    private string p1Wins = "playerOne";
    private string p2Wins = "playerTwo";

    public TieBreak(Hand playerOne , Hand playerTwo)
    {
        PlayerOne = playerOne;
        PlayerTwo = playerTwo;

        Resolution = ResolveTie();

    }



    private string ResolveTie()
    {
        var resolution = "";

        var tieFound = PlayerOne.Score == PlayerTwo.Score;

        if (!tieFound) { throw new Exception("  No Tie Found. "); }
        else
        {

            switch ((int)PlayerOne.Score)
            {
                case 1: { resolution = HighCard(); } break;
                case 2: { resolution = OnePair(); } break;
                default: { Console.WriteLine(" No Resolution Method Found. "); } break;
                //default: { throw new Exception( " No Resolution Method Found. "); }

            }
        }

        return resolution;
    }

    private string HighCard()
    {
        var highCardDraw = PlayerOne.HighCard.Value == PlayerTwo.HighCard.Value;

        if (highCardDraw)
        {
           PlayerOne.Cards.OrderBy(it => it.Value);
           PlayerTwo.Cards.OrderBy(it => it.Value);

           while (highCardDraw) 
           {
            
            PlayerOne.Cards.RemoveAt( PlayerOne.Cards.Count - 1);
            PlayerTwo.Cards.RemoveAt(PlayerTwo.Cards.Count -1);

            highCardDraw = PlayerOne.Cards.Last().Value == PlayerTwo.Cards.Last().Value;
           }
           
        }
      
        if (PlayerOne.HighCard.Value>PlayerTwo.HighCard.Value)
        { return p1Wins; }
        else { return p2Wins; }
        
        

        

    }

    private string OnePair()
    {
        var playerOnePairValue = 0;
        
        PlayerOne.Cards.OrderBy(it => it.Value);
       
        for (int i = 1; i < PlayerOne.Cards.Count; i++)
        {
            if (PlayerOne.Cards[(i - 1)] == PlayerOne.Cards[i])
            { 
                playerOnePairValue = PlayerOne.Cards[i].Value; 
                i = PlayerOne.Cards.Count + 10;
            }
        }

        var playerTwoPairValue = 0;

        PlayerTwo.Cards.OrderBy(it => it.Value);

        for (int i = 1; i < PlayerTwo.Cards.Count; i++)
        {
            if (PlayerTwo.Cards[(i - 1)] == PlayerTwo.Cards[i])
            {
                playerTwoPairValue = PlayerTwo.Cards[i].Value;
                i = PlayerTwo.Cards.Count + 10;
            }
        }

        if (playerOnePairValue == playerTwoPairValue && playerOnePairValue == 0)
        { throw new Exception(" No Pair Found."); }
        else if(playerOnePairValue == playerTwoPairValue)
        { return HighCard(); }
        else if(playerOnePairValue > playerTwoPairValue)
        { return p1Wins; }
        else { return p2Wins; }
    }

}