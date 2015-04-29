namespace CER.DirectedGraphs
{
    using System.Collections.Generic;

    /// <summary>
    /// X -> Y
    /// </summary>
    public class Node<T>
    {
        /// <summary>
        /// Y
        /// </summary>
        public string Variable { get; set; }

        /// <summary>
        /// Parent nodes which contribute to causal support.
        /// </summary>
        protected IList<T> parents = new List<T>();

        /// <summary>
        /// Children nodes which contribute to evidential support.
        /// </summary>
        protected IList<T> children = new List<T>();

        public void Initialize(T parent)
        {
            this.parents.Add(parent);
        }
    }
}
