namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Shell;

    public class ApplicationJumpPathWrap : JumpPath
    {
        public ApplicationJumpPathWrap(ApplicationJumpPathInfo applicationJumpPath)
        {
            GuardHelper.ArgumentNotNull(applicationJumpPath, "applicationJumpPath");
            this.ApplicationJumpPath = applicationJumpPath;
        }

        public override bool Equals(object obj) => 
            this == (obj as ApplicationJumpPathWrap);

        public override int GetHashCode() => 
            this.ApplicationJumpPath.GetHashCode();

        public static bool operator ==(ApplicationJumpPathWrap a, ApplicationJumpPathWrap b)
        {
            bool flag = ReferenceEquals(a, null);
            bool flag2 = ReferenceEquals(b, null);
            return (!(flag & flag2) ? (!(flag | flag2) ? ReferenceEquals(a.ApplicationJumpPath, b.ApplicationJumpPath) : false) : true);
        }

        public static bool operator !=(ApplicationJumpPathWrap a, ApplicationJumpPathWrap b) => 
            !(a == b);

        public ApplicationJumpPathInfo ApplicationJumpPath { get; private set; }
    }
}

