using System.Collections.Generic;
using NSubstitute;
using PokerKata;
using PokerKata.Enums;
using PokerKata.Exceptions;
using PokerKata.RuleHandling;
using Shouldly;
using Xunit;

namespace PokerKataTests
{
    public class CardHandlerTest
    {
        private readonly IRuleChecker _mockedRuleChecker;

        public CardHandlerTest()
        {
            _mockedRuleChecker = Substitute.For<IRuleChecker>();
        }

        [Fact]
        public void WhenThereAreNotFiveCards_ThrowNotEnoughCardsException()
        {
            var cardHandler = new CardHandler(_mockedRuleChecker);
            var cardsToBeProcessed = new List<Card>
            {
                new Card(Suit.Heart, Value.Ace)
            };
            
            Assert.Throws<NotEnoughCardsException>(() => cardHandler.ProcessCards(cardsToBeProcessed));
        }

        [Fact]
        public void WhenFiveValidCardsAreProcessed_ApplicableRuleIsReturned()
        {
            var cardHandler = new CardHandler(_mockedRuleChecker);
            var cardsToBeProcessed = new List<Card>
            {
                new Card(Suit.Heart, Value.Ace),
                new Card(Suit.Heart, Value.Ace),
                new Card(Suit.Heart, Value.Ace),
                new Card(Suit.Heart, Value.Ace),
                new Card(Suit.Heart, Value.Ace)
            };
            _mockedRuleChecker.CheckMetRules(cardsToBeProcessed).Returns(CardRules.HighCard);

            var rule = cardHandler.ProcessCards(cardsToBeProcessed);

            _mockedRuleChecker.Received(1).CheckMetRules(cardsToBeProcessed);
            rule.ShouldBe(CardRules.HighCard);
        }
    }
}
