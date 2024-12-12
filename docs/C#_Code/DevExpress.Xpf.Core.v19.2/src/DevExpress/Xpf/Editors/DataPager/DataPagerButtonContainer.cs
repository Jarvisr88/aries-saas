namespace DevExpress.Xpf.Editors.DataPager
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Controls;

    public class DataPagerButtonContainer : ContentControl
    {
        public event EventHandler NumericButtonContainerChanged;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            DataPagerNumericButtonContainer numericButtonContainer = this.NumericButtonContainer;
            this.NumericButtonContainer = (DataPagerNumericButtonContainer) base.GetTemplateChild("PART_NumButtonContainer");
            if (!ReferenceEquals(this.NumericButtonContainer, numericButtonContainer))
            {
                this.RaiseNumericButtonContainerChanged();
            }
        }

        protected void RaiseNumericButtonContainerChanged()
        {
            if (this.NumericButtonContainerChanged != null)
            {
                this.NumericButtonContainerChanged(this, EventArgs.Empty);
            }
        }

        public DataPagerNumericButtonContainer NumericButtonContainer { get; private set; }
    }
}

