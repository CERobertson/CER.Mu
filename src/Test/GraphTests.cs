namespace CER.Test
{
    using CER.DirectedGraphs;
    using CER.DirectedGraphs.SetExtensions;
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
                    Json = JsonResource.Minimal_DAG, 
                    Roots = new[] { "A" }, 
                    Sinks = new[] { "B" }}
            };
            var invalid_DAG = new DirectedGraphContext[]
            {
                new DirectedGraphContext{ 
                    Json = JsonResource.Minimal_DCG, 
                    Roots = new string[] { }, 
                    Sinks = new string[] { }}

            };
            this.Assert_AreDAGs(valid_DAG);
            this.Assert_AreDAGs(invalid_DAG, true);
        }

        private void Assert_AreDAGs(DirectedGraphContext[] directed_graphs_in_json, bool invert = false)
        {
            foreach (var expected in directed_graphs_in_json)
            {
                var dg = DirectedGraph.Parse(expected.Json);
                expected.Roots.Assert_NoDifferences(dg.Roots);
                expected.Sinks.Assert_NoDifferences(dg.Sinks);
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
