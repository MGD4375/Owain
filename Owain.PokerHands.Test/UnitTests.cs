namespace Owain.PokerHands.Test
{
    public class UnitTest1RoyalFlush
    {
        [Theory]
        [InlineData("TH", "JH", "QH", "KH", "AH", true)]    //   suit 1
        [InlineData("TS", "JS", "QS", "KS", "AS", true)]    //   different suit
        [InlineData("TS", "JH", "QH", "KH", "AH", false)]
        [InlineData("9H", "JH", "QH", "KH", "AH", false)]   
        [InlineData("QH", "JH", "TH", "KH", "AH", true)]    //  out of order
        public void CanFindRoyalFlush(string data1, string data2, string data3, string data4, string data5, bool isFlush)
        {
            //  Arrange  
            var royalFlushProcessor = new RoyalFlushHandRule();
            var hand = new List<Card> {
            new Card(data1),
            new Card(data2),
            new Card(data3),
            new Card(data4),
            new Card(data5)
            };

            //  Act
            var isThereARoyalFlush = royalFlushProcessor.isTrue(hand);

            //  Assert 
            Assert.True(isThereARoyalFlush == isFlush);
        }
    }

    public class UnitTest2StraightFlush
    {
        [Theory]
        [InlineData("TH", "JH", "QH", "KH", "AH", true)]    //   suit 1
        [InlineData("TS", "JS", "QS", "KS", "AS", true)]    //   different suit
        [InlineData("TS", "JH", "QH", "KH", "AH", false)]
        [InlineData("9H", "JH", "QH", "KH", "AH", false)]
        [InlineData("QH", "JH", "TH", "KH", "AH", true)]    //  out of order
        public void CanFindStraightFlush(string data1, string data2, string data3, string data4, string data5, bool isFlush)
        {
            //  Arrange  
            var StraightFlushProcessor = new StraightFlushHandRule();
            var hand = new List<Card> {
            new Card(data1),
            new Card(data2),
            new Card(data3),
            new Card(data4),
            new Card(data5)
            };

            //  Act
            var isThereAStraightFlush = StraightFlushProcessor.isTrue(hand);

            //  Assert 
            Assert.True(isThereAStraightFlush == isFlush);
        }
    }

}