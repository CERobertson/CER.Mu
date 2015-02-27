namespace CER.Text
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Compiler
    {
        public Compiler()
        {
            this.Regex = new Dictionary<object, string>();
            this.Rules= new List<Rule>();
        }

        public IEnumerable<Token> Scan(string message)
        {
            var tokenToMatch = this.ConstructRegex();
            while (true)
            {
                var index = 0;
                bool match = false;
                foreach (var possible_match in tokenToMatch)
                {
                    match = possible_match.Value.IsMatch(message);
                    if (match)
                    {
                        var result = possible_match.Value.Match(message);
                        yield return new Token { Id = possible_match.Key, Value = result.Value };
                        if (result.Length == 0)
                        {
                            throw new ScanningException(string.Format(
                                @"{0} has matched with a length of zero. Regex(""{1}"")",
                                possible_match.Key,
                                this.Regex[possible_match.Key]));
                        }
                        message = message.Substring(result.Length);
                        break;
                    }
                }
                if (!match)
                {
                    if (message.Length - 1 < 1)
                    {
                        break;
                    }
                    message = message.Substring(1);
                }
            }
        }
        
        public Dictionary<object, string> Regex { get; set; }
        public List<Rule> Rules { get; set; }

        private Dictionary<int, Regex> ConstructRegex()
        {
            var dictionary = new Dictionary<int, Regex>();
            foreach (var regexString in this.Regex)
            {
                var regex = regexString.Value.StartsWith("^")
                    ? regexString.Value
                    : "^" + regexString.Value;
                dictionary[(int)regexString.Key] = new Regex(regex);
            }
            return dictionary;
        }
    }
    public class Rule
    {
        public Rule(object id, params object[] tokens)
        {
            this.Id = (int)id;
            this.RuleSequence = new Rule[tokens.Length];
            for(int i=0; i<tokens.Length; i++)
            {
                this.RuleSequence[i] = new Rule((int)tokens[i]);
            }
        }
        public Rule(){}
        private Rule(int id)
        {
            this.Id = id;
        }
        public int Id { get; private set; }
        public Rule[] RuleSequence { get; set; }
        public Action Execute { get; set; }
    }

    public class Token
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class ScanningException : Exception
    {
        public ScanningException(string message) : base(message) { }
    }
}

