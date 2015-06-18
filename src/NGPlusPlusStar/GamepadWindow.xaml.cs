
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace NGPlusPlusStar.Windows
{
    /// <summary>
    /// Interaction logic for GamepadWindow.xaml
    /// </summary>
    public partial class GamepadWindow : Window
    {
        public DispatcherTimer gamepadPoll;

        public GamepadWindow()
        {
            InitializeComponent();
            this.gamepadPoll = new DispatcherTimer();
            this.gamepadPoll.Tick += gamepadPoll_Tick;
            this.gamepadPoll.Interval = new TimeSpan(0, 0, 0, 0, 12);
            this.gamepadPoll.Start();
        }

        private float vibrationAmount;
        void gamepadPoll_Tick(object sender, EventArgs e)
        {
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);

            // Process input only if connected and button A is pressed.
            if (currentState.IsConnected && currentState.Buttons.A ==
                ButtonState.Pressed)
            {
                // Button A is currently being pressed; add vibration.
                vibrationAmount =
                    MathHelper.Clamp(vibrationAmount + 0.03f, 0.0f, 1.0f);
                GamePad.SetVibration(PlayerIndex.One,
                    vibrationAmount, vibrationAmount);
            }
            else
            {
                // Button A is not being pressed; subtract some vibration.
                vibrationAmount =
                    MathHelper.Clamp(vibrationAmount - 0.05f, 0.0f, 1.0f);
                GamePad.SetVibration(PlayerIndex.One,
                    vibrationAmount, vibrationAmount);
            }
        }
    }
}
