namespace ActiproSoftware.WinUICore
{
    using #H;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class StandardColorPickerPopup : PopupControl
    {
        private StandardColorPicker #WI;

        [Category("Action"), Description("Occurs after the value of the SelectedColor property changes.")]
        public event EventHandler SelectedColorChanged;

        private void #B5d()
        {
            this.#WI = new StandardColorPicker();
            this.SuspendLayout();
            this.#WI.Dock = DockStyle.Fill;
            this.#WI.Name = #G.#eg(0x54f);
            this.#WI.Size = new Size(200, 0xae);
            this.#WI.TabIndex = 0;
            this.#WI.SelectedColorChanged += new EventHandler(this.#vye);
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(200, 0xae);
            Control[] controls = new Control[] { this.#WI };
            base.Controls.AddRange(controls);
            base.Name = #G.#eg(0x560);
            this.Text = #G.#eg(0x560);
            base.ResumeLayout(false);
        }

        private void #vye(object #xhb, EventArgs #yhb)
        {
            this.OnSelectedColorChanged(#yhb);
            base.DialogResult = DialogResult.OK;
            base.Hide();
        }

        public StandardColorPickerPopup()
        {
            this.#B5d();
        }

        protected virtual void OnSelectedColorChanged(EventArgs e)
        {
            if (this.#1ue != null)
            {
                this.#1ue(this, e);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color SelectedColor =>
            this.#WI.SelectedColor;
    }
}

