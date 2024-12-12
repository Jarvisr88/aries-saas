namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PaginationEditValueChangedBehavior : Behavior<SpinEdit>
    {
        private void EditValueChanging(object sender, EditValueChangingEventArgs e)
        {
            int result = 0;
            DocumentViewerControl actualViewer = DocumentViewerControl.GetActualViewer(sender as FrameworkElement) as DocumentViewerControl;
            Func<object, string> evaluator = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<object, string> local1 = <>c.<>9__1_0;
                evaluator = <>c.<>9__1_0 = x => x.ToString();
            }
            if (int.TryParse(e.NewValue.With<object, string>(evaluator), out result))
            {
                Func<DocumentViewerControl, bool> func2 = <>c.<>9__1_1;
                if (<>c.<>9__1_1 == null)
                {
                    Func<DocumentViewerControl, bool> local2 = <>c.<>9__1_1;
                    func2 = <>c.<>9__1_1 = x => x.IsDocumentContainPages();
                }
                if (!actualViewer.Return<DocumentViewerControl, bool>(func2, (<>c.<>9__1_2 ??= () => false)) || ((result > 0) && (result <= actualViewer.PageCount)))
                {
                    return;
                }
            }
            e.Handled = true;
            e.IsCancel = true;
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            base.AssociatedObject.EditValueChanging += new EditValueChangingEventHandler(this.EditValueChanging);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            base.AssociatedObject.EditValueChanging -= new EditValueChangingEventHandler(this.EditValueChanging);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PaginationEditValueChangedBehavior.<>c <>9 = new PaginationEditValueChangedBehavior.<>c();
            public static Func<object, string> <>9__1_0;
            public static Func<DocumentViewerControl, bool> <>9__1_1;
            public static Func<bool> <>9__1_2;

            internal string <EditValueChanging>b__1_0(object x) => 
                x.ToString();

            internal bool <EditValueChanging>b__1_1(DocumentViewerControl x) => 
                x.IsDocumentContainPages();

            internal bool <EditValueChanging>b__1_2() => 
                false;
        }
    }
}

