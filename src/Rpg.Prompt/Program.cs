namespace CER.Rpg.Prompt
{
    using CER.Azure;
    using CER.Executable;
    using CER.Text;
    using Microsoft.WindowsAzure.Storage;
    using System;
    using System.Configuration;
    using System.Threading;

    class Program : CER.Executable.Program
    {
        private static readonly CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
        private static readonly string WarningTable = "CerRpgPrompt";
        private static Compiler RpgInterpreter = new Compiler();

        static void Main(string[] args)
        {
            Program.LogV("Entering CER.Rpg.Prompt.Program.Main");
            Program.LogI(
                string.Format("Running program {0}Prompt.exe{0}", Program.Separator) +
                string.Join(Program.Separator.ToString(), args));
            try
            {
                Program.ConnectAzureTableWarningListener();
                Program.InitializeInterpreter();
                Program.LogW("Completed Initialization.");

                Console.CancelKeyPress += delegate
                {
                    Program.CancellationTokenSource.Cancel();
                };
                Program.LogV("Entering main loop. Ctrl-C to exit.");
                string input;
                while (true)
                {
                    input = Console.ReadLine();
                    foreach (var token in Program.RpgInterpreter.Scan(input))
                    {
                        Program.LogV(string.Join(Program.Separator.ToString(), token.Name, token.Value));
                    }
                }
            }
            catch (Exception e)
            {
                Program.LogE(e.DetailedMessage());
            }
        }

        private static void InitializeInterpreter()
        {
            Program.LogV("Initializing the Rpg interpreter.");
            Program.RpgInterpreter.TokenRegex["character"] = "character";
            Program.RpgInterpreter.TokenRegex["game"] = "game";
            Program.RpgInterpreter.TokenRegex["plot"] = "plot";
            Program.RpgInterpreter.TokenRegex["identifier"] = @"\S+";
        }

        private static void ConnectAzureTableWarningListener()
        {
            if (bool.Parse(ConfigurationManager.AppSettings["LogWarningsInAzureTable"]))
            {
                Program.LogV("Connecting AzureTableWarningListener to log warnings.");
                var azure_trace_listener = new AzureTableWarningListener
                {
                    Name = "AzureTableWarningListener",
                    CancellationToken = Program.CancellationTokenSource.Token,
                    Table_Name = Program.WarningTable,
                    Separator = Program.Separator,
                    Storage_Account = CloudStorageAccount.Parse(Koans.What_is_storage_when_lost_or_locked),
                };
                azure_trace_listener.Table.CreateIfNotExists();
                Program.External.Listeners.Add(azure_trace_listener);
            }
        }
    }
}
