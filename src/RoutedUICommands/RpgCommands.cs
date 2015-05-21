namespace CER.RoutedUICommands
{
    using System.Windows.Input;

    public class RpgCommands
    {
        private static RoutedUICommand newGame;
        static RpgCommands()
        {
            var inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.G, ModifierKeys.Alt, "Alt-G"));
            RpgCommands.newGame = new RoutedUICommand("new game", "new game", typeof(RpgCommands), inputs);
        }

        public static RoutedUICommand NewGame { get { return newGame; } }
    }
}
