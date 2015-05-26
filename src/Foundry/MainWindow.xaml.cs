namespace CER.Foundry
{
    using CER.Mu;
    using CER.RoutedUICommands;
    using rpg = CER.Rpg;
    using Microsoft.Win32;
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Navigation;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly string DialogFilter = "XAML Files (*.xaml)|*.xaml|RichText Files (*.rtf)|*.rtf|All files (*.*)|*.*";
        private rpg.DbContext rpg = new rpg.DbContext(new CreateSeedDatabaseIfNotExists());

        public static DependencyProperty InternalDocument_PreviousPartitionHeight_Property = DependencyProperty.Register("InternalDocument_PreviousPartitionHeight", typeof(int), typeof(MainWindow));

        public int InternalDocument_PreviousPartitionHeight
        {
            get { return (int)this.GetValue(MainWindow.InternalDocument_PreviousPartitionHeight_Property); }
            set { this.SetValue(MainWindow.InternalDocument_PreviousPartitionHeight_Property, value); }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //hooks up the collection viewer, most likely wont last
            var gameViewSource = (CollectionViewSource)this.FindResource("gameViewSource");
            this.rpg.Games.ToList();
            gameViewSource.Source = this.rpg.Games.Local;

            CER.RoutedUICommands.FoundryCommands.RefreshLinks.Execute(null, null);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            this.rpg.SaveChanges();
            this.rpg.Dispose();
        }

        private void NewCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            this.Editor.Document = new FlowDocument();
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
            CER.RoutedUICommands.FoundryCommands.RefreshLinks.Execute(null, null);
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

        void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            var game = e.ExtraData as rpg.game;
            int g = 0;
            int l = 0;
            if (game != null)
            {
                g++;
            }
            var location = e.ExtraData as rpg.location;
            if (location != null)
            {
                l++;
            }
        }

        private void BeliefBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.InsertLink("pack://application:,,,/Belief.xaml");
        }

        private void CharacterBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.InsertLink("pack://application:,,,/Character.xaml");
        }

        private void HypothesesBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.InsertLink("pack://application:,,,/Hypotheses.xaml");
        }

        private void NewGameBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            this.InsertLink("pack://application:,,,/Game.xaml");
        }

        private void NewLocationBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.InsertLink("pack://application:,,,/Location.xaml");
        }

        private void InsertLink(string uri)
        {
            var selection = this.Editor.Selection;
            var link = new Hyperlink(selection.Start, selection.End);
            link.NavigateUri = new Uri(uri);
            this.newRpgBinding(link);

            using (var s = new MemoryStream())
            {
                var textRange = new TextRange(link.ContentStart, link.ContentEnd);
                textRange.Save(s, DataFormats.Xaml);
                s.Position = 0;
                selection.Load(s, DataFormats.Xaml);
            }
            CER.RoutedUICommands.FoundryCommands.RefreshLinks.Execute(null, null);
        }

        private void newRpgBinding(Hyperlink link)
        {
            var page = link.NavigateUri.LocalPath.ToLower().TrimStart('/').Split('.')[0];
            var game = Application.LoadComponent(new Uri(link.NavigateUri.LocalPath, UriKind.Relative)) as game;
            if (game != null)
            {
                game.Navigation = this.RpgFrame.NavigationService;
                link.RequestNavigate += game.Hyperlink_RequestNavigate;
            }
            var character = Application.LoadComponent(new Uri(link.NavigateUri.LocalPath, UriKind.Relative)) as character;
            if (character != null)
            {
                character.Navigation = this.RpgFrame.NavigationService;
                link.RequestNavigate += character.Hyperlink_RequestNavigate;
            }
            var hypotheses = Application.LoadComponent(new Uri(link.NavigateUri.LocalPath, UriKind.Relative)) as hypotheses;
            if (hypotheses != null)
            {
                hypotheses.Navigation = this.RpgFrame.NavigationService;
                link.RequestNavigate += hypotheses.Hyperlink_RequestNavigate;
            }

            switch (page)
            {
                case "belief":
                    var belief = new belief();
                    belief.Navigation = this.RpgFrame.NavigationService;
                    link.RequestNavigate += belief.Hyperlink_RequestNavigate;
                    break;
                case"location":
                    var location = new location();
                    location.Navigation = this.RpgFrame.NavigationService;
                    link.RequestNavigate += location.Hyperlink_RequestNavigate;
                    break;
                case "game":
                    break;
                case "character":
                    break;
                case "hypotheses":
                    break;
                default:
                    throw new Exception(string.Format("Page does not exist for local path {0}", link.NavigateUri.LocalPath));
            }
        }

        private void RefreshBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.iterateSections(this.Editor.Document.Blocks);
            this.iterateParagraphs(this.Editor.Document.Blocks);
        }

        private void iterateSections(BlockCollection blocks)
        {
            foreach (var block in blocks)
            {
                var section = block as Section;
                if (section != null)
                {
                    iterateParagraphs(section.Blocks);
                }
            }
        }

        private void iterateParagraphs(BlockCollection blocks)
        {
            foreach (var block in blocks)
            {
                var paragraph = block as Paragraph;
                if (paragraph != null)
                {
                    foreach (var inline in paragraph.Inlines)
                    {
                        var link = inline as Hyperlink;
                        if (link != null)
                        {
                            this.newRpgBinding(link);
                        }
                    }
                }
            }
        }
    }
}
