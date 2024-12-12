namespace DMEWorks.Forms
{
    using DMEWorks.Forms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Expander : UserControl
    {
        private bool m_collapsed;
        private Control m_control;
        private IContainer components;
        private Label lblImage;
        private Label lblHeader;

        public Expander()
        {
            this.InitializeComponent();
            this.lblImage.Click += new EventHandler(this.Toggle);
            this.lblHeader.Click += new EventHandler(this.Toggle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblImage = new Label();
            this.lblHeader = new Label();
            base.SuspendLayout();
            this.lblImage.Dock = DockStyle.Left;
            this.lblImage.Image = Resources.ImageExpanded;
            this.lblImage.Location = new Point(0, 0);
            this.lblImage.Name = "lblImage";
            this.lblImage.Size = new Size(0x18, 0x18);
            this.lblImage.TabIndex = 0;
            this.lblImage.TextAlign = ContentAlignment.MiddleLeft;
            this.lblHeader.Dock = DockStyle.Fill;
            this.lblHeader.Location = new Point(0x18, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new Size(0x114, 0x18);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.TextAlign = ContentAlignment.MiddleLeft;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.lblHeader);
            base.Controls.Add(this.lblImage);
            base.Name = "Expander";
            base.Size = new Size(300, 0x18);
            base.ResumeLayout(false);
        }

        private void Toggle(object sender, EventArgs args)
        {
            this.Collapsed = !this.Collapsed;
        }

        [DefaultValue(false)]
        public bool Collapsed
        {
            get => 
                this.m_collapsed;
            set
            {
                if (this.m_collapsed != value)
                {
                    this.m_collapsed = value;
                    this.lblImage.Image = value ? Resources.ImageCollapsed : Resources.ImageExpanded;
                    if (this.m_control != null)
                    {
                        this.m_control.Visible = !this.m_collapsed;
                    }
                }
            }
        }

        [DefaultValue("")]
        public string Header
        {
            get => 
                this.lblHeader.Text;
            set => 
                this.lblHeader.Text = value;
        }

        [Category("Appearance"), Description("Content"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Control Content
        {
            get => 
                this.m_control;
            set
            {
                if (!ReferenceEquals(this.m_control, value))
                {
                    this.m_control = value;
                    if (this.m_control != null)
                    {
                        this.m_control.Visible = !this.m_collapsed;
                    }
                }
            }
        }
    }
}

