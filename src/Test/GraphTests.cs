﻿namespace CER.Test
{
    using CER.Graphs;
    using CER.Graphs.SetExtensions;
    using CER.Runtime.Serialization;
    using CER.Test.Extensions;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GraphTests : Tests<DirectedGraph, GraphTests.DirectedGraphContext>
    {
        [TestMethod]
        public void TestJsonDAGs()
        {
            var valid_DAG = new DirectedGraphContext[]
            {
                new DirectedGraphContext{ 
                    Json = string.Empty,  
                    Roots = Set.Empty, 
                    Sinks = Set.Empty},
                new DirectedGraphContext{ 
                    Json = "{'A':['B']}", 
                    Roots = new[] { "A" }, 
                    Sinks = new[] { "B" }},
                new DirectedGraphContext{ 
                    Json = "{'A':['B'],'B':['C']}", 
                    Roots = new[] { "A" }, 
                    Sinks = new[] { "C" }},
                new DirectedGraphContext{ 
                    Json = "{'A':['B','D'],'B':['C']}",
                    NodeRelations = "{'A':{'children':['B','D'],'parents':[]},'B':{'children':['C'],'parents':['A']},'C':{'children':[],'parents':['B']},'D':{'children':[],'parents':['A']}}",
                    Roots = new[] { "A" }, 
                    Sinks = new[] { "C", "D" }}
            };
            var invalid_DAG = new DirectedGraphContext[]
            {
                new DirectedGraphContext{ 
                    Json = "{'A':['A']}", 
                    Roots = Set.Empty, 
                    Sinks = Set.Empty},
                new DirectedGraphContext{ 
                    Json = "{'A':['B'],'B':['A']}",  
                    Roots = Set.Empty, 
                    Sinks = Set.Empty},
                new DirectedGraphContext{ 
                    Json = "{'A':['B'],'C':['C']}", 
                    Roots = new[] { "A" }, 
                    Sinks = new[] { "B" }},
                new DirectedGraphContext{ 
                    Json = "{'A':['B'],'C':['D'],'B':['C'],'D':['A']}",  
                    Roots = Set.Empty, 
                    Sinks = Set.Empty}

            };
            this.Assert_AreDAGs(valid_DAG);
            this.Assert_AreDAGs(invalid_DAG, true);
        }
        
        private void Assert_AreDAGs(DirectedGraphContext[] directed_graphs_in_json, bool invert = false)
        {
            foreach (var expected in directed_graphs_in_json)
            {
                var dag = new DirectedAcyclicGraph<Node>(expected.Json);
                expected.Roots.Assert_NoDifferences(dag.Template.Roots);
                expected.Sinks.Assert_NoDifferences(dag.Template.Sinks);
                Assert.IsTrue(dag.Template.IsDirectedAcyclicGraph ^ invert);
                if (!string.IsNullOrEmpty(expected.NodeRelations))
                {
                    var nodes = new NodeDictionary(expected.NodeRelations);
                    foreach (var n in nodes.ToArray())
                    {
                        dag[n.Key]._children.Select(x => x.variable).ToArray().Assert_NoDifferences(n.Value["children"]);
                        dag[n.Key]._parents.Select(x => x.variable).ToArray().Assert_NoDifferences(n.Value["parents"]);
                        dag.Remove(n.Key);
                    }
                    Assert.IsTrue(dag.Count == 0);
                }
            }
        }

        public class DirectedGraphContext
        {
            public string Json { get; set; }
            public string NodeRelations { get; set; }
            public string[] Roots { get; set; }
            public string[] Sinks { get; set; }
        }

        [TestMethod]
        public void TestOscilattors()
        {
            var oscillators = new OscilattorContext[]
            {
                new OscilattorContext {
                    Object = new Oscillator<Node>(),
                    Loops = new string[] { "A", "B" },
                    NodeRelations = "{'A':{'children':['B'],'parents':['B']},'B':{'children':['A'],'parents':['A']}}"
                },
                new OscilattorContext {
                    Object = new Oscillator<Node>("{'A':['B'],'B':['A'],'C':['A']}"),
                    Loops = new string[] { "A", "B" },
                    NodeRelations = "{'A':{'children':['B'],'parents':['B','C']},'B':{'children':['A'],'parents':['A']},'C':{'children':['A'],'parents':[]}}"
                },
                new OscilattorContext {
                    Object = new Oscillator<Node>("{'A':['B','C'],'B':['A']}"),
                    Loops = new string[] { "A", "B" },
                    NodeRelations = "{'A':{'children':['B','C'],'parents':['B']},'B':{'children':['A'],'parents':['A']},'C':{'children':[],'parents':['A']}}"
                }
            };
            foreach (var expected in oscillators)
            {
                //{string:[string]} directed graph testing i.e. compact serialization form.
                expected.Loops.Assert_NoDifferences(expected.Object.Loops.Select(x => x.Key).ToArray());
                expected.Loops.Assert_NoDifferences(expected.Object.Loops.SelectMany(x => x.Value).Distinct().ToArray());

                //{string:{string:[string]}} directed graph testing i.e. independent element form.
                var nodes = new NodeDictionary(expected.NodeRelations);
                foreach (var n in nodes.ToArray())
                {
                    expected.Object.Graph[n.Key]._children.Select(x => x.variable).ToArray().Assert_NoDifferences(n.Value["children"]);
                    expected.Object.Graph[n.Key]._parents.Select(x => x.variable).ToArray().Assert_NoDifferences(n.Value["parents"]);
                    expected.Object.Graph.Remove(n.Key);
                }
                Assert.IsTrue(expected.Object.Graph.Count == 0);
            }
        }
        [TestMethod]
        public void HypothesesTesting()
        {
            var hypothesis_matrix = "[['0.8','0.1','0.1'],['.3','.3','3']]".Replace("'", "\"");
            List<List<string>> simple = hypothesis_matrix.ParseJsonToSimple<List<List<string>>>();
        }

        public class OscilattorContext 
        {   
            public Oscillator<Node> Object { get; set; }
            public string[] Loops { get; set; }
            public string NodeRelations { get; set; }
        }
    }
}
