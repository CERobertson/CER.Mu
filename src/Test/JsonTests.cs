namespace CER.Test
{
    using CER.Runtime.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using System.Linq;

    [TestClass]
    public class JsonTests
    {
        [TestMethod]
        public void DirectedGraph()
        {
            var valid_DAG = new string[]
            {
                JsonResource.Minimal_DAG
            };
            var invalid_DAG = new string[]
            {

            };
            this.Assert_AreDAGs(valid_DAG);
            this.Assert_AreDAGs(invalid_DAG, true);
        }

        private void Assert_AreDAGs(string[] directed_graphs, bool invert = false)
        {
            foreach (string da in directed_graphs)
            {

            }
        }
    }
}
