namespace DevExpress.Xpf.Grid
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ExcelDateColumnFilterValue : ExcelColumnFilterValue
    {
        public event EventHandler ItemsCheckedChanged;

        public ExcelDateColumnFilterValue(object editValue, ExcelDateColumnFilterScope innerScope) : base(editValue)
        {
            this.Items = new ExcelDateColumnFilterList(innerScope);
            this.Items.AddSelectAllItem(string.Empty, false, false);
            this.Items.ServiceItem_SelectAll.PropertyChanged += new PropertyChangedEventHandler(this.ServiceItem_SelectAll_PropertyChanged);
            this.Items.IsCheckedChanged += new EventHandler(this.Items_IsCheckedChanged);
        }

        private void Items_IsCheckedChanged(object sender, EventArgs e)
        {
            if (this.ItemsCheckedChanged != null)
            {
                this.ItemsCheckedChanged(this, EventArgs.Empty);
            }
        }

        protected override void OnCheckedChanged(bool? oldValue, bool? newValue)
        {
            this.Items.LockCheckedChanged = true;
            if (newValue != null)
            {
                this.Items.ChangeSelectAll(newValue.Value);
            }
            this.Items.LockCheckedChanged = false;
        }

        private void ServiceItem_SelectAll_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                base.IsChecked = this.Items.ServiceItem_SelectAll.IsChecked;
            }
        }

        public ExcelDateColumnFilterList Items { get; private set; }
    }
}

