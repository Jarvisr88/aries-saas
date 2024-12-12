namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Data.Helpers;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class FilterControlEditor : ContentControl, IFilterControlNavigationItem
    {
        public static readonly DependencyProperty EditModeProperty;
        public static readonly DependencyProperty NodeProperty;
        public static readonly DependencyProperty FieldInValueProperty;
        public static readonly DependencyProperty OperatorProperty;
        public static readonly DependencyProperty IndexProperty;
        private ClauseNode lastNode;
        private bool isInitialized;
        private ContentPresenter editorPresenter;

        static FilterControlEditor()
        {
            Type ownerType = typeof(FilterControlEditor);
            EditModeProperty = DependencyPropertyManager.Register("EditMode", typeof(DevExpress.Xpf.Editors.EditMode), ownerType, new PropertyMetadata(DevExpress.Xpf.Editors.EditMode.InplaceInactive, (d, e) => ((FilterControlEditor) d).OnEditModeChanged()));
            NodeProperty = DependencyPropertyManager.Register("Node", typeof(ClauseNode), ownerType, new PropertyMetadata(null, (d, e) => ((FilterControlEditor) d).OnNodeChanged(e)));
            FieldInValueProperty = DependencyPropertyManager.Register("FieldInValue", typeof(bool), ownerType, new PropertyMetadata(false));
            OperatorProperty = DependencyPropertyManager.Register("Operator", typeof(CriteriaOperator), ownerType, new PropertyMetadata(null));
            IndexProperty = DependencyPropertyManager.Register("Index", typeof(int), ownerType, new PropertyMetadata(-1, (d, e) => ((FilterControlEditor) d).IndexChanged((int) e.NewValue)));
        }

        public FilterControlEditor()
        {
            this.SetDefaultStyleKey(typeof(FilterControlEditor));
            base.Loaded += new RoutedEventHandler(this.FilterControlEditorLoaded);
            base.Unloaded += new RoutedEventHandler(this.FilterControlEditorUnloaded);
            this.ChangeOperandTypeCommand = DelegateCommandFactory.Create<object>(obj => this.ChangeOperandType(), false);
            this.ChangeColumnCommand = DelegateCommandFactory.Create<string>(str => this.ChangeColumn(str), false);
        }

        private void AddEditorToContent(BaseEditSettings editSettings)
        {
            base.Content = new InplaceFilterEditor(this.Node.Owner.EditorsOwner, new InplaceFilterEditorColumn(editSettings, this.Node.Owner.EmptyValueTemplate, this.Node.Owner.EmptyStringTemplate, this.Node.Owner.ValueTemplate, this.Node.Owner.BooleanValueTemplate, this.Data), this.Node);
        }

        private void ApplyColumnTemplate(string fieldName)
        {
            FilterColumn columnByFieldName = this.Node.Owner.GetColumnByFieldName(fieldName);
            if ((columnByFieldName != null) && (this.ColumnButton != null))
            {
                this.ColumnButton.ContentTemplate = columnByFieldName.HeaderTemplate;
                this.ColumnButton.ContentTemplateSelector = columnByFieldName.HeaderTemplateSelector;
            }
        }

        private void ChangeColumn(string parameter)
        {
            int navigationIndex = this.NavigationIndex;
            this.ResetOperator(new OperandProperty(parameter));
            this.ApplyColumnTemplate(parameter);
            this.Node.Owner.FocusNodeChild(this.Node, navigationIndex);
        }

        protected virtual void ChangeOperandType()
        {
            int navigationIndex = this.NavigationIndex;
            if (!this.FieldInValue)
            {
                this.ResetOperator(new OperandValue(null));
            }
            else
            {
                this.ResetOperator(this.Node.Owner.CreateDefaultProperty(this.Node));
                this.CreateEditor();
            }
            this.Node.Owner.FocusNodeChild(this.Node, navigationIndex);
        }

        internal void ColumnButtonMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Node.Owner.FocusNodeChild(this.Node, this.NavigationIndex);
            this.Node.VisualClauseNode.CreateColumnsMenu(this.ColumnButton, this.ChangeColumnCommand);
        }

        private void CreateEditor()
        {
            this.SetUpEditorData();
            this.RefreshContent();
        }

        private void DataContentChanged(object sender, EventArgs e)
        {
            if (this.Node != null)
            {
                FilterColumn columnByFieldName = this.Node.Owner.GetColumnByFieldName(this.Node.FirstOperand.PropertyName);
                if (columnByFieldName != null)
                {
                    this.ResetOperator(new OperandValue(FilterHelperBase.CorrectFilterValueType(columnByFieldName.ColumnType, ((EditableDataObject) sender).Value)));
                }
            }
        }

        bool IFilterControlNavigationItem.ShowPopupMenu()
        {
            this.ColumnButtonMouseUp(null, null);
            return true;
        }

        private void FilterControlEditorLoaded(object sender, RoutedEventArgs e)
        {
            if (this.Node != null)
            {
                this.InitializeEditor(this.Index);
            }
        }

        private void FilterControlEditorUnloaded(object sender, RoutedEventArgs e)
        {
            this.isInitialized = false;
            this.lastNode.Editors.Remove(this);
        }

        private void IndexChanged(int newValue)
        {
            this.InitializeEditor(newValue);
        }

        private void InitializeEditor(int index)
        {
            if ((this.Node != null) && (!this.isInitialized && (index >= 0)))
            {
                this.isInitialized = true;
                this.CreateEditor();
                this.Node.Editors.Insert(index, this);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.editorPresenter = (ContentPresenter) base.GetTemplateChild("PART_EditorPresenter");
            if (this.ColumnButton != null)
            {
                this.ColumnButton.RemoveMouseUpHandler(new MouseButtonEventHandler(this.ColumnButtonMouseUp));
            }
            this.ColumnButton = base.GetTemplateChild("PART_Column") as XPFContentControl;
            if (this.ColumnButton != null)
            {
                this.ColumnButton.AddMouseUpHandler(new MouseButtonEventHandler(this.ColumnButtonMouseUp));
            }
            this.UpdateVisualStateCore(false);
        }

        private void OnEditModeChanged()
        {
            this.UpdateVisualStateCore(true);
        }

        private void OnNodeChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                this.lastNode = (ClauseNode) e.NewValue;
            }
        }

        protected void RefreshContent()
        {
            FilterColumn columnByFieldName = this.Node.Owner.GetColumnByFieldName(this.Node.FirstOperand.PropertyName);
            BaseEditSettings editSettings = (columnByFieldName != null) ? columnByFieldName.EditSettings : new TextEditSettings();
            this.AddEditorToContent(editSettings);
        }

        protected void ResetData(object value, bool shouldResetOperator = true)
        {
            if (this.Data != null)
            {
                this.Data.ContentChanged -= new EventHandler(this.DataContentChanged);
            }
            EditableDataObject obj1 = new EditableDataObject();
            obj1.Value = value;
            this.Data = obj1;
            this.Data.ContentChanged += new EventHandler(this.DataContentChanged);
            if (shouldResetOperator && !this.FieldInValue)
            {
                FilterColumn columnByFieldName = this.Node.Owner.GetColumnByFieldName(this.Node.FirstOperand.PropertyName);
                value = (columnByFieldName != null) ? FilterHelperBase.CorrectFilterValueType(columnByFieldName.ColumnType, value) : value;
                this.ResetOperator(new OperandValue(value));
            }
        }

        public void ResetEditor()
        {
            if ((this.Node != null) && (this.Node.Owner.GetColumnByFieldName(this.Node.FirstOperand.PropertyName) != null))
            {
                object currentValue = this.Data.Value;
                if (currentValue != null)
                {
                    this.ResetEditorData(this.Node.Owner.GetColumnByFieldName(this.Node.FirstOperand.PropertyName).ColumnType, currentValue);
                }
                this.AddEditorToContent(this.Node.Owner.GetColumnByFieldName(this.Node.FirstOperand.PropertyName).EditSettings);
            }
        }

        protected virtual void ResetEditorData(Type columnType, object currentValue)
        {
            if (!TypeConvertionValidator.CanConvert(currentValue, columnType))
            {
                this.ResetData(null, true);
            }
            else
            {
                columnType = Nullable.GetUnderlyingType(columnType) ?? columnType;
                currentValue = (currentValue is IConvertible) ? currentValue : Convert.ToString(currentValue, CultureInfo.CurrentCulture);
                try
                {
                    object obj2 = Convert.ChangeType(currentValue, columnType, CultureInfo.CurrentCulture);
                    this.ResetData(obj2, true);
                }
                catch
                {
                    this.ResetData(null, true);
                }
            }
        }

        private void ResetOperator(CriteriaOperator newOperator)
        {
            if (this.Index >= 0)
            {
                this.Node.ResetAdditionalOperand(newOperator, this.Index);
            }
        }

        protected virtual void SetUpEditorData()
        {
            if (this.Operator is OperandValue)
            {
                this.ResetData(((OperandValue) this.Operator).Value, true);
            }
            else if (!(this.Operator is OperandProperty))
            {
                this.ResetData(null, true);
            }
            else
            {
                this.FieldInValue = true;
                this.ResetData(null, true);
                this.ApplyColumnTemplate(((OperandProperty) this.Operator).PropertyName);
            }
        }

        private void UpdateVisualStateCore(bool useTransition)
        {
            if (this.EditMode == DevExpress.Xpf.Editors.EditMode.InplaceActive)
            {
                this.editorPresenter.ClearValue(FrameworkElement.CursorProperty);
            }
            else
            {
                this.editorPresenter.Cursor = Cursors.Hand;
            }
            VisualStateManager.GoToState(this, this.EditMode.ToString(), useTransition);
        }

        public ClauseNode Node
        {
            get => 
                (ClauseNode) base.GetValue(NodeProperty);
            set => 
                base.SetValue(NodeProperty, value);
        }

        public bool FieldInValue
        {
            get => 
                (bool) base.GetValue(FieldInValueProperty);
            set => 
                base.SetValue(FieldInValueProperty, value);
        }

        public CriteriaOperator Operator
        {
            get => 
                (CriteriaOperator) base.GetValue(OperatorProperty);
            set => 
                base.SetValue(OperatorProperty, value);
        }

        public int Index
        {
            get => 
                (int) base.GetValue(IndexProperty);
            set => 
                base.SetValue(IndexProperty, value);
        }

        public DevExpress.Xpf.Editors.EditMode EditMode
        {
            get => 
                (DevExpress.Xpf.Editors.EditMode) base.GetValue(EditModeProperty);
            set => 
                base.SetValue(EditModeProperty, value);
        }

        public ICommand ChangeOperandTypeCommand { get; private set; }

        public ICommand ChangeColumnCommand { get; private set; }

        internal EditableDataObject Data { get; set; }

        private XPFContentControl ColumnButton { get; set; }

        private int NavigationIndex
        {
            get
            {
                UIElement child = ((IFilterControlNavigationItem) this).Child;
                return this.Node.VisualClauseNode.Children.IndexOf(child);
            }
        }

        UIElement IFilterControlNavigationItem.Child =>
            this.FieldInValue ? (base.GetTemplateChild("PART_Column") as UIElement) : (base.Content as UIElement);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterControlEditor.<>c <>9 = new FilterControlEditor.<>c();

            internal void <.cctor>b__5_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterControlEditor) d).OnEditModeChanged();
            }

            internal void <.cctor>b__5_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterControlEditor) d).OnNodeChanged(e);
            }

            internal void <.cctor>b__5_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterControlEditor) d).IndexChanged((int) e.NewValue);
            }
        }
    }
}

