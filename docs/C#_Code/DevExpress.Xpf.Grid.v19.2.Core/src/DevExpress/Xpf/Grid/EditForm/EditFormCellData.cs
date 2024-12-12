namespace DevExpress.Xpf.Grid.EditForm
{
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;

    public class EditFormCellData : EditFormCellDataBase, INotifyPropertyChanged
    {
        internal const string ValidationErrorPropertyName = "ValidationError";
        private IDefaultEditorViewInfo editorViewInfoCore;
        private BaseValidationError validationErrorCore;
        private object valueCore;
        private object initialValue;
        private bool isValueInited;

        public event PropertyChangedEventHandler PropertyChanged;

        public event ValueChangedEventHandler ValueChangedEvent;

        protected internal override void Assign(EditFormColumnSource source)
        {
            base.Assign(source);
            this.FieldName = source.FieldName;
            this.EditSettings = source.EditSettings;
            this.EditorTemplate = source.EditorTemplate;
            this.ReadOnly = source.ReadOnly;
            this.EditorViewInfo = source.EditorViewInfo;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void RaiseValueChangedEvent()
        {
            if (this.ValueChangedEvent != null)
            {
                this.ValueChangedEvent(this, new EventArgs());
            }
        }

        internal void ResetValue()
        {
            if (this.isValueInited)
            {
                this.Value = this.initialValue;
            }
        }

        public BaseEditSettings EditSettings { get; internal set; }

        public string FieldName { get; internal set; }

        public DataTemplate EditorTemplate { get; internal set; }

        public int VisibleIndex { get; internal set; }

        public EditFormRowData RowData { get; set; }

        public bool ReadOnly { get; internal set; }

        public IDefaultEditorViewInfo EditorViewInfo
        {
            get => 
                this.editorViewInfoCore ?? EmptyDefaultEditorViewInfo.Instance;
            set => 
                this.editorViewInfoCore = value;
        }

        protected override EditFormLayoutItemType ItemTypeCore =>
            EditFormLayoutItemType.Editor;

        public BaseValidationError ValidationError
        {
            get => 
                this.validationErrorCore;
            internal set
            {
                if (!ReferenceEquals(this.validationErrorCore, value))
                {
                    this.validationErrorCore = value;
                    this.RaisePropertyChanged("ValidationError");
                }
            }
        }

        internal bool HasInnerError { get; set; }

        public object Value
        {
            get => 
                this.valueCore;
            internal set
            {
                if (this.valueCore != value)
                {
                    this.valueCore = value;
                    if (!this.isValueInited)
                    {
                        this.initialValue = value;
                        this.isValueInited = true;
                    }
                    this.RaiseValueChangedEvent();
                }
            }
        }

        public delegate void ValueChangedEventHandler(object sender, EventArgs e);
    }
}

