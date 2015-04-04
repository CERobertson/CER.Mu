namespace CER.Mu
{
    using CER.Rpg;
    using System;
    using System.Linq;

    public class Games : game
    {
        public Games()
        {
            this.Plots = new Plots();
        }
        public Plots Plots { get; set; }
        public game Mu
        {
            get
            {
                return new game
                {
                    gm_name = "mu",
                    description = @"A plot focused on exposing details of the characters and their world.
                        Mu visits the planet.  The next 29 days will introduce the players, their
                    characters, their characters' place in soceity and how that society responds at first
                    to the vision of Mu and finally the execution of his obligation.
                        Depending on the outcome of the adventure players may decide to track down
                    the creator of the recording.  Or perhaps they  have learned a personal detail that 
                    requires immediate action?  Old power structures will be left shaking from the airing of 
                    their secrets or the unchallenege bluffs to knowledge that are made possible by Mu.  
                    Chaos is spreading, what will the owner of the information do next?  
                    What remains of the moon's eye?",
                    current_chapter = 0,
                    chapters = new plot[]
                    {
                        Plots.Mu_Prologue
                    }.ToList(),
                    creation_history = new creation_process[] 
                    {
                        CreationProcesses.Game.GameMasterCreated
                    }.ToList()
                };
            }
        }
        public game OutsideTime
        {
            get
            {
                return new game
                {
                    gm_name = "OutsideTime",
                    description = @" A player is given powers to pause time, to manage inventory,
                    keep people in for conversation for as long as needed and a number of other
                    rpgs tropes.  NPC will notice the use and act.",
                    current_chapter = 0,
                    creation_history = new creation_process[] 
                    {
                        CreationProcesses.Game.GameMasterCreated
                    }.ToList()
                };
            }
        }
    }
}
