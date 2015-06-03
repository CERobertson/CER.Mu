namespace CER.Foundry
{
    using CER.ng;
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
    using ef = System.Data.Entity;
    using rpg = CER.Rpg;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Default constructor/deconstructor and datacontext to CollectionViewSource management. (Importance:not really Status:proof of concept for CollectionViewSource)

        public GameContext CurrentGame { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.ApplicationName = this.Title;
            this.CurrentGame = new GameContext(string.Empty);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //hooks up the collection viewer, most likely wont last
            var gameViewSource = (CollectionViewSource)this.FindResource("gameViewSource");
            this.CurrentGame.Games.ToList();
            gameViewSource.Source = this.CurrentGame.Games.Local;

            CER.RoutedUICommands.FoundryCommands.RefreshLinks.Execute(null, null);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            this.CurrentGame.SaveChanges();
            this.CurrentGame.Dispose();
        }
        #endregion

        #region New, open, and save commands. (Importance:important Status:needs to be linked to game partition)
        public readonly string DialogFilter = "XAML Files (*.xaml)|*.xaml|RichText Files (*.rtf)|*.rtf|All files (*.*)|*.*";
        public readonly string ApplicationName;

        private void NewCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            this.Editor.Document = new FlowDocument();
            this.SaveBinding_Executed(sender, e);
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
                this.CurrentGame.LoadGame(dialog.FileName);
                this.Title = this.ApplicationName + " - " + dialog.FileName;
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
                this.CurrentGame.LoadGame(dialog.FileName);
                this.Title = this.ApplicationName + " - " + dialog.FileName;
            }
        }
        #endregion

        #region Links flow document hyperlinks to rpgFrame page event handlers. (Importance:important Status:working well)
        private void newRpgBinding(Hyperlink link)
        {
            var page = link.NavigateUri.LocalPath.ToLower().TrimStart('/').Split('.')[0];
            var game = Application.LoadComponent(new Uri(link.NavigateUri.LocalPath, UriKind.Relative)) as game;
            if (game != null)
            {
                game.Navigation = this.RpgFrame.NavigationService;
                game.CurrentGame = this.CurrentGame;
                link.RequestNavigate += game.Hyperlink_RequestNavigate;
            }
            var character = Application.LoadComponent(new Uri(link.NavigateUri.LocalPath, UriKind.Relative)) as character;
            if (character != null)
            {
                character.Navigation = this.RpgFrame.NavigationService;
                character.CurrentGame = this.CurrentGame;
                link.RequestNavigate += character.Hyperlink_RequestNavigate;
            }
            var hypotheses = Application.LoadComponent(new Uri(link.NavigateUri.LocalPath, UriKind.Relative)) as hypotheses;
            if (hypotheses != null)
            {
                hypotheses.Navigation = this.RpgFrame.NavigationService;
                hypotheses.CurrentGame = this.CurrentGame;
                link.RequestNavigate += hypotheses.Hyperlink_RequestNavigate;
            }
            var belief = Application.LoadComponent(new Uri(link.NavigateUri.LocalPath, UriKind.Relative)) as belief;
            if (belief != null)
            {

                belief.Navigation = this.RpgFrame.NavigationService;
                belief.CurrentGame = this.CurrentGame;
                link.RequestNavigate += belief.Hyperlink_RequestNavigate;
            }
            var location = Application.LoadComponent(new Uri(link.NavigateUri.LocalPath, UriKind.Relative)) as location;
            if (location != null)
            {
                location.Navigation = this.RpgFrame.NavigationService;
                location.CurrentGame = this.CurrentGame;
                link.RequestNavigate += location.Hyperlink_RequestNavigate;
            }
        }
        #endregion

        #region Commands to link editor selection to page subclass. (Importance:important Status:working well)
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
        #endregion

        #region Traverses flow document and links hyperlinks to rpgFrame pages. (Importance:important Status:working well)
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
        #endregion

        #region Reflect changes to flow documents underlying markup (Importance:minimal Status:ineffiecent but quick way to obseve the results of edits)
        //private void Editor_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        //{
        //    using (var s = new MemoryStream())
        //    {
        //        var textRange = new TextRange(
        //            this.Editor.Document.ContentStart,
        //            this.Editor.Document.ContentEnd);
        //        textRange.Save(s, DataFormats.Xaml);
        //        s.Position = 0;

        //        using (var r = new StreamReader(s))
        //        {
        //            this.EditorMarkup.Text = r.ReadToEnd();
        //        }
        //    }
        //}
        #endregion

        #region Post processing of rpg objects after frame has loaded. (Importance:minimal Status:useful proof of concept)
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
        #endregion
    }
}
