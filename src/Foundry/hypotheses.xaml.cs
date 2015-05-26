namespace CER.Foundry
{
    using CER.Mu;
    using r = CER.Rpg;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;


    /// <summary>
    /// Interaction logic for hypotheses.xaml
    /// </summary>
    public partial class hypotheses : Page
    {
        private r.DbContext rpg = new r.DbContext(new CreateSeedDatabaseIfNotExists());

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
            var belief = this.rpg
                .Characters.Single(x => x.gm_name == character_gm_name)
                .beliefs.Single(x => x.variable == belief_variable);
            var prototype = this.rpg.SaveHypothesesToBelief(character_belief_hypothesis_split[2], belief);

            this.Navigation.Navigate(this, prototype);
        }

        public NavigationService Navigation { get; set; }
    }
}
