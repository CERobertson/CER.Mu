namespace CER.Commands
{
    using System.Windows.Input;

    public class RpgCommands
    {
        private static RoutedUICommand newBelief;
        private static RoutedUICommand newCharacter;
        private static RoutedUICommand newHypotheses;
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
            RpgCommands.newBelief = new RoutedUICommand("new belief", "new belief", typeof(RpgCommands), inputs);

            inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.C, ModifierKeys.Alt, "Alt-C"));
            RpgCommands.newCharacter = new RoutedUICommand("new character", "new character", typeof(RpgCommands), inputs);

            inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.H, ModifierKeys.Alt, "Alt-H"));
            RpgCommands.newHypotheses = new RoutedUICommand("new hypotheses", "new hypotheses", typeof(RpgCommands), inputs);
        }

        public static RoutedUICommand NewBelief { get { return newBelief; } }
        public static RoutedUICommand NewCharacter { get { return newCharacter; } }
        public static RoutedUICommand NewHypotheses { get { return newHypotheses; } }
        public static RoutedUICommand NewGame { get { return newGame; } }
        public static RoutedUICommand NewLocation { get { return newLocation; } }
    }
}
