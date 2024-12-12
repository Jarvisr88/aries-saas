namespace DevExpress.Utils.CommonDialogs.Internal
{
    using System;

    internal interface ISlot<out TDialogType> where TDialogType: ICommonDialog
    {
        Func<TDialogType> Creator { get; }
    }
}

