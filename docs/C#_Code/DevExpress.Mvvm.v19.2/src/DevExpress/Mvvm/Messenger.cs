namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Internal;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Threading;

    public class Messenger : IMessenger
    {
        private const DispatcherPriority CleanUpPriority = DispatcherPriority.ApplicationIdle;
        private static readonly object defaultMessengerLock = new object();
        private static IMessenger defaultMessenger;
        private bool isMultiThreadSafe;
        private ActionInvokerCollection actionInvokers;
        private IActionInvokerFactory actionInvokerFactory;
        private bool cleanupScheduled;

        public Messenger() : this(false, ActionReferenceType.WeakReference)
        {
        }

        public Messenger(bool isMultiThreadSafe, ActionReferenceType actionReferenceType = 0) : this(isMultiThreadSafe, CreateActionInvokerFactory(actionReferenceType))
        {
        }

        public Messenger(bool isMultiThreadSafe, IActionInvokerFactory actionInvokerFactory)
        {
            this.actionInvokers = new ActionInvokerCollection();
            this.actionInvokerFactory = actionInvokerFactory;
            this.isMultiThreadSafe = isMultiThreadSafe;
        }

        public void Cleanup()
        {
            try
            {
                if (this.isMultiThreadSafe)
                {
                    Monitor.Enter(this.actionInvokers);
                }
                this.actionInvokers.CleanUp();
            }
            finally
            {
                if (this.isMultiThreadSafe)
                {
                    Monitor.Exit(this.actionInvokers);
                }
            }
            this.cleanupScheduled = false;
        }

        private static IActionInvokerFactory CreateActionInvokerFactory(ActionReferenceType actionReferenceType) => 
            (actionReferenceType == ActionReferenceType.WeakReference) ? ((IActionInvokerFactory) new WeakReferenceActionInvokerFactory()) : ((IActionInvokerFactory) new StrongReferenceActionInvokerFactory());

        public virtual void Register<TMessage>(object recipient, object token, bool receiveInheritedMessages, Action<TMessage> action)
        {
            try
            {
                if (this.isMultiThreadSafe)
                {
                    Monitor.Enter(this.actionInvokers);
                }
                IActionInvoker actionInvoker = this.actionInvokerFactory.CreateActionInvoker<TMessage>(recipient, action);
                this.RegisterCore(token, receiveInheritedMessages, typeof(TMessage), actionInvoker);
            }
            finally
            {
                if (this.isMultiThreadSafe)
                {
                    Monitor.Exit(this.actionInvokers);
                }
            }
            this.RequestCleanup();
        }

        protected void RegisterCore(object token, bool receiveInheritedMessages, Type messageType, IActionInvoker actionInvoker)
        {
            this.actionInvokers.Register(token, receiveInheritedMessages, messageType, actionInvoker);
        }

        public void RequestCleanup()
        {
            if (!this.cleanupScheduled)
            {
                this.cleanupScheduled = true;
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action(this.Cleanup), DispatcherPriority.ApplicationIdle, null);
            }
        }

        public virtual void Send<TMessage>(TMessage message, Type messageTargetType, object token)
        {
            try
            {
                if (this.isMultiThreadSafe)
                {
                    Monitor.Enter(this.actionInvokers);
                }
                this.actionInvokers.Send(message, messageTargetType, token, typeof(TMessage));
            }
            finally
            {
                if (this.isMultiThreadSafe)
                {
                    Monitor.Exit(this.actionInvokers);
                }
            }
            this.RequestCleanup();
        }

        public virtual void Unregister(object recipient)
        {
            this.UnregisterCore(recipient, null, null, null);
        }

        public virtual void Unregister<TMessage>(object recipient, object token, Action<TMessage> action)
        {
            this.UnregisterCore(recipient, token, action, typeof(TMessage));
        }

        protected void UnregisterCore(object recipient, object token, Delegate action, Type messageType)
        {
            try
            {
                if (this.isMultiThreadSafe)
                {
                    Monitor.Enter(this.actionInvokers);
                }
                this.actionInvokers.Unregister(recipient, token, action, messageType);
            }
            finally
            {
                if (this.isMultiThreadSafe)
                {
                    Monitor.Exit(this.actionInvokers);
                }
            }
            this.RequestCleanup();
        }

        public static IMessenger Default
        {
            get
            {
                if (defaultMessenger == null)
                {
                    object defaultMessengerLock = Messenger.defaultMessengerLock;
                    lock (defaultMessengerLock)
                    {
                        defaultMessenger ??= new Messenger();
                    }
                }
                return defaultMessenger;
            }
            set => 
                defaultMessenger = value;
        }

        public class ActionInvokerCollection : FuzzyDictionary<Type, List<Messenger.ActionInvokerTokenPair>>
        {
            public ActionInvokerCollection() : base(new Func<Type, Type, bool>(Messenger.ActionInvokerCollection.TypeInclude))
            {
            }

            public void CleanUp()
            {
                List<FuzzyKeyValuePair<Type, List<Messenger.ActionInvokerTokenPair>>> list = new List<FuzzyKeyValuePair<Type, List<Messenger.ActionInvokerTokenPair>>>();
                foreach (FuzzyKeyValuePair<Type, List<Messenger.ActionInvokerTokenPair>> pair in this)
                {
                    foreach (Messenger.ActionInvokerTokenPair pair2 in new List<Messenger.ActionInvokerTokenPair>(pair.Value))
                    {
                        if (pair2.ActionInvoker.Target == null)
                        {
                            pair.Value.Remove(pair2);
                        }
                    }
                    if (pair.Value.Count == 0)
                    {
                        list.Add(pair);
                    }
                }
                foreach (FuzzyKeyValuePair<Type, List<Messenger.ActionInvokerTokenPair>> pair3 in list)
                {
                    base.Remove(pair3.Key, pair3.UseIncludeCondition);
                }
            }

            public void Register(object token, bool receiveInheritedMessagesToo, Type messageType, IActionInvoker actionInvoker)
            {
                List<Messenger.ActionInvokerTokenPair> list;
                if (!base.TryGetValue(messageType, receiveInheritedMessagesToo, out list))
                {
                    list = new List<Messenger.ActionInvokerTokenPair>();
                    base.Add(messageType, list, receiveInheritedMessagesToo);
                }
                list.Add(new Messenger.ActionInvokerTokenPair(actionInvoker, token));
            }

            public void Send(object message, Type messageTargetType, object token, Type messageType)
            {
                foreach (List<Messenger.ActionInvokerTokenPair> list in base.GetValues(messageType))
                {
                    foreach (Messenger.ActionInvokerTokenPair pair in list.ToArray())
                    {
                        if (Equals(pair.Token, token))
                        {
                            pair.ActionInvoker.ExecuteIfMatched(messageTargetType, message);
                        }
                    }
                }
            }

            private static bool TypeInclude(Type baseType, Type type) => 
                type.IsSubclassOf(baseType) || baseType.IsAssignableFrom(type);

            public void Unregister(object recipient, object token, Delegate action, Type messageType)
            {
                if (recipient != null)
                {
                    foreach (List<Messenger.ActionInvokerTokenPair> list in base.GetValues(messageType))
                    {
                        foreach (Messenger.ActionInvokerTokenPair pair in list)
                        {
                            if ((token == null) || token.Equals(pair.Token))
                            {
                                pair.ActionInvoker.ClearIfMatched(action, recipient);
                            }
                        }
                    }
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ActionInvokerTokenPair
        {
            public readonly IActionInvoker ActionInvoker;
            public readonly object Token;
            public ActionInvokerTokenPair(IActionInvoker actionInvoker, object token)
            {
                this.ActionInvoker = actionInvoker;
                this.Token = token;
            }
        }
    }
}

