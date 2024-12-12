namespace DevExpress.XtraReports.Parameters
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class ParameterInfo
    {
        private readonly DevExpress.XtraReports.Parameters.Parameter parameter;
        private readonly Function<Control, DevExpress.XtraReports.Parameters.Parameter> createEditor;
        private Control editor;

        public ParameterInfo(DevExpress.XtraReports.Parameters.Parameter parameter, Function<Control, DevExpress.XtraReports.Parameters.Parameter> createEditor)
        {
            this.parameter = parameter;
            this.createEditor = createEditor;
        }

        public ParameterInfo(DevExpress.XtraReports.Parameters.Parameter parameter, Control editor) : this(parameter, function1)
        {
            Function<Control, DevExpress.XtraReports.Parameters.Parameter> function1 = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Function<Control, DevExpress.XtraReports.Parameters.Parameter> local1 = <>c.<>9__4_0;
                function1 = <>c.<>9__4_0 = (Function<Control, DevExpress.XtraReports.Parameters.Parameter>) (_ => null);
            }
            this.editor = editor;
        }

        public Control GetEditor(bool forceCreate)
        {
            if (forceCreate && ((this.editor == null) && (this.createEditor != null)))
            {
                this.editor = this.createEditor(this.parameter);
            }
            return this.editor;
        }

        public DevExpress.XtraReports.Parameters.Parameter Parameter =>
            this.parameter;

        public Control Editor
        {
            get => 
                this.GetEditor(true);
            set
            {
                if (!ReferenceEquals(this.editor, value))
                {
                    using (this.editor)
                    {
                    }
                    this.editor = value;
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ParameterInfo.<>c <>9 = new ParameterInfo.<>c();
            public static Function<Control, Parameter> <>9__4_0;

            internal Control <.ctor>b__4_0(Parameter _) => 
                null;
        }
    }
}

