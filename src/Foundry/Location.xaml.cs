namespace CER.Foundry
{
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Navigation;
    using r = CER.Rpg;

    /// <summary>
    /// Interaction logic for Location.xaml
    /// </summary>
    public partial class location : Page
    {
        public location()
        {
            InitializeComponent();
        }
        
        public void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            var link = (Hyperlink)sender;
            var text = new TextRange(link.ContentStart, link.ContentEnd).Text;
            var prototype = new r.location { gm_name = text };
            this.Navigation.Navigate(link.NavigateUri, this.CurrentGame.SingleOrCreate(this.CurrentGame.Locations, prototype, true));
        }

        public NavigationService Navigation { get; set; }
        public r.DbContext CurrentGame { get; set; }
    }
}
