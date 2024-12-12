using Devart.Data.MySql;
using DMEWorks;
using DMEWorks.CMN;
using DMEWorks.Controls;
using DMEWorks.Forms;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

[DesignerGenerated]
public class Control_DME0903 : Control_CMNBase
{
    private IContainer components;

    public Control_DME0903()
    {
        this.InitializeComponent();
    }

    public override void Clear()
    {
        Functions.SetRadioGroupValue(this.rgAnswer3, DBNull.Value);
        Functions.SetRadioGroupValue(this.rgAnswer4, DBNull.Value);
        Functions.SetTextBoxText(this.txtAnswer1a, DBNull.Value);
        Functions.SetTextBoxText(this.txtAnswer1b, DBNull.Value);
        Functions.SetTextBoxText(this.txtAnswer1c, DBNull.Value);
        Functions.SetTextBoxText(this.txtAnswer2a, DBNull.Value);
        Functions.SetTextBoxText(this.txtAnswer2b, DBNull.Value);
        Functions.SetTextBoxText(this.txtAnswer2c, DBNull.Value);
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
        this.rgAnswer3 = new RadioGroup();
        this.rgAnswer4 = new RadioGroup();
        this.lblAnswers = new Label();
        this.lblQuestion3 = new Label();
        this.pnlQuestions = new Panel();
        this.lblQuestion4 = new Label();
        this.lblQuestion2 = new Label();
        this.lblQuestion1 = new Label();
        this.lblQuestions = new TextBox();
        this.pnlAnswers = new Panel();
        this.pnlAnswer4 = new Panel();
        this.txtAnswer2c = new TextBox();
        this.lblLetter2c = new Label();
        this.txtAnswer2b = new TextBox();
        this.txtAnswer2a = new TextBox();
        this.lblLetter2b = new Label();
        this.lblLetter2a = new Label();
        this.pnlAnswer3 = new Panel();
        this.lblLetter1c = new Label();
        this.txtAnswer1c = new TextBox();
        this.lblLetter1b = new Label();
        this.lblLetter1a = new Label();
        this.txtAnswer1b = new TextBox();
        this.txtAnswer1a = new TextBox();
        this.pnlQuestions.SuspendLayout();
        this.pnlAnswers.SuspendLayout();
        this.pnlAnswer4.SuspendLayout();
        this.pnlAnswer3.SuspendLayout();
        base.SuspendLayout();
        this.rgAnswer3.BackColor = SystemColors.Control;
        this.rgAnswer3.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer3.Dock = DockStyle.Top;
        this.rgAnswer3.Items = new string[] { "1", "2", "3", "4" };
        this.rgAnswer3.Location = new Point(0, 160);
        this.rgAnswer3.Name = "rgAnswer3";
        this.rgAnswer3.Size = new Size(300, 0x20);
        this.rgAnswer3.TabIndex = 3;
        this.rgAnswer3.Value = "";
        this.rgAnswer4.BackColor = SystemColors.Control;
        this.rgAnswer4.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer4.Dock = DockStyle.Top;
        this.rgAnswer4.Items = new string[] { "1", "2" };
        this.rgAnswer4.Location = new Point(0, 0xc0);
        this.rgAnswer4.Name = "rgAnswer4";
        this.rgAnswer4.Size = new Size(300, 0x20);
        this.rgAnswer4.TabIndex = 4;
        this.rgAnswer4.Value = "";
        this.lblAnswers.BackColor = Color.Transparent;
        this.lblAnswers.BorderStyle = BorderStyle.FixedSingle;
        this.lblAnswers.Dock = DockStyle.Top;
        this.lblAnswers.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblAnswers.Location = new Point(0, 0);
        this.lblAnswers.Name = "lblAnswers";
        this.lblAnswers.Size = new Size(300, 20);
        this.lblAnswers.TabIndex = 0;
        this.lblAnswers.Text = "ANSWERS";
        this.lblAnswers.TextAlign = ContentAlignment.TopCenter;
        this.lblQuestion3.BackColor = Color.Transparent;
        this.lblQuestion3.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion3.Dock = DockStyle.Top;
        this.lblQuestion3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion3.Location = new Point(0, 160);
        this.lblQuestion3.Name = "lblQuestion3";
        this.lblQuestion3.Size = new Size(500, 0x20);
        this.lblQuestion3.TabIndex = 3;
        this.lblQuestion3.Text = "3. Circle number for route of administration?\r\n    1 — Intravenous    2 — Subcutaneous    3 — Epidural    4 — Other";
        this.pnlQuestions.BackColor = Color.Transparent;
        this.pnlQuestions.Controls.Add(this.lblQuestion4);
        this.pnlQuestions.Controls.Add(this.lblQuestion3);
        this.pnlQuestions.Controls.Add(this.lblQuestion2);
        this.pnlQuestions.Controls.Add(this.lblQuestion1);
        this.pnlQuestions.Controls.Add(this.lblQuestions);
        this.pnlQuestions.Dock = DockStyle.Fill;
        this.pnlQuestions.Location = new Point(300, 0);
        this.pnlQuestions.Name = "pnlQuestions";
        this.pnlQuestions.Size = new Size(500, 0xe0);
        this.pnlQuestions.TabIndex = 3;
        this.lblQuestion4.BackColor = Color.Transparent;
        this.lblQuestion4.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion4.Dock = DockStyle.Top;
        this.lblQuestion4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion4.Location = new Point(0, 0xc0);
        this.lblQuestion4.Name = "lblQuestion4";
        this.lblQuestion4.Size = new Size(500, 0x20);
        this.lblQuestion4.TabIndex = 4;
        this.lblQuestion4.Text = "4. Circle number for method of administration?\r\n    1 – Continuous    2 – Intermittent";
        this.lblQuestion2.BackColor = Color.Transparent;
        this.lblQuestion2.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion2.Dock = DockStyle.Top;
        this.lblQuestion2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion2.Location = new Point(0, 90);
        this.lblQuestion2.Name = "lblQuestion2";
        this.lblQuestion2.Size = new Size(500, 70);
        this.lblQuestion2.TabIndex = 2;
        this.lblQuestion2.Text = "2. If a NOC (not otherwise classified) HCPCS code is listed in question 1, print name of drug.";
        this.lblQuestion1.BackColor = Color.Transparent;
        this.lblQuestion1.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion1.Dock = DockStyle.Top;
        this.lblQuestion1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion1.Location = new Point(0, 20);
        this.lblQuestion1.Name = "lblQuestion1";
        this.lblQuestion1.Size = new Size(500, 70);
        this.lblQuestion1.TabIndex = 1;
        this.lblQuestion1.Text = "1. Provide the HCPCS code(s) for the drug(s) that requires the use of the pump.";
        this.lblQuestions.BackColor = SystemColors.Control;
        this.lblQuestions.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestions.Dock = DockStyle.Top;
        this.lblQuestions.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestions.Location = new Point(0, 0);
        this.lblQuestions.Multiline = true;
        this.lblQuestions.Name = "lblQuestions";
        this.lblQuestions.ReadOnly = true;
        this.lblQuestions.Size = new Size(500, 20);
        this.lblQuestions.TabIndex = 0;
        this.lblQuestions.TabStop = false;
        this.lblQuestions.Text = "ANSWER QUESTIONS 1 - 4 FOR EXTERNAL INFUSION PUMP.";
        this.pnlAnswers.BackColor = Color.Transparent;
        this.pnlAnswers.Controls.Add(this.rgAnswer4);
        this.pnlAnswers.Controls.Add(this.rgAnswer3);
        this.pnlAnswers.Controls.Add(this.pnlAnswer4);
        this.pnlAnswers.Controls.Add(this.pnlAnswer3);
        this.pnlAnswers.Controls.Add(this.lblAnswers);
        this.pnlAnswers.Dock = DockStyle.Left;
        this.pnlAnswers.Location = new Point(0, 0);
        this.pnlAnswers.Name = "pnlAnswers";
        this.pnlAnswers.Size = new Size(300, 0xe0);
        this.pnlAnswers.TabIndex = 2;
        this.pnlAnswer4.BorderStyle = BorderStyle.FixedSingle;
        this.pnlAnswer4.Controls.Add(this.txtAnswer2c);
        this.pnlAnswer4.Controls.Add(this.lblLetter2c);
        this.pnlAnswer4.Controls.Add(this.txtAnswer2b);
        this.pnlAnswer4.Controls.Add(this.txtAnswer2a);
        this.pnlAnswer4.Controls.Add(this.lblLetter2b);
        this.pnlAnswer4.Controls.Add(this.lblLetter2a);
        this.pnlAnswer4.Dock = DockStyle.Top;
        this.pnlAnswer4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.pnlAnswer4.Location = new Point(0, 90);
        this.pnlAnswer4.Margin = new Padding(0);
        this.pnlAnswer4.Name = "pnlAnswer4";
        this.pnlAnswer4.Size = new Size(300, 70);
        this.pnlAnswer4.TabIndex = 2;
        this.txtAnswer2c.BorderStyle = BorderStyle.FixedSingle;
        this.txtAnswer2c.Location = new Point(0x1c, 0x2e);
        this.txtAnswer2c.Name = "txtAnswer2c";
        this.txtAnswer2c.Size = new Size(0x10c, 20);
        this.txtAnswer2c.TabIndex = 5;
        this.txtAnswer2c.TextAlign = HorizontalAlignment.Left;
        this.lblLetter2c.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblLetter2c.Location = new Point(4, 0x2f);
        this.lblLetter2c.Name = "lblLetter2c";
        this.lblLetter2c.Size = new Size(20, 20);
        this.lblLetter2c.TabIndex = 4;
        this.lblLetter2c.Text = "c)";
        this.lblLetter2c.TextAlign = ContentAlignment.MiddleRight;
        this.txtAnswer2b.BorderStyle = BorderStyle.FixedSingle;
        this.txtAnswer2b.Location = new Point(0x1c, 0x18);
        this.txtAnswer2b.Name = "txtAnswer2b";
        this.txtAnswer2b.Size = new Size(0x10c, 20);
        this.txtAnswer2b.TabIndex = 3;
        this.txtAnswer2b.TextAlign = HorizontalAlignment.Left;
        this.txtAnswer2a.BorderStyle = BorderStyle.FixedSingle;
        this.txtAnswer2a.Location = new Point(0x1c, 2);
        this.txtAnswer2a.Name = "txtAnswer2a";
        this.txtAnswer2a.Size = new Size(0x10c, 20);
        this.txtAnswer2a.TabIndex = 1;
        this.txtAnswer2a.TextAlign = HorizontalAlignment.Left;
        this.lblLetter2b.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblLetter2b.Location = new Point(4, 0x18);
        this.lblLetter2b.Name = "lblLetter2b";
        this.lblLetter2b.Size = new Size(20, 20);
        this.lblLetter2b.TabIndex = 2;
        this.lblLetter2b.Text = "b)";
        this.lblLetter2b.TextAlign = ContentAlignment.MiddleRight;
        this.lblLetter2a.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblLetter2a.Location = new Point(4, 2);
        this.lblLetter2a.Name = "lblLetter2a";
        this.lblLetter2a.Size = new Size(20, 20);
        this.lblLetter2a.TabIndex = 0;
        this.lblLetter2a.Text = "a)";
        this.lblLetter2a.TextAlign = ContentAlignment.MiddleRight;
        this.pnlAnswer3.BorderStyle = BorderStyle.FixedSingle;
        this.pnlAnswer3.Controls.Add(this.lblLetter1c);
        this.pnlAnswer3.Controls.Add(this.txtAnswer1c);
        this.pnlAnswer3.Controls.Add(this.lblLetter1b);
        this.pnlAnswer3.Controls.Add(this.lblLetter1a);
        this.pnlAnswer3.Controls.Add(this.txtAnswer1b);
        this.pnlAnswer3.Controls.Add(this.txtAnswer1a);
        this.pnlAnswer3.Dock = DockStyle.Top;
        this.pnlAnswer3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.pnlAnswer3.Location = new Point(0, 20);
        this.pnlAnswer3.Margin = new Padding(0);
        this.pnlAnswer3.Name = "pnlAnswer3";
        this.pnlAnswer3.Size = new Size(300, 70);
        this.pnlAnswer3.TabIndex = 1;
        this.lblLetter1c.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblLetter1c.Location = new Point(4, 0x2f);
        this.lblLetter1c.Name = "lblLetter1c";
        this.lblLetter1c.Size = new Size(20, 20);
        this.lblLetter1c.TabIndex = 5;
        this.lblLetter1c.Text = "c)";
        this.lblLetter1c.TextAlign = ContentAlignment.MiddleRight;
        this.txtAnswer1c.BorderStyle = BorderStyle.FixedSingle;
        this.txtAnswer1c.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.txtAnswer1c.Location = new Point(0x1c, 0x2e);
        this.txtAnswer1c.Margin = new Padding(0);
        this.txtAnswer1c.Name = "txtAnswer1c";
        this.txtAnswer1c.Size = new Size(0x10c, 20);
        this.txtAnswer1c.TabIndex = 0;
        this.lblLetter1b.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblLetter1b.Location = new Point(4, 0x18);
        this.lblLetter1b.Name = "lblLetter1b";
        this.lblLetter1b.Size = new Size(20, 20);
        this.lblLetter1b.TabIndex = 3;
        this.lblLetter1b.Text = "b)";
        this.lblLetter1b.TextAlign = ContentAlignment.MiddleRight;
        this.lblLetter1a.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblLetter1a.Location = new Point(4, 2);
        this.lblLetter1a.Name = "lblLetter1a";
        this.lblLetter1a.Size = new Size(20, 20);
        this.lblLetter1a.TabIndex = 1;
        this.lblLetter1a.Text = "a)";
        this.lblLetter1a.TextAlign = ContentAlignment.MiddleRight;
        this.txtAnswer1b.BorderStyle = BorderStyle.FixedSingle;
        this.txtAnswer1b.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.txtAnswer1b.Location = new Point(0x1c, 0x18);
        this.txtAnswer1b.Margin = new Padding(0);
        this.txtAnswer1b.Name = "txtAnswer1b";
        this.txtAnswer1b.Size = new Size(0x10c, 20);
        this.txtAnswer1b.TabIndex = 4;
        this.txtAnswer1a.BorderStyle = BorderStyle.FixedSingle;
        this.txtAnswer1a.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.txtAnswer1a.Location = new Point(0x1c, 2);
        this.txtAnswer1a.Margin = new Padding(0);
        this.txtAnswer1a.Name = "txtAnswer1a";
        this.txtAnswer1a.Size = new Size(0x10c, 20);
        this.txtAnswer1a.TabIndex = 2;
        base.Controls.Add(this.pnlQuestions);
        base.Controls.Add(this.pnlAnswers);
        base.Name = "Control_DME0903";
        base.Size = new Size(800, 0xe0);
        this.pnlQuestions.ResumeLayout(false);
        this.pnlQuestions.PerformLayout();
        this.pnlAnswers.ResumeLayout(false);
        this.pnlAnswer4.ResumeLayout(false);
        this.pnlAnswer3.ResumeLayout(false);
        this.pnlAnswer3.PerformLayout();
        base.ResumeLayout(false);
    }

    public override void LoadFromReader(MySqlDataReader reader)
    {
        Functions.SetRadioGroupValue(this.rgAnswer3, reader["Answer3"]);
        Functions.SetRadioGroupValue(this.rgAnswer4, reader["Answer4"]);
        Functions.SetTextBoxText(this.txtAnswer1a, reader["Answer1a"]);
        Functions.SetTextBoxText(this.txtAnswer1b, reader["Answer1b"]);
        Functions.SetTextBoxText(this.txtAnswer1c, reader["Answer1c"]);
        Functions.SetTextBoxText(this.txtAnswer2a, reader["Answer2a"]);
        Functions.SetTextBoxText(this.txtAnswer2b, reader["Answer2b"]);
        Functions.SetTextBoxText(this.txtAnswer2c, reader["Answer2c"]);
    }

    private void rgAnswer3_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void rgAnswer4_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    public override void SaveToCommand(MySqlCommand cmd)
    {
        cmd.Parameters.Add("Answer3", MySqlType.VarChar, 5).Value = this.rgAnswer3.Value;
        cmd.Parameters.Add("Answer4", MySqlType.VarChar, 5).Value = this.rgAnswer4.Value;
        cmd.Parameters.Add("Answer1a", MySqlType.VarChar, 10).Value = this.txtAnswer1a.Text;
        cmd.Parameters.Add("Answer1b", MySqlType.VarChar, 10).Value = this.txtAnswer1b.Text;
        cmd.Parameters.Add("Answer1c", MySqlType.VarChar, 10).Value = this.txtAnswer1c.Text;
        cmd.Parameters.Add("Answer2a", MySqlType.VarChar, 50).Value = this.txtAnswer2a.Text;
        cmd.Parameters.Add("Answer2b", MySqlType.VarChar, 50).Value = this.txtAnswer2b.Text;
        cmd.Parameters.Add("Answer2c", MySqlType.VarChar, 50).Value = this.txtAnswer2c.Text;
    }

    private void txtAnswer1a_TextChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void txtAnswer1b_TextChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void txtAnswer1c_TextChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void txtAnswer2a_TextChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void txtAnswer2b_TextChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void txtAnswer2c_TextChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    [field: AccessedThroughProperty("lblAnswers")]
    private Label lblAnswers { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblLetter1a")]
    private Label lblLetter1a { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblLetter1b")]
    private Label lblLetter1b { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblLetter1c")]
    private Label lblLetter1c { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblLetter2a")]
    private Label lblLetter2a { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblLetter2b")]
    private Label lblLetter2b { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblLetter2c")]
    private Label lblLetter2c { get; [MethodImpl(MethodImplOptions.Synchronized)]
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

    [field: AccessedThroughProperty("lblQuestions")]
    private TextBox lblQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlAnswer3")]
    private Panel pnlAnswer3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlAnswer4")]
    private Panel pnlAnswer4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlAnswers")]
    private Panel pnlAnswers { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlQuestions")]
    private Panel pnlQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer3")]
    private RadioGroup rgAnswer3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer4")]
    private RadioGroup rgAnswer4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("txtAnswer1a")]
    private TextBox txtAnswer1a { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("txtAnswer1b")]
    private TextBox txtAnswer1b { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("txtAnswer1c")]
    private TextBox txtAnswer1c { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("txtAnswer2a")]
    private TextBox txtAnswer2a { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("txtAnswer2b")]
    private TextBox txtAnswer2b { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("txtAnswer2c")]
    private TextBox txtAnswer2c { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    public override DmercType Type =>
        DmercType.DME_0903;
}

