namespace CER.Foundry
{
    using CER.ng;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Navigation;
    using r = CER.Rpg;


    /// <summary>
    /// Interaction logic for hypotheses.xaml
    /// </summary>
    public partial class hypotheses : Page
    {
        public hypotheses()
        {
            InitializeComponent();
        }

        public void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            var link = (Hyperlink)sender;
            var text = new TextRange(link.ContentStart, link.ContentEnd).Text;
            
            var character_prototype = new r.character();
            var belief_prototype = new r.belief();
            var character_belief_hypothesis_split = text.Split(new char[] { character_prototype.seperator });
            var character_gm_name = character_belief_hypothesis_split[0];
            var belief_variable = character_belief_hypothesis_split[1];
            var character = this.CurrentGame
                .Characters.Single(x => x.gm_name == character_gm_name && x.partition == this.CurrentGame.Partition);
            var belief = character
                .beliefs.Single(x => x.variable == belief_variable && x.partition == this.CurrentGame.Partition);
            var prototype = this.CurrentGame.SaveHypothesesToBelief(character_belief_hypothesis_split[2], belief);

            this.Navigation.Navigate(this, prototype);
        }

        public NavigationService Navigation { get; set; }
        public GameContext CurrentGame { get; set; }
    }
}
