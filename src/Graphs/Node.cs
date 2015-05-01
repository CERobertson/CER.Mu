namespace CER.Graphs
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
        public IList<T> parents = new List<T>();

        /// <summary>
        /// Children nodes which contribute to evidential support.
        /// </summary>
        public IList<T> children = new List<T>();

        public void Initialize(T parent)
        {
            this.parents.Add(parent);
        }
    }

    /// <summary>
    /// A default node.
    /// </summary>
    public class Node : Node<Node>
    {

    }
}
