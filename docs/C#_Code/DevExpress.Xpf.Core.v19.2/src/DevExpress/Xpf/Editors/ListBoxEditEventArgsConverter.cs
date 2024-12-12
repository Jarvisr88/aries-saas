namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Popups;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class ListBoxEditEventArgsConverter : IDataRowEventArgsConverter
    {
        public ListBoxEditEventArgsConverter(ListBoxEdit dataControl)
        {
            this.OwnerEdit = dataControl;
        }

        object IDataRowEventArgsConverter.GetDataRow(RoutedEventArgs e)
        {
            Func<EditorListBox, object> evaluator = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<EditorListBox, object> local1 = <>c.<>9__5_0;
                evaluator = <>c.<>9__5_0 = x => x.SelectedItem;
            }
            return this.OwnerEdit.ListBoxCore.With<EditorListBox, object>(evaluator);
        }

        private ListBoxEdit OwnerEdit { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ListBoxEditEventArgsConverter.<>c <>9 = new ListBoxEditEventArgsConverter.<>c();
            public static Func<EditorListBox, object> <>9__5_0;

            internal object <DevExpress.Xpf.Core.Native.IDataRowEventArgsConverter.GetDataRow>b__5_0(EditorListBox x) => 
                x.SelectedItem;
        }
    }
}

