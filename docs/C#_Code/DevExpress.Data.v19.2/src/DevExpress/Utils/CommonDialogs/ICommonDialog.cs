namespace DevExpress.Utils.CommonDialogs
{
    using DevExpress.Utils.CommonDialogs.Internal;
    using System;
    using System.Runtime.CompilerServices;

    public interface ICommonDialog : IDisposable
    {
        event EventHandler HelpRequest;

        void Reset();
        DialogResult ShowDialog();
        DialogResult ShowDialog(object owner);
    }
}

