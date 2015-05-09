namespace CER.Foundry
{
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


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DbContext rpg = new DbContext(new CreateSeedDatabaseIfNotExists());

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource gameViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("gameViewSource")));
            this.rpg.Games.ToList();
            this.gameListView.DataContext = this.rpg.Games.Local;
            gameViewSource.Source = this.rpg.Games.Local;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            this.rpg.Dispose();
        }
    }
}
