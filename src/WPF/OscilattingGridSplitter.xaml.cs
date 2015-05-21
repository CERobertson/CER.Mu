using CER.Graphs;
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

namespace CER.WPF
{
    /// <summary>
    /// Interaction logic for OscilattingGridSplitter.xaml
    /// </summary>
    public partial class OscilattingGridSplitter : GridSplitter
    {
        public readonly static string Template = "{Initial:[Expanded],Expanded:[Collapsed],Collapsed:[Expanded]}";

        public Oscillator<State> Oscillator { get; private set; }

        public State Initial { get; private set; }

        public OscilattingGridSplitter()
        {
            InitializeComponent();
            this.Oscillator = new Oscillator<State>(OscilattingGridSplitter.Template);
            this.Initial = this.Oscillator.Graph[this.Oscillator.Roots[0]];
            this.Initial.children[0].Switch = (() => this.;
        }
    }
}
