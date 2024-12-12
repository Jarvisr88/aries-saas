namespace DMEWorks.CMN
{
    using Devart.Data.MySql;
    using DMEWorks;
    using DMEWorks.Data.MySql;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class Control_CMNBase : UserControl
    {
        private IContainer components;
        protected FormMirHelper F_MirHelper;

        public event ValueChangedEventHandler ValueChanged;

        public Control_CMNBase()
        {
            this.InitializeComponent();
        }

        public virtual void Clear()
        {
        }

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
            base.Name = "Control_CMNBase";
            base.Size = new Size(800, 200);
        }

        public void LoadFromDB(MySqlConnection cnn, int CMNFormID)
        {
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            try
            {
                using (MySqlCommand command = new MySqlCommand("", cnn))
                {
                    command.CommandText = "SELECT * FROM " + this.TableName + " WHERE CMNFormID = :CMNFormID";
                    command.Parameters.Add("CMNFormID", MySqlType.Int).Value = CMNFormID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            this.Clear();
                        }
                        else
                        {
                            this.LoadFromReader(reader);
                        }
                    }
                }
            }
            finally
            {
                bool flag;
                if (flag)
                {
                    cnn.Close();
                }
            }
        }

        public virtual void LoadFromReader(MySqlDataReader reader)
        {
        }

        public void OnChanged()
        {
            ValueChangedEventHandler valueChangedEvent = this.ValueChangedEvent;
            if (valueChangedEvent != null)
            {
                valueChangedEvent(this, EventArgs.Empty);
            }
        }

        public virtual void SaveToCommand(MySqlCommand cmd)
        {
        }

        public void SaveToDB(MySqlConnection cnn, int CMNFormID)
        {
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            try
            {
                using (MySqlCommand command = new MySqlCommand("", cnn))
                {
                    this.SaveToCommand(command);
                    command.Parameters.Add("CMNFormID", MySqlType.Int).Value = CMNFormID;
                    string[] whereParameters = new string[] { "CMNFormID" };
                    if (command.ExecuteUpdate(this.TableName, whereParameters) == 0)
                    {
                        command.ExecuteInsert(this.TableName);
                    }
                }
            }
            finally
            {
                bool flag;
                if (flag)
                {
                    cnn.Close();
                }
            }
        }

        public virtual void ShowMissingInformation(ErrorProvider MissingProvider, bool show, string MirKeys)
        {
            MissingProvider.SetIconAlignment(this, ErrorIconAlignment.TopRight);
            if (show)
            {
                this.MirHelper.ShowMissingInformation(MissingProvider, MirKeys);
            }
            else
            {
                this.MirHelper.ShowMissingInformation(MissingProvider, "");
            }
        }

        public virtual DmercType Type =>
            (DmercType) 0;

        public string Caption =>
            DmercHelper.GetName(this.Type);

        public string FormName =>
            DmercHelper.Dmerc2String(this.Type);

        public string TableName =>
            DmercHelper.GetTableName(this.Type);

        public virtual FormMirHelper MirHelper
        {
            get
            {
                if (this.F_MirHelper == null)
                {
                    this.F_MirHelper = new FormMirHelper();
                    this.F_MirHelper.Add("Answers", this, "At least one answer must be yes");
                }
                return this.F_MirHelper;
            }
        }

        public delegate void ValueChangedEventHandler(object sender, EventArgs e);
    }
}

