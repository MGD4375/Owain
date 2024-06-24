public class TieBreak
{
    public Hand PlayerOne;
    public Hand PlayerTwo;

    public string Resolution;

    private string p1Wins = MagicStrings.PLAYER_ONE;
    private string p2Wins = MagicStrings.PLAYER_TWO;

    public TieBreak(Hand playerOne, Hand playerTwo)
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
                case 2: { resolution = CompairOnePair(); } break;
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
            if (PlayerOne.HighCard.Value > PlayerTwo.HighCard.Value)
            { return p1Wins; }
            else { return p2Wins; }
        }

        if (highCardDraw)
        {
            while (highCardDraw)
            {
                PlayerOne.Cards.Remove(PlayerOne.Cards.OrderBy(it => it.Value).Last());
                PlayerTwo.Cards.Remove(PlayerTwo.Cards.OrderBy(it => it.Value).Last());

                highCardDraw = PlayerOne.Cards.OrderBy(it => it.Value).Last().Value == PlayerTwo.Cards.OrderBy(it => it.Value).Last().Value;

                if (!highCardDraw)
                {
                    if (PlayerOne.Cards.OrderBy(it => it.Value).Last().Value > PlayerTwo.Cards.OrderBy(it => it.Value).Last().Value)
                    { return p1Wins; }
                    else { return p2Wins; }
                }

                if (highCardDraw && PlayerOne.Cards.Count() == 0)
                { break; }

            }





        }
        return "Draw No Resalution Found";
    }

    private string CompairOnePair()
    {
        var p1PairCard = PlayerOne.Cards.FirstOrDefault(x => PlayerOne.Cards.Count(y => x.Value == y.Value) > 1)
            ?? throw new Exception("No Pair Found During Tie break CompairOnePair");

        var p2PairCard = PlayerTwo.Cards.FirstOrDefault(x => PlayerTwo.Cards.Count(y => x.Value == y.Value) > 1)
            ?? throw new Exception("No Pair Found During Tie break CompairOnePair");

        if (p1PairCard.Value == p2PairCard.Value)
        {
            return HighCard();
        }
        else if (p1PairCard.Value > p2PairCard.Value)
        {
            return p1Wins;
        }
        else
        {
            return p2Wins;
        }
    }

    private string TwoPairs()
    {
        List<int> player1Pairs = new List<int>();
        List<int> player2Pairs = new List<int>();

        foreach (var card in PlayerOne.Cards)
        {
            var duplicates = PlayerOne.Cards.Count(it => it.Name == card.Name);

            if (duplicates > 1)
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

        if (player1Pairs.Count != 2 || player2Pairs.Count != 2)
        { throw new Exception("Could Not FindTwo Pairs"); }

        player1Pairs.Sort();
        player2Pairs.Sort();

        var highPairDraw = player1Pairs[1] == player2Pairs[1];

        if (!highPairDraw)
        {
            if (player1Pairs[1] > player2Pairs[1])
            { return p1Wins; }
            else { return p2Wins; }
        }
        else
        {
            var lowPairDraw = player1Pairs[0] == player2Pairs[0];

            if (!lowPairDraw)
            {
                if (player1Pairs[0] > player2Pairs[0])
                { return p1Wins; }
                else { return p2Wins; }
            }
            else { return HighCard(); }

        }

    }




}