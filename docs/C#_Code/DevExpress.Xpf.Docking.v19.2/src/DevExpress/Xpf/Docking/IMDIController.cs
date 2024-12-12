namespace DevExpress.Xpf.Docking
{
    using System;

    public interface IMDIController : IActiveItemOwner, IDisposable
    {
        bool ArrangeIcons(BaseLayoutItem item);
        bool Cascade(BaseLayoutItem item);
        bool ChangeMDIStyle(BaseLayoutItem item);
        T CreateCommand<T>(BaseLayoutItem[] items) where T: MDIControllerCommand, new();
        bool Maximize(BaseLayoutItem document);
        bool Maximize(DocumentPanel document);
        bool Minimize(BaseLayoutItem document);
        bool Minimize(DocumentPanel document);
        bool Restore(BaseLayoutItem document);
        bool Restore(DocumentPanel document);
        bool TileHorizontal(BaseLayoutItem item);
        bool TileVertical(BaseLayoutItem item);

        DevExpress.Xpf.Docking.MDIMenuBar MDIMenuBar { get; }
    }
}

