
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
            if (hasAnAce && hasATwo)
            {
                var ace = hand.First(it => it.Name == 'A');
                ace.Value = 14;
            }
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
        var hasLowAce = hand.Any(it => it.Value == 2) && hasAnAce;
      
        if (hasLowAce) 
        {
            var ace = hand.First(it => it.Name == 'A');
            ace.Value = 1;
        }
        

        var isAStraight = hand.Max(it => it.Value) - hand.Min(it => it.Value) == hand.Count() - 1;

        if (!isAStraight) 
        {
            if (hasLowAce)
            {
                var ace = hand.First(it => it.Name == 'A');
                ace.Value = 14;
            }
            return false; 
        }
        else
        {
            if (hasLowAce)
            {
                var ace = hand.First(it => it.Name == 'A');
                ace.Value = 14;
            }
            return true;
        }
       
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
                case 3: { resolution = TwoPairs(); } break;
                default: { Console.WriteLine(" No Resolution Method Found. "); } break;
                //default: { throw new Exception(" No Resolution Method Found. "); }

            }
        }

        return resolution;
    }

    private string HighCard()
    {
        var highCardDraw = PlayerOne.HighCard.Value == PlayerTwo.HighCard.Value;

        if (!highCardDraw)
        { 
         if(PlayerOne.HighCard.Value > PlayerTwo.HighCard.Value) 
            { return p1Wins; }
         else { return p2Wins; }
        }

        if (highCardDraw)
        {
            while(highCardDraw)
            {
                PlayerOne.Cards.Remove(PlayerOne.Cards.OrderBy(it=>it.Value).Last());
                PlayerTwo.Cards.Remove(PlayerTwo.Cards.OrderBy(it => it.Value).Last());

                highCardDraw = PlayerOne.Cards.OrderBy(it => it.Value).Last().Value == PlayerTwo.Cards.OrderBy(it => it.Value).Last().Value;

                if(!highCardDraw)
                {
                    if(PlayerOne.Cards.OrderBy(it => it.Value).Last().Value> PlayerTwo.Cards.OrderBy(it => it.Value).Last().Value)
                    { return p1Wins;}
                    else { return p2Wins; }
                }

                if(highCardDraw && PlayerOne.Cards.Count() == 0)
                { break; }

            }
            
                
                

            
        }
        return "Draw No Resalution Found";
    }

    private string OnePair()
    {
        
        var playerOnePairValue = 0;

        foreach(var card in PlayerOne.Cards)
        {
            var pairFound = PlayerOne.Cards.Count(it => it.Name == card.Name) == 1;

            if(pairFound)
            {  
                playerOnePairValue=card.Value;
                break;
            }
        }

        var playerTwoPairValue = 0;

        foreach (var card in PlayerTwo.Cards)
        {
            var pairFound = PlayerTwo.Cards.Count(it => it.Name == card.Name) == 1;

            if (pairFound)
            {
                playerTwoPairValue = card.Value;
                break;
            }
        }

        if (playerOnePairValue == playerTwoPairValue && playerOnePairValue == 0)
        { throw new Exception(" No Pair Found."); }
        else if (playerOnePairValue == playerTwoPairValue)
        { return HighCard(); }
        else if (playerOnePairValue > playerTwoPairValue)
        { return p1Wins; }
        else { return p2Wins; }
    }

    private string TwoPairs()
    {
        List<int> player1Pairs = new List<int>();
        List<int> player2Pairs = new List<int>();

        foreach (var card in PlayerOne.Cards)
        {
            var duplicates = PlayerOne.Cards.Count(it => it.Name == card.Name);

            if (duplicates>1)
            {
                if (duplicates > 2)
                {
                    player1Pairs.Add(card.Value);
                    player1Pairs.Add(card.Value);
                    break;
                }
                else if (duplicates == 2)
                {
                    if (!player1Pairs.Contains(card.Value))
                    { player1Pairs.Add(card.Value); }
                }
            }
        }

        foreach (var card in PlayerTwo.Cards)
        {
            var duplicates = PlayerTwo.Cards.Count(it => it.Name == card.Name);

            if (duplicates > 1)
            {
                if (duplicates > 2)
                {
                    player2Pairs.Add(card.Value);
                    player2Pairs.Add(card.Value);
                    break;
                }
                else if (duplicates == 2)
                {
                    if (!player2Pairs.Contains(card.Value))
                    { player2Pairs.Add(card.Value); }
                }
            }
        }

        if(player1Pairs.Count != 2 || player2Pairs.Count != 2)
        { throw new Exception("Could Not FindTwo Pairs"); }

        player1Pairs.Sort();
        player2Pairs.Sort();

        var highPairDraw = player1Pairs[1] == player2Pairs[1];

        if(!highPairDraw)
        {
            if (player1Pairs[1] > player2Pairs[1])
            { return p1Wins; }
            else { return p2Wins; }
        }
        else 
        {
            var lowPairDraw = player1Pairs[0] == player2Pairs[0];

            if(!lowPairDraw)
            {
                if (player1Pairs[0] > player2Pairs[0])
                { return p1Wins; }
                else { return p2Wins; }
            }
            else { return HighCard(); }
        
        }
    
    }




}