namespace DMEWorks.Forms
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class ControlPriceAdjustment : ControlAdjustmentBase
    {
        private IContainer components;

        public ControlPriceAdjustment()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
        }

        private void txtModified_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != '\b')
                {
                    if ((e.KeyChar != '-') && ((e.KeyChar != '0') && ((e.KeyChar != '1') && ((e.KeyChar != '2') && ((e.KeyChar != '3') && ((e.KeyChar != '4') && ((e.KeyChar != '5') && ((e.KeyChar != '6') && ((e.KeyChar != '7') && ((e.KeyChar != '8') && ((e.KeyChar != '9') && (e.KeyChar != '.'))))))))))))
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        double num;
                        string text = this.txtModified.Text;
                        if (this.txtModified.SelectionLength > 0)
                        {
                            text = text.Remove(this.txtModified.SelectionStart, this.txtModified.SelectionLength);
                        }
                        text = text.Insert(this.txtModified.SelectionStart, Conversions.ToString(e.KeyChar));
                        if (((text != "") && (text != "-")) && !double.TryParse(text, out num))
                        {
                            e.Handled = true;
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        [DefaultValue(null)]
        public double? OriginalValue
        {
            get
            {
                double? nullable;
                double num;
                if (double.TryParse(this.lblOriginal.Text, out num))
                {
                    nullable = new double?(num);
                }
                else
                {
                    nullable = null;
                }
                return nullable;
            }
            set
            {
                if (value != null)
                {
                    this.lblOriginal.Text = value.Value.ToString("0.00", null);
                }
                else
                {
                    this.lblOriginal.Text = "";
                }
            }
        }

        [DefaultValue(null)]
        public double? ModifiedValue
        {
            get
            {
                double? nullable;
                double num;
                if (double.TryParse(this.txtModified.Text, out num))
                {
                    nullable = new double?(num);
                }
                else
                {
                    nullable = null;
                }
                return nullable;
            }
            set
            {
                if (value != null)
                {
                    this.txtModified.Text = value.Value.ToString("0.00", null);
                }
                else
                {
                    this.txtModified.Text = "";
                }
            }
        }

        protected override TextBox txtModified
        {
            get => 
                base.txtModified;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                KeyPressEventHandler handler = new KeyPressEventHandler(this.txtModified_KeyPress);
                TextBox txtModified = base.txtModified;
                if (txtModified != null)
                {
                    txtModified.KeyPress -= handler;
                }
                base.txtModified = value;
                txtModified = base.txtModified;
                if (txtModified != null)
                {
                    txtModified.KeyPress += handler;
                }
            }
        }
    }
}

