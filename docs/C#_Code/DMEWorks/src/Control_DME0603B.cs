using Devart.Data.MySql;
using DMEWorks;
using DMEWorks.CMN;
using DMEWorks.Controls;
using DMEWorks.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

[DesignerGenerated]
public class Control_DME0603B : Control_CMNBase
{
    private IContainer components;

    public Control_DME0603B()
    {
        this.InitializeComponent();
    }

    public override void Clear()
    {
        Functions.SetRadioGroupValue(this.rgAnswer1, DBNull.Value);
        Functions.SetNumericBoxValue(this.nmbAnswer2, DBNull.Value);
        Functions.SetRadioGroupValue(this.rgAnswer3, DBNull.Value);
        Functions.SetRadioGroupValue(this.rgAnswer4, DBNull.Value);
        Functions.SetRadioGroupValue(this.rgAnswer5, DBNull.Value);
        Functions.SetDateBoxValue(this.dtbAnswer6, DBNull.Value);
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

    private void dtbAnswer6_begun_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
        ComponentResourceManager manager = new ComponentResourceManager(typeof(Control_DME0603B));
        this.dtbAnswer6 = new UltraDateTimeEditor();
        this.pnlAnswer6 = new Panel();
        this.rgAnswer5 = new RadioGroup();
        this.rgAnswer4 = new RadioGroup();
        this.nmbAnswer2 = new NumericBox();
        this.Label10 = new Label();
        this.rgAnswer3 = new RadioGroup();
        this.pnlAnswer2 = new Panel();
        this.rgAnswer1 = new RadioGroup();
        this.lblAnswers = new Label();
        this.lblQuestion6 = new Label();
        this.lblQuestion5 = new Label();
        this.lblQuestion4 = new Label();
        this.pnlQuestions = new Panel();
        this.lblQuestion3 = new Label();
        this.lblQuestion2 = new Label();
        this.lblQuestion1 = new Label();
        this.lblQuestions = new Label();
        this.pnlAnswers = new Panel();
        this.pnlAnswer6.SuspendLayout();
        this.pnlAnswer2.SuspendLayout();
        this.pnlQuestions.SuspendLayout();
        this.pnlAnswers.SuspendLayout();
        base.SuspendLayout();
        this.dtbAnswer6.BorderStyle = UIElementBorderStyle.Solid;
        this.dtbAnswer6.Location = new Point(8, 1);
        this.dtbAnswer6.Margin = new Padding(0);
        this.dtbAnswer6.Name = "dtbAnswer6";
        this.dtbAnswer6.Size = new Size(0x68, 0x13);
        this.dtbAnswer6.TabIndex = 0;
        this.pnlAnswer6.BorderStyle = BorderStyle.FixedSingle;
        this.pnlAnswer6.Controls.Add(this.dtbAnswer6);
        this.pnlAnswer6.Dock = DockStyle.Top;
        this.pnlAnswer6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.pnlAnswer6.Location = new Point(0, 0x94);
        this.pnlAnswer6.Name = "pnlAnswer6";
        this.pnlAnswer6.Size = new Size(0xa8, 0x18);
        this.pnlAnswer6.TabIndex = 6;
        this.rgAnswer5.BackColor = SystemColors.Control;
        this.rgAnswer5.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer5.Dock = DockStyle.Top;
        this.rgAnswer5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.rgAnswer5.Items = new string[] { "Y", "N", "D" };
        this.rgAnswer5.Location = new Point(0, 0x80);
        this.rgAnswer5.Name = "rgAnswer5";
        this.rgAnswer5.Size = new Size(0xa8, 20);
        this.rgAnswer5.TabIndex = 5;
        this.rgAnswer5.Value = "";
        this.rgAnswer4.BackColor = SystemColors.Control;
        this.rgAnswer4.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer4.Dock = DockStyle.Top;
        this.rgAnswer4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.rgAnswer4.Items = new string[] { "Y", "N", "D" };
        this.rgAnswer4.Location = new Point(0, 0x6c);
        this.rgAnswer4.Name = "rgAnswer4";
        this.rgAnswer4.Size = new Size(0xa8, 20);
        this.rgAnswer4.TabIndex = 4;
        this.rgAnswer4.Value = "";
        this.nmbAnswer2.BorderStyle = BorderStyle.FixedSingle;
        this.nmbAnswer2.Location = new Point(8, 1);
        this.nmbAnswer2.Margin = new Padding(0);
        this.nmbAnswer2.Name = "nmbAnswer2";
        this.nmbAnswer2.Size = new Size(0x68, 20);
        this.nmbAnswer2.TabIndex = 0;
        this.nmbAnswer2.TextAlign = HorizontalAlignment.Left;
        this.Label10.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.Label10.Location = new Point(120, 2);
        this.Label10.Name = "Label10";
        this.Label10.Size = new Size(40, 20);
        this.Label10.TabIndex = 1;
        this.Label10.Text = "month";
        this.rgAnswer3.BackColor = SystemColors.Control;
        this.rgAnswer3.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer3.Spacing = 0;
        this.rgAnswer3.Dock = DockStyle.Top;
        this.rgAnswer3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.rgAnswer3.Items = new string[] { "1", "2", "3", "4", "5" };
        this.rgAnswer3.Location = new Point(0, 0x4c);
        this.rgAnswer3.Name = "rgAnswer3";
        this.rgAnswer3.Size = new Size(0xa8, 0x20);
        this.rgAnswer3.TabIndex = 3;
        this.rgAnswer3.Value = "";
        this.pnlAnswer2.BorderStyle = BorderStyle.FixedSingle;
        this.pnlAnswer2.Controls.Add(this.nmbAnswer2);
        this.pnlAnswer2.Controls.Add(this.Label10);
        this.pnlAnswer2.Dock = DockStyle.Top;
        this.pnlAnswer2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.pnlAnswer2.Location = new Point(0, 0x34);
        this.pnlAnswer2.Name = "pnlAnswer2";
        this.pnlAnswer2.Size = new Size(0xa8, 0x18);
        this.pnlAnswer2.TabIndex = 2;
        this.rgAnswer1.BackColor = SystemColors.Control;
        this.rgAnswer1.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer1.Dock = DockStyle.Top;
        this.rgAnswer1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.rgAnswer1.Items = new string[] { "Y", "N", "D" };
        this.rgAnswer1.Location = new Point(0, 0x20);
        this.rgAnswer1.Name = "rgAnswer1";
        this.rgAnswer1.Size = new Size(0xa8, 20);
        this.rgAnswer1.TabIndex = 1;
        this.rgAnswer1.Value = "";
        this.lblAnswers.BorderStyle = BorderStyle.FixedSingle;
        this.lblAnswers.Dock = DockStyle.Top;
        this.lblAnswers.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblAnswers.Location = new Point(0, 0);
        this.lblAnswers.Name = "lblAnswers";
        this.lblAnswers.Size = new Size(0xa8, 0x20);
        this.lblAnswers.TabIndex = 0;
        this.lblAnswers.Text = "ANSWERS";
        this.lblAnswers.TextAlign = ContentAlignment.TopCenter;
        this.lblQuestion6.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion6.Dock = DockStyle.Top;
        this.lblQuestion6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion6.Location = new Point(0, 0x94);
        this.lblQuestion6.Name = "lblQuestion6";
        this.lblQuestion6.Size = new Size(0x278, 0x18);
        this.lblQuestion6.TabIndex = 6;
        this.lblQuestion6.Text = "6. What is the date that you reevaluated the patient at the end of the trial period?";
        this.lblQuestion5.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion5.Dock = DockStyle.Top;
        this.lblQuestion5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion5.Location = new Point(0, 0x80);
        this.lblQuestion5.Name = "lblQuestion5";
        this.lblQuestion5.Size = new Size(0x278, 20);
        this.lblQuestion5.TabIndex = 5;
        this.lblQuestion5.Text = "5. Has the patient received a TENS trial of at least 30 days?";
        this.lblQuestion4.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion4.Dock = DockStyle.Top;
        this.lblQuestion4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion4.Location = new Point(0, 0x6c);
        this.lblQuestion4.Name = "lblQuestion4";
        this.lblQuestion4.Size = new Size(0x278, 20);
        this.lblQuestion4.TabIndex = 4;
        this.lblQuestion4.Text = "4. Is there documentation in the medical record of multiple medications and/or other therapies that have been tried and failed?";
        this.pnlQuestions.Controls.Add(this.lblQuestion6);
        this.pnlQuestions.Controls.Add(this.lblQuestion5);
        this.pnlQuestions.Controls.Add(this.lblQuestion4);
        this.pnlQuestions.Controls.Add(this.lblQuestion3);
        this.pnlQuestions.Controls.Add(this.lblQuestion2);
        this.pnlQuestions.Controls.Add(this.lblQuestion1);
        this.pnlQuestions.Controls.Add(this.lblQuestions);
        this.pnlQuestions.Dock = DockStyle.Fill;
        this.pnlQuestions.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.pnlQuestions.Location = new Point(0xa8, 0);
        this.pnlQuestions.Name = "pnlQuestions";
        this.pnlQuestions.Size = new Size(0x278, 0xac);
        this.pnlQuestions.TabIndex = 3;
        this.lblQuestion3.BackColor = SystemColors.Control;
        this.lblQuestion3.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion3.Dock = DockStyle.Top;
        this.lblQuestion3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion3.Location = new Point(0, 0x4c);
        this.lblQuestion3.Name = "lblQuestion3";
        this.lblQuestion3.Size = new Size(0x278, 0x20);
        this.lblQuestion3.TabIndex = 3;
        this.lblQuestion3.Text = manager.GetString("lblQuestion3.Text");
        this.lblQuestion2.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion2.Dock = DockStyle.Top;
        this.lblQuestion2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion2.Location = new Point(0, 0x34);
        this.lblQuestion2.Name = "lblQuestion2";
        this.lblQuestion2.Size = new Size(0x278, 0x18);
        this.lblQuestion2.TabIndex = 2;
        this.lblQuestion2.Text = "2. How long has the patient had intractable pain? (Enter number of months, 1 - 99.)";
        this.lblQuestion1.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion1.Dock = DockStyle.Top;
        this.lblQuestion1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion1.Location = new Point(0, 0x20);
        this.lblQuestion1.Name = "lblQuestion1";
        this.lblQuestion1.Size = new Size(0x278, 20);
        this.lblQuestion1.TabIndex = 1;
        this.lblQuestion1.Text = "1. Does the patient have chronic, intractable pain?\r\n";
        this.lblQuestions.BackColor = SystemColors.Control;
        this.lblQuestions.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestions.Dock = DockStyle.Top;
        this.lblQuestions.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestions.Location = new Point(0, 0);
        this.lblQuestions.Name = "lblQuestions";
        this.lblQuestions.Size = new Size(0x278, 0x20);
        this.lblQuestions.TabIndex = 0;
        this.lblQuestions.Text = "ANSWER QUESTIONS 1-6 for purchase of TENS\r\n(Circle Y for Yes, N for No,)";
        this.pnlAnswers.Controls.Add(this.pnlAnswer6);
        this.pnlAnswers.Controls.Add(this.rgAnswer5);
        this.pnlAnswers.Controls.Add(this.rgAnswer4);
        this.pnlAnswers.Controls.Add(this.rgAnswer3);
        this.pnlAnswers.Controls.Add(this.pnlAnswer2);
        this.pnlAnswers.Controls.Add(this.rgAnswer1);
        this.pnlAnswers.Controls.Add(this.lblAnswers);
        this.pnlAnswers.Dock = DockStyle.Left;
        this.pnlAnswers.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.pnlAnswers.Location = new Point(0, 0);
        this.pnlAnswers.Name = "pnlAnswers";
        this.pnlAnswers.Size = new Size(0xa8, 0xac);
        this.pnlAnswers.TabIndex = 2;
        base.Controls.Add(this.pnlQuestions);
        base.Controls.Add(this.pnlAnswers);
        base.Name = "Control_DME0603B";
        base.Size = new Size(800, 0xac);
        this.pnlAnswer6.ResumeLayout(false);
        this.pnlAnswer2.ResumeLayout(false);
        this.pnlQuestions.ResumeLayout(false);
        this.pnlQuestions.PerformLayout();
        this.pnlAnswers.ResumeLayout(false);
        base.ResumeLayout(false);
    }

    public override void LoadFromReader(MySqlDataReader reader)
    {
        Functions.SetRadioGroupValue(this.rgAnswer1, reader["Answer1"]);
        Functions.SetNumericBoxValue(this.nmbAnswer2, reader["Answer2"]);
        Functions.SetRadioGroupValue(this.rgAnswer3, reader["Answer3"]);
        Functions.SetRadioGroupValue(this.rgAnswer4, reader["Answer4"]);
        Functions.SetRadioGroupValue(this.rgAnswer5, reader["Answer5"]);
        Functions.SetDateBoxValue(this.dtbAnswer6, reader["Answer6"]);
    }

    private void nmbAnswer2_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void rgAnswer1_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void rgAnswer3_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void rgAnswer4_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void rgAnswer5_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    public override void SaveToCommand(MySqlCommand cmd)
    {
        cmd.Parameters.Add("Answer1", MySqlType.VarChar, 5).Value = this.rgAnswer1.Value;
        cmd.Parameters.Add("Answer2", MySqlType.Int).Value = this.nmbAnswer2.AsInt32.GetValueOrDefault(0);
        cmd.Parameters.Add("Answer3", MySqlType.VarChar, 5).Value = this.rgAnswer3.Value;
        cmd.Parameters.Add("Answer4", MySqlType.VarChar, 5).Value = this.rgAnswer4.Value;
        cmd.Parameters.Add("Answer5", MySqlType.VarChar, 5).Value = this.rgAnswer5.Value;
        cmd.Parameters.Add("Answer6", MySqlType.Date).Value = this.dtbAnswer6.Value;
    }

    [field: AccessedThroughProperty("dtbAnswer6")]
    private UltraDateTimeEditor dtbAnswer6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("Label10")]
    private Label Label10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblAnswers")]
    private Label lblAnswers { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion1")]
    private Label lblQuestion1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion2")]
    private Label lblQuestion2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion3")]
    private Label lblQuestion3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion4")]
    private Label lblQuestion4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion5")]
    private Label lblQuestion5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion6")]
    private Label lblQuestion6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestions")]
    private Label lblQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("nmbAnswer2")]
    private NumericBox nmbAnswer2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlAnswer2")]
    private Panel pnlAnswer2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlAnswer6")]
    private Panel pnlAnswer6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlAnswers")]
    private Panel pnlAnswers { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlQuestions")]
    private Panel pnlQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer1")]
    private RadioGroup rgAnswer1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer3")]
    private RadioGroup rgAnswer3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer4")]
    private RadioGroup rgAnswer4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer5")]
    private RadioGroup rgAnswer5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    public override DmercType Type =>
        DmercType.DME_0603B;
}

