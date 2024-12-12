namespace DevExpress.Utils.CommonDialogs.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class Slot<TDialogType> : ISlot<TDialogType> where TDialogType: ICommonDialog
    {
        public Slot(Func<TDialogType> creator)
        {
            this.Creator = creator;
        }

        public Func<TDialogType> Creator { get; private set; }
    }
}

