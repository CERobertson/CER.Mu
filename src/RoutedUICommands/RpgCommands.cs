namespace CER.RoutedUICommands
{
    using System.Windows.Input;

    public class RpgCommands
    {
        private static RoutedUICommand newBelief;
        private static RoutedUICommand newGame;
        private static RoutedUICommand newLocation;
        static RpgCommands()
        {
            var inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.G, ModifierKeys.Alt, "Alt-G"));
            RpgCommands.newGame = new RoutedUICommand("new game", "new game", typeof(RpgCommands), inputs);

            inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.L, ModifierKeys.Alt, "Alt-L"));
            RpgCommands.newLocation = new RoutedUICommand("new location", "new location", typeof(RpgCommands), inputs);

            inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.B, ModifierKeys.Alt, "Alt-B"));
            RpgCommands.newBelief = new RoutedUICommand("new belief", "new location", typeof(RpgCommands), inputs);
        }

        public static RoutedUICommand NewBelief { get { return newBelief; } }
        public static RoutedUICommand NewGame { get { return newGame; } }
        public static RoutedUICommand NewLocation { get { return newLocation; } }
    }
}
