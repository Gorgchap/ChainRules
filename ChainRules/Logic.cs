using System.Collections.Generic;

namespace ChainRules
{
    class Logic
    {
        private readonly Grammar grammar;
        private readonly List<Symbol> terms;
        private readonly List<Symbol> nonTerms;

        public Logic(string terminals, string nonTerminals, string rules)
        {
            grammar = new Grammar();
            terms = new List<Symbol>();
            nonTerms = new List<Symbol>();
            Add(terminals, "terminal");
            Add(nonTerminals, "non-terminal");
            Add(rules, "rules");
        }

        private void Add(string symbols, string type)
        {
            foreach (string str in symbols.Split(new char[] { ',', '\n' }, System.StringSplitOptions.RemoveEmptyEntries))
                if (type == "terminal")
                    terms.Add(new Symbol(str));
                else if (type == "non-terminal")
                    nonTerms.Add(new Symbol(str));
                else
                    AddRule(str);
        }

        private Symbol GetSymbol(string name, bool isTerm) => (isTerm ? terms : nonTerms).Find(e => e.Name == name.Trim());

        private void AddRule(string rule)
        {
            if (rule.Length < 1)
                return;

            string[] str = rule.Split(new char[] { '-', '>' }, System.StringSplitOptions.RemoveEmptyEntries);
            if (str.Length < 2 || GetSymbol(str[0], false) == null)
                return;

            List<Chain> rules = new List<Chain>();
            foreach (string subRule in str[1].Split(new char[] { '|' }, System.StringSplitOptions.RemoveEmptyEntries))
            {
                Chain chain = new Chain();
                foreach (char symbol in subRule)
                {
                    Symbol a = GetSymbol(symbol.ToString(), true);
                    Symbol b = GetSymbol(symbol.ToString(), false);
                    if (a != null || b != null)
                        chain.Symbols.Add(a ?? b);
                    else if (symbol == 'ε' && subRule.Length == 1)
                        chain.Symbols.Add(new Symbol("ε"));
                }
                rules.Add(chain);
            }
            grammar[GetSymbol(str[0], false)] = rules;
        }

        public List<string> GetRules() => grammar.GetRules();

        public List<string> Transform()
        {
            List<ChainRule> chainRules = new List<ChainRule>();
            Grammar result = new Grammar();
            foreach (Symbol symbol in nonTerms)
                grammar[symbol]?.ForEach(chain =>
                {
                    Symbol s = new Symbol(chain.ToString());
                    if (nonTerms.Contains(s) && !symbol.Equals(s))
                        chainRules.Add(new ChainRule(symbol.Name, s.Name));
                    else
                        result[symbol] = new List<Chain>() { chain };
                });

            TransformRules(chainRules).ForEach(rule =>
            {
                List<Chain> foundRules = result[new Symbol(rule.Right)];
                result[new Symbol(rule.Left)] = foundRules ?? new List<Chain>();
            });

            return result.GetRules();
        }

        private List<ChainRule> TransformRules(List<ChainRule> rules)
        {
            List<ChainRule> result = new List<ChainRule>();
            foreach (ChainRule rule in rules)
            {
                ChainRule foundRule = rules.Find(item => item.Left == rule.Right);
                result.Add(foundRule == null ? rule : new ChainRule(rule.Left, foundRule.Right));
            }
            
            foreach (ChainRule rule in rules)
                if (rules.Exists(item => item.Left == rule.Right))
                    return TransformRules(new List<ChainRule>(result));
            return result;
        }
    }
}
