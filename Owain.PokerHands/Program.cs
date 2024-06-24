
using Poker;
using System.Xml;

RunTest();

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

    Hand playerOne = new Hand(hands[0].Select(it => it.Card).ToList());
    Hand playerTwo = new Hand(hands[1].Select(it => it.Card).ToList());

    var roundResult = "Result Not Found.";

    if (playerOne.Score > playerTwo.Score)
    {
        p1Score++;
        roundResult="Player One Wins.";
    }

    if (playerOne.Score < playerTwo.Score)
    {
        p2Score++;
        roundResult = "Player Two wins.";
    }

    if (playerOne.Score == playerTwo.Score)
    {
        roundResult = "Draw.";
    }

    Console.WriteLine();
    Console.WriteLine($" Round:{roundNumber}   ||   {roundResult}");
    Console.WriteLine();

    if (roundResult == "Draw.")
    {
        TieBreak tieBreak = new TieBreak(playerOne,playerTwo);
        string winner = tieBreak.Resolution;

        if (winner == "playerOne")
        {
            p1Score++;
            roundResult = "Player One Wins.";
        }
        else if (winner == "playedTwo")
        {
            p2Score++;
            roundResult = "Player Two wins.";
        }
        else { roundResult = " No Winner Found!"; }

        Console.WriteLine();
        Console.WriteLine($" Tie Break   ||   {roundResult}");
        Console.WriteLine();

    }

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
    Console.WriteLine($"         1000 hands played.   ||    Hands Unrisolved:  {1000-(p1Score+p2Score)}       ");
    Console.WriteLine();
    Console.WriteLine("   .......... .......... .......... .......... ..........   ");
    Console.WriteLine();

}


void RunTest()
{
    //[InlineData("KD", "3C", "3S", "KS", "5H",     "KH", "AS", "3H", "KC", "3D", "playerTwo")] 

    List<Card> p1Cards = new List<Card>();
    p1Cards.Add(new Card("KD"));
    p1Cards.Add(new Card("3C"));
    p1Cards.Add(new Card("3S"));
    p1Cards.Add(new Card("KS"));
    p1Cards.Add(new Card("5H"));

    List<Card> p2Cards = new List<Card>();
    p2Cards.Add(new Card("AH"));
    p2Cards.Add(new Card("AS"));
    p2Cards.Add(new Card("3H"));
    p2Cards.Add(new Card("AC"));
    p2Cards.Add(new Card("AD"));

    Hand p1 = new Hand(p1Cards);
    Hand p2 = new Hand(p2Cards);
    
    TieBreak round = new TieBreak(p1,p2);
    var result = round.Resolution;

}




