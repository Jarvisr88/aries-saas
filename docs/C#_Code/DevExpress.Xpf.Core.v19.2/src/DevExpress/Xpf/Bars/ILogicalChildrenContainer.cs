namespace DevExpress.Xpf.Bars
{
    using System;

    public interface ILogicalChildrenContainer
    {
        void AddLogicalChild(object child);
        void RemoveLogicalChild(object child);
    }
}

