namespace DMEWorks.Forms
{
    using DMEWorks.Forms.Properties;
    using System;
    using System.Windows.Forms;

    public class DmeForm : Form
    {
        public DmeForm()
        {
            base.Icon = Resources.IconDme;
        }

        protected virtual void InitDropdowns()
        {
        }

        private void InvokeInitDropdownsRecursively(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                this.InvokeInitDropdownsRecursively(control.Controls[i]);
            }
            DmeForm form = control as DmeForm;
            if (form != null)
            {
                this.SafeInvoke(new Action(form.InitDropdowns));
            }
            else
            {
                DmeUserControl control2 = control as DmeUserControl;
                if (control2 != null)
                {
                    this.SafeInvoke(new Action(control2.InitDropdowns));
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.InvokeInitDropdownsRecursively(this);
        }
    }
}

