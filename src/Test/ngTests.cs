namespace CER.Test
{
    using NGPlusPlusStar;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

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
    }

}
