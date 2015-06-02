namespace CER.Foundry
{
    using CER.ng;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Navigation;
    using ef = System.Data.Entity;
    using rpg = CER.Rpg;

    /// <summary>
    /// Interaction logic for character.xaml
    /// </summary>
    public partial class character : Page
    {
        public character()
        {
            InitializeComponent();
        }

        private GameContext rpg = new GameContext();

        public void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            var link = (Hyperlink)sender;
            var text = new TextRange(link.ContentStart, link.ContentEnd).Text;
            var prototype = new rpg.character();
            var character_belief_split = text.Split(new char[] {prototype.seperator});
            prototype.gm_name = character_belief_split[0];
            prototype = this.rpg.SingleOrCreate(this.rpg.Characters, prototype, true);
            prototype.beliefs.AddRange(this.rpg.SaveBeliefNetworkToCharacter(character_belief_split[1], prototype).Roots);

            this.Navigation.Navigate(link.NavigateUri, prototype);
        }

        public NavigationService Navigation { get; set; }
    }
}
