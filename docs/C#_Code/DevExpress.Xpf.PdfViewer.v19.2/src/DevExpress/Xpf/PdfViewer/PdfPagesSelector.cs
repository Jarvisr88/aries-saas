namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.PdfViewer.Internal;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class PdfPagesSelector : DocumentViewerItemsControl
    {
        public static readonly DependencyProperty IsSearchControlVisibleProperty;
        public static readonly DependencyProperty SearchParameterProperty;

        static PdfPagesSelector()
        {
            Type ownerType = typeof(PdfPagesSelector);
            IsSearchControlVisibleProperty = DependencyPropertyManager.Register("IsSearchControlVisible", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            SearchParameterProperty = DependencyPropertyManager.Register("SearchParameter", typeof(TextSearchParameter), ownerType, new FrameworkPropertyMetadata(null));
        }

        public PdfPagesSelector()
        {
            this.InitScrolling();
        }

        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            PdfPageControl d = (PdfPageControl) element;
            if (d.HasEditor())
            {
                Func<PdfViewerControl, InteractionProvider> evaluator = <>c.<>9__14_0;
                if (<>c.<>9__14_0 == null)
                {
                    Func<PdfViewerControl, InteractionProvider> local1 = <>c.<>9__14_0;
                    evaluator = <>c.<>9__14_0 = x => x.InteractionProvider;
                }
                Action<InteractionProvider> action = <>c.<>9__14_1;
                if (<>c.<>9__14_1 == null)
                {
                    Action<InteractionProvider> local2 = <>c.<>9__14_1;
                    action = <>c.<>9__14_1 = x => x.CommitEditor();
                }
                (DocumentViewerControl.GetActualViewer(d) as PdfViewerControl).With<PdfViewerControl, InteractionProvider>(evaluator).Do<InteractionProvider>(action);
                d.RemoveEditor();
            }
            d.Pages = null;
            d.DataContext = null;
        }

        protected override DependencyObject GetContainerForItemOverride() => 
            new PdfPageControl();

        private void InitScrolling()
        {
            FieldInfo field = typeof(VirtualizingPanel).GetField("ScrollUnitProperty");
            if (field != null)
            {
                DependencyProperty dp = (DependencyProperty) field.GetValue(this);
                base.SetValue(dp, Enum.GetValues(dp.DefaultMetadata.DefaultValue.GetType()).Cast<object>().First<object>());
            }
        }

        protected override bool IsItemItsOwnContainerOverride(object item) => 
            item is PdfPageControl;

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            PdfPageControl control = (PdfPageControl) element;
            PdfPageWrapper wrapper = item as PdfPageWrapper;
            control.Pages = wrapper.Pages.Cast<PdfPageViewModel>();
            control.DataContext = wrapper;
        }

        public TextSearchParameter SearchParameter
        {
            get => 
                (TextSearchParameter) base.GetValue(SearchParameterProperty);
            set => 
                base.SetValue(SearchParameterProperty, value);
        }

        public bool IsSearchControlVisible
        {
            get => 
                (bool) base.GetValue(IsSearchControlVisibleProperty);
            set => 
                base.SetValue(IsSearchControlVisibleProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfPagesSelector.<>c <>9 = new PdfPagesSelector.<>c();
            public static Func<PdfViewerControl, InteractionProvider> <>9__14_0;
            public static Action<InteractionProvider> <>9__14_1;

            internal InteractionProvider <ClearContainerForItemOverride>b__14_0(PdfViewerControl x) => 
                x.InteractionProvider;

            internal void <ClearContainerForItemOverride>b__14_1(InteractionProvider x)
            {
                x.CommitEditor();
            }
        }
    }
}

