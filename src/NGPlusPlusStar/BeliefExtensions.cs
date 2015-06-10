namespace NGPlusPlusStar
{
    using CER.JudeaPearl;
    using CER.JudeaPearl.CausalNetwork;
    using CER.Rpg;
    using System.Collections.Generic;
    using System.Linq;

    public static class BeliefExtensions
    {
        public static ConditionalProbability SetHypotheses(this belief b, string json)
        {
            var condition_probablity = new ConditionalProbability(json);
            var hypothesis_list = new List<hypothesis>();
            int h_index = 0;
            foreach (var h in condition_probablity)
            {
                var hypothesis = new hypothesis
                {
                    belief = b,
                    name = h_index.ToString(),
                    partition = b.partition,
                    propositions = new List<proposition>(h.Count)
                };
                var proposition_list = new List<proposition>();
                for (int i = 0; i < h.Count; i++)
                {
                    var proposition = new proposition
                    {
                        hypothesis = hypothesis,
                        name = i.ToString(),
                        partition = b.partition,
                        value = decimal.Parse(h.ElementAt(i))
                    };
                    proposition_list.Add(proposition);
                }
                hypothesis.propositions = proposition_list;
                hypothesis_list.Add(hypothesis);
                h_index++;
            }
            b.hypotheses = hypothesis_list;
            return condition_probablity;
        }

        public static decimal[][] ToConditionalProbability(this belief b)
        {
            var result = new decimal[b.hypotheses.Count][];
            int i = 0;
            foreach (var h in b.hypotheses)
            {
                var row = new decimal[h.propositions.Count];
                int j = 0;
                foreach (var p in h.propositions)
                {
                    row[j] = p.value;
                    j++;
                }
                result[i] = row;
                i++;
            }
            return result;
        }
        public static decimal[][] UniformDistribution(int length)
        {
            var conditional_probability = new decimal[length][];
            for (int i = 0; i < length; i++)
            {
                var row = new decimal[length];
                var distribution = 1 / length;
                for(int j=0; j<length; j++)
                {
                    row[j] = distribution;
                }
                conditional_probability[i] = row;
            }
            return conditional_probability;
        }
    }
}
