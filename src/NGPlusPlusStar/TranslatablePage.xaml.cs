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

namespace NGPlusPlusStar.Pages
{
    /// <summary>
    /// Interaction logic for TranslatablePage.xaml
    /// </summary>
    public partial class TranslatablePage : Page
    {
        public TranslatablePage()
        {
            InitializeComponent();
            this.TranslateXSlider.Minimum = double.MinValue;
            this.TranslateXSlider.Maximum = double.MaxValue;
            this.TranslateYSlider.Minimum = double.MinValue;
            this.TranslateYSlider.Maximum = double.MaxValue;

            this.TranslationFrame.Focus();
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            var translation_increment = 1;
            switch (e.Key)
            {
                case Key.W:
                    this.TranslateYSlider.Value = this.TranslateYSlider.Value - translation_increment;
                    break;
                case Key.A:
                    this.TranslateXSlider.Value = this.TranslateXSlider.Value - translation_increment;
                    break;
                case Key.S:
                    this.TranslateYSlider.Value = this.TranslateYSlider.Value + translation_increment;
                    break;
                case Key.D:
                    this.TranslateXSlider.Value = this.TranslateXSlider.Value + translation_increment;
                    break;
            }
        }
    }
}
