﻿namespace CER.Foundry
{
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Navigation;
    using r = CER.Rpg;

    /// <summary>
    /// Interaction logic for character.xaml
    /// </summary>
    public partial class character : Page
    {
        public character()
        {
            InitializeComponent();
        }

        public void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            var link = (Hyperlink)sender;
            var text = new TextRange(link.ContentStart, link.ContentEnd).Text;
            var prototype = new r.character();
            var character_belief_split = text.Split(new char[] {prototype.seperator});
            prototype.gm_name = character_belief_split[0];
            prototype = this.CurrentGame.SingleOrCreate(this.CurrentGame.Characters, prototype, true);
            this.CurrentGame.SaveBeliefNetworkToCharacter(character_belief_split[1], prototype);

            this.Navigation.Navigate(link.NavigateUri, prototype);
        }

        public NavigationService Navigation { get; set; }
        public r.DbContext CurrentGame { get; set; }
    }
}
