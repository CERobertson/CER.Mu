namespace CER.Foundry
{
    using CER.Mu;
    using rpg = CER.Rpg;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Navigation;

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
            var prototype = new rpg.belief{ gm_name = text };
            this.Navigation.Navigate(link.NavigateUri, this.rpg.SingleOrCreate(this.rpg.Beliefs, prototype, true));
        }

        public NavigationService Navigation { get; set; }
    }
}
