namespace CER.Graphs
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// X
    /// </summary>
    public class Entity
    {
        public readonly char seperator = '|';
        public virtual string variable { get; set; }
        public virtual int id { get; set; }
        public virtual string Address { get { return string.Join(this.seperator.ToString(), this.GetType().FullName, this.id); } }
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
        public List<T> _parents = new List<T>();

        /// <summary>
        /// Children nodes which contribute to evidential support.
        /// </summary>
        public List<T> _children = new List<T>();

        public void Initialize(T parent)
        {
            this._parents.Add(parent);
        }
    }

    /// <summary>
    /// A default node.
    /// </summary>
    public class Node : Node<Node>
    {

    }
}
