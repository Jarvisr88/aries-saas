namespace DMEWorks.Forms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    [DefaultEvent("ValueChanged")]
    public class NumericBox : ContainerControl
    {
        private static readonly object EVENT_VALUECHANGED = new object();
        private TextBox txtInternal;

        public event EventHandler ValueChanged
        {
            add
            {
                base.Events.AddHandler(EVENT_VALUECHANGED, value);
            }
            remove
            {
                base.Events.RemoveHandler(EVENT_VALUECHANGED, value);
            }
        }

        public NumericBox()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.txtInternal = new TextBox();
            this.txtInternal.AutoSize = false;
            this.txtInternal.Dock = DockStyle.Fill;
            this.txtInternal.Name = "txtInternal";
            this.txtInternal.Size = new Size(240, 20);
            this.txtInternal.TabIndex = 0;
            this.txtInternal.Text = "";
            this.txtInternal.TextChanged += new EventHandler(this.txtInternal_TextChanged);
            this.txtInternal.KeyPress += new KeyPressEventHandler(this.txtInternal_KeyPress);
            base.Controls.Add(this.txtInternal);
            base.Name = "NumericBox";
            base.Size = new Size(240, 20);
            base.ResumeLayout(false);
        }

        private static bool IsNumeric(string s) => 
            ParseDouble(s) != null;

        protected virtual void OnValueChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler) base.Events[EVENT_VALUECHANGED];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private static decimal? ParseDecimal(string s)
        {
            decimal num;
            if (decimal.TryParse(s, out num))
            {
                return new decimal?(num);
            }
            return null;
        }

        private static double? ParseDouble(string s)
        {
            double num;
            if (double.TryParse(s, out num))
            {
                return new double?(num);
            }
            return null;
        }

        private static int? ParseInt32(string s)
        {
            double? nullable = ParseDouble(s);
            if (nullable != null)
            {
                double num = Math.Round(nullable.Value);
                if (num < -2147483648.0)
                {
                    return null;
                }
                if (2147483647.0 >= num)
                {
                    return new int?((int) num);
                }
            }
            return null;
        }

        private static string ToString<T>(T? value) where T: struct => 
            (value != null) ? value.Value.ToString() : "";

        private void txtInternal_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if (keyChar != '\b')
            {
                switch (keyChar)
                {
                    case '-':
                    case '.':
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        try
                        {
                            string text = this.txtInternal.Text;
                            StringBuilder builder = new StringBuilder(text, text.Length + 1);
                            if (0 < this.txtInternal.SelectionLength)
                            {
                                builder.Remove(this.txtInternal.SelectionStart, this.txtInternal.SelectionLength);
                            }
                            builder.Insert(this.txtInternal.SelectionStart, e.KeyChar);
                            text = builder.ToString();
                            if (((text != "") && ((text != "-") && ((text != ".") && (text != "-.")))) && !IsNumeric(text))
                            {
                                e.Handled = true;
                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                        }
                        return;
                }
                e.Handled = true;
            }
        }

        private void txtInternal_TextChanged(object sender, EventArgs e)
        {
            this.OnValueChanged(e);
        }

        [DefaultValue(2)]
        public System.Windows.Forms.BorderStyle BorderStyle
        {
            get => 
                this.txtInternal.BorderStyle;
            set => 
                this.txtInternal.BorderStyle = value;
        }

        public override Color BackColor
        {
            get => 
                this.txtInternal.BackColor;
            set => 
                this.txtInternal.BackColor = value;
        }

        [DefaultValue(0)]
        public HorizontalAlignment TextAlign
        {
            get => 
                this.txtInternal.TextAlign;
            set => 
                this.txtInternal.TextAlign = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal? AsDecimal
        {
            get => 
                ParseDecimal(this.txtInternal.Text);
            set => 
                this.txtInternal.Text = ToString<decimal>(value);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double? AsDouble
        {
            get => 
                ParseDouble(this.txtInternal.Text);
            set => 
                this.txtInternal.Text = ToString<double>(value);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int? AsInt32
        {
            get => 
                ParseInt32(this.txtInternal.Text);
            set => 
                this.txtInternal.Text = ToString<int>(value);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get
            {
                string text = this.txtInternal.Text;
                return (IsNumeric(text) ? text : "");
            }
            set => 
                this.txtInternal.Text = IsNumeric(value) ? value : "";
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ReadOnly
        {
            get => 
                this.txtInternal.ReadOnly;
            set => 
                this.txtInternal.ReadOnly = value;
        }
    }
}

