using CER.Mu;
using CER.Rpg;
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
    public partial class Game : Page
    {
        public static DependencyProperty ToObserveProperty = DependencyProperty.Register("ToObserve", typeof(game), typeof(Game));

        public game ToObserve
        {
            get { return (game)this.GetValue(Game.ToObserveProperty); }
            set { this.SetValue(Game.ToObserveProperty, value); }
        }

        private DbContext rpg = new DbContext(new CreateSeedDatabaseIfNotExists());

        public Game()
        {
            InitializeComponent();
        }
    }
}
