namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public interface IExpandableChild
    {
        event ValueChangedEventHandler<bool> IsExpandedChanged;

        bool IsExpanded { get; set; }

        double CollapseWidth { get; }

        double ExpandedWidth { get; set; }
    }
}

