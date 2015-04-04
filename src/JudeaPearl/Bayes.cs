namespace CER.JudeaPearl
{
    /// <summary>
    /// In this attempt at Bayesian formalism, belief measures obey the 
    /// three basic axioms of probability theory:
    /// 0 -le P(A) -le 1
    /// P(Sure proposition) = 1
    /// P(A or B) = P(A) + P(B) if A and B are mutually exclusive.
    /// </summary>
    public static class Bayes
    {
        /// <summary>
        /// Prior odds on Hypothesis
        /// </summary>
        /// <param name="pHypothesis">P(H)</param>
        public static decimal PriorOdds(this decimal pHypothesis)
        {
            return pHypothesis / (1 - pHypothesis); 
        }

        /// <summary>
        /// Likelihood ratio of evidence given Hypothesis
        /// </summary>
        /// <param name="pEvidence_Hypothesis">P(e|H)</param>
        /// <param name="pEvidence_NoHypothesis">P(e|~H)</param>
        public static decimal LikelihoodRatio(this decimal pEvidence_Hypothesis, decimal pEvidence_NoHypothesis)
        {
            return pEvidence_Hypothesis / pEvidence_NoHypothesis;
        }

        /// <summary>
        /// Posterior odds of Hypothesis given evidence
        /// </summary>
        /// <param name="lEvidence_Hypothesis">L(e|H)</param>
        /// <param name="oHypothesis">O(H)</param>
        public static decimal PosteriorOdds(this decimal lEvidence_Hypothesis, decimal oHypothesis)
        {
            return lEvidence_Hypothesis * oHypothesis;
        }

        /// <summary>
        /// Probablity of Hypothesis
        /// </summary>
        /// <param name="oHypothesis">O(H)</param>
        public static decimal Probablity(this decimal oHypothesis)
        {
            return oHypothesis / (1 + oHypothesis);
        }
    }
}
