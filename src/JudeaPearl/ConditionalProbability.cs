namespace CER.JudeaPearl
{
    using CER.Runtime.Serialization;
    using System.Collections.Generic;
    using System.Linq;

    public class ConditionalProbability : List<List<string>>
    {
        public ConditionalProbability() : base() { }
        public ConditionalProbability(string json, string quote = "'") : this(json.ParseJsonToSimple<ConditionalProbability>(quote)) { }
        public ConditionalProbability(List<List<string>> template) : base(template) { }
        public decimal[][] ToMatrix()
        {
            int i = 0;
            var matrix = new decimal[this.Count][];
            foreach (var list in this)
            {
                int j = 0;
                var row = new decimal[list.Count];
                foreach (var ij in list)
                {
                    row[j] = decimal.Parse(ij);
                    j++;
                }
                matrix[i] = row;
                i++;
            }
            return matrix;
        }
    }
}
