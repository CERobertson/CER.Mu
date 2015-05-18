namespace CER.Graphs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Oscillator<T> : DirectedGraph where T : Node<T>, new()
    {
        public static readonly string Template = "{'A':['B'],'B':['A']}";

        public Oscillator() : this(Oscillator<T>.Template) { }
        public Oscillator(string json)
            : base(json)
        {
            if (base.IsDirectedAcyclicGraph)
            {
                throw new OscillatorException("There should be a loop.");
            }
            this.Graph = new DirectedAcyclicGraph<T>(this);
            var remaining_edges = new List<Edge>();
            foreach (var element in 
                this.DisassemblesToLoops(new DirectedGraph(this)).Select(x => x.Key).Union(
                this.DisassemblesToLoops(new DirectedGraph(this)).SelectMany(x => x.Value)))
            {
                T node;
                if (!this.Graph.TryGetValue(element, out node))
                {
                    this.Graph[element] = new T { variable = element };
                    remaining_edges.AddRange(this[element].Select(x => new Edge { Parent = element, Child = x }));
                }
            }
            foreach (var e in remaining_edges.ToArray())
            {
                var parent = this.Graph[e.Parent];
                var child = this.Graph[e.Child];
                parent.children.Add(child);
                child.parents.Add(parent);
                remaining_edges.Remove(e);
            }
            if (remaining_edges.Count != 0)
            {
                throw new OscillatorException("All edges should have been processed.");
            }
        }

        public DirectedAcyclicGraph<T> Graph { get; private set; }
    }
    public class OscillatorException : Exception { public OscillatorException(string message) : base(message) { } }
}
