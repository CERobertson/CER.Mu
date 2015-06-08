namespace CER.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using CER.ng;

    [TestClass]
    public class ngTests
    {
        [TestMethod]
        public void PlayerTest()
        {
            Player p1 = new Player { Name = "Cer" };
            var capabilities = new Abilities[] { Abilities.Intuition, Abilities.Explore, Abilities.Convince };
            foreach (var capability in capabilities)
            {
                p1.SwitchAbility(capability);
            }
            foreach (var e in (int[])Enum.GetValues(typeof(Abilities)))
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
