namespace CER.RoutedUICommands
{
    using System.Windows.Input;

    public class FoundryCommands
    {
        private static RoutedUICommand refreshLinks;
        static FoundryCommands()
        {
            var inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.F5));
            FoundryCommands.refreshLinks = new RoutedUICommand("refresh links", "refresh links", typeof(FoundryCommands), inputs);
        }

        public static RoutedUICommand RefreshLinks { get { return refreshLinks; } }
    }
}
