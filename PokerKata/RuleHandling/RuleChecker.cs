using System.Collections.Generic;
using System.Linq;
using PokerKata.Enums;

namespace PokerKata.RuleHandling
{
    public class RuleChecker : IRuleChecker
    {
        public CardRules CheckMetRules(List<Card> cardsToProcess)
        {
            cardsToProcess = cardsToProcess.OrderBy(x => (int) x.Value).ToList();

            var duplicateMap = new Dictionary<Value, int>();
            var straightCount = 0;
            var flush = false;
            var straight = false;
            var royal = false;

            for (var i = 0; i < cardsToProcess.Count; i++)
            {
                var card = cardsToProcess[i];
                if (!duplicateMap.ContainsKey(card.Value))
                {
                    duplicateMap[card.Value] = 0;
                }
                duplicateMap[card.Value]++;

                if (i > 0)
                {
                    var previousCard = cardsToProcess[i - 1];
                    straightCount = (int) card.Value == (int) (previousCard.Value + 1) ? straightCount + 1 : 0;
                    straight = straightCount == 4;
                    if (straightCount == 3 && cardsToProcess[0].Value == Value.Ace &&
                        cardsToProcess[4].Value == Value.King)
                    {
                        royal = true;
                    }
                    flush = card.Suit.Equals(previousCard.Suit);
                }
            }

            return CalculateMetRule(flush, straight, royal, duplicateMap);
        }

        private static CardRules CalculateMetRule(bool flush, bool straight, bool royal, Dictionary<Value, int> duplicateMap)
        {
            var pairCount = 0;
            var tripleCount = 0;
            foreach (var kvPairs in duplicateMap)
            {
                switch (kvPairs.Value)
                {
                    case 2:
                        pairCount++;
                        break;
                    case 3:
                        tripleCount++;
                        break;
                    case 4:
                        return CardRules.FourOfAKind;
                }
            }

            if (tripleCount == 1 && pairCount == 1)
            {
                return CardRules.FullHouse;
            }
            if (flush)
            {
                if (royal)
                {
                    return CardRules.RoyalFlush;
                }
                return straight ? CardRules.StraightFlush : CardRules.Flush;
            }

            if (straight)
            {
                return CardRules.Straight;
            }
            if (tripleCount > 0)
            {
                return CardRules.ThreeOfAKind;
            }
            if (pairCount == 2)
            {
                return CardRules.TwoPair;
            }

            return pairCount == 1 ? CardRules.Pair : CardRules.HighCard;
        }
    }
}