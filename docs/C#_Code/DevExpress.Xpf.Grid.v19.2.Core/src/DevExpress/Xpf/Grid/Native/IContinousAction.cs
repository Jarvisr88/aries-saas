namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Editors;
    using System;

    public interface IContinousAction : IAction
    {
        void ForceComplete();
        void Prepare();

        bool IsDone { get; }
    }
}

