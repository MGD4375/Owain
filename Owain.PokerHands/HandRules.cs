
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

        for (int i = 0; i < hand.Count; i++)
        {
            var duplicates = 0;

            var currentCardValue = hand[i].Value;

            foreach (var card in hand)
            { if (card.Value == currentCardValue) { duplicates++; } }

            if (duplicates == 3)
            { 
                hasThreeOfAKind = true; 
                threeOfAKindValue = currentCardValue;
                i = hand.Count+1;
            }
        }
            if(!hasThreeOfAKind)
            { return false; }
            else 
            { 
                List<Card> handCopy = new List<Card>();
                foreach (var card in hand)
                { handCopy.Add(card); }

                handCopy.RemoveAll( card => card.Value == threeOfAKindValue);

                handCopy.OrderBy(card => card.Value);


                for (int j = 1; j < handCopy.Count; j++)
                {
                    if (handCopy[(j - 1)] == handCopy[j])
                    { hasAPair = true; }
                }

            }

            if (hasAPair && hasThreeOfAKind) { return true; }
            else { return false; }

        

        return false;

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
        var handInAscendingOrder = hand.OrderBy(card => card.Value);

        int min = handInAscendingOrder.First().Value;
        var max = handInAscendingOrder.Last().Value;

        var isAStraight = (max - min) == (handInAscendingOrder.Count() - 1);

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

        for (int i = 1; i < cardValues.Count; i++)
        {
            if (cardValues[(i - 1)] == cardValues[i])
            { return true; }
        }

        return false;
    }
};


// may not be necasery. 

//public class HighCardHandRule : IHandRule
//{
//    public bool isTrue(List<Card> hand)
//    {


//        return false;
//    }
//};