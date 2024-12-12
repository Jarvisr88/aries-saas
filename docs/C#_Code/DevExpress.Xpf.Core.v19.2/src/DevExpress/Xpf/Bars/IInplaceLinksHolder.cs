namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public interface IInplaceLinksHolder : ILinksHolder, IMultipleElementRegistratorSupport, IBarNameScopeSupport, IInputElement, ILogicalChildrenContainer
    {
        event ValueChangedEventHandler<bool?> IsExpandedChanged;

        void Update();

        bool? IsExpanded { get; }
    }
}

