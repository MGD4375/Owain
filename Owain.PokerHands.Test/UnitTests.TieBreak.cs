using System.Runtime.Intrinsics.X86;

namespace Owain.TieBreakResult.Test
{
    public class UnitTestTieBreakHighCard
    {
        [Theory]
        
        [InlineData("4D", "7C", "QS", "KS", "5H",    "TS", "JH", "2H", "9S", "6C" ,      "playerOne")]           // High King : High Jack
        [InlineData("3D", "7C", "QS", "KS", "5H",    "TS", "JH", "2H", "AS", "6C" ,      "playerTwo")]           // High King : High Ace

        [InlineData("4D", "7C", "QS", "KS", "5H",    "3H", "7S", "QH", "KD", "5D",       "playerOne")]           // High King(4D) : High King
        [InlineData("2D", "7C", "QS", "KS", "5H",    "3H", "7S", "QH", "KD", "5D",       "playerTwo")]           // High King : High King(3H)

        

        public void HighCard
            (string data1, string data2, string data3, string data4, string data5,
             string data6, string data7, string data8, string data9, string data10,
             string expectedResult)
        {
            //  Arrange  

            var cardList1 = new List<Card> {
            new Card(data1),
            new Card(data2),
            new Card(data3),
            new Card(data4),
            new Card(data5) };

            var cardList2 = new List<Card> {
            new Card(data6),
            new Card(data7),
            new Card(data8),
            new Card(data9),
            new Card(data10) };

            var hand1 = new Hand(cardList1);
            var hand2 = new Hand(cardList2);


            var tieBreak = new TieBreak(hand1,hand2);
            
            //  Act
            string tieBreakResolution = tieBreak.Resolution;

            //  Assert 
            Assert.True(tieBreakResolution == expectedResult);
        }
    }

    public class UnitTestTieBreakOnePair
    {
        [Theory]

        [InlineData("KD", "7C", "QS", "KS", "JC",    "TS", "JH", "2H", "JS", "6C", "playerOne")]           // Pair King : Pair Jack
        [InlineData("3D", "7C", "QS", "KS", "KH",    "AS", "JH", "2H", "AD", "6C", "playerTwo")]           // Pair King : Pair Ace

        [InlineData("4D", "7C", "5S", "KS", "KC",    "3H", "7S", "KH", "KD", "5D", "playerOne")]           // Pair King(High 4) : Pair King
        [InlineData("KD", "7C", "QS", "KS", "5H",    "KH", "AS", "QH", "KC", "5D", "playerTwo")]           // High King : High King(High Ace) 



        public void OnePair
            (string data1, string data2, string data3, string data4, string data5,
             string data6, string data7, string data8, string data9, string data10,
             string expectedResult)
        {
            //  Arrange  

            var cardList1 = new List<Card> {
            new Card(data1),
            new Card(data2),
            new Card(data3),
            new Card(data4),
            new Card(data5) };

            var cardList2 = new List<Card> {
            new Card(data6),
            new Card(data7),
            new Card(data8),
            new Card(data9),
            new Card(data10) };

            var hand1 = new Hand(cardList1);
            var hand2 = new Hand(cardList2);


            var tieBreak = new TieBreak(hand1, hand2);

            //  Act
            string tieBreakResolution = tieBreak.Resolution;

            //  Assert 
            Assert.True(tieBreakResolution == expectedResult);
        }
    }

    public class UnitTestTieBreakTwoPairs
    {
        [Theory]

        [InlineData("KD", "4C", "4S", "KS", "JC",     "4D", "JH", "4H", "JS", "6C", "playerOne")]           //(Pair King) , Pair 4s : Pair Jack , Pair 4s
        //[InlineData("4D", "7C", "4S", "KS", "KH",     "AS", "JH", "AH", "AD", "AC", "playerTwo")]  //fail (four of A Kind Beat two Pairs)         //  Pair King , Pair 4s : (Pair Ace) , Pair Aces
        [InlineData("4D", "7C", "4S", "KS", "KH",     "AS", "JH", "AH", "KD", "KC", "playerTwo")]           //  Pair King , Pair 4s : (Pair Ace) , Pair Kings


        [InlineData("QD", "7C", "QS", "KS", "KC",     "3H", "2S", "KH", "KD", "2D", "playerOne")]           // Pair King , (Pair Queens) : Pair King Pair 2s
        [InlineData("KD", "3C", "3S", "KS", "5H",     "KH", "AS", "3H", "KC", "3D", "playerTwo")]           // Pair King , Pair 3s : High King , Pair 3s  (High Ace)



        public void TwoPairs
            (string data1, string data2, string data3, string data4, string data5,
             string data6, string data7, string data8, string data9, string data10,
             string expectedResult)
        {
            //  Arrange  

            var cardList1 = new List<Card> {
            new Card(data1),
            new Card(data2),
            new Card(data3),
            new Card(data4),
            new Card(data5) };

            var cardList2 = new List<Card> {
            new Card(data6),
            new Card(data7),
            new Card(data8),
            new Card(data9),
            new Card(data10) };

            var hand1 = new Hand(cardList1);
            var hand2 = new Hand(cardList2);


            var tieBreak = new TieBreak(hand1, hand2);

            //  Act
            string tieBreakResolution = tieBreak.Resolution;

            //  Assert 
            Assert.True(tieBreakResolution == expectedResult);
        }
    }

}
    

