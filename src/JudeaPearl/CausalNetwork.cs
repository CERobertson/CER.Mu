namespace CER.JudeaPearl.CausalNetwork
{
    using CER.DirectedGraphs;
    using CER.JudeaPearl;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// X -> Y
    /// </summary>
    public class Belief : Node<Belief>
    {
        /// <summary>
        /// x in M y|x
        /// </summary>
        public string[] MutuallyExclusiveHypotheses { get; set; }

        public decimal[][] Value { get; private set; }
        /// <summary>
        /// BEL(x)
        /// </summary>
        public void UpdateBelief(int variable)
        {
            this.Value[variable] = this
                .DiagnosticSupport[variable].VectorProduct(this.CausalSupport[variable])
                .Normalize();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
        }

        public decimal[][] CausalSupport { get; private set; }
        /// <summary>
        /// π(x)
        /// </summary>
        public void UpdateCausalSupport(int variable, decimal[] support)
        {
            this.CausalSupport[variable] = support.MatrixProduct(this.ConditionalProbability);
            this.UpdateBelief(variable);
            foreach (var child in this.children)
            {
                child.UpdateCausalSupport(variable, this.CausalSupport[variable]);
            }
        }

        public decimal[][] DiagnosticSupport { get; private set; }
        /// <summary>
        /// λ(x)
        /// </summary>
        public void UpdateDiagnosticSupport(int variable, decimal[] support)
        {
            this.DiagnosticSupport[variable] = DiagnosticSupport[variable].VectorProduct(support);
            this.UpdateBelief(variable);

            if (this.parents.Count > 0)
            {
                this.parents.ElementAt(0).UpdateDiagnosticSupport(variable, this.ConditionalProbability.MatrixProduct(support.Transpose()).Transpose());
            }
        }

        private decimal[][] conditional_probability;
        /// <summary>
        /// M y|x is given by the (x, y) entry.
        /// </summary>
        public decimal[][] ConditionalProbability
        {
            get
            {
                return this.conditional_probability;
            }
            set
            {
                this.conditional_probability = value;
                this.DiagnosticSupport = this.conditional_probability.ConstructUnit(this.DiagnosticSupport);
                this.CausalSupport = this.conditional_probability.Construct(this.CausalSupport);
                this.Value = this.conditional_probability.Construct(this.Value);
            }
        }

        public void Causes(Belief child)
        {
            if (this.parents.Count == 0)
            {
                this.CausalSupport = child.ConditionalProbability.Duplicate();
                this.Value = child.ConditionalProbability.Construct(this.Value);
                this.DiagnosticSupport = child.ConditionalProbability.ConstructUnit(this.DiagnosticSupport);
            }
            child.Initialize(this);
            this.children.Add(child);
            for (int i = 0; i < child.ConditionalProbability.Length; i++)
            {
                child.UpdateCausalSupport(i, this.CausalSupport[i]);
            }
        }
    }
}
