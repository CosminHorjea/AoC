using System.Collections.Immutable;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Nodes;

public class Day7 : Solution
{
    const string CARDS_STRENGTHS = "23456789TJQKA";
    const string CARDS_STRENGTHS_PART_2 = "J23456789TQKA";

    public class Hand
    {
        public enum HAND_TYPE
        {
            HIGH_CARD = 0,
            ONE_PAIR = 1,
            TWO_PAIR = 2,
            THREE_KIND = 3,
            FULL_HOUSE = 4,
            FOUR_KIND = 5,
            FIVE_KIND = 6,
        }
        public string cards { get; set; }
        public BigInteger bet { get; set; }

        public Hand(string line)
        {
            var items = line.Split(" ");
            cards = items[0];
            bet = BigInteger.Parse(items[1]);
        }
        public Hand(string cards, int bet)
        {
            this.cards = cards;
            this.bet = bet;
        }
        public HAND_TYPE GetHandType()
        {
            Dictionary<char, int> freq = cards.GroupBy(char.ToUpper).ToDictionary(g => g.Key, g => g.Count());
            Dictionary<int, int> pairs = freq.Values.GroupBy(a => a).ToDictionary(g => g.Key, g => g.Count());
            List<int> highestPairs = pairs.Keys.OrderByDescending(k => k).ToList();
            if (highestPairs[0] >= 4)
            {
                return highestPairs[0] == 5 ? HAND_TYPE.FIVE_KIND : HAND_TYPE.FOUR_KIND;
            }
            if (highestPairs[0] == 3 && highestPairs[1] == 2)
            {
                return HAND_TYPE.FULL_HOUSE;
            }
            if (highestPairs[0] == 3)
            {
                return HAND_TYPE.THREE_KIND;
            }
            if (highestPairs[0] == 2 && pairs[2] == 2)
            {
                return HAND_TYPE.TWO_PAIR;
            }
            return highestPairs[0] == 2 ? HAND_TYPE.ONE_PAIR : HAND_TYPE.HIGH_CARD;
        }

        public HAND_TYPE GetHandTypePart2()
        {
            var cardsComparer = Comparer<char>.Create((key1, key2) =>
                {
                    return CARDS_STRENGTHS_PART_2.IndexOf(key1) - CARDS_STRENGTHS_PART_2.IndexOf(key2);
                });
            Dictionary<char, int> freq = cards.GroupBy(char.ToUpper)
                                                .ToDictionary(g => g.Key, g => g.Count())
                                                .OrderByDescending(a => a.Value)
                                                .ThenByDescending(a => a.Key, cardsComparer)
                                                .ToDictionary();
            var key_highest_value = freq.First().Key;
            if (freq.ContainsKey('J') && freq['J'] == 5)
            {
                key_highest_value = 'A';
            }
            else
            {
                key_highest_value = freq.First(a => a.Key != 'J').Key;
            }
            var temp_cards = cards.Replace('J', key_highest_value);
            return new Hand(temp_cards, 1).GetHandType();
        }
        public int BasicCompare(Hand other, string alphabet)
        {
            for (int i = 0; i < 5; i++)
            {
                if (other.cards[i] == cards[i])
                {
                    continue;
                }
                if (alphabet.IndexOf(other.cards[i]) > alphabet.IndexOf(cards[i]))
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            return 0;
        }
    }
    string Solution.Part1()
    {
        var contents = File.ReadLines("Inputs/Day7.in");
        List<Hand> hands = contents.Select(line => new Hand(line)).ToList();
        // hands.ForEach(h => Console.WriteLine(h.GetHandType()));
        Console.WriteLine(hands.Count);
        hands.Sort((a, b) =>
        {
            if (a.GetHandType() == b.GetHandType())
            {
                return a.BasicCompare(b, CARDS_STRENGTHS);
            }
            return a.GetHandType() - b.GetHandType();
        });
        // Console.WriteLine(JsonSerializer.Serialize(hands.Select(h => $"{h.cards} - {h.GetHandType()} - {h.bet}")));
        var scores = hands.Select((h, i) => h.bet * (i + 1)).Aggregate((a, x) => a + x);
        return $"{scores}";
    }
    // 250811294 too low
    // 251058093 

    string Solution.Part2()
    {
        var contents = File.ReadLines("Inputs/Day7.in");
        List<Hand> hands = contents.Select(line => new Hand(line)).ToList();
        // Console.WriteLine(hands.Count);
        hands.Sort((a, b) =>
        {
            if (a.GetHandTypePart2() == b.GetHandTypePart2())
            {
                return a.BasicCompare(b, CARDS_STRENGTHS_PART_2);
            }
            return a.GetHandTypePart2() - b.GetHandTypePart2();
        });
        // Console.WriteLine(JsonSerializer.Serialize(hands.Select(h => $"{h.cards} - {h.GetHandTypePart2()} - {h.bet}")));
        var scores = hands.Select((h, i) => h.bet * (i + 1)).Aggregate((a, x) => a + x);
        return $"{scores}";
    }
    // 251058093 -- high
    // 249694980 -- low
    // 249698551 -- low
    // 249781879 
    // 249842266 -- not right
    // 249829015 -- not right
    // 250816616 -- not right
}