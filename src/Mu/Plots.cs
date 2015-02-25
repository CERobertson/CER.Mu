namespace CER.Mu
{
    using CER.Rpg;
    using System;
    using System.Linq;

    public class Plots : plot
    {
        public plot Mu_Prologue
        {
            get
            {
                return new plot
                {
                    gm_name = "Prologue",
                    estimated_duration = TimeSpan.TicksPerDay * 29,
                    confidence_duration = .99M,
                    scale_duration = 1,
                    introduction =
                    @"As you are looking into the night sky an intense white light blooms around you.
                    Searching for the source, your eye is pulled up and out towards the horizon and 
                    eventually focuses on the moon.  In the center of the moon is an intense blazing light 
                    casting the familiar surface in high relief.
                        Pulling your eyes away from this spectacle, you notice that where the light is 
                    landing it coallesses into a shimmering, glowing mist. As you watch it, swaying back
                    and forth, it start pool and eventually flow.  Where-ever you look it seems to be flowing
                    away from the moon.",
                    description =
                    @"This prologue is intended to be used during character creation.  The moon will stay
                    in this state until the subplot Mu_AskMeAnything ends.  The glowing mist appear invisible
                    during the daylight, as though the sunlight is out shinning the mist. When the player is on 
                    the opposite side of the planet in relation to the moon, and it is dark, the mist seems to drift
                    into the sky.  If you follow the trial into the night sky you will see a small grey-white disk.
                    Over the course of hours the grey-white seems to shift on the disk but stays the same.  The disk
                    drift across the sky and over the course of days it is determined that it is always opposite the 
                    moon.(This could probably be better orgainzed as a small subplot)",
                    subplots = new plot[]
                    {
                        this.Mu_AskMeAnything,
                        this.Mu_Extinguished
                    }.ToList(),
                    creation_history = new creation_process[] 
                    {
                        CreationProcesses.Plot.GameMasterCreated
                    }.ToList()
                };
            }
        }
        public plot Mu_AskMeAnything
        {
            get
            {
                return new plot
                {
                    gm_name = "Ask Mu Anything",
                    estimated_duration = TimeSpan.TicksPerHour * 1,
                    confidence_duration = .99M,
                    scale_duration = 1,
                    introduction =
            @"  Everyone stops. A deep tone begins to eminate from the center of your body.
            ""Muuuuuuuuuuuuuuuuuuuuuuuuu"". The sound continues and then starts to move through the room.
            It becomes obvious that you all are hearing the note from a different location as it drifts around
            the room.  There is a break in the sounds.  Maybe a heartbeat of silence and then ""I answer all.""",
                    description = @"Mu answers any questions it is asked. This will occure for exactly 1 hour. 
            The results of this question and answer will differ wildly depending on the location and intelligence 
            of the local population.  Mu wants to know all things of the planet and knows an amazing level of detail.
            The last 28 days has been its complete recording of the planet. If ask its motive for the upcoming 1 hour 
            conversation it will explain the need to contextualize the previous 28 day recording.  It will explain side 
            effects of the recording process.  It will answer intimate details of all alive and dead people; 
            Though its answer will be precise and considered by some as cryptic and hedged with probabities.
            It is a polite oracle. Its cadence will pickup in tempo after the initial statements. It will
            project intelligence in whatever form is native to the culture listening.
                Mu can direct its communications to any number of listeners and converse with
            the entire planet simultanously.  Its general purpose it to collect the recording of the planet.
            It will not acknowledge a physical location. It will only speak in probablities based on the 
            state of the world during the recording when answering questions about the past or future. That said,
            it knows almost all things very confidently. The GM should attempt to know everything about the game
            world but that is not always the case.  Many times a GM is setting their adventure in a shared world
            and they might not know a specific fact.  In this case the GM could commit to tracking down the answer.
            Another option is to ask the players if they know the answer and provisionally accept a their answer 
            until they havetime to recheck. To prevent these gaps in the GM's knowledge from undermining Mu's 
            omniscense make sure they know the details to the their planned adventure.
                Any malformed question will be ansered with ""Mu"" which can be translated
            as a request to ""Unask the question"" or ""Your question is a contridiction in terms"".  
            As the questions terms are illogical it is not concerned with answer a question with this command. To some
            this will be considered obstinate. An example of a malformed question could be ""How many sacks are in a bag"".
            Unless the person is pointing to something in specific it will consider the question too general.
                As the hour comes to an end Mu will say ""Ask your final questions my obligation ends soon.""
            If anyone looks at the moon they will notice that light goes out at the precise time that Mu stops
            answering questions.  If the first question asked after this announcement is about the source of
            his obligations he will have enough time to tell of a nameless intelligence who built the tools nessary
            to create the recording and summon Mu. The cost for summoning this sprit was the world wide
            conversation. In exchange the summoner will continute to have access to Mu and thus the knowledge
            contained in the recording.  Mu can only say what the intelligence is not.  Any direct question, for
            example ""What is the intelligences name"" will be answered with ""I can only tell you what it is not,
            it is hole in my vision of your world.""  The best Mu can do is make guess of the intelligence during
            the last 28 days and it can be guessed that the intelligence has known this and taken precautions.  The
            light fades on the moon and the voice stops.",
                    creation_history = new creation_process[] 
            {
                CreationProcesses.Plot.GameMasterCreated
            }.ToList()
                };
            }
        }
        public plot Mu_Extinguished
        {
            get
            {
                return new plot
                {
                    gm_name = "Ask Mu Anything",
                    estimated_duration = TimeSpan.TicksPerHour * 1,
                    confidence_duration = .99M,
                    scale_duration = 1,
                    introduction =
            @"  The last answer fades into silience.  Those with a view of the moon see the wink out.
            Though the central light is gone, the surface continues to softy glow; particulary around
            the center which fading to a glossy cool rock color on the parimeter of where the light touched.
            The last of the glowing mist streams away. The night seems colder.",
                    description = @"This is the completion of the prologue. Will our heros hunt the controller of Mu?
            Has a character finally learned the truth to a past event that now demand justice? Will a previous foe
            be able to strike at the party? Hopefully this prologue lays the ground work for an exciting campaign
            as the world comes to terms with a the wielder of this vast power.",
                    creation_history = new creation_process[] 
            {
                CreationProcesses.Plot.GameMasterCreated
            }.ToList()
                };
            }
        }
    }
}
