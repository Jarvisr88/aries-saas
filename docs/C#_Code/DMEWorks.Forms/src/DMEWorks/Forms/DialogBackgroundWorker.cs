namespace DMEWorks.Forms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class DialogBackgroundWorker : DmeForm
    {
        private readonly object Argument;
        private IContainer components;
        private Button btnCancel;
        private BackgroundWorker backgroundWorker1;
        private Label lblProgress;
        private Label label2;
        private Label label3;
        private Label lblStatus;
        private ProgressBar progressBar1;

        public DialogBackgroundWorker(string caption, DoWorkEventHandler work) : this(caption, work, null)
        {
        }

        public DialogBackgroundWorker(string caption, DoWorkEventHandler work, object argument)
        {
            this.InitializeComponent();
            this.Text = caption;
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += work;
            this.Argument = argument;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
            this.lblProgress.Text = e.ProgressPercentage.ToString() + "%";
            this.lblStatus.Text = e.UserState as string;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.ShowException(e.Error);
            }
            base.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.CancelAsync();
            this.lblStatus.Text = "Cancel pending";
            this.btnCancel.Enabled = false;
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
            this.lblProgress = new Label();
            this.btnCancel = new Button();
            this.progressBar1 = new ProgressBar();
            this.backgroundWorker1 = new BackgroundWorker();
            this.label2 = new Label();
            this.label3 = new Label();
            this.lblStatus = new Label();
            base.SuspendLayout();
            this.lblProgress.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblProgress.Location = new Point(80, 40);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Padding = new Padding(2, 2, 2, 2);
            this.lblProgress.Size = new Size(0x120, 0x13);
            this.lblProgress.TabIndex = 3;
            this.lblProgress.TextAlign = ContentAlignment.MiddleLeft;
            this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnCancel.Location = new Point(0x138, 0x60);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x17);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.progressBar1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.progressBar1.Location = new Point(0x10, 0x48);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(0x160, 0x11);
            this.progressBar1.TabIndex = 4;
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.label2.Location = new Point(0x10, 40);
            this.label2.Name = "label2";
            this.label2.Padding = new Padding(2);
            this.label2.Size = new Size(0x38, 0x13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Progress:";
            this.label2.TextAlign = ContentAlignment.MiddleLeft;
            this.label3.Location = new Point(0x10, 0x10);
            this.label3.Name = "label3";
            this.label3.Padding = new Padding(2);
            this.label3.Size = new Size(0x38, 0x13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Status:";
            this.label3.TextAlign = ContentAlignment.MiddleLeft;
            this.lblStatus.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblStatus.Location = new Point(80, 0x10);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new Padding(2);
            this.lblStatus.Size = new Size(0x120, 0x13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x17e, 0x81);
            base.ControlBox = false;
            base.Controls.Add(this.lblStatus);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.lblProgress);
            base.Controls.Add(this.progressBar1);
            base.Margin = new Padding(2, 2, 2, 2);
            base.Name = "ProgressWithCancel";
            base.SizeGripStyle = SizeGripStyle.Hide;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Progress";
            base.Load += new EventHandler(this.Progress_Load);
            base.ResumeLayout(false);
        }

        private void Progress_Load(object sender, EventArgs e)
        {
            this.backgroundWorker1.RunWorkerAsync(this.Argument);
        }
    }
}

