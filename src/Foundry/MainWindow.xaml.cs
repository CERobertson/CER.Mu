namespace CER.Foundry
{
    using CER.Mu;
    using CER.Rpg;
    using Microsoft.Win32;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Documents;


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly string DialogFilter = "XAML Files (*.xaml)|*.xaml|RichText Files (*.rtf)|*.rtf|All files (*.*)|*.*";
        private DbContext rpg = new DbContext(new CreateSeedDatabaseIfNotExists());

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var gameViewSource = (CollectionViewSource)this.FindResource("gameViewSource");
            this.rpg.Games.ToList();
            gameViewSource.Source = this.rpg.Games.Local;
            this.NavigateRpgFrameToGame("mu");
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            this.rpg.SaveChanges();
            this.rpg.Dispose();
        }

        private void OpenCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = this.DialogFilter;

            if (dialog.ShowDialog() == true)
            {
                var textRange = new TextRange(
                    this.Editor.Document.ContentStart,
                    this.Editor.Document.ContentEnd);
                using (var fs = File.Open(dialog.FileName, FileMode.Open))
                {
                    if (Path.GetExtension(dialog.FileName).ToLower() == ".rtf")
                    {
                        textRange.Load(fs, DataFormats.Rtf);
                    }
                    else
                    {
                        textRange.Load(fs, DataFormats.Xaml);
                    }
                }
            }
        }

        private void SaveBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = this.DialogFilter;

            if (dialog.ShowDialog() == true)
            {
                var textRange = new TextRange(
                    this.Editor.Document.ContentStart,
                    this.Editor.Document.ContentEnd);
                using (var fs = File.Create(dialog.FileName))
                {
                    if (Path.GetExtension(dialog.FileName).ToLower() == ".rtf")
                    {
                        textRange.Save(fs, DataFormats.Rtf);
                    }
                    else
                    {
                        textRange.Save(fs, DataFormats.Xaml);
                    }
                }
            }
        }

        private void Editor_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            using (var s = new MemoryStream())
            {
                var textRange = new TextRange(
                    this.Editor.Document.ContentStart,
                    this.Editor.Document.ContentEnd);
                textRange.Save(s, DataFormats.Xaml);
                s.Position = 0;

                using (var r = new StreamReader(s))
                {
                    this.EditorMarkup.Text = r.ReadToEnd();
                }
            }
        }
        
        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            var link = (Hyperlink)sender;
            var text = new TextRange(link.ContentStart, link.ContentEnd).Text;
            var prototype = new game { gm_name = text };
            this.NavigateRpgFrameToGame(text, prototype);
        }

        private void NavigateRpgFrameToGame(string key, game obj = null)
        {
            var game = new Game();
            game.ToObserve = this.rpg.SingleOrCreate(this.rpg.Games, x => x.gm_name == key, obj);
            this.RpgFrame.Navigate(game);
        }
    }
}
