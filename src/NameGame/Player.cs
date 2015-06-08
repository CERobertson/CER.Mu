namespace CER.ng
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public class Player
    {
        public static readonly int MAX_NAME_LENGTH = 32;

        private string name;
        public string Name
        {
            get { return this.name; }
            set { this.name = value.Length <= Player.MAX_NAME_LENGTH ? 
                value : 
                value.Remove(Player.MAX_NAME_LENGTH, value.Length - Player.MAX_NAME_LENGTH); }
        }

        public int Abilities { get; private set; }

        public bool IsCapable(Abilities ability)
        {
            return (this.Abilities & (int)ability) == (int)ability;
        }

        public int SwitchAbility(Abilities ability)
        {
            this.Abilities ^= (int)ability;
            return this.Abilities & (int)ability;
        }
    }
    public enum Abilities
    {
        Intuition = 1,
        Telepathy = 2,
        Demonstrate = 4,
        Convince = 8,
        Explore = 16,
        Regional = 32,
        Global = 64,
        Assault = 128,
        Freeze = 256,
        Reflection = 512,
        Defend = 1024,
        OffScript = 2048,
    }
}
