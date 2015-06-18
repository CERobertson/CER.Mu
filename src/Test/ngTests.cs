namespace CER.Test
{
    using NGPlusPlusStar;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework.Input;

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
            int found_ability_count = 0;
            foreach (var possible_ability in (Abilities[])Enum.GetValues(typeof(Abilities)))
            {
                if (p1.IsCapable(possible_ability))
                {
                    Assert.IsTrue(capabilities.Contains(possible_ability), "player has an unexpected ability.");
                    found_ability_count++;
                }
            }
            Assert.AreEqual(capabilities.Count(), found_ability_count);
        }
        [TestMethod]
        public void CharacterTest()
        {
            var characters = new Character[]
            {
                new Character { Name = "Mu" },
                new Character { Name = "Mu", HypothesisCapacity=2}
            };
            foreach (var c in characters)
            {
                Assert.AreEqual(c.HypothesisCapacity, c.RespondTo(Topic.WhoAmITalkingTo).ConditionalProbability.Length);
            }
        }
        [TestMethod]
        public void Around()
        {
            var i = int.MaxValue;
            var m1 = Math.Abs(i % 360);
            i++;
            var m2 = Math.Abs(i % 360);
            Assert.AreEqual(int.MinValue, i);
            Assert.AreNotEqual(0, m2);
            Assert.AreEqual(m1 + 1, m2);
        }
        [TestMethod]
        public void GamePadThroughXNA()
        {
            
        }
    }

}
