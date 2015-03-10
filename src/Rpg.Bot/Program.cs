namespace CER.Rpg.Bot
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using CER.Executable;
    using CER.Azure;
    using CER.Roslyn;
    using Microsoft.WindowsAzure.Storage;
    using System.Threading;
    //using Meebey.SmartIrc4net;
    //using Mu;

    public class Program : CER.Executable.Program
    {
        private static readonly CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
        private static readonly string WarningTable = "CerDnDMu";

        private static void Main(string[] args)
        {
            Program.LogV("Entering CER.Rpg.Bot.Program.Main");
            Program.LogI(string.Format("Running program {0}CER.Rpg.Bot.exe{0}", Program.Separator) + string.Join(Program.Separator.ToString(), args));

            try
            {
                Program.ConnectAzureTableWarningListener();
                //Program.ConnectToIrc();
                Program.LogW("Completed Initialization.");

                Console.CancelKeyPress += delegate
                {
                    Program.CancellationTokenSource.Cancel();
                };
                Program.LogV("Entering main loop. Ctrl-C to exit.");
                string input;
                while (true)
                {

                }
            }
            catch (Exception e)
            {
                Program.LogE(e.DetailedMessage());
            }
        }

        //public static IrcClient IRC = new IrcClient();

        // this method will get all IRC messages
        //public static void OnRawMessage(object sender, IrcEventArgs e)
        //{
        //    Program.LogI(string.Format("{0}{1}{2}", "Program.OnRawMessage", Program.Separator, e.Data.RawMessage));
        //}

        private static void ConnectAzureTableWarningListener()
        {
            if (bool.Parse(ConfigurationManager.AppSettings["LogWarningsInAzureTable"]))
            {
                Program.LogV("Connecting AzureTableWarningListener to log warnings");
                var azure_trace_listener = new AzureTableWarningListener
                {
                    Name = "AzureTableWarningListener",
                    CancellationToken = Program.CancellationTokenSource.Token,
                    Table_Name = Program.WarningTable,
                    Separator = Program.Separator,
                    //Storage_Account = CloudStorageAccount.Parse(Koans.What_is_storage_when_lost_or_locked),
                };
                azure_trace_listener.Table.CreateIfNotExists();
                Program.External.Listeners.Add(azure_trace_listener);
            }
        }
        //private static void ConnectToIrc()
        //{
        //    // UTF-8 test
        //    Program.IRC.Encoding = System.Text.Encoding.UTF8;

        //    // wait time between messages, we can set this lower on own irc servers
        //    Program.IRC.SendDelay = 200;

        //    // we use channel sync, means we can use irc.GetChannel() and so on
        //    Program.IRC.ActiveChannelSyncing = true;

        //    // here we connect the events of the API to our written methods
        //    // most have own event handler types, because they ship different data
        //    Program.IRC.OnQueryMessage += new IrcEventHandler(OnRawMessage);
        //    Program.IRC.OnError += new ErrorEventHandler(OnRawMessage);
        //    Program.IRC.OnRawMessage += new IrcEventHandler(OnRawMessage);

        //    string[] serverlist;
        //    // the server we want to connect to, could be also a simple string
        //    serverlist = new string[] { "irc.twitch.tv" };
        //    int port = 6667;
        //    string channel = "#user_with_name17";
        //    Program.IRC.Connect(serverlist, port);
        //    // here we logon and register our nickname and so on 
        //    Program.IRC.Login(
        //        Mu.Koans.Who_is_this_presense,
        //        Mu.Koans.Who_is_this_presense, 0,
        //        Mu.Koans.Who_is_this_presense,
        //        Mu.Koans.How_do_we_know_you_speak_the_truth);
        //    // join the channel
        //    Program.IRC.RfcJoin(channel);

        //}
    }
}