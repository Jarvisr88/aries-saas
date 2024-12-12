namespace DMEWorks.Forms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Security.Permissions;
    using System.Windows.Forms;

    [DefaultEvent("ValueChanged")]
    public class RadioGroup : ContainerControl
    {
        private static readonly object EVENT_VALUECHANGED = new object();
        private int _spacing = 4;
        private System.Windows.Forms.BorderStyle _borderStyle;

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

        public RadioGroup()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            base.Name = "RadioGroup";
            base.Size = new Size(0x80, 0x18);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            this.UpdatePositions();
        }

        protected override void OnStyleChanged(EventArgs e)
        {
            base.OnStyleChanged(e);
            this.UpdatePositions();
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler) base.Events[EVENT_VALUECHANGED];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.OnValueChanged(EventArgs.Empty);
        }

        private void UpdatePositions()
        {
            System.Windows.Forms.BorderStyle borderStyle = this.BorderStyle;
            int y = (borderStyle == System.Windows.Forms.BorderStyle.FixedSingle) ? 1 : ((borderStyle != System.Windows.Forms.BorderStyle.Fixed3D) ? 0 : 2);
            int num2 = 0;
            for (int i = 0; i < base.Controls.Count; i++)
            {
                RadioButton button = base.Controls[i] as RadioButton;
                if (button != null)
                {
                    num2 += button.Width + this.Spacing;
                }
            }
            int x = ((base.Width - num2) + this.Spacing) / 2;
            for (int j = 0; j < base.Controls.Count; j++)
            {
                RadioButton button2 = base.Controls[j] as RadioButton;
                if (button2 != null)
                {
                    button2.Height = (base.Height - y) - y;
                    button2.Location = new Point(x, y);
                    x += button2.Width + this.Spacing;
                }
            }
        }

        public string[] Items
        {
            get
            {
                int num = 0;
                for (int i = 0; i < base.Controls.Count; i++)
                {
                    if (base.Controls[i] is RadioButton)
                    {
                        num++;
                    }
                }
                string[] strArray = new string[num];
                num = 0;
                for (int j = 0; j < base.Controls.Count; j++)
                {
                    RadioButton button = base.Controls[j] as RadioButton;
                    if (button != null)
                    {
                        strArray[num++] = button.Text;
                    }
                }
                return strArray;
            }
            set
            {
                base.SuspendLayout();
                try
                {
                    int num = base.Controls.Count - 1;
                    while (true)
                    {
                        if (0 > num)
                        {
                            if (value != null)
                            {
                                for (int i = 0; i < value.Length; i++)
                                {
                                    RadioButton button1 = new RadioButton();
                                    button1.BackColor = Color.Transparent;
                                    button1.Location = new Point(8, 0);
                                    button1.Name = "Button" + i.ToString();
                                    button1.Size = new Size(0x20, 0x18);
                                    button1.FlatStyle = FlatStyle.Flat;
                                    button1.TabIndex = i;
                                    button1.Text = value[i];
                                    button1.Parent = this;
                                    button1.CheckedChanged += new EventHandler(this.RadioButton_CheckedChanged);
                                }
                            }
                            break;
                        }
                        RadioButton button = base.Controls[num] as RadioButton;
                        if (button != null)
                        {
                            button.Parent = null;
                            button.Dispose();
                        }
                        num--;
                    }
                }
                finally
                {
                    base.ResumeLayout();
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Value
        {
            get
            {
                for (int i = 0; i < base.Controls.Count; i++)
                {
                    RadioButton button = base.Controls[i] as RadioButton;
                    if ((button != null) && button.Checked)
                    {
                        return button.Text;
                    }
                }
                return string.Empty;
            }
            set
            {
                for (int i = 0; i < base.Controls.Count; i++)
                {
                    RadioButton button = base.Controls[i] as RadioButton;
                    if (button != null)
                    {
                        button.Checked = string.Compare(value, button.Text, true) == 0;
                    }
                }
            }
        }

        [DefaultValue(4)]
        public int Spacing
        {
            get => 
                this._spacing;
            set
            {
                if (this._spacing != value)
                {
                    this._spacing = value;
                    this.UpdatePositions();
                }
            }
        }

        [Description("BorderStyle"), Category("Appearance"), DefaultValue(0), EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        public System.Windows.Forms.BorderStyle BorderStyle
        {
            get => 
                this._borderStyle;
            set
            {
                if (this._borderStyle != value)
                {
                    switch (value)
                    {
                        case System.Windows.Forms.BorderStyle.None:
                        case System.Windows.Forms.BorderStyle.FixedSingle:
                        case System.Windows.Forms.BorderStyle.Fixed3D:
                            this._borderStyle = value;
                            base.UpdateStyles();
                            break;

                        default:
                            throw new InvalidEnumArgumentException("value", (int) value, typeof(System.Windows.Forms.BorderStyle));
                    }
                }
            }
        }

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                System.Windows.Forms.CreateParams createParams = base.CreateParams;
                System.Windows.Forms.BorderStyle style = this._borderStyle;
                if (style == System.Windows.Forms.BorderStyle.FixedSingle)
                {
                    createParams.Style |= 0x800000;
                }
                else if (style == System.Windows.Forms.BorderStyle.Fixed3D)
                {
                    createParams.ExStyle |= 0x200;
                }
                return createParams;
            }
        }
    }
}

