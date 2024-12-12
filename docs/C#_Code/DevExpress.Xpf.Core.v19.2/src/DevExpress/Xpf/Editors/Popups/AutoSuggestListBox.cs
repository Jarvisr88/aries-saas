namespace DevExpress.Xpf.Editors.Popups
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class AutoSuggestListBox : ListBox
    {
        static AutoSuggestListBox()
        {
            SelectorWrapper.Register(typeof(AutoSuggestListBox), x => new AutoSuggestListBoxWrapper((AutoSuggestListBox) x));
        }

        protected override DependencyObject GetContainerForItemOverride() => 
            new AutoSuggestListBoxItem();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AutoSuggestListBox.<>c <>9 = new AutoSuggestListBox.<>c();

            internal SelectorWrapper <.cctor>b__0_0(object x) => 
                new AutoSuggestListBoxWrapper((AutoSuggestListBox) x);
        }
    }
}

