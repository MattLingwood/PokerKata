using PokerKata.Enums;

namespace PokerKata
{
    public class Card
    {
        public Suit Suit { get; }
        public Value Value { get; }
        public Card(Suit suit, Value value)
        {
            Suit = suit;
            Value = value;
        }

        public bool Equals(Card card)
        {
            return Value == card.Value;
        }
    }
}