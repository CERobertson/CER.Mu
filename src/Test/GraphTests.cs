namespace CER.Test
{
    using CER.Graphs;
    using CER.Graphs.SetExtensions;
    using CER.Test.Extensions;
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
            }
        }

        public class DirectedGraphContext
        {
            public string Json { get; set; }
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
                    Loops = new string[] { "A", "B" }
                }

            };
            foreach (var expected in oscillators)
            {
                var loops = new DirectedGraph(expected.Object);
                expected.Object.DisassemblesToLoops(loops);
                expected.Loops.Assert_NoDifferences(loops.Select(x => x.Key).ToArray());
            }
        }

        public class OscilattorContext
        {
            public Oscillator<Node> Object { get; set; }
            public string[] Loops { get; set; }
        }
    }
}
