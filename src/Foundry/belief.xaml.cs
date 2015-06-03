namespace CER.Foundry
{
    using CER.ng;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Navigation;
    using r = CER.Rpg;

    /// <summary>
    /// Interaction logic for belief.xaml
    /// </summary>
    public partial class belief : Page
    {
        public belief()
        {
            InitializeComponent();
        }
        
        public void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            var link = (Hyperlink)sender;
            var text = new TextRange(link.ContentStart, link.ContentEnd).Text;
            var character = this.CurrentGame.SingleOrCreate(this.CurrentGame.Characters, new r.character { gm_name = "chaos" });
            var dag = this.CurrentGame.SaveBeliefNetworkToCharacter(text, character);
            this.Navigation.Navigate(link.NavigateUri, character);
        }

        public NavigationService Navigation { get; set; }
        public GameContext CurrentGame { get; set; }
    }
}
