using System.Collections.Generic;
using System.Linq;

namespace ChainRules
{
    class Grammar
    {
        public List<Rule> Rules { get; }

        public Grammar()
        {
            Rules = new List<Rule>();
        }

        public List<string> GetRules()
        {
            List<string> result = new List<string>();
            if (Rules.Count > 0)
                result.AddRange(Rules.Select(item => item.ToString()));
            else
                result.Add("No rules");
            return result;
        }

        public List<Chain> this[Symbol key]
        {
            get => Rules.Find(item => item.Key.Equals(key))?.Rules;
            set
            {
                Rule rule = Rules.Find(item => item.Key.Equals(key));
                if (rule != null)
                    rule.Rules.AddRange(value);
                else
                    Rules.Add(new Rule(key, value));

            }
        }
    }
}
