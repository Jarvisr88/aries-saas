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
public class Control_DME0404C : Control_CMNBase
{
    private IContainer components;

    public Control_DME0404C()
    {
        this.InitializeComponent();
    }

    public override void Clear()
    {
        Functions.SetRadioGroupValue(this.rgAnswer6, DBNull.Value);
        Functions.SetRadioGroupValue(this.rgAnswer7a, DBNull.Value);
        Functions.SetTextBoxText(this.txtAnswer7b, DBNull.Value);
        Functions.SetRadioGroupValue(this.rgAnswer8, DBNull.Value);
        Functions.SetRadioGroupValue(this.rgAnswer9a, DBNull.Value);
        Functions.SetTextBoxText(this.txtAnswer9b, DBNull.Value);
        Functions.SetRadioGroupValue(this.rgAnswer10a, DBNull.Value);
        Functions.SetTextBoxText(this.txtAnswer10b, DBNull.Value);
        Functions.SetTextBoxText(this.txtAnswer10c, DBNull.Value);
        Functions.SetRadioGroupValue(this.rgAnswer11, DBNull.Value);
        Functions.SetRadioGroupValue(this.rgAnswer12, DBNull.Value);
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
        ComponentResourceManager manager = new ComponentResourceManager(typeof(Control_DME0404C));
        this.rgAnswer12 = new RadioGroup();
        this.lblQuestions = new Label();
        this.lblAnswers = new Label();
        this.lblQuestion11 = new Label();
        this.lblQuestion10 = new Label();
        this.lblQuestion6 = new Label();
        this.pnlQuestions = new Panel();
        this.lblQuestion12 = new Label();
        this.lblQuestion9 = new Label();
        this.lblQuestion8 = new Label();
        this.lblQuestion7 = new Label();
        this.pnlAnswers = new Panel();
        this.rgAnswer11 = new RadioGroup();
        this.pnlAnswer10 = new Panel();
        this.lblLetter10c = new Label();
        this.txtAnswer10c = new TextBox();
        this.lblLetter10b = new Label();
        this.lblLetter10a = new Label();
        this.txtAnswer10b = new TextBox();
        this.rgAnswer10a = new RadioGroup();
        this.pnlAnswer9 = new Panel();
        this.lblLetter9b = new Label();
        this.lblLetter9a = new Label();
        this.txtAnswer9b = new TextBox();
        this.rgAnswer9a = new RadioGroup();
        this.rgAnswer8 = new RadioGroup();
        this.pnlQuestion7 = new Panel();
        this.lblLetter7b = new Label();
        this.lblLetter7a = new Label();
        this.txtAnswer7b = new TextBox();
        this.rgAnswer7a = new RadioGroup();
        this.pnlAnswer6 = new Panel();
        this.lblLetter6a = new Label();
        this.rgAnswer6 = new RadioGroup();
        this.pnlQuestions.SuspendLayout();
        this.pnlAnswers.SuspendLayout();
        this.pnlAnswer10.SuspendLayout();
        this.pnlAnswer9.SuspendLayout();
        this.pnlQuestion7.SuspendLayout();
        this.pnlAnswer6.SuspendLayout();
        base.SuspendLayout();
        this.rgAnswer12.BackColor = SystemColors.Control;
        this.rgAnswer12.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer12.Dock = DockStyle.Top;
        this.rgAnswer12.Items = new string[] { "Y", "N", "D" };
        this.rgAnswer12.Location = new Point(0, 0x130);
        this.rgAnswer12.Name = "rgAnswer12";
        this.rgAnswer12.Size = new Size(160, 20);
        this.rgAnswer12.TabIndex = 11;
        this.rgAnswer12.Value = "";
        this.lblQuestions.BackColor = Color.Transparent;
        this.lblQuestions.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestions.Dock = DockStyle.Top;
        this.lblQuestions.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestions.Location = new Point(0, 0);
        this.lblQuestions.Name = "lblQuestions";
        this.lblQuestions.Size = new Size(640, 0x48);
        this.lblQuestions.TabIndex = 0;
        this.lblQuestions.Text = manager.GetString("lblQuestions.Text");
        this.lblAnswers.BackColor = Color.Transparent;
        this.lblAnswers.BorderStyle = BorderStyle.FixedSingle;
        this.lblAnswers.Dock = DockStyle.Top;
        this.lblAnswers.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblAnswers.Location = new Point(0, 0);
        this.lblAnswers.Name = "lblAnswers";
        this.lblAnswers.Size = new Size(160, 0x48);
        this.lblAnswers.TabIndex = 0;
        this.lblAnswers.Text = "ANSWERS";
        this.lblAnswers.TextAlign = ContentAlignment.TopCenter;
        this.lblQuestion11.BackColor = Color.Transparent;
        this.lblQuestion11.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion11.Dock = DockStyle.Top;
        this.lblQuestion11.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion11.Location = new Point(0, 0x11c);
        this.lblQuestion11.Name = "lblQuestion11";
        this.lblQuestion11.Size = new Size(640, 20);
        this.lblQuestion11.TabIndex = 13;
        this.lblQuestion11.Text = "11. Is the device being ordered following multi-level spinal fusion surgery?";
        this.lblQuestion10.BackColor = Color.Transparent;
        this.lblQuestion10.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion10.Dock = DockStyle.Top;
        this.lblQuestion10.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion10.Location = new Point(0, 0xd6);
        this.lblQuestion10.Name = "lblQuestion10";
        this.lblQuestion10.Size = new Size(640, 70);
        this.lblQuestion10.TabIndex = 10;
        this.lblQuestion10.Text = manager.GetString("lblQuestion10.Text");
        this.lblQuestion6.BackColor = Color.Transparent;
        this.lblQuestion6.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion6.Dock = DockStyle.Top;
        this.lblQuestion6.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion6.Location = new Point(0, 0x48);
        this.lblQuestion6.Name = "lblQuestion6";
        this.lblQuestion6.Size = new Size(640, 0x1a);
        this.lblQuestion6.TabIndex = 8;
        this.lblQuestion6.Text = "6. In a fracture, has there been no clinically significant radiographic evidence of healing for a minimum of 90 days?";
        this.pnlQuestions.BackColor = Color.Transparent;
        this.pnlQuestions.Controls.Add(this.lblQuestion12);
        this.pnlQuestions.Controls.Add(this.lblQuestion11);
        this.pnlQuestions.Controls.Add(this.lblQuestion10);
        this.pnlQuestions.Controls.Add(this.lblQuestion9);
        this.pnlQuestions.Controls.Add(this.lblQuestion8);
        this.pnlQuestions.Controls.Add(this.lblQuestion7);
        this.pnlQuestions.Controls.Add(this.lblQuestion6);
        this.pnlQuestions.Controls.Add(this.lblQuestions);
        this.pnlQuestions.Dock = DockStyle.Fill;
        this.pnlQuestions.Location = new Point(160, 0);
        this.pnlQuestions.Name = "pnlQuestions";
        this.pnlQuestions.Size = new Size(640, 0x144);
        this.pnlQuestions.TabIndex = 3;
        this.lblQuestion12.BackColor = Color.Transparent;
        this.lblQuestion12.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion12.Dock = DockStyle.Top;
        this.lblQuestion12.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion12.Location = new Point(0, 0x130);
        this.lblQuestion12.Name = "lblQuestion12";
        this.lblQuestion12.Size = new Size(640, 20);
        this.lblQuestion12.TabIndex = 15;
        this.lblQuestion12.Text = "12. Has there been at least one open surgical intervention for treatment of the fracture?";
        this.lblQuestion9.BackColor = Color.Transparent;
        this.lblQuestion9.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion9.Dock = DockStyle.Top;
        this.lblQuestion9.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion9.Location = new Point(0, 0xa6);
        this.lblQuestion9.Name = "lblQuestion9";
        this.lblQuestion9.Size = new Size(640, 0x30);
        this.lblQuestion9.TabIndex = 6;
        this.lblQuestion9.Text = manager.GetString("lblQuestion9.Text");
        this.lblQuestion8.BackColor = Color.Transparent;
        this.lblQuestion8.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion8.Dock = DockStyle.Top;
        this.lblQuestion8.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion8.Location = new Point(0, 0x92);
        this.lblQuestion8.Name = "lblQuestion8";
        this.lblQuestion8.Size = new Size(640, 20);
        this.lblQuestion8.TabIndex = 5;
        this.lblQuestion8.Text = "8. Does the patient have a congenital pseudoarthrosis?";
        this.lblQuestion7.BackColor = Color.Transparent;
        this.lblQuestion7.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion7.Dock = DockStyle.Top;
        this.lblQuestion7.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion7.Location = new Point(0, 0x62);
        this.lblQuestion7.Name = "lblQuestion7";
        this.lblQuestion7.Size = new Size(640, 0x30);
        this.lblQuestion7.TabIndex = 3;
        this.lblQuestion7.Text = "7. (a) Does the patient have a failed fusion of a joint other than the spine?\r\n    (b) How many months prior to ordering the device did the patient have the fusion?";
        this.pnlAnswers.BackColor = Color.Transparent;
        this.pnlAnswers.Controls.Add(this.rgAnswer12);
        this.pnlAnswers.Controls.Add(this.rgAnswer11);
        this.pnlAnswers.Controls.Add(this.pnlAnswer10);
        this.pnlAnswers.Controls.Add(this.pnlAnswer9);
        this.pnlAnswers.Controls.Add(this.rgAnswer8);
        this.pnlAnswers.Controls.Add(this.pnlQuestion7);
        this.pnlAnswers.Controls.Add(this.pnlAnswer6);
        this.pnlAnswers.Controls.Add(this.lblAnswers);
        this.pnlAnswers.Dock = DockStyle.Left;
        this.pnlAnswers.Location = new Point(0, 0);
        this.pnlAnswers.Name = "pnlAnswers";
        this.pnlAnswers.Size = new Size(160, 0x144);
        this.pnlAnswers.TabIndex = 2;
        this.rgAnswer11.BackColor = SystemColors.Control;
        this.rgAnswer11.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer11.Dock = DockStyle.Top;
        this.rgAnswer11.Items = new string[] { "Y", "N", "D" };
        this.rgAnswer11.Location = new Point(0, 0x11c);
        this.rgAnswer11.Name = "rgAnswer11";
        this.rgAnswer11.Size = new Size(160, 20);
        this.rgAnswer11.TabIndex = 13;
        this.rgAnswer11.Value = "";
        this.pnlAnswer10.BorderStyle = BorderStyle.FixedSingle;
        this.pnlAnswer10.Controls.Add(this.lblLetter10c);
        this.pnlAnswer10.Controls.Add(this.txtAnswer10c);
        this.pnlAnswer10.Controls.Add(this.lblLetter10b);
        this.pnlAnswer10.Controls.Add(this.lblLetter10a);
        this.pnlAnswer10.Controls.Add(this.txtAnswer10b);
        this.pnlAnswer10.Controls.Add(this.rgAnswer10a);
        this.pnlAnswer10.Dock = DockStyle.Top;
        this.pnlAnswer10.Location = new Point(0, 0xd6);
        this.pnlAnswer10.Name = "pnlAnswer10";
        this.pnlAnswer10.Size = new Size(160, 70);
        this.pnlAnswer10.TabIndex = 20;
        this.lblLetter10c.Location = new Point(4, 0x2e);
        this.lblLetter10c.Name = "lblLetter10c";
        this.lblLetter10c.Size = new Size(20, 20);
        this.lblLetter10c.TabIndex = 8;
        this.lblLetter10c.Text = "c)";
        this.lblLetter10c.TextAlign = ContentAlignment.MiddleRight;
        this.txtAnswer10c.BorderStyle = BorderStyle.FixedSingle;
        this.txtAnswer10c.Location = new Point(0x1d, 0x2e);
        this.txtAnswer10c.Name = "txtAnswer10c";
        this.txtAnswer10c.Size = new Size(0x70, 20);
        this.txtAnswer10c.TabIndex = 7;
        this.lblLetter10b.Location = new Point(4, 0x18);
        this.lblLetter10b.Name = "lblLetter10b";
        this.lblLetter10b.Size = new Size(20, 20);
        this.lblLetter10b.TabIndex = 6;
        this.lblLetter10b.Text = "b)";
        this.lblLetter10b.TextAlign = ContentAlignment.MiddleRight;
        this.lblLetter10a.Location = new Point(4, 2);
        this.lblLetter10a.Name = "lblLetter10a";
        this.lblLetter10a.Size = new Size(20, 20);
        this.lblLetter10a.TabIndex = 5;
        this.lblLetter10a.Text = "a)";
        this.lblLetter10a.TextAlign = ContentAlignment.MiddleRight;
        this.txtAnswer10b.BorderStyle = BorderStyle.FixedSingle;
        this.txtAnswer10b.Location = new Point(0x1d, 0x18);
        this.txtAnswer10b.Name = "txtAnswer10b";
        this.txtAnswer10b.Size = new Size(0x70, 20);
        this.txtAnswer10b.TabIndex = 4;
        this.rgAnswer10a.BackColor = SystemColors.Control;
        this.rgAnswer10a.Items = new string[] { "Y", "N", "D" };
        this.rgAnswer10a.Location = new Point(0x1d, 2);
        this.rgAnswer10a.Name = "rgAnswer10a";
        this.rgAnswer10a.Size = new Size(0x70, 20);
        this.rgAnswer10a.TabIndex = 3;
        this.rgAnswer10a.Value = "";
        this.pnlAnswer9.BorderStyle = BorderStyle.FixedSingle;
        this.pnlAnswer9.Controls.Add(this.lblLetter9b);
        this.pnlAnswer9.Controls.Add(this.lblLetter9a);
        this.pnlAnswer9.Controls.Add(this.txtAnswer9b);
        this.pnlAnswer9.Controls.Add(this.rgAnswer9a);
        this.pnlAnswer9.Dock = DockStyle.Top;
        this.pnlAnswer9.Location = new Point(0, 0xa6);
        this.pnlAnswer9.Name = "pnlAnswer9";
        this.pnlAnswer9.Size = new Size(160, 0x30);
        this.pnlAnswer9.TabIndex = 0x13;
        this.lblLetter9b.Location = new Point(4, 0x18);
        this.lblLetter9b.Name = "lblLetter9b";
        this.lblLetter9b.Size = new Size(20, 20);
        this.lblLetter9b.TabIndex = 6;
        this.lblLetter9b.Text = "b)";
        this.lblLetter9b.TextAlign = ContentAlignment.MiddleRight;
        this.lblLetter9a.Location = new Point(4, 2);
        this.lblLetter9a.Name = "lblLetter9a";
        this.lblLetter9a.Size = new Size(20, 20);
        this.lblLetter9a.TabIndex = 5;
        this.lblLetter9a.Text = "a)";
        this.lblLetter9a.TextAlign = ContentAlignment.MiddleRight;
        this.txtAnswer9b.BorderStyle = BorderStyle.FixedSingle;
        this.txtAnswer9b.Location = new Point(0x1d, 0x18);
        this.txtAnswer9b.Name = "txtAnswer9b";
        this.txtAnswer9b.Size = new Size(0x70, 20);
        this.txtAnswer9b.TabIndex = 4;
        this.rgAnswer9a.BackColor = SystemColors.Control;
        this.rgAnswer9a.Items = new string[] { "Y", "N", "D" };
        this.rgAnswer9a.Location = new Point(0x1d, 2);
        this.rgAnswer9a.Name = "rgAnswer9a";
        this.rgAnswer9a.Size = new Size(0x70, 20);
        this.rgAnswer9a.TabIndex = 3;
        this.rgAnswer9a.Value = "";
        this.rgAnswer8.BackColor = SystemColors.Control;
        this.rgAnswer8.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer8.Dock = DockStyle.Top;
        this.rgAnswer8.Items = new string[] { "Y", "N", "D" };
        this.rgAnswer8.Location = new Point(0, 0x92);
        this.rgAnswer8.Name = "rgAnswer8";
        this.rgAnswer8.Size = new Size(160, 20);
        this.rgAnswer8.TabIndex = 0x15;
        this.rgAnswer8.Value = "";
        this.pnlQuestion7.BorderStyle = BorderStyle.FixedSingle;
        this.pnlQuestion7.Controls.Add(this.lblLetter7b);
        this.pnlQuestion7.Controls.Add(this.lblLetter7a);
        this.pnlQuestion7.Controls.Add(this.txtAnswer7b);
        this.pnlQuestion7.Controls.Add(this.rgAnswer7a);
        this.pnlQuestion7.Dock = DockStyle.Top;
        this.pnlQuestion7.Location = new Point(0, 0x62);
        this.pnlQuestion7.Name = "pnlQuestion7";
        this.pnlQuestion7.Size = new Size(160, 0x30);
        this.pnlQuestion7.TabIndex = 0x12;
        this.lblLetter7b.Location = new Point(4, 0x18);
        this.lblLetter7b.Name = "lblLetter7b";
        this.lblLetter7b.Size = new Size(20, 20);
        this.lblLetter7b.TabIndex = 6;
        this.lblLetter7b.Text = "b)";
        this.lblLetter7b.TextAlign = ContentAlignment.MiddleRight;
        this.lblLetter7a.Location = new Point(4, 2);
        this.lblLetter7a.Name = "lblLetter7a";
        this.lblLetter7a.Size = new Size(20, 20);
        this.lblLetter7a.TabIndex = 5;
        this.lblLetter7a.Text = "a)";
        this.lblLetter7a.TextAlign = ContentAlignment.MiddleRight;
        this.txtAnswer7b.BorderStyle = BorderStyle.FixedSingle;
        this.txtAnswer7b.Location = new Point(0x1d, 0x18);
        this.txtAnswer7b.Name = "txtAnswer7b";
        this.txtAnswer7b.Size = new Size(0x70, 20);
        this.txtAnswer7b.TabIndex = 4;
        this.rgAnswer7a.BackColor = SystemColors.Control;
        this.rgAnswer7a.Items = new string[] { "Y", "N", "D" };
        this.rgAnswer7a.Location = new Point(0x1d, 2);
        this.rgAnswer7a.Name = "rgAnswer7a";
        this.rgAnswer7a.Size = new Size(0x70, 20);
        this.rgAnswer7a.TabIndex = 3;
        this.rgAnswer7a.Value = "";
        this.pnlAnswer6.BorderStyle = BorderStyle.FixedSingle;
        this.pnlAnswer6.Controls.Add(this.lblLetter6a);
        this.pnlAnswer6.Controls.Add(this.rgAnswer6);
        this.pnlAnswer6.Dock = DockStyle.Top;
        this.pnlAnswer6.Location = new Point(0, 0x48);
        this.pnlAnswer6.Name = "pnlAnswer6";
        this.pnlAnswer6.Size = new Size(160, 0x1a);
        this.pnlAnswer6.TabIndex = 0x11;
        this.lblLetter6a.Location = new Point(4, 2);
        this.lblLetter6a.Name = "lblLetter6a";
        this.lblLetter6a.Size = new Size(20, 20);
        this.lblLetter6a.TabIndex = 5;
        this.lblLetter6a.Text = "a)";
        this.lblLetter6a.TextAlign = ContentAlignment.MiddleRight;
        this.rgAnswer6.BackColor = SystemColors.Control;
        this.rgAnswer6.Items = new string[] { "Y", "N", "D" };
        this.rgAnswer6.Location = new Point(0x1d, 2);
        this.rgAnswer6.Name = "rgAnswer6a";
        this.rgAnswer6.Size = new Size(0x70, 20);
        this.rgAnswer6.TabIndex = 3;
        this.rgAnswer6.Value = "";
        base.Controls.Add(this.pnlQuestions);
        base.Controls.Add(this.pnlAnswers);
        base.Name = "Control_DME0404C";
        base.Size = new Size(800, 0x144);
        this.pnlQuestions.ResumeLayout(false);
        this.pnlAnswers.ResumeLayout(false);
        this.pnlAnswer10.ResumeLayout(false);
        this.pnlAnswer10.PerformLayout();
        this.pnlAnswer9.ResumeLayout(false);
        this.pnlAnswer9.PerformLayout();
        this.pnlQuestion7.ResumeLayout(false);
        this.pnlQuestion7.PerformLayout();
        this.pnlAnswer6.ResumeLayout(false);
        base.ResumeLayout(false);
    }

    public override void LoadFromReader(MySqlDataReader reader)
    {
        Functions.SetRadioGroupValue(this.rgAnswer6, reader["Answer6"]);
        Functions.SetRadioGroupValue(this.rgAnswer7a, reader["Answer7a"]);
        Functions.SetTextBoxText(this.txtAnswer7b, reader["Answer7b"]);
        Functions.SetRadioGroupValue(this.rgAnswer8, reader["Answer8"]);
        Functions.SetRadioGroupValue(this.rgAnswer9a, reader["Answer9a"]);
        Functions.SetTextBoxText(this.txtAnswer9b, reader["Answer9b"]);
        Functions.SetRadioGroupValue(this.rgAnswer10a, reader["Answer10a"]);
        Functions.SetTextBoxText(this.txtAnswer10b, reader["Answer10b"]);
        Functions.SetTextBoxText(this.txtAnswer10c, reader["Answer10c"]);
        Functions.SetRadioGroupValue(this.rgAnswer11, reader["Answer11"]);
        Functions.SetRadioGroupValue(this.rgAnswer12, reader["Answer12"]);
    }

    private void rgAnswer10a_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void rgAnswer11_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void rgAnswer12_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void rgAnswer6_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void rgAnswer7a_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void rgAnswer8_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void rgAnswer9a_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    public override void SaveToCommand(MySqlCommand cmd)
    {
        cmd.Parameters.Add("Answer6", MySqlType.VarChar, 5).Value = this.rgAnswer6.Value;
        cmd.Parameters.Add("Answer7a", MySqlType.VarChar, 5).Value = this.rgAnswer7a.Value;
        cmd.Parameters.Add("Answer7b", MySqlType.VarChar, 5).Value = this.txtAnswer7b.Text;
        cmd.Parameters.Add("Answer8", MySqlType.VarChar, 5).Value = this.rgAnswer8.Value;
        cmd.Parameters.Add("Answer9a", MySqlType.VarChar, 5).Value = this.rgAnswer9a.Value;
        cmd.Parameters.Add("Answer9b", MySqlType.VarChar, 5).Value = this.txtAnswer9b.Text;
        cmd.Parameters.Add("Answer10a", MySqlType.VarChar, 5).Value = this.rgAnswer10a.Value;
        cmd.Parameters.Add("Answer10b", MySqlType.VarChar, 5).Value = this.txtAnswer10b.Text;
        cmd.Parameters.Add("Answer10c", MySqlType.VarChar, 5).Value = this.txtAnswer10c.Text;
        cmd.Parameters.Add("Answer11", MySqlType.VarChar, 5).Value = this.rgAnswer11.Value;
        cmd.Parameters.Add("Answer12", MySqlType.VarChar, 5).Value = this.rgAnswer12.Value;
    }

    private void txtAnswer10b_TextChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void txtAnswer10c_TextChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void txtAnswer7b_TextChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void txtAnswer9b_TextChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    [field: AccessedThroughProperty("lblAnswers")]
    private Label lblAnswers { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblLetter10a")]
    private Label lblLetter10a { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblLetter10b")]
    private Label lblLetter10b { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblLetter10c")]
    private Label lblLetter10c { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblLetter6a")]
    private Label lblLetter6a { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblLetter7a")]
    private Label lblLetter7a { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblLetter7b")]
    private Label lblLetter7b { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblLetter9a")]
    private Label lblLetter9a { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblLetter9b")]
    private Label lblLetter9b { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion10")]
    private Label lblQuestion10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion11")]
    private Label lblQuestion11 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion12")]
    private Label lblQuestion12 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion6")]
    private Label lblQuestion6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion7")]
    private Label lblQuestion7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion8")]
    private Label lblQuestion8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion9")]
    private Label lblQuestion9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestions")]
    private Label lblQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlAnswer10")]
    private Panel pnlAnswer10 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlAnswer6")]
    private Panel pnlAnswer6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlAnswer9")]
    private Panel pnlAnswer9 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlAnswers")]
    private Panel pnlAnswers { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlQuestion7")]
    private Panel pnlQuestion7 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlQuestions")]
    private Panel pnlQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer10a")]
    private RadioGroup rgAnswer10a { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer11")]
    private RadioGroup rgAnswer11 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer12")]
    private RadioGroup rgAnswer12 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer6")]
    private RadioGroup rgAnswer6 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer7a")]
    private RadioGroup rgAnswer7a { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer8")]
    private RadioGroup rgAnswer8 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer9a")]
    private RadioGroup rgAnswer9a { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("txtAnswer10b")]
    private TextBox txtAnswer10b { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("txtAnswer10c")]
    private TextBox txtAnswer10c { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("txtAnswer7b")]
    private TextBox txtAnswer7b { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("txtAnswer9b")]
    private TextBox txtAnswer9b { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    public override DmercType Type =>
        DmercType.DME_0404C;
}

