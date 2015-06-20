using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace NGPlusPlusStar.Applications
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class GamepadApplication : Application
    {
        public DispatcherTimer GamepadPoll { get; private set; }
        public Player CurrentPlayer { get; private set; }

        public GamepadApplication()
        {
            InitializeComponent();

            this.GamepadPoll = new DispatcherTimer();
            this.GamepadPoll.Tick += gamepadPoll_Tick;
            this.GamepadPoll.Interval = new TimeSpan(0, 0, 0, 0, 12);
            this.GamepadPoll.Start();

            this.CurrentPlayer = new Player { Name = "CER" };
        }

        private float vibrationAmount;
        void gamepadPoll_Tick(object sender, EventArgs e)
        {
            if (this.process_gamepad)
            {
                GamePadState currentState = GamePad.GetState((PlayerIndex)this.CurrentPlayer.PlayerIndex);

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

        private bool process_gamepad;

        private void Application_Activated(object sender, EventArgs e)
        {
            this.process_gamepad = true;

        }

        private void Application_Deactivated(object sender, EventArgs e)
        {
            this.process_gamepad = false;
        }
    }
}
