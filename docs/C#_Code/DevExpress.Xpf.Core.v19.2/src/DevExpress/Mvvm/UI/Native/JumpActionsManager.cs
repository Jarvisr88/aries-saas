namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.ServiceModel;
    using System.Threading;
    using System.Windows.Threading;

    public class JumpActionsManager : JumpActionsManagerBase, IJumpActionsManager
    {
        private static object factoryLock = new object();
        private static Func<JumpActionsManager> factory = () => new JumpActionsManager(null, 500);
        private static JumpActionsManager current;
        private readonly Dictionary<string, RegisteredJumpAction> jumpActions;
        private JumpActionsManagerBase.GuidData applicationInstanceId;
        private ServiceHost applicationInstanceHost;
        private Tuple<IntPtr, IntPtr> isAliveFlagFile;
        private volatile bool registered;
        private ICurrentProcess currentProcess;
        private volatile bool updating;

        public JumpActionsManager(ICurrentProcess currentProcess = null, int millisecondsTimeout = 500) : base(millisecondsTimeout)
        {
            this.jumpActions = new Dictionary<string, RegisteredJumpAction>();
            ICurrentProcess process1 = currentProcess;
            if (currentProcess == null)
            {
                ICurrentProcess local1 = currentProcess;
                process1 = new CurrentProcess();
            }
            this.currentProcess = process1;
        }

        private void AddAction(RegisteredJumpAction jumpAction)
        {
            this.jumpActions[jumpAction.Id] = jumpAction;
        }

        public void BeginUpdate()
        {
            if (this.updating)
            {
                throw new InvalidOperationException();
            }
            Monitor.Enter(this.jumpActions);
            try
            {
                Mutex mutex = base.WaitMainMutex(false);
                try
                {
                    this.ClearActions();
                }
                catch
                {
                    mutex.ReleaseMutex();
                    throw;
                }
            }
            catch
            {
                Monitor.Exit(this.jumpActions);
                throw;
            }
            this.updating = true;
        }

        private void ClearActions()
        {
            this.jumpActions.Clear();
        }

        [SecuritySafeCritical]
        private void CreateInstance()
        {
            bool flag;
            this.applicationInstanceId = new JumpActionsManagerBase.GuidData(Guid.NewGuid());
            Uri[] baseAddresses = new Uri[] { new Uri(GetServiceUri(this.applicationInstanceId)) };
            this.applicationInstanceHost = new ServiceHost(new ApplicationInstance(this), baseAddresses);
            this.applicationInstanceHost.AddServiceEndpoint(typeof(JumpActionsManagerBase.IApplicationInstance), new NetNamedPipeBinding(), "Pipe");
            this.applicationInstanceHost.Open(new TimeSpan(0, 0, 0, 0, base.MillisecondsTimeout));
            this.isAliveFlagFile = base.CreateFileMappingAndMapView(1, GetIsAliveFlagFileName(this.applicationInstanceId), out flag);
        }

        [SecuritySafeCritical]
        private void DeleteInstance()
        {
            UnmapViewAndCloseFileMapping(this.isAliveFlagFile);
            this.isAliveFlagFile = null;
            this.applicationInstanceHost.Close(new TimeSpan(0, 0, 0, 0, base.MillisecondsTimeout));
            this.applicationInstanceHost = null;
            this.applicationInstanceId = new JumpActionsManagerBase.GuidData(Guid.Empty);
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    Monitor.Enter(this.jumpActions);
                }
                try
                {
                    Mutex mutex = base.WaitMainMutex(!disposing);
                    try
                    {
                        this.UnregisterInstance(!disposing);
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                }
                finally
                {
                    if (disposing)
                    {
                        Monitor.Exit(this.jumpActions);
                    }
                }
            }
            catch (TimeoutException)
            {
                if (disposing)
                {
                    throw;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        public void EndUpdate()
        {
            if (!this.updating)
            {
                throw new InvalidOperationException();
            }
            try
            {
                base.GetMainMutex(false).ReleaseMutex();
            }
            finally
            {
                this.updating = false;
                Monitor.Exit(this.jumpActions);
            }
        }

        private void ExecuteCore(string command)
        {
            RegisteredJumpAction action;
            if (this.jumpActions.TryGetValue(command, out action))
            {
                IJumpAction jumpAction = action.GetJumpAction();
                if (jumpAction == null)
                {
                    this.jumpActions.Remove(command);
                }
                else
                {
                    action.Dispatcher.BeginInvoke(new Action(jumpAction.Execute), new object[0]);
                }
            }
        }

        protected virtual RegisteredJumpAction PrepareJumpActionToRegister(IJumpAction jumpAction, string commandLineArgumentPrefix, Func<string> launcherPath)
        {
            RegisteredJumpAction action = new RegisteredJumpAction(jumpAction);
            string applicationPath = jumpAction.ApplicationPath;
            string str2 = jumpAction.Arguments ?? string.Empty;
            if (string.IsNullOrEmpty(applicationPath))
            {
                applicationPath = this.currentProcess.ExecutablePath;
            }
            if (string.Equals(applicationPath, this.currentProcess.ExecutablePath, StringComparison.OrdinalIgnoreCase))
            {
                string str4 = commandLineArgumentPrefix + Uri.EscapeDataString(action.Id);
                str2 = string.IsNullOrEmpty(str2) ? str4 : (str2 + str4);
            }
            string[] textArray1 = new string[] { this.currentProcess.ApplicationId, Uri.EscapeDataString(action.Id), Uri.EscapeDataString(applicationPath), $""{Uri.EscapeDataString(str2)}"" };
            string arguments = string.Join(" ", textArray1);
            if (!string.IsNullOrEmpty(jumpAction.WorkingDirectory))
            {
                string[] textArray2 = new string[] { arguments, Uri.EscapeDataString(jumpAction.WorkingDirectory) };
                arguments = string.Join(" ", textArray2);
            }
            jumpAction.SetStartInfo(launcherPath(), arguments);
            return action;
        }

        [SecuritySafeCritical]
        public void RegisterAction(IJumpAction jumpAction, string commandLineArgumentPrefix, Func<string> launcherPath)
        {
            GuardHelper.ArgumentNotNull(jumpAction, "jumpAction");
            if (!this.updating)
            {
                throw new InvalidOperationException();
            }
            this.RegisterInstance();
            RegisteredJumpAction action = this.PrepareJumpActionToRegister(jumpAction, commandLineArgumentPrefix, launcherPath);
            this.AddAction(action);
            if (this.ShouldExecute(jumpAction.CommandId, commandLineArgumentPrefix))
            {
                this.ExecuteCore(jumpAction.CommandId);
            }
        }

        private void RegisterInstance()
        {
            if (!this.registered)
            {
                JumpActionsManagerBase.GuidData[] applicationInstances = base.GetApplicationInstances(true);
                this.CreateInstance();
                JumpActionsManagerBase.GuidData[] destinationArray = new JumpActionsManagerBase.GuidData[] { this.applicationInstanceId };
                Array.Copy(applicationInstances, 0, destinationArray, 1, applicationInstances.Length);
                base.UpdateInstancesFile(destinationArray);
                this.registered = true;
            }
        }

        private bool ShouldExecute(string command, string commandLineArgumentPrefix)
        {
            string arg = commandLineArgumentPrefix + Uri.EscapeDataString(command);
            return (from a in this.currentProcess.CommandLineArgs.Skip<string>(1)
                where string.Equals(a, arg)
                select a).Any<string>();
        }

        private void UnregisterInstance(bool safe)
        {
            if (this.registered)
            {
                JumpActionsManagerBase.GuidData[] applicationInstances = base.GetApplicationInstances(true);
                JumpActionsManagerBase.GuidData[] instances = new JumpActionsManagerBase.GuidData[applicationInstances.Length - 1];
                int index = 0;
                foreach (JumpActionsManagerBase.GuidData data in applicationInstances)
                {
                    if (data.AsGuid != this.applicationInstanceId.AsGuid)
                    {
                        instances[index] = data;
                        index++;
                    }
                }
                base.UpdateInstancesFile(instances);
                this.registered = false;
                if (!safe && (this.applicationInstanceHost != null))
                {
                    this.DeleteInstance();
                }
            }
        }

        public static Func<JumpActionsManager> Factory
        {
            get => 
                factory;
            set
            {
                object factoryLock = JumpActionsManager.factoryLock;
                lock (factoryLock)
                {
                    GuardHelper.ArgumentNotNull(value, "value");
                    factory = value;
                }
            }
        }

        public static JumpActionsManager Current
        {
            get
            {
                object factoryLock = JumpActionsManager.factoryLock;
                lock (factoryLock)
                {
                    if (current == null)
                    {
                        current = Factory();
                        if (current == null)
                        {
                            throw new InvalidOperationException();
                        }
                    }
                    return current;
                }
            }
        }

        protected override string ApplicationId =>
            this.currentProcess.ApplicationId;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly JumpActionsManager.<>c <>9 = new JumpActionsManager.<>c();

            internal JumpActionsManager <.cctor>b__33_0() => 
                new JumpActionsManager(null, 500);
        }

        [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
        private class ApplicationInstance : JumpActionsManagerBase.IApplicationInstance
        {
            private readonly JumpActionsManager manager;

            public ApplicationInstance(JumpActionsManager manager)
            {
                this.manager = manager;
            }

            void JumpActionsManagerBase.IApplicationInstance.Execute(string command)
            {
                this.manager.ExecuteCore(command);
            }
        }

        protected class RegisteredJumpAction
        {
            private WeakReference taskReference;

            public RegisteredJumpAction(IJumpAction jumpAction)
            {
                this.Id = jumpAction.CommandId;
                this.taskReference = new WeakReference(jumpAction);
                this.Dispatcher = System.Windows.Threading.Dispatcher.CurrentDispatcher;
            }

            public IJumpAction GetJumpAction() => 
                (this.taskReference == null) ? null : ((IJumpAction) this.taskReference.Target);

            public string Id { get; private set; }

            public System.Windows.Threading.Dispatcher Dispatcher { get; private set; }
        }
    }
}

