namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Shell;

    public class ApplicationJumpTaskWrap : JumpTask, IJumpAction
    {
        public ApplicationJumpTaskWrap(ApplicationJumpTaskInfo applicationJumpTask)
        {
            GuardHelper.ArgumentNotNull(applicationJumpTask, "applicationJumpTask");
            this.ApplicationJumpTask = applicationJumpTask;
        }

        void IJumpAction.Execute()
        {
            ((IApplicationJumpTaskInfoInternal) this.ApplicationJumpTask).Execute();
        }

        void IJumpAction.SetStartInfo(string applicationPath, string arguments)
        {
            base.ApplicationPath = applicationPath;
            base.Arguments = arguments;
        }

        public override bool Equals(object obj) => 
            this == (obj as ApplicationJumpTaskWrap);

        public override int GetHashCode() => 
            this.ApplicationJumpTask.GetHashCode();

        public static bool operator ==(ApplicationJumpTaskWrap a, ApplicationJumpTaskWrap b)
        {
            bool flag = ReferenceEquals(a, null);
            bool flag2 = ReferenceEquals(b, null);
            return (!(flag & flag2) ? (!(flag | flag2) ? ReferenceEquals(a.ApplicationJumpTask, b.ApplicationJumpTask) : false) : true);
        }

        public static bool operator !=(ApplicationJumpTaskWrap a, ApplicationJumpTaskWrap b) => 
            !(a == b);

        public ApplicationJumpTaskInfo ApplicationJumpTask { get; private set; }

        string IJumpAction.CommandId =>
            this.ApplicationJumpTask.CommandId;

        string IJumpAction.ApplicationPath =>
            this.ApplicationJumpTask.ApplicationPath;

        string IJumpAction.Arguments =>
            this.ApplicationJumpTask.Arguments;

        string IJumpAction.WorkingDirectory =>
            this.ApplicationJumpTask.WorkingDirectory;
    }
}

