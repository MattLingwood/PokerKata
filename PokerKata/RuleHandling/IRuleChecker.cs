using System.Collections.Generic;
using PokerKata.Enums;

namespace PokerKata.RuleHandling
{
    public interface IRuleChecker
    {
        CardRules CheckMetRules(List<Card> cardsToProcess);
    }
}