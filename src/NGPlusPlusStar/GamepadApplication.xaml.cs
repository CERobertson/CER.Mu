using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NGPlusPlusStar.Pages;
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
        public PlayerIndex CurrentPlayerGamepadIndex
        {
            get
            {
                var current_player_index = this.CurrentPlayer.PlayerIndex;
                if(current_player_index > -1)
                {
                    return (PlayerIndex)current_player_index;
                }
                throw new NoGamepadExcpetion(string.Format(NoGamepadExcpetion.MessageTemplate, "this.CurrentPlayer.PlayerIndex must be greater than zero."));
            }
        }
        public MainPage MainPage { get; private set; }


        public GamepadApplication()
        {
            InitializeComponent();
            this.CurrentPlayer = new Player { Name = "CER" };
        }

        void GamepadApplication_MainPageNavigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            this.MainPage = e.Content as MainPage;
            if (this.MainPage != null)
            {
                this.GamepadPoll = new DispatcherTimer();
                this.GamepadPoll.Tick += gamepadPoll_Tick;
                this.GamepadPoll.Interval = new TimeSpan(0, 0, 0, 0, 12);
                this.GamepadPoll.Start();

                this.Navigated -= this.GamepadApplication_MainPageNavigated;
            }
        }

        private float vibrationAmount;
        void gamepadPoll_Tick(object sender, EventArgs e)
        {
            if (this.MainPage.ProcessGamepad)
            {
                try
                {
                    GamePadState currentState = GamePad.GetState(this.CurrentPlayerGamepadIndex);

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
                catch (NoGamepadExcpetion)
                {
                    this.MainPage.ProcessGamepad = false;
                }
            }
        }


        private void GamepadApplication_Activated(object sender, EventArgs e)
        {
            this.MainPage.ProcessGamepad = true;
        }

        private void GamepadApplication_Deactivated(object sender, EventArgs e)
        {
            this.MainPage.ProcessGamepad = false;
        }
    }

    public class NoGamepadExcpetion : Exception
    {
        public static readonly string MessageTemplate = @"No gamepad detected. Details: {0}";
        public NoGamepadExcpetion() : this(string.Format(NoGamepadExcpetion.MessageTemplate, "No additional details.")) { }
        public NoGamepadExcpetion(string message) : base(message) { }
    }
}
