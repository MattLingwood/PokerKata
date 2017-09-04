using System.Collections.Generic;
using PokerKata.Enums;
using PokerKata.Exceptions;
using PokerKata.RuleHandling;

namespace PokerKata
{
    public class CardHandler
    {
        private readonly IRuleChecker _ruleChecker;

        public CardHandler(IRuleChecker ruleChecker)
        {
            _ruleChecker = ruleChecker;
        }

        public CardRules ProcessCards(List<Card> cards)
        {
            if (cards.Count != 5)
            {
                throw new NotEnoughCardsException();
            }
            var metRule = _ruleChecker.CheckMetRules(cards);

            return metRule;
        }
    }
}