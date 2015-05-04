namespace CER.Test
{
    using CER.Rpg;
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EntityTests
    {
        [TestMethod]
        public void Rpg()
        {
            var rpg = new DbContext();
            rpg.Elements().Count(); 
        }
    }
}
