namespace CER.Foundry
{
    using CER.Mu;
    using rpg = CER.Rpg;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Navigation;
    using CER.Graphs;
using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interaction logic for belief.xaml
    /// </summary>
    public partial class belief : Page
    {
        public belief()
        {
            InitializeComponent();
        }
        
        private rpg.DbContext rpg = new rpg.DbContext(new CreateSeedDatabaseIfNotExists());

        public void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            var link = (Hyperlink)sender;
            var text = new TextRange(link.ContentStart, link.ContentEnd).Text;
            var dag = this.rpg.SaveBeliefNetworkToCharacter(text, new rpg.character { gm_name="chaos"});
        }

        public NavigationService Navigation { get; set; }
    }
}
