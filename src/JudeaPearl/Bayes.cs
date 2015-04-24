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

        /// <summary>
        /// Vectory product
        /// </summary>
        /// <param name="v">v1</param>
        /// <param name="vector">v2</param>
        /// <returns>v1 * v2</returns>
        public static decimal[] VectorProduct(this decimal[] v, decimal[] vector)
        {
            var result = new decimal[v.Length];
            for (int i = 0; i < v.Length; i++)
            {
                result[i] = v[i] * vector[i];
            }
            return result;
        }

        public static decimal[][] VectorProduct(this decimal[][] v, decimal[][] vector)
        {
            var result = new decimal[v.Length][];
            for (int i = 0; i < v.Length; i++)
            {
                result[i] = new decimal[] { v[i][0] * vector[i][0] };
            }
            return result;
        }

        /// <summary>
        /// Likelihood vector from evidence.
        /// </summary>
        /// <param name="e">Evidence</param>
        /// <param name="i">i-th likelihood vector.</param>
        /// <returns>Likelihood vector.</returns>
        public static decimal[] LikelihoodVector(this decimal[][] e, int i)
        {

            var result = new decimal[e.Length];
            for (int j = 0; j < e.Length; j++)
            {
                result[j] = e[j][i];
            }
            return result;
        }

        /// <summary>
        /// Normalize a vector to 1.0 (unity)
        /// </summary>
        /// <param name="v">vector</param>
        /// <returns>Normalized vector.</returns>
        public static decimal[] Normalize(this decimal[] v)
        {
            var normalizing_constant = 0.0M;
            for (int i = 0; i < v.Length; i++)
            {
                normalizing_constant += v[i];
            }
            var result = new decimal[v.Length];
            for (int i = 0; i < v.Length; i++)
            {
                result[i] = v[i] / normalizing_constant;
            }
            return result;
        }

        public static decimal[][] Normalize(this decimal[][] v)
        {
            var normalizing_constant = 0.0M;
            for (int i = 0; i < v.Length; i++)
            {
                normalizing_constant += v[i][0];
            }
            var result = new decimal[v.Length][];
            for (int i = 0; i < v.Length; i++)
            {
                result[i] = new decimal[] { v[i][0] / normalizing_constant };
            }
            return result;
        }
        
        public static decimal[][] MatrixProduct(this decimal[][] m_, decimal[][] matrix)
        {
            var result = new decimal[m_.Length][];
            for (int i = 0; i < m_.Length; i++)
            {
                result[i] = new decimal[matrix[0].Length];
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    var accumulator = 0.0M;
                    for (int k = 0; k < m_[0].Length; k++)
                    {
                        accumulator += m_[i][k] * matrix[k][j];
                    }
                    result[i][j] = accumulator;
                }
            }
            return result;
        }

        public static decimal[][] MatrixProduct(this decimal[][] m_, decimal[] row)
        {
            return m_.MatrixProduct(new decimal[][] { row });
        }

        public static decimal[] MatrixProduct(this decimal[] m_, decimal[][] matrix)
        {
            var result = new decimal[matrix[0].Length];
            for (int i = 0; i < matrix.Length; i++)
            {
                var accumulator = 0.0M;
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    accumulator += m_[j] * matrix[i][j];
                }
                result[i] = accumulator;
            }
            return result;
        }

        public static decimal[][] Transpose(this decimal[] v)
        {
            var result = new decimal[v.Length][];
            for (int i = 0; i < v.Length; i++)
            {
                result[i] = new decimal[] { v[i] };
            }
            return result;
        }

        public static decimal[] Transpose(this decimal[][] v)
        {
            var result = new decimal[v.Length];
            for (int i = 0; i < v.Length; i++)
            {
                result[i] = v[i][0];
            }
            return result;
        }

        public static decimal[] Vector(this decimal[][] e, int i)
        {
            var result = new decimal[e.Length];
            for (int j = 0; j < e.Length; j++)
            {
                result[j] = e[j][i];
            }
            return result;
        }

        public static decimal[][] Unit(this decimal[][] schema)
        {
            var result = new decimal[schema.Length][];
            for (int i = 0; i < schema.Length; i++)
            {
                result[i] = new decimal[schema[0].Length];
                for (int j = 0; j < schema[0].Length; j++)
                {
                    result[i][j] = 1;
                }
            }
            return result;
        }

        public static decimal[][] Duplicate(this decimal[][] schema)
        {
            var result = new decimal[schema.Length][];
            for (int i = 0; i < schema.Length; i++)
            {
                result[i] = new decimal[schema[0].Length];
                for (int j = 0; j < schema[0].Length; j++)
                {
                    result[i][j] = schema[i][j];
                }
            }
            return result;
        }

        public static decimal[][] Construct(this decimal[][] template, decimal[][] target)
        {
            if (target == null)
            {
                target = new decimal[template.Length][];
                for (int i = 0; i < template.Length; i++)
                {
                    target[i] = new decimal[template[0].Length];
                }
            }
            return target;
        }

        public static decimal[][] ConstructUnit(this decimal[][] template, decimal[][] target)
        {
            if (target == null)
            {
                target = template.Construct(target).Unit();
            }
            return target;
        }
    }
}
