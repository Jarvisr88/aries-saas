namespace DevExpress.Xpo.Logger
{
    using DevExpress.Xpo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public class LogManager
    {
        public const string LogCategorySQL = "SQL";
        public const string LogParam_StackTrace = "StackTrace";
        private const string STR_Dummy = "Dummy";
        private static ILogger logServer = new DummyServer();
        private static string serverType = "Dummy";
        private static volatile bool hasTransport;
        private static volatile bool hasCategoryList;
        private static readonly Dictionary<string, bool> categoryList = new Dictionary<string, bool>();
        private static bool includeStackTrace;
        public static readonly int LogSessionId = Guid.NewGuid().GetHashCode();
        private static volatile bool initialized = false;
        private static readonly object initializeSyncRoot = new object();

        public static bool HasCategory(string category) => 
            categoryList.ContainsKey(category);

        private static void Init()
        {
            ProfilerConfigSection section = (ProfilerConfigSection) ConfigurationManager.GetSection("DevExpressXpoProfiler");
            if ((section == null) || (string.IsNullOrEmpty(section.ServerType) || string.IsNullOrEmpty(section.ServerAssembly)))
            {
                hasTransport = false;
            }
            else
            {
                object[] args = new object[] { section.Port };
                SetTransport((ILogger) Activator.CreateInstance(XPTypeActivator.GetType(section.ServerAssembly, section.ServerType), args), section.ServerType, section.Categories);
            }
        }

        public static bool IsLogActive(string category)
        {
            if (!initialized)
            {
                object initializeSyncRoot = LogManager.initializeSyncRoot;
                lock (initializeSyncRoot)
                {
                    if (!initialized)
                    {
                        Init();
                        initialized = true;
                    }
                }
            }
            ILogger logServer = LogManager.logServer;
            return (hasTransport && ((!hasCategoryList || HasCategory(category)) && logServer.IsServerActive));
        }

        public static T Log<T>(string category, LogHandler<T> handler, MessageHandler<LogMessage> createMessageHandler)
        {
            LogMessageTimer timer = null;
            string str = null;
            T local;
            if (IsLogActive(category))
            {
                timer = new LogMessageTimer();
            }
            try
            {
                if (handler != null)
                {
                    local = handler();
                }
                else
                {
                    local = default(T);
                    local = local;
                }
            }
            catch (Exception exception1)
            {
                str = exception1.ToString();
                throw;
            }
            finally
            {
                if ((timer != null) && (createMessageHandler != null))
                {
                    LogMessage message = createMessageHandler(timer.Stop());
                    if (IncludeStackTrace)
                    {
                        message.ParameterList.Add(new LogMessageParameter("StackTrace", new StackTrace(1).ToString()));
                    }
                    if (str != null)
                    {
                        message.Error = str;
                    }
                    LogServer.Log(message);
                }
            }
            return local;
        }

        public static void Log(string category, LogHandlerVoid handler, MessageHandler<LogMessage> createMessageHandler, ExceptionHandler exceptionHandler)
        {
            LogMessageTimer timer = null;
            string str = null;
            if (IsLogActive(category))
            {
                timer = new LogMessageTimer();
            }
            try
            {
                if (handler != null)
                {
                    handler();
                }
            }
            catch (Exception exception)
            {
                str = exception.ToString();
                if ((exceptionHandler == null) || exceptionHandler(exception))
                {
                    throw;
                }
            }
            finally
            {
                if ((timer != null) && (createMessageHandler != null))
                {
                    LogMessage message = createMessageHandler(timer.Stop());
                    if (IncludeStackTrace)
                    {
                        message.ParameterList.Add(new LogMessageParameter("StackTrace", new StackTrace(1).ToString()));
                    }
                    if (str != null)
                    {
                        message.Error = str;
                    }
                    LogServer.Log(message);
                }
            }
        }

        [AsyncStateMachine(typeof(<LogAsync>d__37))]
        public static Task<T> LogAsync<T>(string category, LogHandler<Task<T>> handler, MessageHandler<LogMessage> createMessageHandler)
        {
            <LogAsync>d__37<T> d__;
            d__.category = category;
            d__.handler = handler;
            d__.createMessageHandler = createMessageHandler;
            d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<LogAsync>d__37<T>>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<LogAsync>d__39))]
        public static Task LogAsync(string category, LogHandler<Task> handler, MessageHandler<LogMessage> createMessageHandler, ExceptionHandler exceptionHandler)
        {
            <LogAsync>d__39 d__;
            d__.category = category;
            d__.handler = handler;
            d__.createMessageHandler = createMessageHandler;
            d__.exceptionHandler = exceptionHandler;
            d__.<>t__builder = AsyncTaskMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<LogAsync>d__39>(ref d__);
            return d__.<>t__builder.Task;
        }

        public static T LogMany<T>(string category, LogHandler<T> handler, MessageHandler<LogMessage[]> createMessageHandler)
        {
            LogMessageTimer timer = null;
            string str = null;
            T local;
            if (IsLogActive(category))
            {
                timer = new LogMessageTimer();
            }
            try
            {
                if (handler != null)
                {
                    local = handler();
                }
                else
                {
                    local = default(T);
                    local = local;
                }
            }
            catch (Exception exception1)
            {
                str = exception1.ToString();
                throw;
            }
            finally
            {
                if ((timer != null) && (createMessageHandler != null))
                {
                    LogMessage[] messages = createMessageHandler(timer.Stop());
                    LogMessageParameter item = null;
                    if (includeStackTrace)
                    {
                        item = new LogMessageParameter("StackTrace", new StackTrace(1).ToString());
                    }
                    if ((item != null) || (str != null))
                    {
                        foreach (LogMessage message in messages)
                        {
                            if (item != null)
                            {
                                message.ParameterList.Add(item);
                            }
                            if (str != null)
                            {
                                message.Error = str;
                            }
                        }
                    }
                    LogServer.Log(messages);
                }
            }
            return local;
        }

        public static void ResetTransport()
        {
            object initializeSyncRoot = LogManager.initializeSyncRoot;
            lock (initializeSyncRoot)
            {
                IDisposable logServer = LogManager.logServer as IDisposable;
                hasTransport = false;
                LogManager.logServer = new DummyServer();
                if (logServer != null)
                {
                    logServer.Dispose();
                }
                serverType = "Dummy";
            }
        }

        public static void SetCategories(string categories)
        {
            if (string.IsNullOrEmpty(categories))
            {
                hasCategoryList = false;
            }
            else
            {
                hasCategoryList = true;
                categoryList.Clear();
                char[] separator = new char[] { ';' };
                foreach (string str in categories.Split(separator))
                {
                    categoryList[str] = true;
                }
            }
        }

        public static void SetTransport(ILogger logger)
        {
            SetTransport(logger, null);
        }

        public static void SetTransport(ILogger logger, string categories)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            SetTransport(logger, logger.GetType().FullName, categories);
        }

        private static void SetTransport(ILogger logger, string serverType, string categories)
        {
            object initializeSyncRoot = LogManager.initializeSyncRoot;
            lock (initializeSyncRoot)
            {
                if (!(logServer is DummyServer))
                {
                    ResetTransport();
                }
                logServer = logger;
                LogManager.serverType = serverType;
                SetCategories(categories);
                hasTransport = true;
                initialized = true;
            }
        }

        public static ILogger LogServer =>
            logServer;

        public static string ServerType =>
            serverType;

        public static bool HasTransport =>
            hasTransport;

        public static bool HasCategoryList =>
            hasCategoryList;

        public static bool IncludeStackTrace
        {
            get => 
                includeStackTrace;
            set => 
                includeStackTrace = value;
        }

        [CompilerGenerated]
        private struct <LogAsync>d__37<T> : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<T> <>t__builder;
            public string category;
            public LogManager.LogHandler<Task<T>> handler;
            private LogMessageTimer <sw>5__1;
            public LogManager.MessageHandler<LogMessage> createMessageHandler;
            private int <callerThreadId>5__2;
            private string <callerThreadName>5__3;
            private string <exceptionText>5__4;
            private ConfiguredTaskAwaitable<T>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                T local;
                int num = this.<>1__state;
                try
                {
                    if (num != 0)
                    {
                        this.<callerThreadId>5__2 = Thread.CurrentThread.ManagedThreadId;
                        this.<callerThreadName>5__3 = Thread.CurrentThread.Name;
                        this.<sw>5__1 = null;
                        this.<exceptionText>5__4 = null;
                        if (LogManager.IsLogActive(this.category))
                        {
                            this.<sw>5__1 = new LogMessageTimer();
                        }
                    }
                    try
                    {
                        ConfiguredTaskAwaitable<T>.ConfiguredTaskAwaiter awaiter;
                        T local3;
                        if (num == 0)
                        {
                            awaiter = this.<>u__1;
                            this.<>u__1 = new ConfiguredTaskAwaitable<T>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_000E;
                        }
                        else if (this.handler != null)
                        {
                            awaiter = this.handler().ConfigureAwait(false).GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_000E;
                            }
                            else
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<T>.ConfiguredTaskAwaiter, LogManager.<LogAsync>d__37<T>>(ref awaiter, ref (LogManager.<LogAsync>d__37<T>) ref this);
                            }
                            return;
                        }
                        else
                        {
                            local = default(T);
                        }
                        goto TR_000B;
                    TR_000E:
                        local3 = awaiter.GetResult();
                        awaiter = new ConfiguredTaskAwaitable<T>.ConfiguredTaskAwaiter();
                        local = local3;
                    }
                    catch (Exception exception)
                    {
                        this.<exceptionText>5__4 = exception.ToString();
                        throw;
                    }
                    finally
                    {
                        if ((num < 0) && ((this.<sw>5__1 != null) && (this.createMessageHandler != null)))
                        {
                            LogMessage message = this.createMessageHandler(this.<sw>5__1.Stop());
                            message.ThreadId = this.<callerThreadId>5__2;
                            message.ThreadName = this.<callerThreadName>5__3;
                            if (LogManager.IncludeStackTrace)
                            {
                                message.ParameterList.Add(new LogMessageParameter("StackTrace", new StackTrace(1).ToString()));
                            }
                            if (this.<exceptionText>5__4 != null)
                            {
                                message.Error = this.<exceptionText>5__4;
                            }
                            LogManager.LogServer.Log(message);
                        }
                    }
                }
                catch (Exception exception2)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception2);
                    return;
                }
            TR_000B:
                this.<>1__state = -2;
                this.<>t__builder.SetResult(local);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <LogAsync>d__39 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public string category;
            public LogManager.LogHandler<Task> handler;
            public LogManager.ExceptionHandler exceptionHandler;
            private LogMessageTimer <sw>5__1;
            public LogManager.MessageHandler<LogMessage> createMessageHandler;
            private int <callerThreadId>5__2;
            private string <callerThreadName>5__3;
            private string <exceptionText>5__4;
            private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    if (num != 0)
                    {
                        this.<callerThreadId>5__2 = Thread.CurrentThread.ManagedThreadId;
                        this.<callerThreadName>5__3 = Thread.CurrentThread.Name;
                        this.<sw>5__1 = null;
                        this.<exceptionText>5__4 = null;
                        if (LogManager.IsLogActive(this.category))
                        {
                            this.<sw>5__1 = new LogMessageTimer();
                        }
                    }
                    try
                    {
                        ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
                        if (num == 0)
                        {
                            awaiter = this.<>u__1;
                            this.<>u__1 = new ConfiguredTaskAwaitable.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_000F;
                        }
                        else if (this.handler != null)
                        {
                            awaiter = this.handler().ConfigureAwait(false).GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_000F;
                            }
                            else
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, LogManager.<LogAsync>d__39>(ref awaiter, ref this);
                            }
                            return;
                        }
                        goto TR_000A;
                    TR_000F:
                        awaiter.GetResult();
                        awaiter = new ConfiguredTaskAwaitable.ConfiguredTaskAwaiter();
                    }
                    catch (Exception exception)
                    {
                        this.<exceptionText>5__4 = exception.ToString();
                        if ((this.exceptionHandler == null) || this.exceptionHandler(exception))
                        {
                            throw;
                        }
                    }
                    finally
                    {
                        if ((num < 0) && ((this.<sw>5__1 != null) && (this.createMessageHandler != null)))
                        {
                            LogMessage message = this.createMessageHandler(this.<sw>5__1.Stop());
                            message.ThreadId = this.<callerThreadId>5__2;
                            message.ThreadName = this.<callerThreadName>5__3;
                            if (LogManager.IncludeStackTrace)
                            {
                                message.ParameterList.Add(new LogMessageParameter("StackTrace", new StackTrace(1).ToString()));
                            }
                            if (this.<exceptionText>5__4 != null)
                            {
                                message.Error = this.<exceptionText>5__4;
                            }
                            LogManager.LogServer.Log(message);
                        }
                    }
                }
                catch (Exception exception2)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception2);
                    return;
                }
            TR_000A:
                this.<>1__state = -2;
                this.<>t__builder.SetResult();
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        public delegate bool ExceptionHandler(Exception ex);

        public delegate T LogHandler<T>();

        public delegate void LogHandlerVoid();

        public delegate T MessageHandler<T>(TimeSpan duration);
    }
}

