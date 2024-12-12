namespace DevExpress.Xpo.Providers
{
    using DevExpress.Data.Helpers;
    using DevExpress.Xpo.DB;
    using DevExpress.Xpo.Helpers;
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    public class STASafeDataStore : IDataStore, IDataStoreAsync, IDataStoreSchemaExplorer, ICommandChannel, ICommandChannelAsync
    {
        private readonly IDataStore DataStore;

        public STASafeDataStore()
        {
        }

        public STASafeDataStore(IDataStore dataStore) : this()
        {
            this.DataStore = dataStore;
        }

        public object Do(string command, object args)
        {
            ICommandChannel nestedCommandChannel = this.DataStore as ICommandChannel;
            if (nestedCommandChannel != null)
            {
                return StaSafeHelper.Invoke<object>(() => nestedCommandChannel.Do(command, args));
            }
            if (this.DataStore == null)
            {
                throw new NotSupportedException($"Command '{command}' is not supported.");
            }
            throw new NotSupportedException($"Command '{command}' is not supported by {this.DataStore.GetType()}.");
        }

        public System.Threading.Tasks.Task<object> DoAsync(string command, object args, CancellationToken cancellationToken = new CancellationToken())
        {
            ICommandChannelAsync dataStore = this.DataStore as ICommandChannelAsync;
            if ((dataStore != null) && !IsStaCurrentThread)
            {
                return dataStore.DoAsync(command, args, cancellationToken);
            }
            ICommandChannel nestedCommandChannel = this.DataStore as ICommandChannel;
            if (nestedCommandChannel != null)
            {
                return Task.Factory.StartNew<object>(() => nestedCommandChannel.Do(command, args), cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }
            if (this.DataStore == null)
            {
                throw new NotSupportedException($"Command '{command}' is not supported.");
            }
            throw new NotSupportedException($"Command '{command}' is not supported by {this.DataStore.GetType()}.");
        }

        public DBTable[] GetStorageTables(params string[] tables) => 
            StaSafeHelper.Invoke<DBTable[]>(() => ((IDataStoreSchemaExplorer) this.DataStore).GetStorageTables(tables));

        public string[] GetStorageTablesList(bool includeViews) => 
            StaSafeHelper.Invoke<string[]>(() => ((IDataStoreSchemaExplorer) this.DataStore).GetStorageTablesList(includeViews));

        public ModificationResult ModifyData(params ModificationStatement[] dmlStatements) => 
            StaSafeHelper.Invoke<ModificationResult>(() => this.DataStore.ModifyData(dmlStatements));

        public System.Threading.Tasks.Task<ModificationResult> ModifyDataAsync(CancellationToken cancellationToken, params ModificationStatement[] dmlStatements)
        {
            IDataStoreAsync dataStore = this.DataStore as IDataStoreAsync;
            return (((dataStore == null) || IsStaCurrentThread) ? Task.Factory.StartNew<ModificationResult>(() => this.DataStore.ModifyData(dmlStatements), cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default) : dataStore.ModifyDataAsync(cancellationToken, dmlStatements));
        }

        public SelectedData SelectData(params SelectStatement[] selects) => 
            StaSafeHelper.Invoke<SelectedData>(() => this.DataStore.SelectData(selects));

        public System.Threading.Tasks.Task<SelectedData> SelectDataAsync(CancellationToken cancellationToken, params SelectStatement[] selects)
        {
            IDataStoreAsync dataStore = this.DataStore as IDataStoreAsync;
            return (((dataStore == null) || IsStaCurrentThread) ? Task.Factory.StartNew<SelectedData>(() => this.DataStore.SelectData(selects), cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default) : dataStore.SelectDataAsync(cancellationToken, selects));
        }

        public UpdateSchemaResult UpdateSchema(bool doNotCreateIfFirstTableNotExist, params DBTable[] tables) => 
            StaSafeHelper.Invoke<UpdateSchemaResult>(() => this.DataStore.UpdateSchema(doNotCreateIfFirstTableNotExist, tables));

        private static bool IsStaCurrentThread =>
            Thread.CurrentThread.GetApartmentState() == ApartmentState.STA;

        public DevExpress.Xpo.DB.AutoCreateOption AutoCreateOption =>
            StaSafeHelper.Invoke<DevExpress.Xpo.DB.AutoCreateOption>(() => this.DataStore.AutoCreateOption);
    }
}

