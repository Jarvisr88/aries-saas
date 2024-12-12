namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Input;

    public class ExcelCustomFilterAdditionalOperand : INotifyPropertyChanged
    {
        private readonly ExcelCustomFilterClause Root;
        private CriteriaOperator _Operand;
        private int _Index;

        public event PropertyChangedEventHandler PropertyChanged;

        public ExcelCustomFilterAdditionalOperand(ExcelCustomFilterClause root, int index) : this(root, new OperandValue(), index)
        {
        }

        public ExcelCustomFilterAdditionalOperand(ExcelCustomFilterClause root, CriteriaOperator operand, int index)
        {
            this.Operand = operand;
            this.Root = root;
            this.Index = index;
            this.RemoveCommand = new DelegateCommand(new Action(this.Remove));
        }

        private void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void Remove()
        {
            this.Root.RemoveOperandAt(this.Index);
        }

        public CriteriaOperator Operand
        {
            get => 
                this._Operand;
            set
            {
                this._Operand = value;
                this.OnPropertyChanged("Operand");
                if (this.Root != null)
                {
                    this.Root.UpdateFilterCriteria();
                }
            }
        }

        public int Index
        {
            get => 
                this._Index;
            set
            {
                this._Index = value;
                this.OnPropertyChanged("Index");
            }
        }

        public ICommand RemoveCommand { get; private set; }
    }
}

