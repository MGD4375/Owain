

using Poker;
using System.ComponentModel.DataAnnotations;

public class Hand : IComparable<Hand>
{
    //Properties
    public List<Card> Cards;
    public bool ValidHand;

    public Score Score;
    public Card HighCard;

    //Constructor

    public Hand(List<Card> fiveCards)
    {
        Cards = fiveCards;

        if (Cards.Count == 5) { ValidHand = true; }
        else { ValidHand = false; }

        Score = GetScore(Cards);
        HighCard = GetHighCard(Cards);

    }

    // Methods

    public static Score GetScore(List<Card> hand)
    {
        RoyalFlushHandRule royalFlushRuleProcessor = new RoyalFlushHandRule();
        StraightFlushHandRule straightFlushRuleProcessor = new StraightFlushHandRule();
        FourOfAKindHandRule fourOfAKindProcessor = new FourOfAKindHandRule();
        FullHouseHandRule fullHouseProcessor = new FullHouseHandRule();
        FlushHandRule flushProcessor = new FlushHandRule();
        StraightHandRule StraightProcessor = new StraightHandRule();
        ThreeOfAKindHandRule threeOfAKindProcessor = new ThreeOfAKindHandRule();
        TwoPairsHandRule twoPairsProcessor = new TwoPairsHandRule();
        OnePairHandRule onePairProcessor = new OnePairHandRule();

        if (royalFlushRuleProcessor.isTrue(hand))
        {
            Console.WriteLine("Royal Flush!");
            return Score.RoyalFlush;
        }
        else if (straightFlushRuleProcessor.isTrue(hand))
        {
            Console.WriteLine("Straight Flush!");
            return Score.StraightFlush;
        }
        else if (fourOfAKindProcessor.isTrue(hand))
        {
            Console.WriteLine("Four Of A Kind!");
            return Score.FourOfAKind;

        }
        else if (fullHouseProcessor.isTrue(hand))
        {
            Console.WriteLine("Full House!");
            return Score.FullHouse;

        }
        else if (flushProcessor.isTrue(hand))
        {
            Console.WriteLine("Flush!");
            return Score.Flush;

        }
        else if (StraightProcessor.isTrue(hand))
        {
            Console.WriteLine("Straight!");
            return Score.Straight;

        }
        else if (threeOfAKindProcessor.isTrue(hand))
        {
            Console.WriteLine("Three Of A Kind!");
            return Score.ThreeOfAKind;

        }
        else if (twoPairsProcessor.isTrue(hand))
        {
            Console.WriteLine("Two Pairs!");
            return Score.TwoPairs;

        }
        else if (onePairProcessor.isTrue(hand))
        {
            Console.WriteLine("A Pair!");
            return Score.OnePair;

        }
        else
        {
            Console.WriteLine("High Card!");
            return Score.HighCard;

        }

    }

    Card GetHighCard(List<Card> hand)
    {

        Card bestCard = hand.OrderBy(card => card.Value).Last();

        return bestCard;
    }

    public static int Compare(Hand first, Hand second)
    {
        var h1Score = GetScore(first.Cards);
        var h2Score = GetScore(second.Cards);

        return h1Score.CompareTo(h2Score);
    }

    public int CompareTo(Hand? other)
    {
        if (other == null)
        {
            return 1;
        }

        return Hand.Compare(this, other);
    }

    public static bool operator <(Hand first, Hand second)
    {
        return Hand.Compare(first, second) < 0;
    }
    public static bool operator >(Hand first, Hand second)
    {
        return Hand.Compare(first, second) > 0;
    }


}


public class Card
{
    private Card() { }

    public Card(string data)
    {
        Value = data[0] switch
        {
            '2' => 2,
            '3' => 3,
            '4' => 4,
            '5' => 5,
            '6' => 6,
            '7' => 7,
            '8' => 8,
            '9' => 9,
            'T' => 10,
            'J' => 11,
            'Q' => 12,
            'K' => 13,
            'A' => 14,
            _ => throw new Exception("Could not map value from name")
        };


        Name = data[0];
        Suit = data[1];
    }

    public int Value { get; set; } // Terrible thing to do, but it's quick! 
    public char Name { get; private set; }
    public char Suit { get; private set; }
}


