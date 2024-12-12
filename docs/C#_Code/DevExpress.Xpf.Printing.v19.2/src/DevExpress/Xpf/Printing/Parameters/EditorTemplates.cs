namespace DevExpress.Xpf.Printing.Parameters
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Markup;

    public class EditorTemplates : ResourceDictionary, IComponentConnector
    {
        private static readonly EditorTemplates instance = new EditorTemplates();
        private bool _contentLoaded;

        public EditorTemplates()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Printing.v19.2;component/themes/generic/editortemplates.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            this._contentLoaded = true;
        }

        public static Style DescriptionDefaultStyle =>
            (Style) instance["descriptionLabelStyle"];

        public static DataTemplate StringTemplate =>
            (DataTemplate) instance["stringEditorTemplate"];

        public static DataTemplate NumericTemplate =>
            (DataTemplate) instance["numericEditorTemplate"];

        public static DataTemplate NumericFloatTemplate =>
            (DataTemplate) instance["numericFloatEditorTemplate"];

        public static DataTemplate BooleanTemplate =>
            (DataTemplate) instance["booleanEditorTemplate"];

        public static DataTemplate DateTimeTemplate =>
            (DataTemplate) instance["dateTimeEditorTemplate"];

        public static DataTemplate LookUpEditTemplate =>
            (DataTemplate) instance["lookUpEditTemplate"];

        public static DataTemplate GuidTemplate =>
            (DataTemplate) instance["guidTemplate"];

        public static DataTemplate MultiValueTemplate =>
            (DataTemplate) instance["multiValueTemplate"];

        public static DataTemplate MultiValueLookUpTemplate =>
            (DataTemplate) instance["multiValueLookUpTemplate"];

        public static DataTemplate DateRangeTemplate =>
            (DataTemplate) instance["dateRangeTemplate"];
    }
}

