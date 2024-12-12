namespace DevExpress.Xpf.Editors.Settings
{
    using System;

    public interface IHyperlinkPropertyOwner
    {
        string DisplayMember { get; }

        string NavigationUrlMember { get; }
    }
}

