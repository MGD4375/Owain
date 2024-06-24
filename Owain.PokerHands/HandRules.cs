
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
            if (card.Value != threeOfAKindValue)
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
        for (int i = 0; i < hand.Count; i++)
        {
            var duplicates = 0;

            var currentCardValue = hand[i].Value;

            foreach (var card in hand)
            { if (card.Value == currentCardValue) { duplicates++; } }

            if (duplicates >= 3) { return true; }

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
            if (cardValues[(i - 1)] == cardValues[i])
            { pairCount++; }
        }

        if (pairCount >= 2)
        { return true; }
        else { return false; }

    }
};

public class OnePairHandRule : IHandRule
{
    public bool isTrue(List<Card> hand)
    {
        return hand.Any(card => hand.Count(c2 => c2.Value == card.Value) > 1);
    }
};
