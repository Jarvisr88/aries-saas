namespace DMEWorks.Controls
{
    using DMEWorks.Forms;
    using Infragistics.Win.UltraWinEditors;
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Data;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    [StandardModule]
    public sealed class Functions
    {
        public static void AssignDatasource(FindDialog dialog, DataTable table, params string[] KeyMembers)
        {
            if (dialog != null)
            {
                dialog.DataSource = table;
                dialog.SelectedRow = null;
            }
        }

        public static void AssignDatasource(Combobox ComboBox, DataTable table, string DisplayMember, string ValueMember)
        {
            bool enabled = ComboBox.Enabled;
            ComboBox.Enabled = false;
            try
            {
                if ((ComboBox.DisplayMember != DisplayMember) || (ComboBox.ValueMember != ValueMember))
                {
                    ComboBox.DataSource = table;
                    ComboBox.DisplayMember = DisplayMember;
                    ComboBox.ValueMember = ValueMember;
                }
                else
                {
                    object selectedValue = ComboBox.SelectedValue;
                    ComboBox.DataSource = table;
                    ComboBox.DisplayMember = DisplayMember;
                    ComboBox.ValueMember = ValueMember;
                    try
                    {
                        ComboBox.SelectedValue = selectedValue;
                    }
                    catch (Exception exception1)
                    {
                        ProjectData.SetProjectError(exception1);
                        ProjectData.ClearProjectError();
                    }
                }
            }
            finally
            {
                ComboBox.Enabled = enabled;
            }
        }

        public static void AssignDatasource(ComboBox ComboBox, DataTable table, string DisplayMember, string ValueMember)
        {
            bool enabled = ComboBox.Enabled;
            ComboBox.Enabled = false;
            try
            {
                if ((ComboBox.DisplayMember != DisplayMember) || (ComboBox.ValueMember != ValueMember))
                {
                    ComboBox.DataSource = table;
                    ComboBox.DisplayMember = DisplayMember;
                    ComboBox.ValueMember = ValueMember;
                }
                else
                {
                    object selectedValue = ComboBox.SelectedValue;
                    ComboBox.DataSource = table;
                    ComboBox.DisplayMember = DisplayMember;
                    ComboBox.ValueMember = ValueMember;
                    try
                    {
                        ComboBox.SelectedValue = selectedValue;
                    }
                    catch (Exception exception1)
                    {
                        ProjectData.SetProjectError(exception1);
                        ProjectData.ClearProjectError();
                    }
                }
            }
            finally
            {
                ComboBox.Enabled = enabled;
            }
        }

        public static void AttachPhoneAutoInput(TextBox textBox)
        {
            if (textBox == null)
            {
                throw new ArgumentNullException("textBox");
            }
            textBox.KeyPress += new KeyPressEventHandler(Functions.PhoneAutoInput);
        }

        public static int EnumerateErrors(Control parent, ErrorProvider provider)
        {
            int num = 0;
            int num3 = parent.Controls.Count - 1;
            for (int i = 0; i <= num3; i++)
            {
                Control control = parent.Controls[i];
                num += EnumerateErrors(control, provider);
                if (provider.GetError(control) != "")
                {
                    num++;
                }
            }
            return num;
        }

        private static void EnumerateErrors(Control parent, ErrorProvider provider, Hashtable hash)
        {
            int num = parent.Controls.Count - 1;
            for (int i = 0; i <= num; i++)
            {
                Control control = parent.Controls[i];
                EnumerateErrors(control, provider, hash);
                string error = provider.GetError(control);
                if (!string.IsNullOrEmpty(error))
                {
                    hash[error] = error;
                }
            }
        }

        public static int EnumerateErrors(Control parent, ErrorProvider provider, StringBuilder builder)
        {
            IEnumerator enumerator;
            Hashtable hash = CollectionsUtil.CreateCaseInsensitiveHashtable();
            EnumerateErrors(parent, provider, hash);
            try
            {
                enumerator = hash.Keys.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    string str = Conversions.ToString(enumerator.Current);
                    if (builder.Length > 0)
                    {
                        builder.Append("\r\n");
                    }
                    builder.Append(str);
                }
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    (enumerator as IDisposable).Dispose();
                }
            }
            return hash.Count;
        }

        public static object GetDateBoxValue(UltraDateTimeEditor DateBox) => 
            !(DateBox.Value is DateTime) ? ((object) DBNull.Value) : ((object) Conversions.ToDate(DateBox.Value));

        public static DateTime GetDateBoxValue(UltraDateTimeEditor DateBox, DateTime @default) => 
            !(DateBox.Value is DateTime) ? @default : Conversions.ToDate(DateBox.Value);

        public static object GetDatePickerValue2(DateTimePicker DatePicker) => 
            (!DatePicker.ShowCheckBox || DatePicker.Checked) ? ((object) DatePicker.Value) : ((object) DBNull.Value);

        private static string GetFieldType(DataTable table, string name)
        {
            if (table != null)
            {
                string filterExpression = $"[Field] = '{name.Replace("'", "''")}'";
                DataRow[] rowArray = table.Select(filterExpression);
                if (0 <= rowArray.Length)
                {
                    object obj2 = rowArray[0]["Type"];
                    if (!(obj2 is string))
                    {
                        if (obj2 is byte[])
                        {
                            return Encoding.UTF8.GetString(obj2 as byte[]);
                        }
                    }
                    else
                    {
                        return Convert.ToString(obj2);
                    }
                }
            }
            return string.Empty;
        }

        public static bool IsNpiValid(string Value) => 
            (Value != null) ? Regex.IsMatch(Value, @"^[0-9]{10}\s*$", RegexOptions.Singleline | RegexOptions.ExplicitCapture) : false;

        public static bool IsZipValid(string Value) => 
            (Value != null) ? Regex.IsMatch(Value, @"^[0-9]{5}((-| |)[0-9]{4})\s*$", RegexOptions.Singleline | RegexOptions.ExplicitCapture) : false;

        public static void LoadComboBoxItems(ComboBox ComboBox, DataTable table, string name)
        {
            ComboBox.BeginUpdate();
            try
            {
                ComboBox.Items.Clear();
                ComboBox.Items.AddRange(ParseMysqlEnum(table, name).ToArray<string>());
            }
            finally
            {
                ComboBox.EndUpdate();
            }
        }

        public static string NpiValidate(string Value, bool Required = false)
        {
            if (!string.IsNullOrWhiteSpace(Value))
            {
                if (!IsNpiValid(Value))
                {
                    return "NPI must be 10 digits number";
                }
            }
            else if (Required)
            {
                return "NPI must be 10 digits number";
            }
            return "";
        }

        [IteratorStateMachine(typeof(VB$StateMachine_22_ParseMysqlEnum))]
        private static IEnumerable<string> ParseMysqlEnum(string input)
        {
            VB$StateMachine_22_ParseMysqlEnum enum1 = new VB$StateMachine_22_ParseMysqlEnum(-2);
            enum1.$P_input = input;
            return enum1;
        }

        public static IEnumerable<string> ParseMysqlEnum(DataTable table, string name) => 
            ParseMysqlEnum(GetFieldType(table, name));

        private static void PhoneAutoInput(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                PhoneAutoInput(textBox, e);
            }
        }

        private static void PhoneAutoInput(TextBox TextBox, KeyPressEventArgs e)
        {
            try
            {
                if ((e.KeyChar != '\b') && (e.KeyChar != '\x0018'))
                {
                    int selectionStart = TextBox.SelectionStart;
                    if (selectionStart == 0)
                    {
                        char[] chArray1 = new char[] { '(', e.KeyChar };
                        TextBox.SelectedText = new string(chArray1);
                        e.Handled = true;
                    }
                    else if (selectionStart == 4)
                    {
                        char[] chArray2 = new char[] { ')', e.KeyChar };
                        TextBox.SelectedText = new string(chArray2);
                        e.Handled = true;
                    }
                    else if (selectionStart == 8)
                    {
                        char[] chArray3 = new char[] { '-', e.KeyChar };
                        TextBox.SelectedText = new string(chArray3);
                        e.Handled = true;
                    }
                    else if (selectionStart == 13)
                    {
                        char[] chArray4 = new char[] { ' ', e.KeyChar };
                        TextBox.SelectedText = new string(chArray4);
                        e.Handled = true;
                    }
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
            }
        }

        public static string PhoneValidate(string Value)
        {
            string str;
            if (Value == null)
            {
                str = "";
            }
            else
            {
                Value = Value.Trim();
                if (Value == "")
                {
                    str = "";
                }
                else if (Value.Length < 13)
                {
                    str = "Phone must have format \"(999)999-9999 [comments]\"";
                }
                else
                {
                    int num2 = Math.Min(13, Value.Length - 1);
                    int num = 0;
                    while (true)
                    {
                        if (num > num2)
                        {
                            str = "";
                            break;
                        }
                        char c = Value[num];
                        if (num == 0)
                        {
                            if (c != '(')
                            {
                                str = "Phone must have format \"(999)999-9999 [comments]\"";
                                break;
                            }
                        }
                        else if (num == 4)
                        {
                            if (c != ')')
                            {
                                str = "Phone must have format \"(999)999-9999 [comments]\"";
                                break;
                            }
                        }
                        else if (num == 8)
                        {
                            if (c != '-')
                            {
                                str = "Phone must have format \"(999)999-9999 [comments]\"";
                                break;
                            }
                        }
                        else if (num == 13)
                        {
                            if (c != ' ')
                            {
                                str = "Phone must have format \"(999)999-9999 [comments]\"";
                                break;
                            }
                        }
                        else if (!char.IsDigit(c))
                        {
                            str = "Phone must have format \"(999)999-9999 [comments]\"";
                            break;
                        }
                        num++;
                    }
                }
            }
            return str;
        }

        public static void SetCheckBoxChecked(CheckBox CheckBox, object Value)
        {
            if (!Information.IsDBNull(Value))
            {
                CheckBox.Checked = Conversions.ToBoolean(Value);
            }
            else
            {
                CheckBox.Checked = false;
            }
        }

        public static void SetComboBoxItem(ComboBox ComboBox, object Value)
        {
            bool enabled = ComboBox.Enabled;
            ComboBox.Enabled = false;
            try
            {
                ComboBox.SelectedItem = Value;
            }
            finally
            {
                ComboBox.Enabled = enabled;
            }
        }

        public static void SetComboBoxText(ComboBox ComboBox, object Value)
        {
            if (!Information.IsDBNull(Value))
            {
                ComboBox.Text = Conversions.ToString(Value);
            }
            else
            {
                ComboBox.Text = "";
            }
        }

        public static void SetComboBoxValue(Combobox ComboBox, object Value)
        {
            bool enabled = ComboBox.Enabled;
            ComboBox.Enabled = false;
            try
            {
                ComboBox.SelectedValue = Value;
            }
            finally
            {
                ComboBox.Enabled = enabled;
            }
        }

        public static void SetComboBoxValue(ComboBox ComboBox, object Value)
        {
            bool enabled = ComboBox.Enabled;
            ComboBox.Enabled = false;
            try
            {
                ComboBox.SelectedValue = Value;
            }
            finally
            {
                ComboBox.Enabled = enabled;
            }
        }

        public static void SetControlText(Control Control, object Value)
        {
            if (!Information.IsDBNull(Value))
            {
                Control.Text = Conversions.ToString(Value);
            }
            else
            {
                Control.Text = "";
            }
        }

        public static void SetDateBoxValue(UltraDateTimeEditor DateBox, object Value)
        {
            DateBox.Value = Value;
        }

        public static void SetDatePickerValue2(DateTimePicker DatePicker, object Value)
        {
            DatePicker.Value = !Information.IsDate(Value) ? DateTime.Today : Conversions.ToDate(Value);
            if (DatePicker.ShowCheckBox)
            {
                DatePicker.Checked = !DatePicker.Checked;
                DatePicker.Checked = Information.IsDate(Value);
            }
        }

        public static void SetLabelText(Label Label, object Value)
        {
            if (!Information.IsDBNull(Value))
            {
                Label.Text = Conversions.ToString(Value);
            }
            else
            {
                Label.Text = "";
            }
        }

        public static void SetMaskBoxText(MaskedTextBox MaskEdit, object Value)
        {
            if (!Information.IsDBNull(Value))
            {
                MaskEdit.Text = Conversions.ToString(Value);
            }
            else
            {
                MaskEdit.Text = "";
            }
        }

        public static void SetNumericBoxValue(NumericBox NumericBox, object Value)
        {
            NumericBox.AsDouble = ToNullableDouble(Value);
        }

        public static void SetRadioChecked(RadioButton Radio, object Value)
        {
            if (!Information.IsDBNull(Value))
            {
                Radio.Checked = Conversions.ToBoolean(Value);
            }
            else
            {
                Radio.Checked = false;
            }
        }

        public static void SetRadioGroupValue(RadioGroup control, object Value)
        {
            if (!Information.IsDBNull(Value))
            {
                control.Value = Conversions.ToString(Value);
            }
            else
            {
                control.Value = "";
            }
        }

        public static void SetStatusBarPanelText(StatusBarPanel StatusBarPanel, object Value)
        {
            if (!Information.IsDBNull(Value))
            {
                StatusBarPanel.Text = Conversions.ToString(Value);
            }
            else
            {
                StatusBarPanel.Text = "";
            }
        }

        public static void SetTextBoxText(TextBox TextBox, object Value)
        {
            if (!Information.IsDBNull(Value))
            {
                TextBox.Text = Conversions.ToString(Value);
            }
            else
            {
                TextBox.Text = "";
            }
        }

        public static void SetUpDownValue(NumericUpDown UpDown, object Value)
        {
            if (!Information.IsDBNull(Value))
            {
                UpDown.Value = Conversions.ToDecimal(Value);
            }
            else
            {
                UpDown.Value = UpDown.Minimum;
            }
        }

        private static double? ToNullableDouble(object value)
        {
            try
            {
                if (value != null)
                {
                    return new double?(Convert.ToDouble(value));
                }
            }
            catch (InvalidCastException exception1)
            {
                InvalidCastException ex = exception1;
                ProjectData.SetProjectError(ex);
                InvalidCastException exception = ex;
                ProjectData.ClearProjectError();
            }
            catch (FormatException exception4)
            {
                FormatException ex = exception4;
                ProjectData.SetProjectError(ex);
                FormatException exception2 = ex;
                ProjectData.ClearProjectError();
            }
            catch (OverflowException exception5)
            {
                OverflowException ex = exception5;
                ProjectData.SetProjectError(ex);
                OverflowException exception3 = ex;
                ProjectData.ClearProjectError();
            }
            return null;
        }

        [CompilerGenerated]
        private sealed class VB$StateMachine_22_ParseMysqlEnum : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
        {
            public int $State;
            public string $Current;
            public int $InitialThreadId;
            internal string $VB$Local_input;
            internal string $P_input;
            internal Match $VB$ResumableLocal_m$0;

            public VB$StateMachine_22_ParseMysqlEnum(int $State)
            {
                this.$State = $State;
                this.$InitialThreadId = Environment.CurrentManagedThreadId;
            }

            private void Dispose()
            {
            }

            private IEnumerator<string> GetEnumerator()
            {
                Functions.VB$StateMachine_22_ParseMysqlEnum enum2;
                if ((this.$State != -2) || (this.$InitialThreadId != Environment.CurrentManagedThreadId))
                {
                    enum2 = new Functions.VB$StateMachine_22_ParseMysqlEnum(0);
                }
                else
                {
                    this.$State = 0;
                    enum2 = this;
                }
                enum2.$VB$Local_input = this.$P_input;
                return enum2;
            }

            IEnumerator IEnumerable.GetEnumerator() => 
                this.GetEnumerator();

            [CompilerGenerated]
            private bool MoveNext()
            {
                int num = this.$State;
                if (num == 0)
                {
                    this.$State = num = -1;
                    string text1 = this.$VB$Local_input;
                    if (this.$VB$Local_input == null)
                    {
                        string local1 = this.$VB$Local_input;
                        text1 = string.Empty;
                    }
                    this.$VB$Local_input = text1;
                    this.$VB$ResumableLocal_m$0 = Regex.Match(this.$VB$Local_input, @"'([^']*('')*)+'(,|\))");
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.$State = num = -1;
                    goto TR_0007;
                }
            TR_0005:
                if (!this.$VB$ResumableLocal_m$0.Success)
                {
                    return false;
                }
                string str = this.$VB$ResumableLocal_m$0.Value;
                if (3 <= str.Length)
                {
                    this.$Current = str.Substring(1, str.Length - 3);
                    this.$State = num = 1;
                    return true;
                }
            TR_0007:
                while (true)
                {
                    this.$VB$ResumableLocal_m$0 = this.$VB$ResumableLocal_m$0.NextMatch();
                    break;
                }
                goto TR_0005;
            }

            private void Reset()
            {
                throw new NotSupportedException();
            }

            private string Current =>
                this.$Current;

            object IEnumerator.Current =>
                this.$Current;
        }
    }
}

