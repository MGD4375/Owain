//  load the file
using Poker;

var royalFlushRuleProcessor = new RoyalFlushHandRule();
var straightFlushRuleProcessor = new StraightFlushHandRule();

var lines = File.ReadAllText("./poker.txt")
    .Split("\r\n")
    .Where(line => line.Length > 0);

foreach (var line in lines)
{
    var hands = line
    .Split(' ')
    .Select(it => new Card(it))
    .Select((it, index) => new { index, Card = it })
    .Select(it => new { it.index, it.Card })
    .GroupBy(it => it.index < 5)
    .ToList();


    var leftHand = hands[0].ToList();
    var rightHand = hands[1].ToList();

    var leftHandScore = GetScore(leftHand.Select(it => it.Card).ToList());
    var rightHandScore = GetScore(rightHand.Select(it => it.Card).ToList());

    if (leftHandScore > rightHandScore)
    {
        Console.WriteLine("player one wins");
    }

    if (leftHandScore < rightHandScore)
    {
        Console.WriteLine("player two wins");
    }

    if (leftHandScore == rightHandScore)
    {
        Console.WriteLine("draw");
    }
}

Score GetScore(List<Card> hand)
{
    Console.WriteLine(hand);

    if (royalFlushRuleProcessor.isTrue(hand))
    {
        Console.WriteLine("royal flush!");
        return Score.RoyalFlush;
    }
    else if ( straightFlushRuleProcessor.isTrue(hand))
    {
        Console.WriteLine("straight flush!");
        return Score.StraightFlush;
    }


    Console.WriteLine("No hand found");
    return 0;
    //throw new Exception("Hand not found!");
}



