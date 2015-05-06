namespace CER.Graphs
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// X
    /// </summary>
    public class Entity
    {
        private readonly string seperator = "|";
        public virtual string variable { get; set; }
        public virtual int id { get; set; }
        public virtual string Address { get { return string.Join(this.seperator, this.GetType().FullName, this.id); } }
    }

    /// <summary>
    /// X -> Y
    /// </summary>
    public class Node<T> : Entity
    {
        /// <summary>
        /// Y
        /// </summary>
        public override string variable { get; set; }

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
