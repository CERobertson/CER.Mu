namespace CER.Test
{
    using CER.Graphs;
    using CER.Graphs.SetExtensions;
    using CER.Mu;
    using CER.Rpg;
    using CER.Test.Extensions;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EntityTests
    {
        [TestMethod]
        public void Rpg()
        {
            var initializer = new DropCreateDbInitializer();
            var rpg = new DbContext(initializer);
            var directed_graph = new DirectedGraph(rpg.Elements());
            Assert.AreEqual(rpg.Elements().Count(), directed_graph.Count);
            Assert.IsTrue(directed_graph.IsDirectedAcyclicGraph);
        }
    }
}
