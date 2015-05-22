using CER.Mu;
using rpg = CER.Rpg;
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

namespace CER.Foundry
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class game : Page
    {
        public static DependencyProperty ToObserveProperty = DependencyProperty.Register("ToObserve", typeof(rpg.game), typeof(game));

        public void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            var link = (Hyperlink)sender;
            var text = new TextRange(link.ContentStart, link.ContentEnd).Text;
            var prototype = new rpg.game { gm_name = text };
            this.Navigation.Navigate(link.NavigateUri, this.rpg.SingleOrCreate(this.rpg.Games, prototype, true));
        }

        public NavigationService Navigation { get; set; }

        public rpg.game ToObserve
        {
            get { return (rpg.game)this.GetValue(game.ToObserveProperty); }
            set { this.SetValue(game.ToObserveProperty, value); }
        }

        private rpg.DbContext rpg = new rpg.DbContext(new CreateSeedDatabaseIfNotExists());

        public game()
        {
            InitializeComponent();
        }
    }
}
