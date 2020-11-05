using System.Collections.Generic;
using System.Linq;

namespace ChainRules
{
    class Rule
    {
        public Symbol Key { get; set; }

        public List<Chain> Rules { get; set; }

        public Rule(Symbol key, List<Chain> rules)
        {
            Key = key;
            Rules = new List<Chain>();
            Rules.AddRange(rules);
        }

        public override string ToString()
        {
            string rule = "";
            for (int i = 0; i < Rules.Count; i++)
                rule += string.Join("", Rules[i].Symbols.Select(e => e.Name)) + (i < Rules.Count - 1 ? "|" : "");
            return $"{Key.Name}->{rule}";
        }
    }
}
