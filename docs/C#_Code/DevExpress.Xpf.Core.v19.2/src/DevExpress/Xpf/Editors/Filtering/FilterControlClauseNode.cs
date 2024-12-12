namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Filtering.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class FilterControlClauseNode : FilterControlNodeBase
    {
        public static readonly DependencyProperty SecondOperandsCountProperty;
        public static readonly DependencyProperty SecondOperandsOneTemplateProperty;
        public static readonly DependencyProperty SecondOperandsTwoTemplateProperty;
        public static readonly DependencyProperty SecondOperandsSeveralTemplateProperty;
        public static readonly DependencyProperty SecondOperandsLocalDateTimeTemplateProperty;
        private ICommand changeColumnCommand;

        static FilterControlClauseNode()
        {
            Type ownerType = typeof(FilterControlClauseNode);
            SecondOperandsCountProperty = DependencyPropertyManager.Register("SecondOperandsCount", typeof(OperandsCount), ownerType, new PropertyMetadata(OperandsCount.None, (d, e) => ((FilterControlClauseNode) d).UpdateSecondOperandsTemplate()));
            SecondOperandsOneTemplateProperty = DependencyPropertyManager.Register("SecondOperandsOneTemplate", typeof(ControlTemplate), ownerType, new PropertyMetadata(null, (d, e) => ((FilterControlClauseNode) d).UpdateSecondOperandsTemplate()));
            SecondOperandsTwoTemplateProperty = DependencyPropertyManager.Register("SecondOperandsTwoTemplate", typeof(ControlTemplate), ownerType, new PropertyMetadata(null, (d, e) => ((FilterControlClauseNode) d).UpdateSecondOperandsTemplate()));
            SecondOperandsSeveralTemplateProperty = DependencyPropertyManager.Register("SecondOperandsSeveralTemplate", typeof(ControlTemplate), ownerType, new PropertyMetadata(null, (d, e) => ((FilterControlClauseNode) d).UpdateSecondOperandsTemplate()));
            SecondOperandsLocalDateTimeTemplateProperty = DependencyProperty.Register("SecondOperandsLocalDateTimeTemplate", typeof(ControlTemplate), ownerType, new PropertyMetadata(null, (d, e) => ((FilterControlClauseNode) d).UpdateSecondOperandsTemplate()));
        }

        public FilterControlClauseNode()
        {
            this.SetDefaultStyleKey(typeof(FilterControlClauseNode));
            this.RemoveNodeCommand = DelegateCommandFactory.Create<object>(obj => this.RemoveNode(), false);
            this.ChangeFirstOperandNodeCommand = DelegateCommandFactory.Create<string>(str => this.ChangeFirstOperandNode(str), false);
            this.ChangeOperationNodeCommand = DelegateCommandFactory.Create<ClauseType>(clauseType => this.ChangeOperationNode(clauseType), false);
            this.AddOperandNodeCommand = DelegateCommandFactory.Create<object>(obj => this.AddOperandNode(), false);
            this.ChangeLocalDateTimeFunctionTypeCommand = DelegateCommandFactory.Create<FunctionOperatorType>(x => this.ChangeLocalDateTimeFunctionType(x));
            this.LocalDateTimeMenu = new PopupMenu();
        }

        private List<BarItem> AddFirstOperandButtonItemsToPopupMenu(ContentControl button)
        {
            int num = 0;
            List<BarItem> list = new List<BarItem>();
            ICommand command = this.changeColumnCommand ?? this.ChangeFirstOperandNodeCommand;
            foreach (FilterColumn column in this.ClauseNode.Owner.FilterColumns)
            {
                BarItem item = base.AddItemToPopupMenu(button, "Item" + num.ToString(), column.ColumnCaption, column.HeaderTemplate, column.HeaderTemplateSelector, command, column.FieldName, string.Empty);
                list.Add(item);
                num++;
            }
            return list;
        }

        protected override void AddItemsToPopupMenu(ContentControl button)
        {
            base.AddItemsToPopupMenu(button);
            if (ReferenceEquals(button, this.OperationButton))
            {
                this.AddOperationButtonItemsToPopupMenu(this.OperationButton);
            }
            else
            {
                this.AddFirstOperandButtonItemsToPopupMenu(this.FirstOperandButton);
            }
        }

        private void AddOperandNode()
        {
            int count = base.NavigationChildren.Count;
            this.ClauseNode.AddAdditionalOperand(new OperandValue());
            base.Node.Owner.FocusNodeChild(base.Node, count);
        }

        private List<BarItem> AddOperationButtonItemsToPopupMenu(ContentControl button)
        {
            List<BarItem> list = new List<BarItem>();
            List<ClauseType> list3 = new List<ClauseType>();
            foreach (ClauseType type in this.GetAvailableOperations())
            {
                if (!this.IsDateTimeOperatorClause(type))
                {
                    list.Add(base.AddItemToPopupMenu(button, type.ToString(), OperationHelper.GetMenuStringByType(type), this.ChangeOperationNodeCommand, type, type.ToString()));
                    continue;
                }
                if (this.ClauseNode.Owner.ShowDateTimeOperators)
                {
                    list3.Add(type);
                }
            }
            if (list3.Count > 0)
            {
                BarSubItem item = base.AddSubMenuToPopupMenu(button, "DateTimeOperatorsSubMenu", EditorLocalizer.GetString(EditorStringId.FilterDateTimeOperatorMenuCaption));
                list.Add(item);
                foreach (ClauseType type2 in list3)
                {
                    base.AddItemToPopupMenu(item, button, type2.ToString(), OperationHelper.GetMenuStringByType(type2), this.ChangeOperationNodeCommand, type2, string.Empty);
                }
            }
            return list;
        }

        private void ChangeFirstOperandNode(string parameter)
        {
            this.ClauseNode.FirstOperand = new OperandProperty(parameter);
            FilterColumn columnByFieldName = this.ClauseNode.Owner.GetColumnByFieldName(parameter);
            if ((columnByFieldName == null) || !columnByFieldName.IsValidClause(this.ClauseNode.Operation))
            {
                this.ClauseNode.Operation = this.ClauseNode.Owner.GetDefaultOperation(parameter);
            }
            foreach (FilterControlEditor editor in this.ClauseNode.Editors)
            {
                editor.ResetEditor();
            }
            this.FocusNextNavigationChild(this.FirstOperandButton);
        }

        private void ChangeLocalDateTimeFunctionType(FunctionOperatorType newType)
        {
            this.ClauseNode.ResetAdditionalOperand(new FunctionOperator(newType, new CriteriaOperator[0]), 0);
        }

        private void ChangeOperationNode(ClauseType clauseType)
        {
            this.ClauseNode.Operation = clauseType;
            this.FocusNextNavigationChild(this.OperationButton);
        }

        private IEnumerable<BarButtonItem> CreateBars() => 
            FilterControlHelper.GetLocalDateTimeFuncs().Select<FunctionOperatorType, BarButtonItem>((x, i) => new BarButtonItem { 
                Name = "Item" + i,
                Command = this.ChangeLocalDateTimeFunctionTypeCommand,
                CommandParameter = x,
                Content = LocalaizableCriteriaToStringProcessor.Process(new FunctionOperator(x, new CriteriaOperator[0]), null)
            });

        internal void CreateColumnsMenu(ContentControl button, ICommand command)
        {
            this.changeColumnCommand = command;
            base.ButtonMouseUp(button, null);
            this.changeColumnCommand = null;
        }

        private void FocusNextNavigationChild(UIElement child)
        {
            int index = base.NavigationChildren.IndexOf(child);
            base.Node.Owner.FocusNodeChild(base.Node, index + 1);
        }

        protected internal List<ClauseType> GetAvailableOperations() => 
            this.ClauseNode.Owner.GetListOperationsByTypes(this.ClauseNode.FirstOperand.PropertyName);

        private ControlTemplate GetSecondOperandsTemplate()
        {
            switch (this.SecondOperandsCount)
            {
                case OperandsCount.One:
                    return this.SecondOperandsOneTemplate;

                case OperandsCount.Two:
                    return this.SecondOperandsTwoTemplate;

                case OperandsCount.Several:
                    return this.SecondOperandsSeveralTemplate;

                case OperandsCount.OneLocalDateTime:
                    return this.SecondOperandsLocalDateTimeTemplate;
            }
            return null;
        }

        private bool IsDateTimeOperatorClause(ClauseType type)
        {
            switch (type)
            {
                case ClauseType.IsBeyondThisYear:
                case ClauseType.IsLaterThisYear:
                case ClauseType.IsLaterThisMonth:
                case ClauseType.IsNextWeek:
                case ClauseType.IsLaterThisWeek:
                case ClauseType.IsTomorrow:
                case ClauseType.IsToday:
                case ClauseType.IsYesterday:
                case ClauseType.IsEarlierThisWeek:
                case ClauseType.IsLastWeek:
                case ClauseType.IsEarlierThisMonth:
                case ClauseType.IsEarlierThisYear:
                case ClauseType.IsPriorThisYear:
                    return true;
            }
            return false;
        }

        protected internal void LocalDateTimeButtonClicked(object sender, MouseButtonEventArgs e)
        {
            IEnumerable<BarButtonItem> bars = this.CreateBars();
            this.RepopulateMenu(bars);
            this.ShowMenu(sender);
        }

        private void NavigationProcessAddKey(KeyEventArgs e, UIElement focusedChild)
        {
            int count = this.ClauseNode.Editors.Count;
            IList<UIElement> navigationChildren = base.NavigationChildren;
            int index = navigationChildren.IndexOf(focusedChild);
            if ((this.ClauseNode.SecondOperandsCount != OperandsCount.Several) || ((FilterControlKeyboardHelper.GetParentEditor(focusedChild) == null) && (index != ((navigationChildren.Count - count) - 1))))
            {
                this.NavigationParentNode.ProcessKeyDown(e, null);
            }
            else
            {
                this.AddOperandNodeCommand.Execute(null);
                e.Handled = true;
            }
        }

        private void NavigationProcessDeleteKey(KeyEventArgs e, UIElement focusedChild)
        {
            FilterControl owner = base.Node.Owner;
            int index = base.NavigationChildren.IndexOf(this.ClauseNode.Editors[0].Child);
            int num2 = FilterControlKeyboardHelper.GetParentEditor(focusedChild).Index;
            int count = this.ClauseNode.Editors.Count;
            UIElement element = (count != 1) ? ((num2 != (count - 1)) ? ((UIElement) this.ClauseNode.Editors[num2 + 1]) : ((UIElement) this.ClauseNode.Editors[count - 2])) : base.NavigationChildren[index - 1];
            this.ClauseNode.RemoveAdditionalOperandAt(num2);
            owner.FocusElement(element);
            e.Handled = true;
        }

        protected override void NavigationProcessKeyDown(KeyEventArgs e, UIElement focusedChild)
        {
            if (FilterControlKeyboardHelper.IsAddKey(e))
            {
                this.NavigationProcessAddKey(e, focusedChild);
            }
            else if (FilterControlKeyboardHelper.IsDeleteKey(e) && ((this.ClauseNode.SecondOperandsCount == OperandsCount.Several) && (FilterControlKeyboardHelper.GetParentEditor(focusedChild) != null)))
            {
                this.NavigationProcessDeleteKey(e, focusedChild);
            }
            else
            {
                base.NavigationProcessKeyDown(e, focusedChild);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            base.RemoveButtonMouseUpEventHandler(this.FirstOperandButton);
            this.FirstOperandButton = base.GetTemplateChild("PART_FirstOperand") as ContentControl;
            this.FirstOperandButton.AddMouseUpHandler(new MouseButtonEventHandler(this.ButtonMouseUp));
            base.RemoveButtonMouseUpEventHandler(this.OperationButton);
            this.OperationButton = base.GetTemplateChild("PART_Operation") as ContentControl;
            this.OperationButton.AddMouseUpHandler(new MouseButtonEventHandler(this.ButtonMouseUp));
            this.SecondOperandsControl = base.GetTemplateChild("SecondOperandsControl") as ContentControl;
            this.DeleteButton = base.GetTemplateChild("PART_Delete") as Button;
            this.DeleteButton.Command = this.RemoveNodeCommand;
            if (this.ClauseNode.IsLocalDateTimeFunction)
            {
                this.SecondOperandsControl.AddMouseUpHandler(new MouseButtonEventHandler(this.LocalDateTimeButtonClicked));
            }
            this.UpdateSecondOperandsTemplate();
        }

        public void RemoveNode()
        {
            this.ClauseNode.Owner.RemoveNode(this.ClauseNode);
        }

        private void RepopulateMenu(IEnumerable<BarButtonItem> bars)
        {
            this.LocalDateTimeMenu.Items.Clear();
            bars.ForEach<BarButtonItem>(x => this.LocalDateTimeMenu.Items.Add(x));
        }

        internal void SetClauseNodeType(ClauseType type)
        {
            this.ChangeOperationNode(type);
        }

        private void ShowMenu(object elem)
        {
            this.LocalDateTimeMenu.ShowPopup(elem as UIElement);
        }

        private void UpdateSecondOperandsTemplate()
        {
            if (this.SecondOperandsControl != null)
            {
                this.SecondOperandsControl.Template = this.GetSecondOperandsTemplate();
            }
        }

        public OperandsCount SecondOperandsCount
        {
            get => 
                (OperandsCount) base.GetValue(SecondOperandsCountProperty);
            set => 
                base.SetValue(SecondOperandsCountProperty, value);
        }

        public ControlTemplate SecondOperandsOneTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(SecondOperandsOneTemplateProperty);
            set => 
                base.SetValue(SecondOperandsOneTemplateProperty, value);
        }

        public ControlTemplate SecondOperandsTwoTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(SecondOperandsTwoTemplateProperty);
            set => 
                base.SetValue(SecondOperandsTwoTemplateProperty, value);
        }

        public ControlTemplate SecondOperandsSeveralTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(SecondOperandsSeveralTemplateProperty);
            set => 
                base.SetValue(SecondOperandsSeveralTemplateProperty, value);
        }

        public ControlTemplate SecondOperandsLocalDateTimeTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(SecondOperandsLocalDateTimeTemplateProperty);
            set => 
                base.SetValue(SecondOperandsLocalDateTimeTemplateProperty, value);
        }

        private ContentControl FirstOperandButton { get; set; }

        private ContentControl OperationButton { get; set; }

        private Button DeleteButton { get; set; }

        private ContentControl SecondOperandsControl { get; set; }

        public ICommand RemoveNodeCommand { get; private set; }

        public ICommand ChangeFirstOperandNodeCommand { get; private set; }

        public ICommand ChangeOperationNodeCommand { get; private set; }

        public ICommand AddOperandNodeCommand { get; private set; }

        public ICommand ChangeLocalDateTimeFunctionTypeCommand { get; private set; }

        internal DevExpress.Xpf.Editors.Filtering.ClauseNode ClauseNode =>
            base.Node as DevExpress.Xpf.Editors.Filtering.ClauseNode;

        protected internal PopupMenu LocalDateTimeMenu { get; set; }

        protected override IList<UIElement> NavigationChildrenCore
        {
            get
            {
                List<UIElement> list = new List<UIElement>();
                if (this.FirstOperandButton != null)
                {
                    list.Add(this.FirstOperandButton);
                }
                if (this.OperationButton != null)
                {
                    list.Add(this.OperationButton);
                }
                if (this.ClauseNode != null)
                {
                    foreach (FilterControlEditor editor in this.ClauseNode.Editors)
                    {
                        UIElement child = ((IFilterControlNavigationItem) editor).Child;
                        if (child != null)
                        {
                            list.Add(child);
                        }
                    }
                }
                return list;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterControlClauseNode.<>c <>9 = new FilterControlClauseNode.<>c();

            internal void <.cctor>b__20_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterControlClauseNode) d).UpdateSecondOperandsTemplate();
            }

            internal void <.cctor>b__20_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterControlClauseNode) d).UpdateSecondOperandsTemplate();
            }

            internal void <.cctor>b__20_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterControlClauseNode) d).UpdateSecondOperandsTemplate();
            }

            internal void <.cctor>b__20_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterControlClauseNode) d).UpdateSecondOperandsTemplate();
            }

            internal void <.cctor>b__20_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FilterControlClauseNode) d).UpdateSecondOperandsTemplate();
            }
        }
    }
}

