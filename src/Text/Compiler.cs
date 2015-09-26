namespace CER.Text
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using a = Microsoft.Office.Interop.Excel;

    public class Compiler
    {
        public static IEnumerable<string> EnumerateOrdinals(int limit, string input = @"C:\Users\Cory\SkyDrive\Ordinal.txt", string output = @"C:\Users\Cory\SkyDrive\Ordinal.xlsx")
        {
            string[] base_enuneration;
            using (TextReader reader = File.OpenText(input))
            {
                base_enuneration = reader.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            }
            var width = base_enuneration.Length + 1;
            var exponents = Compiler.CalculateExponents(limit, width);
            while (limit > 0)
            {
                yield return string.Format("{0},{1}", limit, Compiler.CreateNumberGlyph(base_enuneration, exponents));
                limit--;
                exponents = Compiler.SubtractOneFromExponents(exponents, width);
            }
        }

        private static string CreateNumberGlyph(string[] enumeration, int[] exponents)
        {
            string result = string.Empty;
            for (int i = 0; i < exponents.Length; i++)
            {
                if (exponents[i] == 0  && i != 0)
                {
                    result += "0";
                }
                else if(exponents[i] != 0)
                {
                    result += enumeration[exponents[i] - 1];
                }
            }
            return result;
        }
        public static int[] SubtractOneFromExponents(int[] exponents, int b)
        {
            if (exponents[0] == 0 && exponents.Length == 1)
            {
                throw new ArgumentException("exponents[0] must not be zero");
            }
            else if (exponents[0] == 0)
            {
                int[] smaller_exponents = new int[exponents.Length - 1];
                for (int i = 0; i < exponents.Length - 1; i++)
                {
                    smaller_exponents[i] = exponents[i + 1];
                }
                return Compiler.SubtractOneFromExponents(smaller_exponents, b);
            }
            for (int j = exponents.Length - 1; j >= 0; j--)
            {
                if (exponents[j] == 0)
                {
                    exponents[j] = b - 1;
                }
                else
                {
                    exponents[j] = exponents[j] - 1;
                    j = -1;
                }
            }
            return exponents;
        }

        public static int[] CalculateExponents(int x, int b)
        {
            int[] exponents;
            Compiler.calc_exp(x, b, 0, out exponents);
            return exponents;
        }
        public static void calc_exp(int x, int b, int depth, out int[] exponents)
        {
            depth++;
            var exponent = x / b;
            if (exponent != 0)
            {
                Compiler.calc_exp(exponent, b, depth, out exponents);
            }
            else
            {
                exponents = new int[depth];
            }
            var index = exponents.Length - depth;
            exponents[index] = x % b;
        }

        public Compiler()
        {
            this.Regex = new Dictionary<object, string>();
            this.Rules = new List<Rule>();
        }

        public IEnumerable<Token> Scan(string message)
        {
            var tokenToMatch = this.ConstructRegex();
            while (true)
            {
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
            for (int i = 0; i < tokens.Length; i++)
            {
                this.RuleSequence[i] = new Rule((int)tokens[i]);
            }
        }
        public Rule() { }
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

