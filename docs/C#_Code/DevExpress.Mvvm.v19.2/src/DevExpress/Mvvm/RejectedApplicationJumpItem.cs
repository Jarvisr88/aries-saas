namespace DevExpress.Mvvm
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Shell;

    public sealed class RejectedApplicationJumpItem
    {
        public RejectedApplicationJumpItem(ApplicationJumpItemInfo jumpItem, JumpItemRejectionReason reason)
        {
            this.JumpItem = jumpItem;
            this.Reason = reason;
        }

        public ApplicationJumpItemInfo JumpItem { get; private set; }

        public JumpItemRejectionReason Reason { get; private set; }
    }
}

