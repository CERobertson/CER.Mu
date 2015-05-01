namespace CER.Test
{
    using CER.Graphs;
    using CER.Graphs.SetExtensions;
    using CER.Test.Extensions;
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
                    Roots = new string[] { }, 
                    Sinks = new string[] { }},
                new DirectedGraphContext{ 
                    Json = JsonResource.DAG_2, 
                    Roots = new[] { "A" }, 
                    Sinks = new[] { "B" }},
                new DirectedGraphContext{ 
                    Json = JsonResource.DAG_3, 
                    Roots = new[] { "A" }, 
                    Sinks = new[] { "C" }}
            };
            var invalid_DAG = new DirectedGraphContext[]
            {
                new DirectedGraphContext{ 
                    Json = JsonResource.DCG_1, 
                    Roots = new string[] { }, 
                    Sinks = new string[] { }},
                new DirectedGraphContext{ 
                    Json = JsonResource.DCG_2, 
                    Roots = new string[] { }, 
                    Sinks = new string[] { }},
                new DirectedGraphContext{ 
                    Json = JsonResource.DCG_3, 
                    Roots = new[] { "A" }, 
                    Sinks = new[] { "B" }}  

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
    }
}
