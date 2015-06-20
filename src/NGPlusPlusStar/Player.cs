namespace NGPlusPlusStar
{
    using CER.JudeaPearl.CausalNetwork;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
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
            set
            {
                this.name = value.Length <= Player.MAX_NAME_LENGTH ?
                    value :
                    value.Remove(Player.MAX_NAME_LENGTH, value.Length - Player.MAX_NAME_LENGTH);
            }
        }

        public int player_index = -1;
        public int PlayerIndex
        {
            get
            {
                if (this.player_index < 0)
                {
                    foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
                    {
                        if (GamePad.GetState(index).IsConnected)
                            return (int)index;
                    }
                }
                else
                {
                    return this.player_index;
                }
                return -1;
            }
            set
            {
                if (value < Enum.GetNames(typeof(PlayerIndex)).Length)
                {
                    this.player_index = value;
                }
                else
                {
                    this.player_index = -1;
                }
            }
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

        public List<CharacterInterests> CharacterKnowledge { get; set; }

        public class CharacterInterests
        {
            public Character Person { get; set; }
            public Topic Interest { get; set; }
        }

        public void Trance() { }
        public Dialogue TalkTo(Character c)
        {
            return new Dialogue
            {
                Interlocutor = c,
                Protagonist = this
            };
        }
    }

    public class Character
    {
        public Belief IDontKnow { get { return new Belief { ConditionalProbability = BeliefExtensions.UniformDistribution(this.HypothesisCapacity) }; } }
        public Character()
        {
            this.Actions = new Dictionary<Topic, Action>();
            this.Evidence = new Dictionary<Topic, Belief>();
            this.Actions[Topic.WhoAmITalkingTo] = () =>
                this.Evidence[Topic.WhoAmITalkingTo].variable = "p(name|evidence)";
        }
        public string Name { get; set; }
        public int HypothesisCapacity { get; set; }

        private Dictionary<Topic, Belief> Evidence { get; set; }
        private Dictionary<Topic, Action> Actions { get; set; }

        public Belief RespondTo(Topic t) 
        {
            var belief = this.InspectBelief(t);
            Action action_in_response_to_question;
            if (this.Actions.TryGetValue(t, out action_in_response_to_question))
            {
                action_in_response_to_question();
            }
            return belief;
        }
        public Belief InspectBelief(Topic t)
        {
            var belief = this.IDontKnow;
            if (this.Evidence.TryGetValue(t, out belief))
            {
                return belief;
            }
            
            return this.Evidence[t] = this.IDontKnow;
        }
    }

    public class Topic
    {
        public static Topic WhoAmITalkingTo = new Topic { AsQuestion = "Who is this person?" };
        public string AsQuestion { get; set; }
    }

    public class Dialogue
    {
        public Character Interlocutor { get; set; }
        public Player Protagonist { get; set; }
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
