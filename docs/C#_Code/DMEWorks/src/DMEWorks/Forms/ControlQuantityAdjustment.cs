namespace DMEWorks.Forms
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class ControlQuantityAdjustment : ControlAdjustmentBase
    {
        private IContainer components;
        private int? F_OriginalValue;

        public ControlQuantityAdjustment()
        {
            this.InitializeComponent();
        }

        private void Changed()
        {
            StringBuilder builder = new StringBuilder();
            if (this.F_OriginalValue != null)
            {
                int num;
                builder.Append(this.F_OriginalValue.Value);
                if (int.TryParse(this.txtModified.Text, out num))
                {
                    builder.AppendFormat("{0: (+0); (-0);}", num - this.F_OriginalValue.Value);
                }
            }
            this.lblOriginal.Text = builder.ToString();
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
                    if ((e.KeyChar != '-') && ((e.KeyChar != '0') && ((e.KeyChar != '1') && ((e.KeyChar != '2') && ((e.KeyChar != '3') && ((e.KeyChar != '4') && ((e.KeyChar != '5') && ((e.KeyChar != '6') && ((e.KeyChar != '7') && ((e.KeyChar != '8') && (e.KeyChar != '9')))))))))))
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        int num;
                        string text = this.txtModified.Text;
                        if (this.txtModified.SelectionLength > 0)
                        {
                            text = text.Remove(this.txtModified.SelectionStart, this.txtModified.SelectionLength);
                        }
                        text = text.Insert(this.txtModified.SelectionStart, Conversions.ToString(e.KeyChar));
                        if (((text != "") && (text != "-")) && !int.TryParse(text, out num))
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

        private void txtModified_TextChanged(object sender, EventArgs e)
        {
            this.Changed();
        }

        [DefaultValue(null)]
        public int? OriginalValue
        {
            get => 
                this.F_OriginalValue;
            set
            {
                this.F_OriginalValue = value;
                this.Changed();
            }
        }

        [DefaultValue(null)]
        public int? ModifiedValue
        {
            get
            {
                int? nullable;
                int num;
                if (int.TryParse(this.txtModified.Text, out num))
                {
                    nullable = new int?(num);
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
                    this.txtModified.Text = value.Value.ToString();
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
                EventHandler handler = new EventHandler(this.txtModified_TextChanged);
                KeyPressEventHandler handler2 = new KeyPressEventHandler(this.txtModified_KeyPress);
                TextBox txtModified = base.txtModified;
                if (txtModified != null)
                {
                    txtModified.TextChanged -= handler;
                    txtModified.KeyPress -= handler2;
                }
                base.txtModified = value;
                txtModified = base.txtModified;
                if (txtModified != null)
                {
                    txtModified.TextChanged += handler;
                    txtModified.KeyPress += handler2;
                }
            }
        }
    }
}

