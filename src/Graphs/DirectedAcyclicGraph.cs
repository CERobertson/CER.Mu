﻿namespace CER.Graphs
{
    using CER.Runtime.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DirectedAcyclicGraph<T> : Dictionary<string, T> where T : Node<T>, new()
    {
        public DirectedAcyclicGraph(string json, string quote = "'", bool throw_exception_on_flaws = false)
            : this(json.ParseJsonToSimple<DirectedGraph>(quote), throw_exception_on_flaws) { }

        public DirectedAcyclicGraph(DirectedGraph template, bool throw_exception_on_flaws = false)
            : this(template, new List<Edge>(), throw_exception_on_flaws) { }

        public DirectedAcyclicGraph(DirectedGraph template, List<Edge> remaining_edges, bool throw_exception_on_flaws = false)
        {
            this.Template = template;

            var graph = new DirectedGraph(this.Template);
            this.ConstructDirectedAcyclicGraph(graph, remaining_edges);

            if (throw_exception_on_flaws && graph.Count != 0)
            {
                throw new DirectedAcyclicGraph_ConstructionException("Cycles found in construction.", graph, remaining_edges);
            }
            if (throw_exception_on_flaws && remaining_edges.Count != 0)
            {
                throw new DirectedAcyclicGraph_ConstructionException("Unaccounted for edges.", graph, remaining_edges);
            }

        }

        private void ConstructDirectedAcyclicGraph(DirectedGraph graph, List<Edge> remaining_edges)
        {
            foreach (var e in remaining_edges.ToArray())
            {
                T child;
                if (this.TryGetValue(e.Child, out child))
                {
                    T parent = this[e.Parent];
                    child._parents.Add(parent);
                    parent._children.Add(child);
                    remaining_edges.Remove(e);
                }
            }

            var roots = graph.Roots;
            foreach (var r in roots)
            {
                this[r] = new T { variable = r };
                remaining_edges.AddRange(graph[r].Select(x => new Edge { Parent = r, Child = x }));
                graph.Remove(r);
            }

            if (roots.Count() == 0)
            {
                foreach (var parents_of_child in remaining_edges.ToArray().GroupBy(x => x.Child))
                {
                    T child = new T { variable = parents_of_child.Key };
                    foreach (var p in parents_of_child)
                    {
                        T parent = this[p.Parent];
                        child._parents.Add(parent);
                        parent._children.Add(child);
                        remaining_edges.Remove(p);
                    }
                    this[parents_of_child.Key] = child;
                }
                foreach (var e in graph)
                {
                    remaining_edges.AddRange(e.Value.Select(x => new Edge { Parent = e.Key, Child = x }));
                }
            }
            else
            {
                this.ConstructDirectedAcyclicGraph(graph, remaining_edges);
            }
        }

        public DirectedGraph Template { get; private set; }

        public IEnumerable<T> Roots { get { return this.Where(x => this.Template.Roots.Contains(x.Key)).Select(x => x.Value); } }
    }

    public class Edge
    {
        public string Parent { get; set; }
        public string Child { get; set; }
    }

    public class DirectedAcyclicGraph_ConstructionException : Exception
    {
        public DirectedAcyclicGraph_ConstructionException(string message, DirectedGraph cycles, List<Edge> disconnected_edges)
            : base(message)
        {
            this.Cycles = cycles;
            this.DisconnectedEdges = disconnected_edges;
        }
        public DirectedGraph Cycles { get; private set; }
        public List<Edge> DisconnectedEdges { get; private set; }
    }
}
