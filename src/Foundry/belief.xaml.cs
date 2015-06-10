namespace CER.Foundry
{
    using CER.JudeaPearl.CausalNetwork;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Navigation;
    using r = CER.Rpg;
    using CER.JudeaPearl;

    public enum V
    {
        empty = 0,
        A,
        B,
        C
    }

    /// <summary>
    /// Interaction logic for belief.xaml
    /// </summary>
    public partial class belief : Page
    {
        public static Belief Belief_NextChar_PreviousChar
        {
            get
            {
                var belief = new Belief();
                belief.variable = "P(v-jth|v-ith)";
                belief.ConditionalProbability = new ConditionalProbability(
                    @"[['.25','.25','.25','.25']
                      ,['.5','0','.25','.25']
                      ,['.125','.5','.125','.25']
                      ,['.25','.125','.5','.125']]").ToMatrix();
                return belief;
            }
        }
        public static Belief Belief_CorruptedChar_SentChar
        {
            get
            {
                var belief = new Belief();
                belief.variable = "P(v-kth|v-jth)";
                belief.ConditionalProbability = new ConditionalProbability(
                    @"[['.9','.1','0','0']
                      ,['.1','.8','.1','0']
                      ,['0','.1','.8','.1']
                      ,['0','.1','.1','.8']]").ToMatrix();
                return belief;
            }
        }
        public belief()
        {
            var received = new[] { 
                V.empty,
                V.empty,
                V.B,
                V.C,
                V.A,
                V.empty,
                V.empty };
            var root = new Belief();
            root.variable = "v-ith";
            var v_ith = root;
            foreach (var i in received)
            {
                var v_jth_given_v_ith = belief.Belief_NextChar_PreviousChar;
                var v_kth_given_v_jth = belief.Belief_CorruptedChar_SentChar;
                v_ith.Causes(v_jth_given_v_ith);
                v_jth_given_v_ith.Causes(v_kth_given_v_jth);
                v_ith = v_kth_given_v_jth;
            }
            this.Root = root;
            InitializeComponent();
        }

        public Belief Root { get; private set; }

        public void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            var link = (Hyperlink)sender;
            var text = new TextRange(link.ContentStart, link.ContentEnd).Text;
            this.Navigation.Navigate(link.NavigateUri, this.Root);
        }

        public NavigationService Navigation { get; set; }
        public r.DbContext CurrentGame { get; set; }
    }
}
