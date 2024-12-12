namespace ActiproSoftware.WinUICore
{
    using #H;
    using ActiproSoftware.Products.Shared;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class StandardColorPicker : UserControl
    {
        private TabPage #Jve;
        private TabPage #Kve;
        private TabPage #Lve;
        private ColorPalettePicker #Mve;
        private ColorListBox #Nve;
        private ColorListBox #Ove;
        private TabControl #DCb;

        [Category("Action"), Description("Occurs after the value of the SelectedColor property changes.")]
        public event EventHandler SelectedColorChanged;

        private void #B5d()
        {
            this.#DCb = new TabControl();
            this.#Jve = new TabPage();
            this.#Mve = new ColorPalettePicker();
            this.#Kve = new TabPage();
            this.#Nve = new ColorListBox();
            this.#Lve = new TabPage();
            this.#Ove = new ColorListBox();
            this.#DCb.SuspendLayout();
            this.#Jve.SuspendLayout();
            this.#Kve.SuspendLayout();
            this.#Lve.SuspendLayout();
            base.SuspendLayout();
            Control[] controls = new Control[] { this.#Jve, this.#Kve, this.#Lve };
            this.#DCb.Controls.AddRange(controls);
            this.#DCb.Dock = DockStyle.Fill;
            this.#DCb.Name = #G.#eg(0x488);
            this.#DCb.SelectedIndex = 0;
            this.#DCb.Size = new Size(200, 0xae);
            this.#DCb.TabIndex = 0;
            this.#DCb.SelectedIndexChanged += new EventHandler(this.#uye);
            Control[] controlArray2 = new Control[] { this.#Mve };
            this.#Jve.Controls.AddRange(controlArray2);
            this.#Jve.Location = new Point(4, 0x16);
            this.#Jve.Name = #G.#eg(0x499);
            this.#Jve.Size = new Size(0xc0, 0x94);
            this.#Jve.TabIndex = 0;
            this.#Jve.Text = #G.#eg(0x4ae);
            this.#Mve.Dock = DockStyle.Fill;
            this.#Mve.Name = #G.#eg(0x4b7);
            this.#Mve.Size = new Size(0xc0, 0x94);
            this.#Mve.TabIndex = 11;
            this.#Mve.Click += new EventHandler(this.#sye);
            this.#Mve.KeyDown += new KeyEventHandler(this.#tye);
            Control[] controlArray3 = new Control[] { this.#Nve };
            this.#Kve.Controls.AddRange(controlArray3);
            this.#Kve.Location = new Point(4, 0x16);
            this.#Kve.Name = #G.#eg(0x4d0);
            this.#Kve.Size = new Size(0xc0, 0x94);
            this.#Kve.TabIndex = 1;
            this.#Kve.Text = #G.#eg(0x4e5);
            this.#Nve.ColorSet = ColorListBoxColorSet.SystemColors;
            this.#Nve.Dock = DockStyle.Fill;
            this.#Nve.DrawMode = DrawMode.OwnerDrawVariable;
            this.#Nve.ItemHeight = 15;
            this.#Nve.Name = #G.#eg(0x4ee);
            this.#Nve.Size = new Size(0xc0, 0x94);
            this.#Nve.TabIndex = 10;
            this.#Nve.KeyDown += new KeyEventHandler(this.#tye);
            this.#Nve.Click += new EventHandler(this.#sye);
            Control[] controlArray4 = new Control[] { this.#Ove };
            this.#Lve.Controls.AddRange(controlArray4);
            this.#Lve.Location = new Point(4, 0x16);
            this.#Lve.Name = #G.#eg(0x507);
            this.#Lve.Size = new Size(0xc0, 0x94);
            this.#Lve.TabIndex = 2;
            this.#Lve.Text = #G.#eg(0x518);
            this.#Ove.ColorSet = ColorListBoxColorSet.WebColors;
            this.#Ove.Dock = DockStyle.Fill;
            this.#Ove.DrawMode = DrawMode.OwnerDrawVariable;
            this.#Ove.ItemHeight = 15;
            this.#Ove.Name = #G.#eg(0x51d);
            this.#Ove.Size = new Size(0xc0, 0x94);
            this.#Ove.TabIndex = 9;
            this.#Ove.KeyDown += new KeyEventHandler(this.#tye);
            this.#Ove.Click += new EventHandler(this.#sye);
            Control[] controlArray5 = new Control[] { this.#DCb };
            base.Controls.AddRange(controlArray5);
            base.Name = #G.#eg(0x532);
            base.Size = new Size(200, 0xae);
            this.#DCb.ResumeLayout(false);
            this.#Jve.ResumeLayout(false);
            this.#Kve.ResumeLayout(false);
            this.#Lve.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void #sye(object #xhb, EventArgs #yhb)
        {
            this.OnSelectedColorChanged(EventArgs.Empty);
        }

        private void #tye(object #xhb, KeyEventArgs #yhb)
        {
            if (#yhb.KeyCode == Keys.Enter)
            {
                this.OnSelectedColorChanged(EventArgs.Empty);
            }
        }

        private void #uye(object #xhb, EventArgs #yhb)
        {
            this.#DCb.SelectedTab.Controls[0].Focus();
        }

        public StandardColorPicker()
        {
            this.#B5d();
            this.#Jve.Text = ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x3ed));
            this.#Kve.Text = ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x422));
            this.#Lve.Text = ActiproSoftware.Products.Shared.SR.GetString(#G.#eg(0x457));
        }

        protected virtual void OnSelectedColorChanged(EventArgs e)
        {
            if (this.#1ue != null)
            {
                this.#1ue(this, e);
            }
        }

        protected override Size DefaultSize =>
            new Size(200, 0xae);

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color SelectedColor =>
            (this.#DCb.SelectedTab != this.#Jve) ? (!ReferenceEquals(this.#DCb.SelectedTab, this.#Kve) ? this.#Ove.SelectedColor : this.#Nve.SelectedColor) : this.#Mve.SelectedColor;
    }
}

