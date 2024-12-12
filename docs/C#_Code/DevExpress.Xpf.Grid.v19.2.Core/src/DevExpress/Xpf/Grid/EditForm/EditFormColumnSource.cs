namespace DevExpress.Xpf.Grid.EditForm
{
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class EditFormColumnSource
    {
        private bool visibleCore = true;

        public object Caption { get; set; }

        public int? ColumnSpan { get; set; }

        public int? RowSpan { get; set; }

        public BaseEditSettings EditSettings { get; set; }

        public DataTemplate EditorTemplate { get; set; }

        public string FieldName { get; set; }

        public bool StartNewRow { get; set; }

        public int VisibleIndex { get; set; }

        public bool ReadOnly { get; set; }

        public bool Visible
        {
            get => 
                this.visibleCore;
            set
            {
                if (this.visibleCore != value)
                {
                    this.visibleCore = value;
                }
            }
        }

        public IDefaultEditorViewInfo EditorViewInfo { get; set; }
    }
}

