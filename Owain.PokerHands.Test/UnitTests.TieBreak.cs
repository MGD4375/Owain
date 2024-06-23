using System.Runtime.Intrinsics.X86;

namespace Owain.TieBreakResult.Test
{
public class UnitTestTieBreak
    {
        [Theory]
        //[InlineData("TH", "JH", "QH", "KH", "AH",    "TS", "3H", "5D", "3C", "7H" ,      "  No Tie Found. ")]    //// RoyalFlush : High 10
        
        [InlineData("4D", "7C", "QS", "KS", "5H",    "TS", "JH", "2H", "9S", "6C" ,      "playerOne")]           // High King : High Jack
        [InlineData("4D", "7C", "QS", "KS", "5H",    "TS", "JH", "2H", "AS", "6C" ,      "playerTwo")]           // High King : High Ace

        [InlineData("4D", "7C", "QS", "KS", "5H",    "3H", "7S", "QH", "KD", "5D",       "playerOne")]           // High King(4D) : High King
        [InlineData("2D", "7C", "QS", "KS", "5H",    "3H", "7S", "QH", "KD", "5D",       "playerTwo")]           // High King : High King(3H)

        [InlineData("2D", "7C", "QS", "KS", "kH",    "3H", "8S", "8H", "KD", "5D",       "playerOne")]           // Pair King : Pair 8s

        public void TieBreakResolution
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

}
    

