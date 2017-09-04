using System.Collections.Generic;
using PokerKata;
using PokerKata.Enums;
using PokerKata.RuleHandling;
using Shouldly;
using Xunit;

namespace PokerKataTests.RuleHandling
{
    public class RuleCheckerTest
    {
        private RuleChecker _ruleChecker;

        public RuleCheckerTest()
        {
            _ruleChecker = new RuleChecker();
        }

        [Fact]
        public void WhenNoRulesAreMet_HighCardIsReturned()
        {
            var cardsToBeProcessed = new List<Card>
            {
                new Card(Suit.Heart, Value.Ace),
                new Card(Suit.Spade, Value.Two),
                new Card(Suit.Heart, Value.Three),
                new Card(Suit.Spade, Value.Four),
                new Card(Suit.Heart, Value.Six)
            };

            var metRule = _ruleChecker.CheckMetRules(cardsToBeProcessed);

            metRule.ShouldBe(CardRules.HighCard);
        }

        [Fact]
        public void WhenThereIsASinglePairOfCards_ReturnsPair()
        {
            var cardsToBeProcessed = new List<Card>
            {
                new Card(Suit.Heart, Value.Ace),
                new Card(Suit.Club, Value.Ace),
                new Card(Suit.Diamond, Value.Three),
                new Card(Suit.Spade, Value.Two),
                new Card(Suit.Heart, Value.Five)
            };

            var metRule = _ruleChecker.CheckMetRules(cardsToBeProcessed);

            metRule.ShouldBe(CardRules.Pair);
        }

        [Fact]
        public void WhenThereAreTwoPairsOfCards_ReturnsTwoPair()
        {
            var cardsToBeProcessed = new List<Card>
            {
                new Card(Suit.Heart, Value.Ace),
                new Card(Suit.Club, Value.Three),
                new Card(Suit.Diamond, Value.Two),
                new Card(Suit.Spade, Value.Three),
                new Card(Suit.Heart, Value.Two)
            };

            var metRule = _ruleChecker.CheckMetRules(cardsToBeProcessed);

            metRule.ShouldBe(CardRules.TwoPair);
        }

        [Fact]
        public void WhenThreeOfTheCardsAreTheSameValue_ReturnsThreeOfAKind()
        {
            var cardsToBeProcessed = new List<Card>
            {
                new Card(Suit.Heart, Value.Ace),
                new Card(Suit.Club, Value.Ace),
                new Card(Suit.Diamond, Value.Ace),
                new Card(Suit.Spade, Value.Two),
                new Card(Suit.Heart, Value.Three)
            };

            var metRule = _ruleChecker.CheckMetRules(cardsToBeProcessed);

            metRule.ShouldBe(CardRules.ThreeOfAKind);
        }

        [Fact]
        public void WhenAllTheCardValuesAreInOrder_ReturnsStraight()
        {
            var cardsToBeProcessed = new List<Card>
            {
                new Card(Suit.Heart, Value.Ace),
                new Card(Suit.Club, Value.Two),
                new Card(Suit.Diamond, Value.Three),
                new Card(Suit.Spade, Value.Four),
                new Card(Suit.Heart, Value.Five)
            };

            var metRule = _ruleChecker.CheckMetRules(cardsToBeProcessed);

            metRule.ShouldBe(CardRules.Straight);
        }

        [Fact]
        public void WhenAllCardValuesAreSequential_ReturnsStraight()
        {
            var cardsToBeProcessed = new List<Card>
            {
                new Card(Suit.Heart, Value.Ace),
                new Card(Suit.Club, Value.Three),
                new Card(Suit.Diamond, Value.Five),
                new Card(Suit.Spade, Value.Four),
                new Card(Suit.Heart, Value.Two)
            };

            var metRule = _ruleChecker.CheckMetRules(cardsToBeProcessed);

            metRule.ShouldBe(CardRules.Straight);
        }

        [Fact]
        public void WhenAllTheCardSuitsAreTheSame_ReturnsFlush()
        {
            var cardsToBeProcessed = new List<Card>
            {
                new Card(Suit.Heart, Value.Ace),
                new Card(Suit.Heart, Value.Three),
                new Card(Suit.Heart, Value.Five),
                new Card(Suit.Heart, Value.Seven),
                new Card(Suit.Heart, Value.Nine)
            };

            var metRule = _ruleChecker.CheckMetRules(cardsToBeProcessed);

            metRule.ShouldBe(CardRules.Flush);
        }

        [Fact]
        public void WhenTheIsAPairAndTripleOfCards_ReturnsFullHouse()
        {
            var cardsToBeProcessed = new List<Card>
            {
                new Card(Suit.Heart, Value.Ace),
                new Card(Suit.Heart, Value.Ace),
                new Card(Suit.Heart, Value.Two),
                new Card(Suit.Heart, Value.Two),
                new Card(Suit.Heart, Value.Two)
            };

            var metRule = _ruleChecker.CheckMetRules(cardsToBeProcessed);

            metRule.ShouldBe(CardRules.FullHouse);
        }

        [Fact]
        public void WhenFourOfTheCardValuesAreTheSame_ReturnsFourOfAKind()
        {
            var cardsToBeProcessed = new List<Card>
            {
                new Card(Suit.Heart, Value.Ace),
                new Card(Suit.Heart, Value.Two),
                new Card(Suit.Heart, Value.Two),
                new Card(Suit.Heart, Value.Two),
                new Card(Suit.Heart, Value.Two)
            };

            var metRule = _ruleChecker.CheckMetRules(cardsToBeProcessed);

            metRule.ShouldBe(CardRules.FourOfAKind);
        }

        [Fact]
        public void WhenTheCardSuitsAreTheSame_AndValueAreSequential_ReturnsStraightFlush()
        {
            var cardsToBeProcessed = new List<Card>
            {
                new Card(Suit.Heart, Value.Ace),
                new Card(Suit.Heart, Value.Two),
                new Card(Suit.Heart, Value.Three),
                new Card(Suit.Heart, Value.Four),
                new Card(Suit.Heart, Value.Five)
            };

            var metRule = _ruleChecker.CheckMetRules(cardsToBeProcessed);

            metRule.ShouldBe(CardRules.StraightFlush);
        }

        [Fact]
        public void WhenTheCardSuitsAreTheSame_AndValueAreSequentialFromTenToAce_ReturnsRoyalFlush()
        {
            var cardsToBeProcessed = new List<Card>
            {
                new Card(Suit.Heart, Value.Ten),
                new Card(Suit.Heart, Value.Jack),
                new Card(Suit.Heart, Value.Queen),
                new Card(Suit.Heart, Value.King),
                new Card(Suit.Heart, Value.Ace)
            };

            var metRule = _ruleChecker.CheckMetRules(cardsToBeProcessed);

            metRule.ShouldBe(CardRules.RoyalFlush);
        }

        [Fact]
        public void WhenTheCardSuitsAreTheSame_AndValueAreNotSequentialFromTenToAce_ReturnsRoyalFlush()
        {
            var cardsToBeProcessed = new List<Card>
            {
                new Card(Suit.Heart, Value.Ten),
                new Card(Suit.Heart, Value.Jack),
                new Card(Suit.Heart, Value.King),
                new Card(Suit.Heart, Value.King),
                new Card(Suit.Heart, Value.Ace)
            };

            var metRule = _ruleChecker.CheckMetRules(cardsToBeProcessed);

            metRule.ShouldNotBe(CardRules.RoyalFlush);
        }
    }
}