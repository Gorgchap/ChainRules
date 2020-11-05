using System.Collections.Generic;
using System.Linq;

namespace ChainRules
{
    class Chain
    {
        public List<Symbol> Symbols { get; set; }

        public Chain()
        {
            Symbols = new List<Symbol>();
        }

        public override string ToString() => string.Join("", Symbols.Select(item => item.Name));
    }
}
