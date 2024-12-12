namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows.Input;

    public interface IInplaceEditingProvider
    {
        bool HandleScrollNavigation(Key key, ModifierKeys keys);
        bool HandleTextNavigation(Key key, ModifierKeys keys);
    }
}

