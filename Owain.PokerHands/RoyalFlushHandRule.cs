//  load the file
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