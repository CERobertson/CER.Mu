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
            this.TokenRegex = new Dictionary<string, string>();
        }

        public IEnumerable<Token> Scan(string message)
        {
            var tokenToMatch = this.TokenToRegex();
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
                        yield return new Token { Name = possible_match.Key, Value = result.Value };
                        if (result.Length == 0)
                        {
                            throw new ScanningException(string.Format(
                                @"{0} has matched with a length of zero. Regex(""{1}"")",
                                possible_match.Key,
                                this.TokenRegex[possible_match.Key]));
                        }
                        index = result.Length;
                    }
                }
                if (!match)
                {
                    index = 1;
                }
                if (message.Length - index < 1)
                {
                    break;
                }
                message = message.Substring(index);
            }
        }

        public Dictionary<string, string> TokenRegex { get; set; }
        private Dictionary<string, Regex> TokenToRegex()
        {
            var dictionary = new Dictionary<string, Regex>();
            foreach (var tokenToRegexString in this.TokenRegex)
            {
                var regex = tokenToRegexString.Value.StartsWith("^")
                    ? tokenToRegexString.Value
                    : "^" + tokenToRegexString.Value;
                dictionary[tokenToRegexString.Key] = new Regex(regex);
            }
            return dictionary;
        }
    }

    public class Token
    {
        public string Value { get; set; }
        public string Name { get; set; }
    }

    public class ScanningException : Exception
    {
        public ScanningException(string message) : base(message) { }
    }
}

