namespace CER.Foundry
{
    using CER.ng;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Navigation;
    using ef = System.Data.Entity;
    using rpg = CER.Rpg;

    /// <summary>
    /// Interaction logic for belief.xaml
    /// </summary>
    public partial class belief : Page
    {
        public belief()
        {
            InitializeComponent();
        }

        private GameContext rpg = new GameContext();

        public void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            var link = (Hyperlink)sender;
            var text = new TextRange(link.ContentStart, link.ContentEnd).Text;
            var character = this.rpg.SingleOrCreate(this.rpg.Characters, new rpg.character { gm_name="chaos"});
            var dag = this.rpg.SaveBeliefNetworkToCharacter(text, character);
        }

        public NavigationService Navigation { get; set; }
    }
}
