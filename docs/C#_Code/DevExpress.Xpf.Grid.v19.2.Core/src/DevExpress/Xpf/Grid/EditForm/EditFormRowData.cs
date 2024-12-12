namespace DevExpress.Xpf.Grid.EditForm
{
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public class EditFormRowData
    {
        private bool isModifiedCore;
        private bool canShowUpdateCancelPanelCore = true;
        private Dictionary<string, DevExpress.Xpf.Grid.EditForm.EditFormCellData> editorDataCache = new Dictionary<string, DevExpress.Xpf.Grid.EditForm.EditFormCellData>();

        protected EditFormRowData()
        {
            this.ActualEditFormOwner = EmptyEditFormOwner.Instance;
        }

        public void Cancel()
        {
            IList<DevExpress.Xpf.Grid.EditForm.EditFormCellData> editorData = this.GetEditorData();
            if (editorData != null)
            {
                if (this.EditFormOwner.EditMode == EditFormPostMode.Immediate)
                {
                    foreach (DevExpress.Xpf.Grid.EditForm.EditFormCellData data in editorData)
                    {
                        data.ResetValue();
                    }
                }
                this.ActualEditFormOwner.OnInlineFormClosed(false);
            }
        }

        public void Close()
        {
            this.ActualEditFormOwner.OnInlineFormClosed(true);
        }

        private void CoerceSource(EditFormColumnSource source, int columnCount)
        {
            if (source.ColumnSpan == null)
            {
                source.ColumnSpan = new int?(DefaultSpanHelper.CalcDefaultColumnSpan(source, columnCount));
            }
            if (source.RowSpan == null)
            {
                source.RowSpan = new int?(DefaultSpanHelper.CalcDefaultRowSpan(source));
            }
            int? columnSpan = source.ColumnSpan;
            int num = columnCount;
            if ((columnSpan.GetValueOrDefault() > num) ? (columnSpan != null) : false)
            {
                source.ColumnSpan = new int?(columnCount);
            }
        }

        public void Commit()
        {
            this.TryCommit();
        }

        private void CommitCore(DevExpress.Xpf.Grid.EditForm.EditFormCellData data)
        {
            if (!data.ReadOnly)
            {
                this.ActualEditFormOwner.SetValue(data);
            }
        }

        protected internal virtual EditFormCaptionData CreateCaptionData() => 
            new EditFormCaptionData();

        protected internal virtual DevExpress.Xpf.Grid.EditForm.EditFormCellData CreateCellData() => 
            new DevExpress.Xpf.Grid.EditForm.EditFormCellData();

        private List<EditFormCellDataBase> CreateCellData(int columnCount)
        {
            List<EditFormCellDataBase> list = new List<EditFormCellDataBase>();
            Func<EditFormColumnSource, bool> keySelector = <>c.<>9__42_0;
            if (<>c.<>9__42_0 == null)
            {
                Func<EditFormColumnSource, bool> local1 = <>c.<>9__42_0;
                keySelector = <>c.<>9__42_0 = x => x.Visible;
            }
            Func<EditFormColumnSource, int> func2 = <>c.<>9__42_1;
            if (<>c.<>9__42_1 == null)
            {
                Func<EditFormColumnSource, int> local2 = <>c.<>9__42_1;
                func2 = <>c.<>9__42_1 = y => y.VisibleIndex;
            }
            IEnumerable<EditFormColumnSource> enumerable = this.ActualEditFormOwner.CreateEditFormColumnSource().OrderByDescending<EditFormColumnSource, bool>(keySelector).ThenBy<EditFormColumnSource, int>(func2);
            int num = 0;
            this.editorDataCache = new Dictionary<string, DevExpress.Xpf.Grid.EditForm.EditFormCellData>();
            foreach (EditFormColumnSource source in enumerable)
            {
                if (!string.IsNullOrEmpty(source.FieldName) && !this.editorDataCache.ContainsKey(source.FieldName))
                {
                    this.CoerceSource(source, columnCount);
                    EditFormCaptionData item = this.CreateCaptionData();
                    item.Assign(source);
                    DevExpress.Xpf.Grid.EditForm.EditFormCellData data = this.CreateCellData();
                    data.Assign(source);
                    data.VisibleIndex = num;
                    num++;
                    data.Value = this.EditFormOwner.GetValue(data);
                    this.Validate(data);
                    data.RowData = this;
                    data.ValueChangedEvent += new DevExpress.Xpf.Grid.EditForm.EditFormCellData.ValueChangedEventHandler(this.OnCellValueChanged);
                    this.editorDataCache.Add(data.FieldName, data);
                    if (source.Visible)
                    {
                        list.Add(item);
                        list.Add(data);
                    }
                }
            }
            return list;
        }

        protected internal virtual EditFormLayoutCalculator CreateLayoutCalculator(int columnCount)
        {
            EditFormLayoutCalculator calculator1 = new EditFormLayoutCalculator();
            calculator1.ColumnCount = columnCount;
            return calculator1;
        }

        private IList<DevExpress.Xpf.Grid.EditForm.EditFormCellData> GetEditorData() => 
            this.editorDataCache.Values.ToList<DevExpress.Xpf.Grid.EditForm.EditFormCellData>();

        internal DevExpress.Xpf.Grid.EditForm.EditFormCellData GetEditorData(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                return null;
            }
            DevExpress.Xpf.Grid.EditForm.EditFormCellData data = null;
            this.editorDataCache.TryGetValue(fieldName, out data);
            return data;
        }

        private void OnCellValueChanged(object sender, EventArgs e)
        {
            DevExpress.Xpf.Grid.EditForm.EditFormCellData data = (DevExpress.Xpf.Grid.EditForm.EditFormCellData) sender;
            this.IsModified = true;
            this.Validate(data);
            if (this.EditFormOwner.EditMode == EditFormPostMode.Immediate)
            {
                this.EditFormOwner.IsEditing = false;
                this.CommitCore(data);
                this.EditFormOwner.IsEditing = true;
            }
        }

        protected void OnEditFormOwnerChanged()
        {
            IEditFormOwner editFormOwner = this.EditFormOwner;
            IEditFormOwner instance = editFormOwner;
            if (editFormOwner == null)
            {
                IEditFormOwner local1 = editFormOwner;
                instance = EmptyEditFormOwner.Instance;
            }
            this.ActualEditFormOwner = instance;
            this.UnsubscribeFromData();
            this.RefreshUpdateCancelPanel();
            this.Source = this.ActualEditFormOwner.Source;
            if (this.Source == null)
            {
                this.ActualEditFormOwner.EnqueueImmediateAction(() => this.Source = (this.ActualEditFormOwner != null) ? this.ActualEditFormOwner.Source : this.Source);
            }
            int columnCount = (this.ActualEditFormOwner.ColumnCount > 0) ? this.ActualEditFormOwner.ColumnCount : 1;
            List<EditFormCellDataBase> items = this.CreateCellData(columnCount);
            EditFormLayoutCalculator calculator = this.CreateLayoutCalculator(columnCount);
            this.UpdatePositions(items, calculator);
            this.LayoutSettings = new EditFormLayoutSettings(calculator.LayoutSettings.ColumnCount * 2, calculator.LayoutSettings.RowCount);
            this.EditFormCellData = items;
        }

        private void OnIsModifiedChanged()
        {
            this.ActualEditFormOwner.OnIsModifiedChanged(this.IsModified);
        }

        private void RefreshUpdateCancelPanel()
        {
            this.ShowUpdateCancelButtons = this.CanShowUpdateCancelButtons && this.ActualEditFormOwner.ShowUpdateCancelButtons;
        }

        internal bool TryCommit()
        {
            IList<DevExpress.Xpf.Grid.EditForm.EditFormCellData> editorData = this.GetEditorData();
            if ((editorData != null) && (editorData.Count != 0))
            {
                Func<DevExpress.Xpf.Grid.EditForm.EditFormCellData, bool> predicate = <>c.<>9__56_0;
                if (<>c.<>9__56_0 == null)
                {
                    Func<DevExpress.Xpf.Grid.EditForm.EditFormCellData, bool> local1 = <>c.<>9__56_0;
                    predicate = <>c.<>9__56_0 = x => x.ValidationError != null;
                }
                if (editorData.Any<DevExpress.Xpf.Grid.EditForm.EditFormCellData>(predicate))
                {
                    return false;
                }
                bool flag = false;
                foreach (DevExpress.Xpf.Grid.EditForm.EditFormCellData data in editorData)
                {
                    this.CommitCore(data);
                    if (data.ValidationError != null)
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    return false;
                }
                if (this.Committer == null)
                {
                    this.Close();
                    return true;
                }
                if (!this.Committer.Commit())
                {
                    return false;
                }
                this.Committer.DoCommitProtectedAction(new Action(this.Close));
            }
            return true;
        }

        private void UnsubscribeFromData()
        {
            IList<DevExpress.Xpf.Grid.EditForm.EditFormCellData> editorData = this.GetEditorData();
            if (editorData != null)
            {
                foreach (DevExpress.Xpf.Grid.EditForm.EditFormCellData data in editorData)
                {
                    data.ValueChangedEvent -= new DevExpress.Xpf.Grid.EditForm.EditFormCellData.ValueChangedEventHandler(this.OnCellValueChanged);
                    data.RowData = null;
                }
            }
        }

        private void UpdatePositions(IList<EditFormCellDataBase> items, EditFormLayoutCalculator calculator)
        {
            List<IEditFormLayoutItem> list = new List<IEditFormLayoutItem>();
            IEditFormLayoutItem caption = null;
            IEditFormLayoutItem editor = null;
            foreach (IEditFormLayoutItem item3 in items)
            {
                if (item3.ItemType == EditFormLayoutItemType.Caption)
                {
                    caption = item3;
                }
                else if (item3.ItemType == EditFormLayoutItemType.Editor)
                {
                    editor = item3;
                }
                if ((caption != null) && (editor != null))
                {
                    list.Add(new CaptionedLayoutItem(caption, editor));
                    caption = null;
                    editor = null;
                }
            }
            calculator.SetPositions(list);
        }

        private void Validate(DevExpress.Xpf.Grid.EditForm.EditFormCellData data)
        {
            data.ValidationError = this.ActualEditFormOwner.Validate(data);
        }

        public static Func<EditFormRowData> Factory =>
            ViewModelSource.Factory<EditFormRowData>(Expression.Lambda<Func<EditFormRowData>>(Expression.New(typeof(EditFormRowData)), new ParameterExpression[0]));

        public virtual IEditFormOwner EditFormOwner { get; set; }

        private IEditFormOwner ActualEditFormOwner { get; set; }

        public virtual IList<EditFormCellDataBase> EditFormCellData { get; protected set; }

        public EditFormLayoutSettings LayoutSettings { get; protected set; }

        public bool IsModified
        {
            get => 
                this.isModifiedCore;
            protected set
            {
                if (this.isModifiedCore != value)
                {
                    this.isModifiedCore = value;
                    this.OnIsModifiedChanged();
                }
            }
        }

        public virtual bool ShowUpdateCancelButtons { get; protected set; }

        public virtual object Source { get; protected set; }

        internal bool CanShowUpdateCancelButtons
        {
            get => 
                this.canShowUpdateCancelPanelCore;
            set
            {
                if (this.canShowUpdateCancelPanelCore != value)
                {
                    this.canShowUpdateCancelPanelCore = value;
                    this.RefreshUpdateCancelPanel();
                }
            }
        }

        internal EditFormCommitterBase Committer { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EditFormRowData.<>c <>9 = new EditFormRowData.<>c();
            public static Func<EditFormColumnSource, bool> <>9__42_0;
            public static Func<EditFormColumnSource, int> <>9__42_1;
            public static Func<EditFormCellData, bool> <>9__56_0;

            internal bool <CreateCellData>b__42_0(EditFormColumnSource x) => 
                x.Visible;

            internal int <CreateCellData>b__42_1(EditFormColumnSource y) => 
                y.VisibleIndex;

            internal bool <TryCommit>b__56_0(EditFormCellData x) => 
                x.ValidationError != null;
        }
    }
}

