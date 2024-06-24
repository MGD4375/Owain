

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
        var rules = new List<IHandRule>()
        {
            new RoyalFlushHandRule()          ,
            new StraightFlushHandRule()     ,
            new FourOfAKindHandRule()           ,
            new FullHouseHandRule()                ,
            new FlushHandRule()                           ,
            new StraightHandRule()                 ,
            new ThreeOfAKindHandRule()         ,
            new TwoPairsHandRule()                      ,
            new OnePairHandRule()
        };


        return rules.Find(rule => rule.isTrue(hand))?.Score
            ?? Score.HighCard;
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


