namespace CER.Test
{
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
            var rpg = new DbContext();
            var games = new Games();
            rpg.Characters.Add(games.Mu.chapters[0].characters[0]);
            rpg.SaveChanges();
        }
    }
}
