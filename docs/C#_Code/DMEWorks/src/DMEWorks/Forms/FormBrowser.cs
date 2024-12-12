namespace DMEWorks.Forms
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class FormBrowser : DmeForm
    {
        private IContainer components;

        public FormBrowser(Uri url, string text = null)
        {
            this.InitializeComponent();
            if (url != null)
            {
                this.wbBrowser.Navigate(url);
            }
            else if (!string.IsNullOrWhiteSpace(text))
            {
                this.wbBrowser.DocumentText = text;
            }
        }

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (this.components != null))
                {
                    this.components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.wbBrowser = new WebBrowser();
            base.SuspendLayout();
            this.wbBrowser.AllowWebBrowserDrop = false;
            this.wbBrowser.Dock = DockStyle.Fill;
            this.wbBrowser.Location = new Point(0, 0);
            this.wbBrowser.MinimumSize = new Size(20, 20);
            this.wbBrowser.Name = "wbBrowser";
            this.wbBrowser.Size = new Size(0x270, 0x1b9);
            this.wbBrowser.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x270, 0x1b9);
            base.Controls.Add(this.wbBrowser);
            base.Name = "Browser";
            this.Text = "Browser";
            base.ResumeLayout(false);
        }

        private void wbBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.Text = "Browser " + this.wbBrowser.DocumentTitle;
        }

        private void wbBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if ("about:close".Equals(e.Url.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                e.Cancel = true;
                base.Close();
            }
        }

        [field: AccessedThroughProperty("wbBrowser")]
        internal virtual WebBrowser wbBrowser { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }
    }
}

