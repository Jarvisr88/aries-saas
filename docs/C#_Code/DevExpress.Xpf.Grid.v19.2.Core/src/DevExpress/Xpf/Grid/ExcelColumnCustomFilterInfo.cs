namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Filtering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ExcelColumnCustomFilterInfo : INotifyPropertyChanged
    {
        private readonly Locker UpdateOperatorLocker = new Locker();
        private readonly GridFilterColumn FilterColumn;
        private readonly OperandProperty FirstOperand;
        private List<ExcelColumnFilterClauseType> _Operators;
        private ExcelCustomFilterClause _Clause;
        private ClauseType? _CurrentOperator;
        private CriteriaOperator _FilterCriteria;

        public event EventHandler FilterCriteriaChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        public ExcelColumnCustomFilterInfo(ExcelColumnFilterInfoBase info, object[] values, CriteriaOperator criteria)
        {
            this.Info = info;
            this.Column = this.Info.Column;
            this.Values = values;
            this.FilterColumn = this.Info.CreateFilterColumn();
            this.FirstOperand = new OperandProperty(this.FilterColumn.FieldName);
            Func<ClauseType?, ExcelColumnFilterClauseType> selector = <>c.<>9__36_0;
            if (<>c.<>9__36_0 == null)
            {
                Func<ClauseType?, ExcelColumnFilterClauseType> local1 = <>c.<>9__36_0;
                selector = <>c.<>9__36_0 = clause => new ExcelColumnFilterClauseType(clause);
            }
            this.Operators = info.CreateRulesOperators().Cast<ClauseType?>().Select<ClauseType?, ExcelColumnFilterClauseType>(selector).ToList<ExcelColumnFilterClauseType>();
            if (criteria == null)
            {
                this.CreateDefaultOperator();
            }
            else
            {
                this.LoadCurrentOperatorFromCriteria(criteria);
            }
        }

        public object[] CopyValues()
        {
            if (this.Values == null)
            {
                return null;
            }
            object[] array = new object[this.Values.Length];
            this.Values.CopyTo(array, 0);
            return array;
        }

        private void CreateDefaultOperator()
        {
            ClauseType? defaultOperator = (this.Info.FilterSettings != null) ? this.Info.FilterSettings.DefaultRulesFilterType : this.Info.GetRulesDefaultFilterType(this.FilterColumn);
            if ((defaultOperator != null) && (!this.IsUnaryOperand(defaultOperator.Value) && (this.Operators.Where<ExcelColumnFilterClauseType>(delegate (ExcelColumnFilterClauseType op) {
                ClauseType? @operator = op.Operator;
                ClauseType? nullable2 = defaultOperator;
                return ((@operator.GetValueOrDefault() == nullable2.GetValueOrDefault()) ? ((@operator != null) == (nullable2 != null)) : false);
            }).FirstOrDefault<ExcelColumnFilterClauseType>() != null)))
            {
                this.CurrentOperator = new ClauseType?(defaultOperator.Value);
            }
            else
            {
                this.CurrentOperator = null;
            }
        }

        private bool IsUnaryOperand(ClauseType operand)
        {
            List<CriteriaOperator> additionalOperands = new List<CriteriaOperator>();
            FilterControlHelpers.ValidateAdditionalOperands(operand, additionalOperands);
            return (additionalOperands.Count == 0);
        }

        private void LoadCurrentOperatorFromCriteria(CriteriaOperator criteria)
        {
            ClauseNode node = CriteriaToTreeProcessor.GetTree(new FilterControlNodesFactory(), criteria, null, false) as ClauseNode;
            if (node == null)
            {
                this.CreateDefaultOperator();
            }
            else
            {
                ClauseType loadedOperator = node.Operation;
                if ((loadedOperator == ClauseType.AnyOf) || (loadedOperator == ClauseType.NoneOf))
                {
                    this.CreateDefaultOperator();
                }
                else
                {
                    this.UpdateOperatorLocker.DoLockedAction(() => this.CurrentOperator = new ClauseType?(loadedOperator));
                    this.FilterCriteria = criteria;
                    if (this.CurrentOperator == null)
                    {
                        this.Clause = null;
                    }
                    else
                    {
                        this.Clause = new ExcelCustomFilterClause(this, this.CurrentOperator.Value, this.FirstOperand, node.AdditionalOperands);
                    }
                }
            }
        }

        private void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void Reset()
        {
            this.UpdateOperatorLocker.DoLockedAction(() => this.CreateDefaultOperator());
        }

        private void UpdateOperatorInfo()
        {
            this.FilterCriteria = null;
            if (this.CurrentOperator == null)
            {
                this.Clause = null;
            }
            else
            {
                ExcelCustomFilterClause newClause = new ExcelCustomFilterClause(this, this.CurrentOperator.Value, this.FirstOperand, null);
                if (this.Clause != null)
                {
                    this.UpdateOperatorLocker.DoIfNotLocked(() => newClause.Apply(this.Clause));
                }
                this.Clause = newClause;
            }
        }

        public ColumnBase Column { get; private set; }

        public ExcelColumnFilterInfoBase Info { get; private set; }

        public List<ExcelColumnFilterClauseType> Operators
        {
            get => 
                this._Operators;
            set
            {
                this._Operators = value;
                this.OnPropertyChanged("Operators");
            }
        }

        public ExcelCustomFilterClause Clause
        {
            get => 
                this._Clause;
            set
            {
                this._Clause = value;
                this.OnPropertyChanged("Clause");
            }
        }

        public ClauseType? CurrentOperator
        {
            get => 
                this._CurrentOperator;
            set
            {
                this._CurrentOperator = value;
                this.UpdateOperatorInfo();
                this.OnPropertyChanged("CurrentOperator");
            }
        }

        public CriteriaOperator FilterCriteria
        {
            get => 
                this._FilterCriteria;
            set
            {
                this._FilterCriteria = value;
                this.OnPropertyChanged("FilterCriteria");
                this.UpdateOperatorLocker.DoIfNotLocked(delegate {
                    if (this.FilterCriteriaChanged != null)
                    {
                        this.FilterCriteriaChanged(this, EventArgs.Empty);
                    }
                });
            }
        }

        public object[] Values { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExcelColumnCustomFilterInfo.<>c <>9 = new ExcelColumnCustomFilterInfo.<>c();
            public static Func<ClauseType?, ExcelColumnFilterClauseType> <>9__36_0;

            internal ExcelColumnFilterClauseType <.ctor>b__36_0(ClauseType? clause) => 
                new ExcelColumnFilterClauseType(clause);
        }
    }
}

