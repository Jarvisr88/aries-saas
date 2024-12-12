namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows.Input;

    public class InplaceEditingProvider : IInplaceEditingProvider
    {
        public static readonly InplaceEditingProvider Default = new InplaceEditingProvider();

        public bool HandleScrollNavigation(Key key, ModifierKeys keys) => 
            false;

        public bool HandleTextNavigation(Key key, ModifierKeys keys) => 
            false;
    }
}

