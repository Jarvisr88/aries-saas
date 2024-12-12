namespace DevExpress.Data.Async.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Async;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class CommandQueue : IAsyncCommandVisitor
    {
        public readonly ManualResetEvent MessageWaiter;
        private readonly Queue<Command> inputQueueNormalPriority;
        private readonly Queue<Command> inputQueueLoweredPriority;
        private readonly Queue<Command> outQueue;
        protected IListServer ListServer;
        public object TypeInfo;
        public PropertyDescriptorCollection PropertyDescriptors;
        public ManualResetEvent TypeInfoObtained;
        public bool PropertyDescriptorsNeedReset;
        private Command CurrentCommand;
        private volatile bool skipPosts;
        private readonly System.Threading.SynchronizationContext SynchronizationContext;
        private readonly SendOrPostCallback SomethingInTheOutputQueueCallback;
        private readonly EventHandler<ListServerGetOrFreeEventArgs> ListServerGet;
        private readonly EventHandler<ListServerGetOrFreeEventArgs> ListServerFree;
        private readonly EventHandler<GetTypeInfoEventArgs> GetTypeInfo;
        private readonly EventHandler<GetWorkerThreadRowInfoEventArgs> GetWorkerThreadRowInfo;
        private static readonly object LowPriorityToken;
        private static readonly object LowPriorityTag;
        private volatile bool _Terminate;
        private CommandQueue.BagOfTricks _bagOfTricks;
        public static bool? ForceThrottleInconsistency;
        private readonly List<DateTime> inconsistenciesWindow;

        static CommandQueue();
        public CommandQueue(System.Threading.SynchronizationContext context, SendOrPostCallback somethingInTheOutputQueueCallback, EventHandler<ListServerGetOrFreeEventArgs> listServerGet, EventHandler<ListServerGetOrFreeEventArgs> listServerFree, EventHandler<GetTypeInfoEventArgs> getTypeInfo, EventHandler<GetWorkerThreadRowInfoEventArgs> getWorkerThreadRowInfo);
        public void AskForPosts();
        public void AskForTermination();
        private void AskUIToDispatchOutputQueue();
        public void Cancel<T>() where T: Command;
        public void CancelAll();
        public void CancelAllButApply();
        public void CancelAllButRefresh();
        private void ClearTricksCache();
        void IAsyncCommandVisitor.Canceled(Command canceled);
        void IAsyncCommandVisitor.Visit(CommandApply result);
        void IAsyncCommandVisitor.Visit(CommandFindIncremental result);
        void IAsyncCommandVisitor.Visit(CommandGetAllFilteredAndSortedRows command);
        void IAsyncCommandVisitor.Visit(CommandGetGroupInfo result);
        void IAsyncCommandVisitor.Visit(CommandGetRow result);
        void IAsyncCommandVisitor.Visit(CommandGetRowIndexByKey result);
        void IAsyncCommandVisitor.Visit(CommandGetTotals result);
        void IAsyncCommandVisitor.Visit(CommandGetUniqueColumnValues result);
        void IAsyncCommandVisitor.Visit(CommandLocateByValue result);
        void IAsyncCommandVisitor.Visit(CommandPrefetchRows command);
        void IAsyncCommandVisitor.Visit(CommandRefresh result);
        private void DoLoop();
        private void DoLoop_Core();
        private void DoTricks();
        private void EstablishNormalPriority();
        private static IListServer ExtractListServer(ListServerGetOrFreeEventArgs args);
        [IteratorStateMachine(typeof(CommandQueue.<GetAllCommands>d__27))]
        private IEnumerable<Command> GetAllCommands();
        public static DictionaryEntry GetLowPriorityTag();
        private object GetRowInfoFromRow(object row);
        private void InputDequeue();
        public void InputEnqueue(Command command);
        private static bool IsLowPriority(Command command);
        private void ListServer_ExceptionThrown(object sender, ServerModeExceptionThrownEventArgs e);
        private void ListServer_InconsistencyDetected(object sender, ServerModeInconsistencyDetectedEventArgs e);
        public Command OutputDequeue();
        private void OutputEnqueue();
        private void OutputEnqueueNotification(Command notification);
        public Command PeekOutput();
        private void Run();
        private void ThrottleInconsistency();
        protected virtual void Visit(CommandRefresh result);
        public void WeakCancel<T>() where T: Command;

        public object SyncRoot { get; }

        private int CountInputCommand { get; }

        public int CountOutputCommand { get; }

        public bool IsSomethingInQueue { get; }

        private CommandQueue.BagOfTricks Tricks { get; }

        private bool HasTricks { get; }


        private class BagOfTricks
        {
            public List<ListSourceGroupInfo> LastExpandedGroups;
            public int? LastIndex;
        }
    }
}

