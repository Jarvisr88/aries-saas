namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Filtering.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;

    public class ClauseNode : NodeBase, IClauseNode, INode, INotifyPropertyChanged
    {
        public static readonly DependencyProperty FirstOperandProperty;
        public static readonly DependencyProperty ColumnHeaderCaptionProperty;
        public static readonly DependencyProperty ColumnHeaderTemplateProperty;
        public static readonly DependencyProperty OperationProperty;
        private static readonly DependencyPropertyKey SecondOperandsCountPropertyKey;
        public static readonly DependencyProperty SecondOperandsCountProperty;
        public static readonly DependencyProperty ColumnHeaderTemplateSelectorProperty = DependencyProperty.Register("ColumnHeaderTemplateSelector", typeof(DataTemplateSelector), typeof(ClauseNode), new FrameworkPropertyMetadata(null));
        private ObservableCollection<CriteriaOperator> additionalOperands;
        private Locker secondOperandsResetLocker = new Locker();
        private string localDateTimeLable;

        public event PropertyChangedEventHandler PropertyChanged;

        static ClauseNode()
        {
            Type ownerType = typeof(ClauseNode);
            FirstOperandProperty = DependencyPropertyManager.Register("FirstOperand", typeof(OperandProperty), ownerType, new PropertyMetadata(null, (d, e) => ((ClauseNode) d).OnFirstOperandChange()));
            ColumnHeaderCaptionProperty = DependencyPropertyManager.Register("ColumnHeaderCaption", typeof(object), ownerType, new PropertyMetadata(null));
            ColumnHeaderTemplateProperty = DependencyPropertyManager.Register("ColumnHeaderTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null));
            OperationProperty = DependencyPropertyManager.Register("Operation", typeof(ClauseType), ownerType, new PropertyMetadata(ClauseType.Equals, (d, e) => ((ClauseNode) d).OnOperationChange()));
            SecondOperandsCountPropertyKey = DependencyPropertyManager.RegisterReadOnly("SecondOperandsCount", typeof(OperandsCount), ownerType, new PropertyMetadata(OperandsCount.One));
            SecondOperandsCountProperty = SecondOperandsCountPropertyKey.DependencyProperty;
        }

        public ClauseNode()
        {
            this.Editors = new List<FilterControlEditor>();
            this.additionalOperands = new ObservableCollection<CriteriaOperator>();
            this.additionalOperands.CollectionChanged += new NotifyCollectionChangedEventHandler(this.AdditionalOperandsCollectionChanged);
        }

        protected override object Accept(INodeVisitor visitor) => 
            visitor.Visit(this);

        public void AddAdditionalOperand(CriteriaOperator op)
        {
            this.AdditionalOperands.Add(op);
            this.UpdateLocalDateTimeLabel();
        }

        private void AdditionalOperandsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.ResetSecondOperandsCount();
        }

        private string GetLocalDateTimeLabel() => 
            (this.AdditionalOperands.Count > 0) ? LocalaizableCriteriaToStringProcessor.Process(this.AdditionalOperands[0], null) : string.Empty;

        protected void OnFirstOperandChange()
        {
            this.ResetColumnHeaderCaption();
        }

        protected void OnOperationChange()
        {
            if (base.Owner != null)
            {
                this.secondOperandsResetLocker.DoLockedAction(delegate {
                    this.SecondOperandsCount = OperandsCount.None;
                    base.Owner.ValidateAdditionalOperands(this);
                });
            }
            this.ResetSecondOperandsCount();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void RemoveAdditionalOperandAt(int index)
        {
            this.AdditionalOperands.RemoveAt(index);
            this.UpdateLocalDateTimeLabel();
        }

        public void RepopulateAdditionalOperands(IEnumerable<CriteriaOperator> newOps)
        {
            this.AdditionalOperands.Clear();
            newOps.ForEach<CriteriaOperator>(x => this.AddAdditionalOperand(x));
            this.UpdateLocalDateTimeLabel();
        }

        public void ResetAdditionalOperand(CriteriaOperator newOperand, int index)
        {
            if (!Equals(this.AdditionalOperands[index], newOperand))
            {
                this.AdditionalOperands[index] = newOperand;
                this.UpdateLocalDateTimeLabel();
            }
        }

        private void ResetColumnHeaderCaption()
        {
            if (base.Owner != null)
            {
                FilterColumn columnByFieldName = base.Owner.GetColumnByFieldName(this.FirstOperand.PropertyName);
                if (columnByFieldName != null)
                {
                    this.ColumnHeaderCaption = columnByFieldName.ColumnCaption;
                    this.ColumnHeaderTemplate = columnByFieldName.HeaderTemplate;
                    this.ColumnHeaderTemplateSelector = columnByFieldName.HeaderTemplateSelector;
                }
                else
                {
                    this.ColumnHeaderCaption = base.Owner.GetDefaultColumnCaption(this);
                    this.ColumnHeaderTemplate = null;
                    this.ColumnHeaderTemplateSelector = null;
                }
            }
        }

        private void ResetSecondOperandsCount()
        {
            if (!this.secondOperandsResetLocker)
            {
                this.SecondOperandsCount = FilterControlHelper.GetSecondOperandsCount(this.Operation, this.AdditionalOperands);
            }
        }

        public override void SetOwner(FilterControl owner, GroupNode parentNode)
        {
            base.SetOwner(owner, parentNode);
            this.ResetColumnHeaderCaption();
        }

        private void SetProperty<T>(ref T storage, T value, string propertyName)
        {
            storage = value;
            this.OnPropertyChanged(propertyName);
        }

        private void UpdateLocalDateTimeLabel()
        {
            if (this.IsLocalDateTimeFunction)
            {
                this.LocalDateTimeFunctionLabel = this.GetLocalDateTimeLabel();
            }
        }

        public OperandProperty FirstOperand
        {
            get => 
                (OperandProperty) base.GetValue(FirstOperandProperty);
            set => 
                base.SetValue(FirstOperandProperty, value);
        }

        public object ColumnHeaderCaption
        {
            get => 
                base.GetValue(ColumnHeaderCaptionProperty);
            set => 
                base.SetValue(ColumnHeaderCaptionProperty, value);
        }

        public DataTemplate ColumnHeaderTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ColumnHeaderTemplateProperty);
            set => 
                base.SetValue(ColumnHeaderTemplateProperty, value);
        }

        public DataTemplateSelector ColumnHeaderTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ColumnHeaderTemplateSelectorProperty);
            set => 
                base.SetValue(ColumnHeaderTemplateSelectorProperty, value);
        }

        public ClauseType Operation
        {
            get => 
                (ClauseType) base.GetValue(OperationProperty);
            set => 
                base.SetValue(OperationProperty, value);
        }

        public OperandsCount SecondOperandsCount
        {
            get => 
                (OperandsCount) base.GetValue(SecondOperandsCountProperty);
            private set => 
                base.SetValue(SecondOperandsCountPropertyKey, value);
        }

        public IList<CriteriaOperator> AdditionalOperands =>
            this.additionalOperands;

        internal List<FilterControlEditor> Editors { get; private set; }

        public FilterControlClauseNode VisualClauseNode =>
            (FilterControlClauseNode) base.VisualNode;

        public string LocalDateTimeFunctionLabel
        {
            get => 
                this.localDateTimeLable;
            private set => 
                this.SetProperty<string>(ref this.localDateTimeLable, value, BindableBase.GetPropertyName<string>(System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ClauseNode)), (MethodInfo) methodof(ClauseNode.get_LocalDateTimeFunctionLabel)), new ParameterExpression[0])));
        }

        protected internal bool IsLocalDateTimeFunction =>
            FilterControlHelper.IsLocalDateTimeFunction(this.AdditionalOperands);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ClauseNode.<>c <>9 = new ClauseNode.<>c();

            internal void <.cctor>b__6_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ClauseNode) d).OnFirstOperandChange();
            }

            internal void <.cctor>b__6_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ClauseNode) d).OnOperationChange();
            }
        }
    }
}

