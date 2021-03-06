﻿namespace CER.Foundry
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Navigation;
    using r = CER.Rpg;

    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class game : Page
    {
        public static DependencyProperty ReferenceProperty = DependencyProperty.Register("Reference", typeof(r.game), typeof(game));

        public void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            var link = (Hyperlink)sender;
            var text = new TextRange(link.ContentStart, link.ContentEnd).Text;
            var prototype = new r.game { gm_name = text };
            this.DataContext = this.CurrentGame.SingleOrCreate(this.CurrentGame.Games, prototype, true);
            this.Navigation.Navigate(this, this.Reference);
        }

        public NavigationService Navigation { get; set; }
        public r.DbContext CurrentGame { get; set; }

        public r.game Reference
        {
            get { return (r.game)this.GetValue(game.ReferenceProperty); }
            set { this.SetValue(game.ReferenceProperty, value); }
        }


        public game()
        {
            InitializeComponent();
        }
    }
}
