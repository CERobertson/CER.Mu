namespace CER.DirectedGraphs
{
    using CER.Runtime.Serialization;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;

    public class DirectedGraph : Dictionary<string, string[]>
    {
        public static DirectedGraph Parse(string json)
        {
            return new MemoryStream(json.ToByteArray())
                .Deserialize<DirectedGraph>(new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true });
        }

        public string[] Roots
        {
            get
            {
                var possible_roots = this.Select(x => x.Key).ToList();
                foreach (var node in this)
                {
                    foreach (var child in node.Value)
                    {
                        possible_roots.Remove(child);
                    }
                }
                return possible_roots.ToArray();
            }
        }

        public string[] Sinks
        {
            get
            {
                var possible_sinks = this.SelectMany(x => x.Value).ToList();
                foreach (var node in this)
                {
                    possible_sinks.Remove(node.Key);
                }
                return possible_sinks.ToArray();
            }
        }
        
        //L ← Empty list that will contain the sorted elements
        //S ← Set of all nodes with no incoming edges
        //while S is non-empty do
        //    remove a node n from S
        //    add n to tail of L
        //    for each node m with an edge e from n to m do
        //        remove edge e from the graph
        //        if m has no other incoming edges then
        //            insert m into S
        //if graph has edges then
        //    return error (graph has at least one cycle)
        //else 
        //    return L (a topologically sorted order)
        public Node<T> ToDAG<T>() where T : Node<T>
        {
            foreach (var n in this)
            {
                var x = new Node<T> { Variable = n.Key };
                foreach (var e in n.Value)
                {
                    var c = (T)(new Node<T> { Variable = e });
                    c.Initialize((T)x);
                    x.children.Add(c);
                }
            }
            return new Node<T>();
        }
    }
}
