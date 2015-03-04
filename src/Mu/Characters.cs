namespace CER.Mu
{
    using CER.Rpg;
    using System.Linq;

    public class Characters : character
    {
        public character Mu_image
        {
            get
            {
                return new character
                {
            gm_name = "Light on the moon",
            description = @"A burning light appears on the moon. As bright 
            as the sun, looking at it causes an after-image to appear when
            you close your eyes and you expect that prolonged staring would 
            cause blindness. Along with the light, there is a slight warmth.",
            creation_history = new creation_process[] 
            {
                CreationProcesses.Character.GameMasterCreated
            }.ToList()
                };
            }
        }
        public character Mu_collector
        {
            get
            {
                return new character
                {
            gm_name = "Reflective plate in orbit",
            description = @"A mirror that is reflecting an image of the planet.
            It always stays opposite to the light on the moon so the image it
            shows a time lapse of the planet with a faint halo.",
            creation_history = new creation_process[] 
            {
                CreationProcesses.Character.GameMasterCreated
            }.ToList()
                };
            }
        }
        public character Mu_voice
        {
            get
            {
                return new character
                {
            gm_name = "Mu",
            description = @"A voice that speaks from any location, in all 
            languages, simalitanouly. It will claim that all voices are one
            and when people compare translations they will appear as identical
            as reasonable. It will attempt to answer all questions, simultanously.
            Its movement and scope of effect are currently unknown.",
            creation_history = new creation_process[] 
            {
                CreationProcesses.Character.GameMasterCreated
            }.ToList()
                };
            }
        }
    }
}
