namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Bars;
    using System;

    internal interface IMDIMergeStyleListener
    {
        void OnMDIMergeStyleChanged(MDIMergeStyle oldValue, MDIMergeStyle newValue);
    }
}

