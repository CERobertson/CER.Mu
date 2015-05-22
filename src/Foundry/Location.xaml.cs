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
using CER.Mu;

namespace CER.Foundry
{
    /// <summary>
    /// Interaction logic for Location.xaml
    /// </summary>
    public partial class location : Page
    {
        public location()
        {
            InitializeComponent();
        }

        
        private rpg.DbContext rpg = new rpg.DbContext(new CreateSeedDatabaseIfNotExists());

        public void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            var link = (Hyperlink)sender;
            var text = new TextRange(link.ContentStart, link.ContentEnd).Text;
            var prototype = new rpg.location { gm_name = text };
            this.Navigation.Navigate(link.NavigateUri, this.rpg.SingleOrCreate(this.rpg.Locations, prototype, true));
        }

        public NavigationService Navigation { get; set; }
    }
}
