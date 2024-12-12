namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Filtering.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class FilterControl : Control, IDialogContent
    {
        public static readonly DependencyProperty FilterCriteriaProperty;
        private static readonly DependencyPropertyKey RootNodePropertyKey;
        public static readonly DependencyProperty RootNodeProperty;
        public static readonly DependencyProperty SourceControlProperty;
        public static readonly DependencyProperty ShowDateTimeOperatorsProperty;
        public static readonly DependencyProperty DefaultColumnProperty;
        public static readonly DependencyProperty ShowOperandTypeIconProperty;
        public static readonly DependencyProperty ShowGroupCommandsIconProperty;
        public static readonly DependencyProperty ShowToolTipsProperty;
        public static readonly DependencyProperty ShowBorderProperty;
        public static readonly DependencyProperty AllowedGroupFiltersProperty;
        public static readonly DependencyProperty EmptyValueTemplateProperty;
        public static readonly DependencyProperty EmptyStringTemplateProperty;
        public static readonly DependencyProperty ValueTemplateProperty;
        public static readonly DependencyProperty BooleanValueTemplateProperty;
        public static readonly RoutedEvent BeforeShowValueEditorEvent;
        private IEnumerable<FilterColumn> filterColumns;
        private IFilteredComponent filterSourceControl;
        private InplaceFilterEditor lastActiveFilterEditor;
        private FilterControlFocusVisualHelper focusVisualHelper;

        public event ShowValueEditorEventHandler BeforeShowValueEditor
        {
            add
            {
                base.AddHandler(BeforeShowValueEditorEvent, value);
            }
            remove
            {
                base.RemoveHandler(BeforeShowValueEditorEvent, value);
            }
        }

        static FilterControl()
        {
            Type ownerType = typeof(FilterControl);
            FilterCriteriaProperty = DependencyPropertyManager.Register("FilterCriteria", typeof(CriteriaOperator), ownerType, new PropertyMetadata(null, (d, e) => ((FilterControl) d).OnFilterCriteriaChanged()));
            RootNodePropertyKey = DependencyPropertyManager.RegisterReadOnly("RootNode", typeof(GroupNode), ownerType, new PropertyMetadata(null));
            RootNodeProperty = RootNodePropertyKey.DependencyProperty;
            SourceControlProperty = DependencyPropertyManager.Register("SourceControl", typeof(object), ownerType, new PropertyMetadata(null, (d, e) => ((FilterControl) d).OnSourceControlChanged()));
            ShowDateTimeOperatorsProperty = DependencyPropertyManager.Register("ShowDateTimeOperators", typeof(bool), ownerType, new PropertyMetadata(true));
            DefaultColumnProperty = DependencyPropertyManager.Register("DefaultColumn", typeof(FilterColumn), ownerType, new PropertyMetadata(null));
            ShowOperandTypeIconProperty = DependencyPropertyManager.Register("ShowOperandTypeIcon", typeof(bool), ownerType, new PropertyMetadata(false));
            ShowGroupCommandsIconProperty = DependencyPropertyManager.Register("ShowGroupCommandsIcon", typeof(bool), ownerType, new PropertyMetadata(false));
            ShowToolTipsProperty = DependencyPropertyManager.Register("ShowToolTips", typeof(bool), ownerType, new PropertyMetadata(true));
            ShowBorderProperty = DependencyPropertyManager.Register("ShowBorder", typeof(bool), ownerType, new PropertyMetadata(true));
            AllowedGroupFiltersProperty = DependencyPropertyManager.Register("AllowedGroupFilters", typeof(DevExpress.Xpf.Editors.Filtering.AllowedGroupFilters), ownerType, new PropertyMetadata(DevExpress.Xpf.Editors.Filtering.AllowedGroupFilters.All));
            EmptyValueTemplateProperty = DependencyProperty.Register("EmptyValueTemplate", typeof(ControlTemplate), ownerType, null);
            EmptyStringTemplateProperty = DependencyProperty.Register("EmptyStringTemplate", typeof(ControlTemplate), ownerType, null);
            ValueTemplateProperty = DependencyProperty.Register("ValueTemplate", typeof(ControlTemplate), ownerType, null);
            BooleanValueTemplateProperty = DependencyProperty.Register("BooleanValueTemplate", typeof(ControlTemplate), ownerType, null);
            BeforeShowValueEditorEvent = EventManager.RegisterRoutedEvent("BeforeShowValueEditor", RoutingStrategy.Direct, typeof(ShowValueEditorEventHandler), ownerType);
        }

        public FilterControl()
        {
            this.ApplyFilterCommand = new DelegateCommand(new Action(this.ApplyFilter));
            this.SetDefaultStyleKey(typeof(FilterControl));
            this.EditorsImmediateActionsManager = new ImmediateActionsManager(this);
            this.EditorsOwner = new InplaceFilterEditorOwner(this);
            this.SetDefaultStyleKey(typeof(FilterControl));
            this.CreateTree(null);
            base.LayoutUpdated += (<sender>, <e>) => this.EditorsImmediateActionsManager.ExecuteActions();
        }

        protected internal ClauseNode AddClauseNode(GroupNode addTo)
        {
            ClauseNode item = this.CreateDefaultClauseNode(this.DefaultColumn);
            if (item != null)
            {
                addTo.SubNodes.Add(item);
                item.SetOwner(addTo.Owner, addTo);
            }
            return item;
        }

        protected internal NodeBase AddGroup(GroupNode currentNode)
        {
            GroupNode item = (GroupNode) this.CreateNodesFactory().Create(this.GetDefaultGroupType(), new INode[0]);
            currentNode.SubNodes.Add(item);
            item.SetOwner(currentNode.Owner, currentNode);
            return (this.AddClauseNode(item) ?? item);
        }

        public void ApplyFilter()
        {
            this.FilterCriteria = this.ToCriteria(this.RootNode);
            if (this.FilterSourceControl != null)
            {
                this.FilterSourceControl.RowCriteria = this.FilterCriteria;
            }
        }

        private void CheckFocusVisual()
        {
            FrameworkElement focusedElement = (!base.IsKeyboardFocusWithin || (ReferenceEquals(FocusHelper.GetFocusedElement(), this) || (this.EditorsOwner.ActiveEditor != null))) ? null : (FocusHelper.GetFocusedElement() as FrameworkElement);
            if (focusedElement != null)
            {
                IFilterControlNavigationNode node;
                UIElement element2;
                this.GetFocusedElements(focusedElement, out node, out element2);
                focusedElement = ((element2 == null) || !LayoutHelper.IsChildElement(element2, focusedElement)) ? null : ((FrameworkElement) element2);
            }
            this.focusVisualHelper.FocusedElement = focusedElement;
        }

        protected internal void ClearAll()
        {
            this.RootNode.SubNodes.Clear();
        }

        protected virtual ClauseNode CreateDefaultClauseNode(FilterColumn column)
        {
            if (this.FilterColumns == null)
            {
                return null;
            }
            if (column == null)
            {
                if (this.FilterColumns.Count<FilterColumn>() == 0)
                {
                    return null;
                }
                column = this.FilterColumns.ElementAt<FilterColumn>(0);
            }
            ClauseType defaultOperation = this.GetDefaultOperation(column.FieldName);
            ClauseNode node = (ClauseNode) this.CreateNodesFactory().Create(defaultOperation, this.CreateDefaultProperty(column), new CriteriaOperator[0]);
            this.ValidateAdditionalOperands(node);
            return node;
        }

        protected internal virtual OperandProperty CreateDefaultProperty(IClauseNode node) => 
            (this.GetColumnByFieldName(node.FirstOperand.PropertyName) == null) ? this.CreateDefaultProperty(this.DefaultColumn) : node.FirstOperand.Clone();

        protected internal OperandProperty CreateDefaultProperty(FilterColumn column) => 
            ((this.FilterColumns == null) || (this.FilterColumns.Count<FilterColumn>() <= 0)) ? new OperandProperty(string.Empty) : ((column == null) ? new OperandProperty(this.FilterColumns.ElementAt<FilterColumn>(0).FieldName) : new OperandProperty(column.FieldName));

        internal NodeBase CreateDefaultRootNode() => 
            (this.DefaultColumn == null) ? ((NodeBase) ((GroupNode) this.CreateNodesFactory().Create(this.GetDefaultGroupType(), new INode[0]))) : ((NodeBase) this.CreateDefaultClauseNode(this.DefaultColumn));

        private void CreateFilterColumnCollection()
        {
            if (this.FilterSourceControl != null)
            {
                object filterColumns = this.FilterColumns;
                this.FilterColumns = this.FilterSourceControl.CreateFilterColumnCollection();
                if (filterColumns == null)
                {
                    this.CreateTree(this.FilterCriteria);
                }
            }
        }

        protected virtual INodesFactory CreateNodesFactory() => 
            new FilterControlNodesFactory();

        private void CreateTree(CriteriaOperator criteria)
        {
            NodeBase base2 = (NodeBase) CriteriaToTreeProcessor.GetTree(this.CreateNodesFactory(), criteria, null, false);
            base2 ??= this.CreateDefaultRootNode();
            if (!(base2 is GroupNode))
            {
                INode[] subNodes = new INode[] { base2 };
                base2 = (GroupNode) this.CreateNodesFactory().Create(GroupType.And, subNodes);
            }
            this.RootNode = (GroupNode) base2;
            this.RootNode.SetOwner(this, null);
        }

        bool IDialogContent.CanCloseWithOKResult() => 
            true;

        void IDialogContent.OnApply()
        {
            this.ApplyFilter();
        }

        void IDialogContent.OnOk()
        {
            this.ApplyFilter();
        }

        internal void EnqueueImmediateAction(IAction action)
        {
            this.EditorsImmediateActionsManager.EnqueueAction(action);
        }

        protected internal void FocusElement(UIElement element)
        {
            if (element is IFilterControlNavigationItem)
            {
                element = (element as IFilterControlNavigationItem).Child;
            }
            this.EditorsOwner.EditorWasClosed = true;
            this.SetFocusedEditor(FilterControlKeyboardHelper.GetParentEditor(element));
            element.Focus();
        }

        protected internal void FocusNodeChild(NodeBase node, int childIndex)
        {
            base.Dispatcher.BeginInvoke(() => this.FocusNodeChildCore1(node, childIndex), DispatcherPriority.Background, null);
        }

        protected internal void FocusNodeChildCore1(NodeBase node, int childIndex)
        {
            IFilterControlNavigationNode visualNode = node.VisualNode;
            if (visualNode != null)
            {
                IList<UIElement> children = visualNode.Children;
                if ((0 <= childIndex) && (childIndex < children.Count))
                {
                    node.Owner.FocusElement(children[childIndex]);
                }
            }
        }

        public FilterColumn GetColumnByFieldName(string fieldName)
        {
            FilterColumn column2;
            if (this.FilterColumns == null)
            {
                return null;
            }
            using (IEnumerator<FilterColumn> enumerator = this.FilterColumns.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        FilterColumn current = enumerator.Current;
                        if (current.FieldName != fieldName)
                        {
                            continue;
                        }
                        column2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return column2;
        }

        protected internal virtual string GetDefaultColumnCaption(IClauseNode node) => 
            node.FirstOperand.PropertyName;

        protected virtual GroupType GetDefaultGroupType() => 
            (this.AllowedGroupFilters != DevExpress.Xpf.Editors.Filtering.AllowedGroupFilters.None) ? (((this.AllowedGroupFilters & DevExpress.Xpf.Editors.Filtering.AllowedGroupFilters.And) != DevExpress.Xpf.Editors.Filtering.AllowedGroupFilters.And) ? (((this.AllowedGroupFilters & DevExpress.Xpf.Editors.Filtering.AllowedGroupFilters.Or) != DevExpress.Xpf.Editors.Filtering.AllowedGroupFilters.Or) ? (((this.AllowedGroupFilters & DevExpress.Xpf.Editors.Filtering.AllowedGroupFilters.NotAnd) != DevExpress.Xpf.Editors.Filtering.AllowedGroupFilters.NotAnd) ? (((this.AllowedGroupFilters & DevExpress.Xpf.Editors.Filtering.AllowedGroupFilters.NotOr) != DevExpress.Xpf.Editors.Filtering.AllowedGroupFilters.NotOr) ? GroupType.And : GroupType.NotOr) : GroupType.NotAnd) : GroupType.Or) : GroupType.And) : GroupType.And;

        protected internal virtual ClauseType GetDefaultOperation(string fieldName)
        {
            FilterColumn columnByFieldName = this.GetColumnByFieldName(fieldName);
            return ((columnByFieldName != null) ? columnByFieldName.GetDefaultOperation() : ClauseType.Equals);
        }

        private void GetFocusedElements(DependencyObject focusedElement, out IFilterControlNavigationNode focusedNode, out UIElement focusedChild)
        {
            focusedNode = LayoutHelper.FindLayoutOrVisualParentObject<IFilterControlNavigationNode>(focusedElement, false, null);
            focusedChild = null;
            if (focusedNode != null)
            {
                IList<UIElement> children = focusedNode.Children;
                for (UIElement element = (UIElement) focusedElement; element != focusedNode; element = (UIElement) LayoutHelper.GetParent(element, false))
                {
                    if (children.IndexOf(element) != -1)
                    {
                        focusedChild = element;
                        return;
                    }
                }
            }
        }

        protected internal List<ClauseType> GetListOperationsByTypes(string fieldName) => 
            FilterControlHelper.GetListOperationsByFilterColumn(this.GetColumnByFieldName(fieldName));

        private bool IsChildNode(GroupNode parentNode, NodeBase node)
        {
            if (parentNode == null)
            {
                throw new ArgumentNullException("parentNode");
            }
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            while ((node.ParentNode != null) && !ReferenceEquals(node.ParentNode, parentNode))
            {
                node = (GroupNode) node.ParentNode;
            }
            return ReferenceEquals(node.ParentNode, parentNode);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.focusVisualHelper = new FilterControlFocusVisualHelper(base.GetTemplateChild("PART_FocusVisualContainer") as Canvas, base.FocusVisualStyle);
        }

        protected void OnFilterCriteriaChanged()
        {
            if (!Equals(this.FilterCriteria, this.ToCriteria(this.RootNode)))
            {
                this.CreateTree(this.FilterCriteria);
            }
        }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);
            if (!ReferenceEquals(FocusHelper.GetFocusedElement(), this) || (this.RootNode.VisualNode == null))
            {
                this.CheckFocusVisual();
            }
            else
            {
                IList<UIElement> children = ((IFilterControlNavigationNode) this.RootNode.VisualNode).Children;
                if (children.Count > 0)
                {
                    this.FocusElement(children[0]);
                }
            }
        }

        protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnIsKeyboardFocusWithinChanged(e);
            this.EditorsOwner.ProcessIsKeyboardFocusWithinChanged();
        }

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnLostKeyboardFocus(e);
            this.CheckFocusVisual();
        }

        protected override void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnPreviewLostKeyboardFocus(e);
            this.EditorsOwner.ProcessPreviewLostKeyboardFocus(e);
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            this.EditorsOwner.ProcessMouseLeftButtonDown(e);
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);
            this.EditorsOwner.ProcessMouseLeftButtonUp(e);
        }

        protected void OnSourceControlChanged()
        {
            this.UpdateFilterSourceControl();
        }

        internal void PerformNavigationOnLeftButtonDown(DependencyObject originalSource)
        {
            FilterControlEditor editor = LayoutHelper.FindLayoutOrVisualParentObject<FilterControlEditor>(originalSource, false, null);
            this.SetFocusedEditor(editor);
        }

        internal void ProcessKeyDown(KeyEventArgs e)
        {
            if ((e.Key == Key.Tab) && ModifierKeysHelper.IsCtrlPressed(e.KeyboardDevice.Modifiers))
            {
                this.EditorsOwner.MoveFocus(e);
            }
            else
            {
                IFilterControlNavigationNode node;
                UIElement element;
                FilterControlNavigationHelper.FilterControlNavigationDirection direction;
                this.GetFocusedElements((DependencyObject) e.OriginalSource, out node, out element);
                if (!e.Handled && ((node != null) && (!(element is InplaceFilterEditor) || !(element as InplaceFilterEditor).IsEditorVisible)))
                {
                    node.ProcessKeyDown(e, element);
                    if (e.Handled)
                    {
                        return;
                    }
                }
                if (FilterControlKeyboardHelper.IsNavigationKey(e, base.FlowDirection == FlowDirection.RightToLeft, out direction))
                {
                    this.ProcessNavigationKey(node, element, direction);
                    e.Handled = true;
                }
                else if (!FilterControlKeyboardHelper.IsShowMenuKey(e) || ((node == null) || ((element == null) || (element is InplaceFilterEditor))))
                {
                    bool flag;
                    if (FilterControlKeyboardHelper.IsTabKey(e, out flag))
                    {
                        e.Handled |= this.ProcessTabKey(flag);
                    }
                }
                else
                {
                    FilterControlEditor parentEditor = FilterControlKeyboardHelper.GetParentEditor(element);
                    if (parentEditor != null)
                    {
                        e.Handled = ((IFilterControlNavigationItem) parentEditor).ShowPopupMenu();
                    }
                    else
                    {
                        e.Handled = node.ShowPopupMenu(element);
                    }
                }
            }
        }

        private void ProcessNavigationKey(IFilterControlNavigationNode focusedNode, UIElement focusedChild, FilterControlNavigationHelper.FilterControlNavigationDirection navigationDirection)
        {
            UIElement element = FilterControlNavigationHelper.GetChildToFocus(this.RootNode.VisualNode, focusedNode, focusedChild, navigationDirection);
            if (element != null)
            {
                this.FocusElement(element);
            }
        }

        private bool ProcessTabKey(bool isShiftPressed) => 
            false;

        protected internal virtual void RaiseBeforeShowValueEditor(ShowValueEditorEventArgs arg)
        {
            arg.RoutedEvent = BeforeShowValueEditorEvent;
            base.RaiseEvent(arg);
        }

        protected internal void RemoveNode(NodeBase node)
        {
            if (node.ParentNode != null)
            {
                FilterControlNodeBase base2 = LayoutHelper.FindLayoutOrVisualParentObject<FilterControlNodeBase>((DependencyObject) FocusHelper.GetFocusedElement(), false, null);
                IFilterControlNavigationNode node2 = null;
                if ((base2 != null) && (ReferenceEquals(base2.Node, node) || ((node is GroupNode) && this.IsChildNode(node as GroupNode, base2.Node))))
                {
                    base.Focus();
                    IFilterControlNavigationNode visualNode = ((GroupNode) node.ParentNode).VisualNode;
                    int index = node.ParentNode.SubNodes.IndexOf(node);
                    node2 = (index >= (visualNode.SubNodes.Count - 1)) ? ((index <= 0) ? visualNode : visualNode.SubNodes[index - 1]) : visualNode.SubNodes[index + 1];
                }
                node.ParentNode.SubNodes.Remove(node);
                if (node2 != null)
                {
                    this.FocusElement(node2.Children[0]);
                }
                GroupNode parentNode = node.ParentNode as GroupNode;
                if ((parentNode != null) && (parentNode.VisualNode is FilterControlGroupNode))
                {
                    ((FilterControlGroupNode) parentNode.VisualNode).UpdateAddButtonVisibility();
                }
            }
        }

        private void SetFocusedEditor(FilterControlEditor editor)
        {
            if (editor == null)
            {
                if (this.lastActiveFilterEditor != null)
                {
                    this.lastActiveFilterEditor.IsEditorFocused = false;
                    this.lastActiveFilterEditor = null;
                }
            }
            else if (!ReferenceEquals(this.lastActiveFilterEditor, (InplaceFilterEditor) editor.Content))
            {
                if (this.lastActiveFilterEditor != null)
                {
                    this.lastActiveFilterEditor.IsEditorFocused = false;
                }
                this.lastActiveFilterEditor = (InplaceFilterEditor) editor.Content;
                if (this.lastActiveFilterEditor != null)
                {
                    this.lastActiveFilterEditor.IsEditorFocused = true;
                }
            }
        }

        private void SourceControlDataSourceChanged(object sender, EventArgs e)
        {
            this.CreateFilterColumnCollection();
        }

        private void SourceControlFilterChanged(object sender, EventArgs e)
        {
            if (this.FilterSourceControl != null)
            {
                this.FilterCriteria = this.FilterSourceControl.RowCriteria;
            }
        }

        protected virtual CriteriaOperator ToCriteria(INode node) => 
            FilterControlHelpers.ToCriteria(node);

        private void UpdateFilterSourceControl()
        {
            if (this.SourceControl is DataTable)
            {
                this.FilterSourceControl = new BindingListFilterProxy(((DataTable) this.SourceControl).DefaultView);
            }
            else if (!(this.SourceControl is IBindingListView) && !(this.SourceControl is IFilteredXtraBindingList))
            {
                this.FilterSourceControl = this.SourceControl as IFilteredComponent;
            }
            else
            {
                this.FilterSourceControl = new BindingListFilterProxy((IBindingList) this.SourceControl);
            }
        }

        protected internal virtual void ValidateAdditionalOperands(ClauseNode node)
        {
            FilterControlHelpers.ValidateAdditionalOperands(node.Operation, node.AdditionalOperands);
        }

        [Category("Options Filter"), Description("Gets or sets the total filter expression.")]
        public CriteriaOperator FilterCriteria
        {
            get => 
                (CriteriaOperator) base.GetValue(FilterCriteriaProperty);
            set => 
                base.SetValue(FilterCriteriaProperty, value);
        }

        [Description("Gets the root node of the tree, representing the filter expression.")]
        public GroupNode RootNode
        {
            get => 
                (GroupNode) base.GetValue(RootNodeProperty);
            private set => 
                base.SetValue(RootNodePropertyKey, value);
        }

        [Description("Gets or sets the source control. This is a dependency property."), Category("Options Filter")]
        public object SourceControl
        {
            get => 
                base.GetValue(SourceControlProperty);
            set => 
                base.SetValue(SourceControlProperty, value);
        }

        [Description("Gets or sets whether date-time specific operators are available for date-time fields. This is a dependency property."), Category("Options Filter")]
        public bool ShowDateTimeOperators
        {
            get => 
                (bool) base.GetValue(ShowDateTimeOperatorsProperty);
            set => 
                base.SetValue(ShowDateTimeOperatorsProperty, value);
        }

        [Category("Options Filter"), Description("Gets or sets the default filter column. This is a dependency property.")]
        public FilterColumn DefaultColumn
        {
            get => 
                (FilterColumn) base.GetValue(DefaultColumnProperty);
            set => 
                base.SetValue(DefaultColumnProperty, value);
        }

        [Category("Options Filter"), Description("Gets or sets whether the operand's value can be swapped. This is a dependency property.")]
        public bool ShowOperandTypeIcon
        {
            get => 
                (bool) base.GetValue(ShowOperandTypeIconProperty);
            set => 
                base.SetValue(ShowOperandTypeIconProperty, value);
        }

        [Description("Gets or sets whether the group commands icon is displayed. This is a dependency property."), Category("Options Filter")]
        public bool ShowGroupCommandsIcon
        {
            get => 
                (bool) base.GetValue(ShowGroupCommandsIconProperty);
            set => 
                base.SetValue(ShowGroupCommandsIconProperty, value);
        }

        [Description("Gets or sets whether to show tooltips for the Filter Control icons. This is a dependency property."), Category("Options Filter")]
        public bool ShowToolTips
        {
            get => 
                (bool) base.GetValue(ShowToolTipsProperty);
            set => 
                base.SetValue(ShowToolTipsProperty, value);
        }

        [Category("Options Filter"), Description("Gets or sets whether to show the border around the Filter Control.")]
        public bool ShowBorder
        {
            get => 
                (bool) base.GetValue(ShowBorderProperty);
            set => 
                base.SetValue(ShowBorderProperty, value);
        }

        [Description("Gets or sets possible group filters that the FilterControl supports."), Category("Options Filter")]
        public DevExpress.Xpf.Editors.Filtering.AllowedGroupFilters AllowedGroupFilters
        {
            get => 
                (DevExpress.Xpf.Editors.Filtering.AllowedGroupFilters) base.GetValue(AllowedGroupFiltersProperty);
            set => 
                base.SetValue(AllowedGroupFiltersProperty, value);
        }

        [Description("Gets or sets the template that defines the presentation of empty filter values. This is a dependency property.")]
        public ControlTemplate EmptyValueTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(EmptyValueTemplateProperty);
            set => 
                base.SetValue(EmptyValueTemplateProperty, value);
        }

        [Description("Gets or sets the template that defines the presentation of filter values set to an empty string. This is a dependency property.")]
        public ControlTemplate EmptyStringTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(EmptyStringTemplateProperty);
            set => 
                base.SetValue(EmptyStringTemplateProperty, value);
        }

        [Description("Gets or sets the template that defines the presentation of filter values. This is a dependency property.")]
        public ControlTemplate ValueTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(ValueTemplateProperty);
            set => 
                base.SetValue(ValueTemplateProperty, value);
        }

        [Description("Gets or sets the template that defines the presentation of Boolean filter values. This is a dependency property.")]
        public ControlTemplate BooleanValueTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(BooleanValueTemplateProperty);
            set => 
                base.SetValue(BooleanValueTemplateProperty, value);
        }

        public bool SupportDomainDataSource { get; set; }

        [Description("Gets the actual filter expression.")]
        public CriteriaOperator ActualFilterCriteria =>
            this.ToCriteria(this.RootNode);

        protected override bool HandlesScrolling =>
            true;

        internal InplaceFilterEditorOwner EditorsOwner { get; private set; }

        private ImmediateActionsManager EditorsImmediateActionsManager { get; set; }

        [Browsable(false)]
        public IEnumerable<FilterColumn> FilterColumns
        {
            get => 
                this.filterColumns;
            set
            {
                if (value == null)
                {
                    this.filterColumns = new List<FilterColumn>();
                }
                else
                {
                    this.filterColumns = value;
                }
            }
        }

        private IFilteredComponent FilterSourceControl
        {
            get => 
                this.filterSourceControl;
            set
            {
                if (!ReferenceEquals(this.FilterSourceControl, value))
                {
                    if (this.FilterSourceControl != null)
                    {
                        this.FilterSourceControl.PropertiesChanged -= new EventHandler(this.SourceControlDataSourceChanged);
                        this.FilterSourceControl.RowFilterChanged -= new EventHandler(this.SourceControlFilterChanged);
                    }
                    this.filterSourceControl = value;
                    if (this.FilterSourceControl != null)
                    {
                        this.FilterSourceControl.PropertiesChanged += new EventHandler(this.SourceControlDataSourceChanged);
                        this.FilterSourceControl.RowFilterChanged += new EventHandler(this.SourceControlFilterChanged);
                    }
                    this.CreateFilterColumnCollection();
                }
            }
        }

        public ICommand ApplyFilterCommand { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterControl.<>c <>9 = new FilterControl.<>c();

            internal void <.cctor>b__16_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterControl) d).OnFilterCriteriaChanged();
            }

            internal void <.cctor>b__16_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterControl) d).OnSourceControlChanged();
            }
        }
    }
}

