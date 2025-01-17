﻿using Devart.Data.MySql;
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
public class Control_DME0703A : Control_CMNBase
{
    private IContainer components;

    public Control_DME0703A()
    {
        this.InitializeComponent();
    }

    public override void Clear()
    {
        Functions.SetRadioGroupValue(this.rgAnswer1, DBNull.Value);
        Functions.SetRadioGroupValue(this.rgAnswer2, DBNull.Value);
        Functions.SetRadioGroupValue(this.rgAnswer3, DBNull.Value);
        Functions.SetRadioGroupValue(this.rgAnswer4, DBNull.Value);
        Functions.SetRadioGroupValue(this.rgAnswer5, DBNull.Value);
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
        ComponentResourceManager manager = new ComponentResourceManager(typeof(Control_DME0703A));
        this.rgAnswer5 = new RadioGroup();
        this.rgAnswer4 = new RadioGroup();
        this.rgAnswer3 = new RadioGroup();
        this.lblQuestionDescription1 = new Label();
        this.rgAnswer2 = new RadioGroup();
        this.rgAnswer1 = new RadioGroup();
        this.lblAnswerDescription1 = new Label();
        this.lblQuestion4 = new Label();
        this.lblQuestion3 = new Label();
        this.lblQuestion2 = new Label();
        this.lblQuestion5 = new Label();
        this.pnlQuestions = new Panel();
        this.lblQuestion1 = new Label();
        this.pnlAnswers = new Panel();
        this.pnlQuestions.SuspendLayout();
        this.pnlAnswers.SuspendLayout();
        base.SuspendLayout();
        this.rgAnswer5.BackColor = SystemColors.Control;
        this.rgAnswer5.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer5.Dock = DockStyle.Top;
        this.rgAnswer5.Items = new string[] { "Y", "N", "D" };
        this.rgAnswer5.Location = new Point(0, 0x70);
        this.rgAnswer5.Name = "rgAnswer5";
        this.rgAnswer5.Size = new Size(0x70, 0x20);
        this.rgAnswer5.TabIndex = 5;
        this.rgAnswer5.Value = "";
        this.rgAnswer4.BackColor = SystemColors.Control;
        this.rgAnswer4.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer4.Dock = DockStyle.Top;
        this.rgAnswer4.Items = new string[] { "Y", "N", "D" };
        this.rgAnswer4.Location = new Point(0, 0x5c);
        this.rgAnswer4.Name = "rgAnswer4";
        this.rgAnswer4.Size = new Size(0x70, 20);
        this.rgAnswer4.TabIndex = 4;
        this.rgAnswer4.Value = "";
        this.rgAnswer3.BackColor = SystemColors.Control;
        this.rgAnswer3.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer3.Dock = DockStyle.Top;
        this.rgAnswer3.Items = new string[] { "Y", "N", "D" };
        this.rgAnswer3.Location = new Point(0, 0x48);
        this.rgAnswer3.Name = "rgAnswer3";
        this.rgAnswer3.Size = new Size(0x70, 20);
        this.rgAnswer3.TabIndex = 3;
        this.rgAnswer3.Value = "";
        this.lblQuestionDescription1.BackColor = Color.Transparent;
        this.lblQuestionDescription1.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestionDescription1.Dock = DockStyle.Top;
        this.lblQuestionDescription1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestionDescription1.Location = new Point(0, 0);
        this.lblQuestionDescription1.Name = "lblQuestionDescription1";
        this.lblQuestionDescription1.Size = new Size(0x2b0, 0x20);
        this.lblQuestionDescription1.TabIndex = 0;
        this.lblQuestionDescription1.Text = "ANSWER QUESTIONS 1-5 FOR SEAT LIFT MECHANISM\r\n(Circle Y for Yes, N for No, or D for Does Not Apply)";
        this.rgAnswer2.BackColor = SystemColors.Control;
        this.rgAnswer2.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer2.Dock = DockStyle.Top;
        this.rgAnswer2.Items = new string[] { "Y", "N", "D" };
        this.rgAnswer2.Location = new Point(0, 0x34);
        this.rgAnswer2.Name = "rgAnswer2";
        this.rgAnswer2.Size = new Size(0x70, 20);
        this.rgAnswer2.TabIndex = 2;
        this.rgAnswer2.Value = "";
        this.rgAnswer1.BackColor = SystemColors.Control;
        this.rgAnswer1.BorderStyle = BorderStyle.FixedSingle;
        this.rgAnswer1.Dock = DockStyle.Top;
        this.rgAnswer1.Items = new string[] { "Y", "N", "D" };
        this.rgAnswer1.Location = new Point(0, 0x20);
        this.rgAnswer1.Name = "rgAnswer1";
        this.rgAnswer1.Size = new Size(0x70, 20);
        this.rgAnswer1.TabIndex = 1;
        this.rgAnswer1.Value = "";
        this.lblAnswerDescription1.BackColor = Color.Transparent;
        this.lblAnswerDescription1.BorderStyle = BorderStyle.FixedSingle;
        this.lblAnswerDescription1.Dock = DockStyle.Top;
        this.lblAnswerDescription1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblAnswerDescription1.Location = new Point(0, 0);
        this.lblAnswerDescription1.Name = "lblAnswerDescription1";
        this.lblAnswerDescription1.Size = new Size(0x70, 0x20);
        this.lblAnswerDescription1.TabIndex = 0;
        this.lblAnswerDescription1.Text = "ANSWERS";
        this.lblAnswerDescription1.TextAlign = ContentAlignment.TopCenter;
        this.lblQuestion4.BackColor = Color.Transparent;
        this.lblQuestion4.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion4.Dock = DockStyle.Top;
        this.lblQuestion4.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion4.Location = new Point(0, 0x5c);
        this.lblQuestion4.Name = "lblQuestion4";
        this.lblQuestion4.Size = new Size(0x2b0, 20);
        this.lblQuestion4.TabIndex = 5;
        this.lblQuestion4.Text = "4. Once standing, does the patient have the ability to ambulate?";
        this.lblQuestion3.BackColor = Color.Transparent;
        this.lblQuestion3.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion3.Dock = DockStyle.Top;
        this.lblQuestion3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion3.Location = new Point(0, 0x48);
        this.lblQuestion3.Name = "lblQuestion3";
        this.lblQuestion3.Size = new Size(0x2b0, 20);
        this.lblQuestion3.TabIndex = 4;
        this.lblQuestion3.Text = "3. Is the patient completely incapable of standing up from a regular armchair or any chair in his/her home?";
        this.lblQuestion2.BackColor = Color.Transparent;
        this.lblQuestion2.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion2.Dock = DockStyle.Top;
        this.lblQuestion2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion2.Location = new Point(0, 0x34);
        this.lblQuestion2.Name = "lblQuestion2";
        this.lblQuestion2.Size = new Size(0x2b0, 20);
        this.lblQuestion2.TabIndex = 3;
        this.lblQuestion2.Text = "2. Does the patient have a severe neuromuscular disease?";
        this.lblQuestion5.BackColor = Color.Transparent;
        this.lblQuestion5.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion5.Dock = DockStyle.Top;
        this.lblQuestion5.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion5.Location = new Point(0, 0x70);
        this.lblQuestion5.Name = "lblQuestion5";
        this.lblQuestion5.Size = new Size(0x2b0, 0x20);
        this.lblQuestion5.TabIndex = 6;
        this.lblQuestion5.Text = manager.GetString("lblQuestion5.Text");
        this.pnlQuestions.BackColor = Color.Transparent;
        this.pnlQuestions.Controls.Add(this.lblQuestion5);
        this.pnlQuestions.Controls.Add(this.lblQuestion4);
        this.pnlQuestions.Controls.Add(this.lblQuestion3);
        this.pnlQuestions.Controls.Add(this.lblQuestion2);
        this.pnlQuestions.Controls.Add(this.lblQuestion1);
        this.pnlQuestions.Controls.Add(this.lblQuestionDescription1);
        this.pnlQuestions.Dock = DockStyle.Fill;
        this.pnlQuestions.Location = new Point(0x70, 0);
        this.pnlQuestions.Name = "pnlQuestions";
        this.pnlQuestions.Size = new Size(0x2b0, 0x90);
        this.pnlQuestions.TabIndex = 3;
        this.lblQuestion1.BackColor = Color.Transparent;
        this.lblQuestion1.BorderStyle = BorderStyle.FixedSingle;
        this.lblQuestion1.Dock = DockStyle.Top;
        this.lblQuestion1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
        this.lblQuestion1.Location = new Point(0, 0x20);
        this.lblQuestion1.Name = "lblQuestion1";
        this.lblQuestion1.Size = new Size(0x2b0, 20);
        this.lblQuestion1.TabIndex = 2;
        this.lblQuestion1.Text = "1. Does the patient have severe arthritis of the hip or knee?";
        this.pnlAnswers.BackColor = Color.Transparent;
        this.pnlAnswers.Controls.Add(this.rgAnswer5);
        this.pnlAnswers.Controls.Add(this.rgAnswer4);
        this.pnlAnswers.Controls.Add(this.rgAnswer3);
        this.pnlAnswers.Controls.Add(this.rgAnswer2);
        this.pnlAnswers.Controls.Add(this.rgAnswer1);
        this.pnlAnswers.Controls.Add(this.lblAnswerDescription1);
        this.pnlAnswers.Dock = DockStyle.Left;
        this.pnlAnswers.Location = new Point(0, 0);
        this.pnlAnswers.Name = "pnlAnswers";
        this.pnlAnswers.Size = new Size(0x70, 0x90);
        this.pnlAnswers.TabIndex = 2;
        base.Controls.Add(this.pnlQuestions);
        base.Controls.Add(this.pnlAnswers);
        base.Name = "Control_DME0703A";
        base.Size = new Size(800, 0x90);
        this.pnlQuestions.ResumeLayout(false);
        this.pnlAnswers.ResumeLayout(false);
        base.ResumeLayout(false);
    }

    public override void LoadFromReader(MySqlDataReader reader)
    {
        Functions.SetRadioGroupValue(this.rgAnswer1, reader["Answer1"]);
        Functions.SetRadioGroupValue(this.rgAnswer2, reader["Answer2"]);
        Functions.SetRadioGroupValue(this.rgAnswer3, reader["Answer3"]);
        Functions.SetRadioGroupValue(this.rgAnswer4, reader["Answer4"]);
        Functions.SetRadioGroupValue(this.rgAnswer5, reader["Answer5"]);
    }

    private void rgAnswer1_ValueChanged(object sender, EventArgs e)
    {
        base.OnChanged();
    }

    private void rgAnswer2_ValueChanged(object sender, EventArgs e)
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
        cmd.Parameters.Add("Answer2", MySqlType.VarChar, 5).Value = this.rgAnswer2.Value;
        cmd.Parameters.Add("Answer3", MySqlType.VarChar, 5).Value = this.rgAnswer3.Value;
        cmd.Parameters.Add("Answer4", MySqlType.VarChar, 5).Value = this.rgAnswer4.Value;
        cmd.Parameters.Add("Answer5", MySqlType.VarChar, 5).Value = this.rgAnswer5.Value;
    }

    [field: AccessedThroughProperty("rgAnswer5")]
    private RadioGroup rgAnswer5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer4")]
    private RadioGroup rgAnswer4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer3")]
    private RadioGroup rgAnswer3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestionDescription1")]
    private Label lblQuestionDescription1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer2")]
    private RadioGroup rgAnswer2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("rgAnswer1")]
    private RadioGroup rgAnswer1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblAnswerDescription1")]
    private Label lblAnswerDescription1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion4")]
    private Label lblQuestion4 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion3")]
    private Label lblQuestion3 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion2")]
    private Label lblQuestion2 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion5")]
    private Label lblQuestion5 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlQuestions")]
    private Panel pnlQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("lblQuestion1")]
    private Label lblQuestion1 { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    [field: AccessedThroughProperty("pnlAnswers")]
    private Panel pnlAnswers { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }

    public override DmercType Type =>
        DmercType.DME_0703A;
}

