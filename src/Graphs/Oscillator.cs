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

            var remaining_edges = new List<Edge>();
            this.Graph = new DirectedAcyclicGraph<T>(this, remaining_edges);
            var loop_cache = this.DisassemblesToLoops(new DirectedGraph(this)).ToArray();
            foreach (var element in
                loop_cache.Select(x => x.Key).Union(
                loop_cache.SelectMany(x => x.Value)))
            {
                T node;
                if (!this.Graph.TryGetValue(element, out node))
                {
                    this.Graph[element] = new T { variable = element };
                    foreach (var arc in this[element].Select(x => new Edge { Parent = element, Child = x }))
                    {
                        if (remaining_edges.Where(x => x.Child != arc.Child && x.Parent != arc.Parent).Count() == 0)
                        {
                            remaining_edges.Add(arc);
                        }
                    }
                }
            }
            foreach (var e in remaining_edges.ToArray())
            {
                var parent = this.Graph[e.Parent];
                var child = this.Graph[e.Child];
                parent.children.Add(child);
                child.parents.Add(parent);
            }
        }

        public DirectedAcyclicGraph<T> Graph { get; private set; }
    }
    public class OscillatorException : Exception { public OscillatorException(string message) : base(message) { } }
}
