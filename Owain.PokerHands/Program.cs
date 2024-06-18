
using Poker;



var royalFlushRuleProcessor = new RoyalFlushHandRule();
var straightFlushRuleProcessor = new StraightFlushHandRule();
var fourOfAKindProcessor = new FourOfAKindHandRule();
var fullHouseProcessor = new FullHouseHandRule();
var flushProcessor = new FlushHandRule();
var StraightProcessor = new StraightHandRule();
var threeOfAKindProcessor = new ThreeOfAKindHandRule();
var twoPairsProcessor = new TwoPairsHandRule();
var onePairProcessor = new OnePairHandRule();
//var highCardProcessor = new HighCardHandRule();

var roundNumber = 1;
var p1Score = 0;
var p2Score = 0;

//  load the file

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

    var roundResult = "Result Not Found.";

    if (leftHandScore > rightHandScore)
    {
        p1Score++;
        roundResult="Player One Wins.";
    }

    if (leftHandScore < rightHandScore)
    {
        p2Score++;
        roundResult = "Player Two wins.";
    }

    if (leftHandScore == rightHandScore)
    {
        roundResult = "Draw.";
    }

    Console.WriteLine();
    Console.WriteLine($" Round:{roundNumber}   ||   {roundResult}");
    Console.WriteLine();

    roundNumber++;

}

DisplayResult();

void DisplayResult()
{
    Console.WriteLine();
    Console.WriteLine("   .......... .......... .......... .......... ..........   ");
    Console.WriteLine();
    Console.WriteLine($"       Player One Score: {p1Score}  ||   Player Two Score: {p2Score}  ");
    Console.WriteLine();
    Console.WriteLine("   .......... .......... .......... .......... ..........   ");
    Console.WriteLine();
}

Score GetScore(List<Card> hand)
{
    //Console.WriteLine(hand);

    if (royalFlushRuleProcessor.isTrue(hand))
    {
        Console.WriteLine("Royal Flush!");
        return Score.RoyalFlush;
    }
    else if ( straightFlushRuleProcessor.isTrue(hand))
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

    //Console.WriteLine("No hand found");
    //return 0;
   
    //throw new Exception("Hand not found!");
}



