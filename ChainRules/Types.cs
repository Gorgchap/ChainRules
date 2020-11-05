namespace ChainRules
{
    class ChainRule
    {
        public string Left { get; }

        public string Right { get; set; }

        public ChainRule(string left, string right)
        {
            Left = left;
            Right = right;
        }

        public override string ToString() => $"{Left}->{Right}";
    }
        

    class Symbol
    {
        public string Name { get; set; }

        public Symbol(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            else if (ReferenceEquals(this, obj))
                return true;
            else
                return Name == (obj as Symbol).Name;
        }

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => Name;
    }
}
