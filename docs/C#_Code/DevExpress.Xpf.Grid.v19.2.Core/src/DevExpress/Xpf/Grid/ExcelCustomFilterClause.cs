namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors.Filtering;
    using DevExpress.Xpf.Editors.Filtering.Native;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Input;

    public class ExcelCustomFilterClause : IClauseNode, INode, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ExcelCustomFilterClause(ExcelColumnCustomFilterInfo info, ClauseType operation, OperandProperty firstOperand, IList<CriteriaOperator> additionalOperands = null)
        {
            this.CustomFilterInfo = info;
            this.Operation = operation;
            this.FirstOperand = firstOperand;
            if (additionalOperands != null)
            {
                this.CreateLocalDateTimeFunctions();
            }
            IList<CriteriaOperator> operands = additionalOperands;
            if (additionalOperands == null)
            {
                IList<CriteriaOperator> local1 = additionalOperands;
                operands = new List<CriteriaOperator>();
            }
            this.PopulateAdditionalOperands(operands);
            this.CreateCommands();
        }

        public object Accept(INodeVisitor visitor) => 
            visitor.Visit(this);

        private void AdditionalOperands_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdateFilterCriteria();
        }

        private void AddOperand()
        {
            this.AdditionalOperands.Add(new ExcelCustomFilterAdditionalOperand(this, this.AdditionalOperands.Count + 1));
        }

        public void Apply(ExcelCustomFilterClause otherClause)
        {
            if ((this.OperandCountMode == otherClause.OperandCountMode) && ((this.OperandCountMode != OperandsCount.None) && (this.OperandCountMode != OperandsCount.Several)))
            {
                for (int i = 0; i < this.AdditionalOperands.Count; i++)
                {
                    this.AdditionalOperands[i].Operand = otherClause.AdditionalOperands[i].Operand;
                }
            }
        }

        private void CreateCommands()
        {
            this.AddAdditionalOperandCommand = new DelegateCommand(new Action(this.AddOperand));
        }

        public void CreateLocalDateTimeFunctions()
        {
            Func<FunctionOperatorType, FunctionOperator> selector = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Func<FunctionOperatorType, FunctionOperator> local1 = <>c.<>9__10_0;
                selector = <>c.<>9__10_0 = f => new FunctionOperator(f, new CriteriaOperator[0]);
            }
            this.LocalDateTimeFunctions = FilterControlHelper.GetLocalDateTimeFuncs().Select<FunctionOperatorType, FunctionOperator>(selector);
        }

        private object GetFilterValue() => 
            !(this.AdditionalOperands[0].Operand is OperandValue) ? null : ((OperandValue) this.AdditionalOperands[0].Operand).Value;

        private IList<CriteriaOperator> GetValidAdditionalOperands()
        {
            if ((this.CustomFilterInfo.Column == null) || ((this.CustomFilterInfo.Column.View == null) || (this.CustomFilterInfo.Column.ColumnFilterMode == ColumnFilterMode.DisplayText)))
            {
                Func<ExcelCustomFilterAdditionalOperand, CriteriaOperator> selector = <>c.<>9__11_0;
                if (<>c.<>9__11_0 == null)
                {
                    Func<ExcelCustomFilterAdditionalOperand, CriteriaOperator> local1 = <>c.<>9__11_0;
                    selector = <>c.<>9__11_0 = operandWrapper => operandWrapper.Operand;
                }
                return this.AdditionalOperands.Select<ExcelCustomFilterAdditionalOperand, CriteriaOperator>(selector).ToList<CriteriaOperator>();
            }
            List<CriteriaOperator> list = new List<CriteriaOperator>();
            foreach (ExcelCustomFilterAdditionalOperand operand in this.AdditionalOperands)
            {
                OperandValue value2 = operand.Operand as OperandValue;
                if (value2 != null)
                {
                    list.Add(new OperandValue(this.CustomFilterInfo.Column.View.DataProviderBase.CorrectFilterValueType(this.CustomFilterInfo.Column.FieldType, value2.Value, null)));
                }
            }
            return list;
        }

        private bool IsValidAdditionalOperands()
        {
            bool flag;
            using (IEnumerator<CriteriaOperator> enumerator = ((IClauseNode) this).AdditionalOperands.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        CriteriaOperator current = enumerator.Current;
                        OperandValue value2 = current as OperandValue;
                        if (value2 == null)
                        {
                            continue;
                        }
                        if (value2.Value == null)
                        {
                            flag = false;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(value2.Value.ToString()))
                            {
                                continue;
                            }
                            flag = false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                    break;
                }
            }
            return flag;
        }

        private void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void PopulateAdditionalOperands(IList<CriteriaOperator> operands)
        {
            this.AdditionalOperands = new ObservableCollection<ExcelCustomFilterAdditionalOperand>();
            FilterControlHelpers.ValidateAdditionalOperands(this.Operation, operands);
            this.OperandCountMode = FilterControlHelper.GetSecondOperandsCount(this.Operation, operands);
            if (this.OperandCountMode == OperandsCount.None)
            {
                this.UpdateFilterCriteria();
            }
            else
            {
                operands.ForEach<CriteriaOperator>(operand => this.AdditionalOperands.Add(new ExcelCustomFilterAdditionalOperand(this, operand, this.AdditionalOperands.Count + 1)));
                this.AdditionalOperands.CollectionChanged += new NotifyCollectionChangedEventHandler(this.AdditionalOperands_CollectionChanged);
            }
        }

        public void RemoveOperandAt(int index)
        {
            this.AdditionalOperands.RemoveAt(index - 1);
            for (int i = 0; i < this.AdditionalOperands.Count; i++)
            {
                this.AdditionalOperands[i].Index = i + 1;
            }
        }

        public void SetParentNode(IGroupNode parentNode)
        {
            this.ParentNode = parentNode;
        }

        public void UpdateFilterCriteria()
        {
            if (!this.IsValidAdditionalOperands())
            {
                this.CustomFilterInfo.FilterCriteria = null;
            }
            else if (FilterClauseHelper.IsSupportAutoFilter(this.Operation))
            {
                this.CustomFilterInfo.FilterCriteria = this.CustomFilterInfo.Info.CreateCustomFilterCriteria(this.CustomFilterInfo.Info.Column.FieldName, this.GetFilterValue(), this.Operation);
            }
            else
            {
                this.CustomFilterInfo.FilterCriteria = FilterControlHelpers.ToCriteria(this);
            }
        }

        IList<CriteriaOperator> IClauseNode.AdditionalOperands =>
            this.GetValidAdditionalOperands();

        public ObservableCollection<ExcelCustomFilterAdditionalOperand> AdditionalOperands { get; set; }

        public OperandProperty FirstOperand { get; private set; }

        public ClauseType Operation { get; private set; }

        public ExcelColumnCustomFilterInfo CustomFilterInfo { get; private set; }

        public OperandsCount OperandCountMode { get; private set; }

        public IGroupNode ParentNode { get; set; }

        public ICommand AddAdditionalOperandCommand { get; set; }

        public IEnumerable<FunctionOperator> LocalDateTimeFunctions { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExcelCustomFilterClause.<>c <>9 = new ExcelCustomFilterClause.<>c();
            public static Func<FunctionOperatorType, FunctionOperator> <>9__10_0;
            public static Func<ExcelCustomFilterAdditionalOperand, CriteriaOperator> <>9__11_0;

            internal FunctionOperator <CreateLocalDateTimeFunctions>b__10_0(FunctionOperatorType f) => 
                new FunctionOperator(f, new CriteriaOperator[0]);

            internal CriteriaOperator <GetValidAdditionalOperands>b__11_0(ExcelCustomFilterAdditionalOperand operandWrapper) => 
                operandWrapper.Operand;
        }
    }
}

