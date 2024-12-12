namespace DMEWorks.Controls
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Core;
    using DMEWorks.Forms;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    [DesignerGenerated]
    public class ControlCmnID : UserControl
    {
        private IContainer components;
        private DmercType _DefaultCmnType = ((DmercType) 0);
        private int? _CustomerID = null;
        private int? _OrderID = null;
        private int? _CmnID = null;
        private bool _AllowEdit = true;
        private static readonly object KEY_BROWSE = new object();
        private static readonly object KEY_CHANGE = new object();

        public ControlCmnID()
        {
            this.InitializeComponent();
            this.Link.Links.Clear();
            this.Link.Text = "CMN/RX";
            if (this._AllowEdit && (this._CustomerID != null))
            {
                this.Link.Links.Add(0, -1, KEY_CHANGE);
            }
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
            this.Link = new LinkLabel();
            base.SuspendLayout();
            this.Link.Dock = DockStyle.Fill;
            this.Link.Location = new Point(0, 0);
            this.Link.Margin = new Padding(0);
            this.Link.Name = "Link";
            this.Link.Size = new Size(200, 0x15);
            this.Link.TabIndex = 0;
            this.Link.TabStop = true;
            this.Link.Text = "CMN/RX";
            this.Link.TextAlign = ContentAlignment.MiddleCenter;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.Link);
            base.Margin = new Padding(0);
            base.Name = "ControlCmnID";
            base.Size = new Size(200, 0x15);
            base.ResumeLayout(false);
        }

        private void Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (e.Link.LinkData == KEY_BROWSE)
                {
                    FormParameters @params = new FormParameters("ID", this._CmnID.Value);
                    ClassGlobalObjects.ShowForm(FormFactories.FormCMNRX(), @params);
                }
                else if ((e.Link.LinkData == KEY_CHANGE) && (this._CustomerID != null))
                {
                    using (FormCmnList2 list = new FormCmnList2())
                    {
                        list.DefaultCmnType = this._DefaultCmnType;
                        list.CustomerID = this._CustomerID.Value;
                        list.OrderID = this._OrderID;
                        list.CmnID = this._CmnID;
                        if (list.ShowDialog() == DialogResult.OK)
                        {
                            this._CmnID = list.CmnID;
                            this.RefreshLink();
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this.ShowException(exception);
                ProjectData.ClearProjectError();
            }
        }

        private void RefreshLink()
        {
            string str;
            this.Link.Links.Clear();
            if (this._CmnID == null)
            {
                this._CmnID = null;
                str = null;
            }
            else
            {
                using (MySqlConnection connection = new MySqlConnection(ClassGlobalObjects.ConnectionString_MySql))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("", connection))
                    {
                        command.CommandText = (this._CustomerID == null) ? $"SELECT ID as CmnID, CMNType as CmnType, CustomerID
FROM tbl_cmnform
WHERE (ID = {this._CmnID.Value})" : $"SELECT ID as CmnID, CMNType as CmnType, CustomerID
FROM tbl_cmnform
WHERE (ID = {this._CmnID.Value})
  AND (CustomerID = {this._CustomerID.Value})";
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                this._CustomerID = null;
                                this._CmnID = null;
                                str = null;
                                goto TR_0011;
                            }
                            else
                            {
                                try
                                {
                                    this._CustomerID = new int?(Convert.ToInt32(reader["CustomerID"]));
                                }
                                catch (InvalidCastException exception1)
                                {
                                    InvalidCastException ex = exception1;
                                    ProjectData.SetProjectError(ex);
                                    InvalidCastException exception = ex;
                                    this._CustomerID = null;
                                    ProjectData.ClearProjectError();
                                }
                            }
                            try
                            {
                                this._CmnID = new int?(Convert.ToInt32(reader["CmnID"]));
                            }
                            catch (InvalidCastException exception3)
                            {
                                InvalidCastException ex = exception3;
                                ProjectData.SetProjectError(ex);
                                InvalidCastException exception2 = ex;
                                this._CmnID = null;
                                ProjectData.ClearProjectError();
                            }
                            str = reader["CmnType"] as string;
                        }
                    }
                }
            }
        TR_0011:
            if (this._CmnID == null)
            {
                this.Link.Text = "CMN/RX";
                if (this._AllowEdit && (this._CustomerID != null))
                {
                    this.Link.Links.Add(0, -1, KEY_CHANGE);
                }
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("CMN/RX : {0} # ", str);
                int length = builder.Length;
                builder.Append(this._CmnID);
                this.Link.Links.Add(length, builder.Length - length, KEY_BROWSE);
                if (this._AllowEdit && (this._CustomerID != null))
                {
                    builder.Append("  ");
                    length = builder.Length;
                    builder.Append("Change");
                    this.Link.Links.Add(length, builder.Length - length, KEY_CHANGE);
                }
                this.Link.Text = builder.ToString();
            }
        }

        [field: AccessedThroughProperty("Link")]
        private LinkLabel Link { get; [MethodImpl(MethodImplOptions.Synchronized)]
        set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool __AllowEdit
        {
            get => 
                this._AllowEdit;
            set => 
                this._AllowEdit = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string DefaultCmnType
        {
            get => 
                DmercHelper.Dmerc2String(this._DefaultCmnType);
            set => 
                this._DefaultCmnType = DmercHelper.String2Dmerc(value);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int? CustomerID
        {
            get => 
                this._CustomerID;
            set
            {
                if (!this._CustomerID.Equals(value))
                {
                    this._CustomerID = value;
                    this.RefreshLink();
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int? OrderID
        {
            get => 
                this._OrderID;
            set
            {
                if (!this._OrderID.Equals(value))
                {
                    this._OrderID = value;
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int? CmnID
        {
            get => 
                this._CmnID;
            set
            {
                if (!this._CmnID.Equals(value))
                {
                    this._CmnID = value;
                    this.RefreshLink();
                }
            }
        }
    }
}

