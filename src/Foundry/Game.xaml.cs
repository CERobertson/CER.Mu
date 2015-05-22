namespace CER.Foundry
{
    using CER.Mu;
    using rpg = CER.Rpg;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Navigation;
    using System.Windows;

    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class game : Page
    {
        public static DependencyProperty ReferenceProperty = DependencyProperty.Register("Reference", typeof(rpg.game), typeof(game));

        public void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            var link = (Hyperlink)sender;
            var text = new TextRange(link.ContentStart, link.ContentEnd).Text;
            var prototype = new rpg.game { gm_name = text };
            this.Reference = this.rpg.SingleOrCreate(this.rpg.Games, prototype, true);
            this.View.DataContext = this.Reference;
            this.Navigation.Navigate(link.NavigateUri, this.Reference);
        }

        public NavigationService Navigation { get; set; }

        public rpg.game Reference
        {
            get { return (rpg.game)this.GetValue(game.ReferenceProperty); }
            set { this.SetValue(game.ReferenceProperty, value); }
        }

        private rpg.DbContext rpg = new rpg.DbContext(new CreateSeedDatabaseIfNotExists());

        public game()
        {
            InitializeComponent();
            this.View.Loaded += game_Loaded;
        }

        void game_Loaded(object sender, RoutedEventArgs e)
        {
            this.View.DataContext = this.Reference;
        }
    }
}
