namespace CER.JudeaPearl
{
    using CER.Runtime.Serialization;
    using System.Collections.Generic;

    public class ConditionalProbability : List<List<string>>
    {
        public ConditionalProbability() : base() { }
        public ConditionalProbability(string json, string quote = "'") : this(json.ParseJsonToSimple<ConditionalProbability>(quote)) { }
        public ConditionalProbability(List<List<string>> template) : base(template) { }
    }
}
