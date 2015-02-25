namespace CER.Azure
{
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;

    public class AzureTableWarningListener : TraceListener
    {
        private char default_separator = '|';
        public char Separator
        { get { return this.default_separator; } set { this.default_separator = value; } }

        #region AzureTable
        public CancellationToken CancellationToken { get; set; }

        public CloudStorageAccount Storage_Account { get; set; }
        public AzureTableWarningListener()
        {
            this.table_client = (() => this.Storage_Account.CreateCloudTableClient());
            this.table = (() => table_client().GetTableReference(this.Table_Name));
        }

        private delegate CloudTableClient default_table_client();
        private default_table_client table_client;
        public CloudTableClient Table_Client
        {
            get
            {
                return this.table_client();
            }
            set
            {
                this.table_client = (() => value);
            }
        }

        private delegate CloudTable default_table();
        private default_table table;
        public string Table_Name { get; set; }
        public CloudTable Table
        {
            get
            {
                return this.table();
            }
            set
            {
                this.table = (() => value);
            }
        }
        #endregion

        #region TraceListener
        private void write(string message)
        {
            var details = message.Split(this.default_separator);
            if (details.Count() != 3)
            {
                throw new ArgumentException("AzureTableTraceListener requires an identity, time and message.");
            }
            var warning = new Warning
            {
                PartitionKey = details[0],
                RowKey = details[1],
                Message = details[2]
            };
            this.Table.ExecuteAsync(TableOperation.Insert(warning), this.CancellationToken);
        }
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            this.write(message);
        }
        public override void Write(string message)
        {
            this.write(message);
        }
        public override void WriteLine(string message)
        {
            this.write(message);
        }
        #endregion
    }

    public class Warning : TableEntity
    {
        public string Message { get; set; }
    }
}
