using System.Runtime.Intrinsics.X86;
using System.Threading;

namespace Owain.PokerHands.Test
{
    public class UnitTestRoyalFlush
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

    public class UnitTestStraightFlush
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

    public class UnitTestFourOfAKind
    {
        [Theory]
        [InlineData("KH", "KD", "KS", "KC", "AH", true)]    //   Four Kings
        [InlineData("4H", "4D", "4S", "4C", "2S", true)]    //   Four 4s
        [InlineData("TS", "JH", "QH", "KH", "AH", false)]      
        [InlineData("9H", "JH", "QH", "KH", "AH", false)]
        [InlineData("QH", "QS", "TH", "QC", "QD", true)]    //  Split Four Queens
        public void CanFindFourOfAKind(string data1, string data2, string data3, string data4, string data5, bool isFourOfAKind)
        {
            //  Arrange  
            var FourOfAKindProcessor = new FourOfAKindHandRule();
            var hand = new List<Card> {
            new Card(data1),
            new Card(data2),
            new Card(data3),
            new Card(data4),
            new Card(data5)
            };

            //  Act
            var isThereFourOfAKind = FourOfAKindProcessor.isTrue(hand);

            //  Assert 
            Assert.True(isThereFourOfAKind == isFourOfAKind);
        }
    }

    public class UnitTestFullHouse
    {
        [Theory]                                            //   Full House = Three Of A Kind And A Pair

        [InlineData("KH", "2D", "5C", "JS", "AH", false)]   //   no Full House           //faild
        [InlineData("KH", "KD", "4S", "3C", "KS", false)]   //   3.Kings no.Pair
        [InlineData("TS", "4S", "QD", "KH", "4H", false)]   //   no.3  pair.4s           
        [InlineData("QS", "KS", "QD", "KH", "KD", true)]    //   3.kings pair.queens     //faild
        [InlineData("2S", "AS", "2H", "2C", "AD", true)]    //   3.2s pair.aces
        public void CanFindFullHouse(string data1, string data2, string data3, string data4, string data5, bool isFullHouse)
        {
            //  Arrange  
            var FullHouseProcessor = new FullHouseHandRule();
            var hand = new List<Card> {
            new Card(data1),
            new Card(data2),
            new Card(data3),
            new Card(data4),
            new Card(data5)
            };

            //  Act
            var isThereFullHouse = FullHouseProcessor.isTrue(hand);

            //  Assert 
            Assert.True(isThereFullHouse == isFullHouse);
        }
    }

    public class UnitTestFlush
    {
        [Theory]                                            //   Flush: All cards of the same suit.
        [InlineData("KH", "2D", "5C", "JS", "AH", false)]   //   No Full House         
        [InlineData("KD", "9D", "4D", "3D", "TD", true)]   //   All Dimonds
        [InlineData("TS", "4S", "QS", "KS", "4S", true)]   //   All Spades
        [InlineData("9H", "4H", "QH", "2H", "KH", true)]    //   All Harts    
        [InlineData("2C", "AC", "9C", "7C", "TC", true)]    //   All Clubs
        public void CanFindFlush(string data1, string data2, string data3, string data4, string data5, bool isFlush)
        {
            //  Arrange  
            var FlushProcessor = new FlushHandRule();
            var hand = new List<Card> {
            new Card(data1),
            new Card(data2),
            new Card(data3),
            new Card(data4),
            new Card(data5)
            };

            //  Act
            var isThereFlush = FlushProcessor.isTrue(hand);

            //  Assert 
            Assert.True(isThereFlush == isFlush);
        }
    }

    public class UnitTestStraight
    {
        [Theory]                                            //Straight: All cards are consecutive values.
     
        [InlineData("KH", "2D", "5C", "JS", "AH", false)]   //   Not a Straight        
        [InlineData("5D", "6D", "7D", "8D", "9D", true)]    //   Straight in order
        [InlineData("7S", "8S", "6S", "9S", "5S", true)]    //   Straight Out Of Order
        [InlineData("TH", "JH", "QH", "KH", "AH", true)]    //   Straight Ace High
        [InlineData("AC", "2C", "3C", "4C", "5C", true)]    //   Straight Ace Low
        public void CanFindStraight(string data1, string data2, string data3, string data4, string data5, bool isStraight)
        {
            //  Arrange  
            var StraightProcessor = new StraightHandRule();
            var hand = new List<Card> {
            new Card(data1),
            new Card(data2),
            new Card(data3),
            new Card(data4),
            new Card(data5)
            };

            //  Act
            var isThereAStraight = StraightProcessor.isTrue(hand);

            //  Assert 
            Assert.True(isThereAStraight == isStraight);
        }
    }

    public class UnitTestThreeOfAKind
    {
        [Theory]                                            //Three of a Kind: Three cards of the same value.
        [InlineData("KH", "2D", "5C", "JS", "AH", false)]   //   No Cards Same      
        [InlineData("KH", "2D", "5C", "AS", "AH", false)]   //   Two Cards Same
        [InlineData("KH", "2D", "KC", "KS", "AH", true)]    //   Three Of A Kind, Out Of Order
        
        public void CanFindThreeOfAKind(string data1, string data2, string data3, string data4, string data5, bool isThreeOfAKind)
        {
            //  Arrange  
            var ThreeOfAKindProcessor = new ThreeOfAKindHandRule();
            var hand = new List<Card> {
            new Card(data1),
            new Card(data2),
            new Card(data3),
            new Card(data4),
            new Card(data5)
            };

            //  Act
            var isThereThreeOfAKind = ThreeOfAKindProcessor.isTrue(hand);

            //  Assert 
            Assert.True(isThereThreeOfAKind == isThreeOfAKind);
        }
    }

    public class UnitTestTwoPairs
    {
        [Theory]                                            //Two Pairs: Two different pairs.
        [InlineData("KH", "2D", "5C", "JS", "AH", false)]   //   No Pairs       
        [InlineData("5D", "KH", "7D", "KD", "9S", false)]   //   One Pair
        [InlineData("7S", "7H", "6D", "6S", "AH", true)]    //   Two Pairs In Order 
        [InlineData("TH", "JH", "AS", "TD", "AH", true)]    //   Two Pairs Out of Order
        
        public void CanFindTwoPairs(string data1, string data2, string data3, string data4, string data5, bool isTwoPairs)
        {
            //  Arrange  
            var TwoPairsProcessor = new TwoPairsHandRule();
            var hand = new List<Card> {
            new Card(data1),
            new Card(data2),
            new Card(data3),
            new Card(data4),
            new Card(data5)
            };

            //  Act
            var isThereTwoPairs = TwoPairsProcessor.isTrue(hand);

            //  Assert 
            Assert.True(isThereTwoPairs == isTwoPairs);
        }
    }

    public class UnitTestOnePair
    {
        [Theory]                                            //One Pair: Two Cards Of TheSameValue.
        [InlineData("KH", "2D", "5C", "JS", "AH", false)]   //   No Pair       
        [InlineData("KS", "KS", "6D", "6S", "AH", true)]    //   Pair In Order 
        [InlineData("TC", "JH", "AS", "TD", "AH", true)]    //   Pair Out of Order

        public void CanFindOnePair(string data1, string data2, string data3, string data4, string data5, bool isPair)
        {
            //  Arrange  
            var PairProcessor = new OnePairHandRule();
            var hand = new List<Card> {
            new Card(data1),
            new Card(data2),
            new Card(data3),
            new Card(data4),
            new Card(data5)
            };

            //  Act
            var isThereAPair = PairProcessor.isTrue(hand);

            //  Assert 
            Assert.True(isThereAPair == isPair);
        }
    }
}






