namespace CER.Rpg
{
    public class CreationProcesses
    {
        public static CreationProcesses Character = new CreationProcesses("character");
        public static CreationProcesses Game = new CreationProcesses("game");
        public static CreationProcesses Plot = new CreationProcesses("plot");

        public CreationProcesses(string noun)
        {
            this.AudienceInput.description = string.Format(this.AudienceInput.description, noun);
            this.PurchasedFromOwner.description = string.Format(this.PurchasedFromOwner.description, noun);
            this.LicensedFromOwner.description = string.Format(this.LicensedFromOwner.description, noun);
            this.GameMasterCreated.description = string.Format(this.GameMasterCreated.description, noun);
            this.GameMasterInput.description = string.Format(this.GameMasterInput.description, noun);
            this.ParticipantInput.description = string.Format(this.ParticipantInput.description, noun);
            this.PlayerInput.description = string.Format(this.PlayerInput.description, noun);
            this.CulturalReference.description = string.Format(this.CulturalReference.description, noun);
            this.InTextAdlib.description = string.Format(this.InTextAdlib.description, noun);
        }

        public readonly creation_process PurchasedFromOwner = new creation_process
        {
            title = "Purchased from owner",
            description = "The {0} has been purchased for use in a role playing game. Depending on the game venue, copyright may need to be reviewed."
        };

        public readonly creation_process LicensedFromOwner = new creation_process
        {
            title = "Licensed from owner.",
            description = "The {0} has been licensed for use in a role playing game. Depending on the game venue, copyright may need to be reviewed."
        };

        public readonly creation_process GameMasterCreated = new creation_process
        {
            title = "Created by game master.",
            description = "The {0} was created by the game master. This creation process should be scoped to the aspects of the {0} determined before interactions with the players."
        };

        public readonly creation_process GameMasterInput = new creation_process
        {
            title = "Game master modified",
            description = "The game master has created aspects of this {0}."
        };

        public readonly creation_process AudienceInput = new creation_process
        {
            title = "Audience to the creation process provided input",
            description = "Parts of the {0} were created with suggestions from a viewing audience."
        };

        public readonly creation_process ParticipantInput = new creation_process
        {
            title = "Game participants provided input",
            description = "A participating player has provided input into the creation of the {0}."
        };

        public readonly creation_process PlayerInput = new creation_process
        {
            title = "Player provided input",
            description = "The initial player of this {0} was involved in its creation."
        };

        public readonly creation_process CulturalReference = new creation_process
        {
            title = "Creator stated cultural reference",
            description = "The creator(s) of the {0} have modeled aspects of it in reference to shared culture."
        };

        public readonly creation_process InTextAdlib = new creation_process
        {
            title = "Description contains a call for in-game adlib",
            description = "The {0} has text that contains a suggestion in paraenthesis to adlib the description using details of the current game."
        };
    }
}
