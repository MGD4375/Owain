
using Poker;

interface IHandRule
{
    Score Score { get; }

    bool isTrue(List<Card> hand);
}
