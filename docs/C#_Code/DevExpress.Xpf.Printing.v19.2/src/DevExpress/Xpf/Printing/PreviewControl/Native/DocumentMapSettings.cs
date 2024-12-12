namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Printing;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class DocumentMapSettings : DevExpress.Xpf.DocumentViewer.DocumentMapSettings
    {
        public static readonly DependencyProperty SelectedNodeProperty;

        static DocumentMapSettings()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings>.New().Register<object>(System.Linq.Expressions.Expression.Lambda<Func<DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings.get_SelectedNode)), parameters), out SelectedNodeProperty, null, frameworkOptions);
        }

        public DocumentMapSettings()
        {
            this.Source = new ObservableRangeCollection<object>();
        }

        protected override void UpdatePropertiesInternal()
        {
            base.UpdatePropertiesInternal();
            this.Source.Clear();
            if ((this.Owner == null) || ((this.Owner.Document == null) || !this.Owner.Document.IsCreated))
            {
                base.GoToCommand = null;
            }
            else
            {
                if (this.Owner.Document.HasBookmarks)
                {
                    this.Source.AddRange(this.Owner.Document.GetBookmarks());
                }
                Func<CommandProvider, ICommand> evaluator = <>c.<>9__11_0;
                if (<>c.<>9__11_0 == null)
                {
                    Func<CommandProvider, ICommand> local1 = <>c.<>9__11_0;
                    evaluator = <>c.<>9__11_0 = x => x.NavigateCommand;
                }
                this.GoToCommand = this.Owner.ActualCommandProvider.Return<CommandProvider, ICommand>(evaluator, <>c.<>9__11_1 ??= ((Func<ICommand>) (() => null)));
            }
        }

        public object SelectedNode
        {
            get => 
                base.GetValue(SelectedNodeProperty);
            set => 
                base.SetValue(SelectedNodeProperty, value);
        }

        private ObservableRangeCollection<object> Source
        {
            get => 
                (ObservableRangeCollection<object>) base.Source;
            set => 
                base.Source = value;
        }

        private DocumentPreviewControl Owner =>
            base.Owner as DocumentPreviewControl;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings.<>c <>9 = new DevExpress.Xpf.Printing.PreviewControl.Native.DocumentMapSettings.<>c();
            public static Func<CommandProvider, ICommand> <>9__11_0;
            public static Func<ICommand> <>9__11_1;

            internal ICommand <UpdatePropertiesInternal>b__11_0(CommandProvider x) => 
                x.NavigateCommand;

            internal ICommand <UpdatePropertiesInternal>b__11_1() => 
                null;
        }
    }
}

