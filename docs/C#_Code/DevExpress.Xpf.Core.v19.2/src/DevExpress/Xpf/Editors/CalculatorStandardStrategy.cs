namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Globalization;
    using System.Media;
    using System.Runtime.InteropServices;

    public class CalculatorStandardStrategy : CalculatorStrategyBase
    {
        public CalculatorStandardStrategy(CalculatorStandardView view) : base(view)
        {
        }

        private decimal CheckFractionalPartLength(decimal num) => 
            decimal.Round(num, base.Precision);

        private string CheckFractionalPartLength(string value)
        {
            int index = value.IndexOf(this.DecimalSeparator);
            return ((index != -1) ? (((value.Length - index) <= base.Precision) ? value : value.Remove((index + base.Precision) + 1, value.Length - ((index + base.Precision) + 1))) : value);
        }

        private void EnterDecimal()
        {
            string displayText = base.DisplayText;
            if (this.StartEnteringDigits())
            {
                displayText = "0";
            }
            if (displayText.IndexOf(this.DecimalSeparator) == -1)
            {
                displayText = displayText + this.DecimalSeparator;
                this.SetDisplayText(this.ToDecimal(displayText), displayText, false);
            }
        }

        private void EnterDigit(int digit)
        {
            string str = (this.StartEnteringDigits() ? string.Empty : base.DisplayText) + digit.ToString();
            int startIndex = 0;
            if (str[0] == '-')
            {
                startIndex++;
            }
            if ((str.Length > (startIndex + 1)) && ((str[startIndex] == '0') && char.IsDigit(str[startIndex + 1])))
            {
                str = str.Remove(startIndex, 1);
            }
            str = this.CheckFractionalPartLength(str);
            try
            {
                this.SetDisplayText(this.ToDecimal(str), str, false);
            }
            catch
            {
                SystemSounds.Beep.Play();
            }
        }

        protected override void Error(string message)
        {
            base.HistoryString = "";
            this.LastOperationButtonID = CalculatorStandardView.ButtonType.Equal;
            base.Error(message);
        }

        private string ExtractDecimalNumber(string val)
        {
            string str = string.Empty;
            bool flag = false;
            int num = 0;
            for (int i = 0; i < val.Length; i++)
            {
                flag = false;
                char c = val[i];
                flag = char.IsDigit(c);
                if ((str.Length == 0) && (c == '-'))
                {
                    flag = true;
                }
                if ((num == 0) && (val.IndexOf(this.DecimalSeparator, i, this.DecimalSeparator.Length) == i))
                {
                    num++;
                    i += Math.Max(0, this.DecimalSeparator.Length - 1);
                    str = str + this.DecimalSeparator;
                }
                if (flag)
                {
                    str = str + c.ToString();
                }
            }
            return str;
        }

        protected virtual string FormatForHistory(decimal value)
        {
            string str = this.CheckFractionalPartLength(value).ToString(CalculatorStandardView.DisplayFormat, base.Culture);
            if (value < 0M)
            {
                str = "(" + str + ")";
            }
            return str;
        }

        private string GetOperationString(CalculatorStandardView.ButtonType buttonType)
        {
            switch (buttonType)
            {
                case CalculatorStandardView.ButtonType.Add:
                    return "+";

                case CalculatorStandardView.ButtonType.Sub:
                    return "-";

                case CalculatorStandardView.ButtonType.Mul:
                    return "*";

                case CalculatorStandardView.ButtonType.Div:
                    return "/";
            }
            return "";
        }

        public override void Init(decimal value, bool resetMemory)
        {
            base.Init(value, resetMemory);
            base.HistoryString = "";
            this.LastOperationButtonID = CalculatorStandardView.ButtonType.Equal;
            base.DisplayValue = base.Result = value;
            if (resetMemory)
            {
                base.Memory = 0M;
            }
            base.Status = CalcStatus.Ok;
            this.SetDisplayText(base.DisplayValue, null, true);
        }

        private bool IsDigitButton(CalculatorStandardView.ButtonType buttonType, out int digit)
        {
            switch (buttonType)
            {
                case CalculatorStandardView.ButtonType.Zero:
                    digit = 0;
                    return true;

                case CalculatorStandardView.ButtonType.One:
                    digit = 1;
                    return true;

                case CalculatorStandardView.ButtonType.Two:
                    digit = 2;
                    return true;

                case CalculatorStandardView.ButtonType.Three:
                    digit = 3;
                    return true;

                case CalculatorStandardView.ButtonType.Four:
                    digit = 4;
                    return true;

                case CalculatorStandardView.ButtonType.Five:
                    digit = 5;
                    return true;

                case CalculatorStandardView.ButtonType.Six:
                    digit = 6;
                    return true;

                case CalculatorStandardView.ButtonType.Seven:
                    digit = 7;
                    return true;

                case CalculatorStandardView.ButtonType.Eight:
                    digit = 8;
                    return true;

                case CalculatorStandardView.ButtonType.Nine:
                    digit = 9;
                    return true;
            }
            digit = 0;
            return false;
        }

        private void ProcessBackKey()
        {
            string str;
            this.StartEnteringDigits();
            decimal v = 0M;
            if ((base.DisplayText.Length > 1) && ((base.DisplayText.Length != 2) || (base.DisplayText[0] != '-')))
            {
                str = base.DisplayText.Remove(base.DisplayText.Length - 1, 1);
                v = this.ToDecimal(str);
            }
            else
            {
                str = "0";
                base.Status = CalcStatus.Ok;
            }
            this.SetDisplayText(v, str, false);
            if (this.LastOperationButtonID.Equals(CalculatorStandardView.ButtonType.Equal))
            {
                base.Result = base.DisplayValue;
            }
        }

        protected override void ProcessButtonClickInternal(object buttonID)
        {
            if (buttonID is CalculatorStandardView.ButtonType)
            {
                CalculatorStandardView.ButtonType buttonType = (CalculatorStandardView.ButtonType) buttonID;
                if ((base.Status != CalcStatus.Error) || (buttonType == CalculatorStandardView.ButtonType.Clear))
                {
                    int num;
                    if (this.IsDigitButton(buttonType, out num))
                    {
                        this.EnterDigit(num);
                    }
                    else
                    {
                        switch (buttonType)
                        {
                            case CalculatorStandardView.ButtonType.MC:
                                base.Memory = 0M;
                                base.Status = CalcStatus.Ok;
                                return;

                            case CalculatorStandardView.ButtonType.MR:
                                base.Status = CalcStatus.Ok;
                                this.SetDisplayText(base.Memory, null, true);
                                return;

                            case CalculatorStandardView.ButtonType.MS:
                                base.Memory = base.DisplayValue;
                                base.Status = CalcStatus.Ok;
                                return;

                            case CalculatorStandardView.ButtonType.MAdd:
                                base.Memory += base.DisplayValue;
                                base.Status = CalcStatus.Ok;
                                return;

                            case CalculatorStandardView.ButtonType.MSub:
                                base.Memory -= base.DisplayValue;
                                base.Status = CalcStatus.Ok;
                                break;

                            case CalculatorStandardView.ButtonType.Back:
                                this.ProcessBackKey();
                                return;

                            case CalculatorStandardView.ButtonType.Cancel:
                                base.Status = CalcStatus.Ok;
                                this.SetDisplayText(0M, null, true);
                                return;

                            case CalculatorStandardView.ButtonType.Clear:
                                this.LastOperationButtonID = CalculatorStandardView.ButtonType.Equal;
                                base.Status = CalcStatus.Ok;
                                base.Result = 0M;
                                this.SetDisplayText(base.Result, null, true);
                                return;

                            case CalculatorStandardView.ButtonType.Zero:
                            case CalculatorStandardView.ButtonType.One:
                            case CalculatorStandardView.ButtonType.Two:
                            case CalculatorStandardView.ButtonType.Three:
                            case CalculatorStandardView.ButtonType.Four:
                            case CalculatorStandardView.ButtonType.Five:
                            case CalculatorStandardView.ButtonType.Six:
                            case CalculatorStandardView.ButtonType.Seven:
                            case CalculatorStandardView.ButtonType.Eight:
                            case CalculatorStandardView.ButtonType.Nine:
                                break;

                            case CalculatorStandardView.ButtonType.Decimal:
                                this.EnterDecimal();
                                return;

                            case CalculatorStandardView.ButtonType.Sign:
                                if (base.DisplayText == ("0" + this.DecimalSeparator))
                                {
                                    this.SetDisplayText(0M, "-" + base.DisplayText, false);
                                }
                                else if (base.DisplayText == ("-0" + this.DecimalSeparator))
                                {
                                    this.SetDisplayText(0M, "0" + this.DecimalSeparator, false);
                                }
                                else
                                {
                                    this.SetDisplayText(-base.DisplayValue, null, true);
                                }
                                if (base.Status != CalcStatus.Ok)
                                {
                                    break;
                                }
                                base.Result = base.DisplayValue;
                                return;

                            case CalculatorStandardView.ButtonType.Add:
                            case CalculatorStandardView.ButtonType.Sub:
                            case CalculatorStandardView.ButtonType.Mul:
                            case CalculatorStandardView.ButtonType.Div:
                            {
                                base.Status = CalcStatus.Ok;
                                CalculatorStandardView.ButtonType type2 = (base.PrevButtonID != null) ? ((CalculatorStandardView.ButtonType) base.PrevButtonID) : CalculatorStandardView.ButtonType.None;
                                if ((this.LastOperationButtonID is CalculatorStandardView.ButtonType) && ((type2 != CalculatorStandardView.ButtonType.Add) && ((type2 != CalculatorStandardView.ButtonType.Sub) && ((type2 != CalculatorStandardView.ButtonType.Mul) && (type2 != CalculatorStandardView.ButtonType.Div)))))
                                {
                                    string operationString = this.GetOperationString((CalculatorStandardView.ButtonType) this.LastOperationButtonID);
                                    if (!string.IsNullOrEmpty(operationString))
                                    {
                                        this.HistoryString = $"{(base.HistoryString == "") ? this.FormatForHistory(base.Result) : base.HistoryString} {operationString} {this.FormatForHistory(base.DisplayValue)}";
                                    }
                                    switch (((CalculatorStandardView.ButtonType) this.LastOperationButtonID))
                                    {
                                        case CalculatorStandardView.ButtonType.Add:
                                            this.SetDisplayText(base.Result + base.DisplayValue, null, true);
                                            break;

                                        case CalculatorStandardView.ButtonType.Sub:
                                            this.SetDisplayText(base.Result - base.DisplayValue, null, true);
                                            break;

                                        case CalculatorStandardView.ButtonType.Mul:
                                            this.SetDisplayText(base.Result * base.DisplayValue, null, true);
                                            break;

                                        case CalculatorStandardView.ButtonType.Div:
                                            if (base.DisplayValue == 0M)
                                            {
                                                this.Error(EditorLocalizer.GetString(EditorStringId.CalculatorDivisionByZeroError));
                                            }
                                            else
                                            {
                                                this.SetDisplayText(base.Result / base.DisplayValue, null, true);
                                            }
                                            break;

                                        default:
                                            break;
                                    }
                                }
                                base.Result = base.DisplayValue;
                                this.LastOperationButtonID = buttonID;
                                return;
                            }
                            case CalculatorStandardView.ButtonType.Fract:
                                if (base.DisplayValue == 0M)
                                {
                                    this.Error(EditorLocalizer.GetString(EditorStringId.CalculatorDivisionByZeroError));
                                    return;
                                }
                                base.Status = CalcStatus.Ok;
                                this.SetDisplayText(1M / base.DisplayValue, null, true);
                                if (!this.LastOperationButtonID.Equals(CalculatorStandardView.ButtonType.Equal))
                                {
                                    break;
                                }
                                base.Result = base.DisplayValue;
                                return;

                            case CalculatorStandardView.ButtonType.Percent:
                                if (base.Status != CalcStatus.OkEnteringDigits)
                                {
                                    break;
                                }
                                base.Status = CalcStatus.Ok;
                                this.SetDisplayText((base.DisplayValue * base.Result) / 100.0M, null, true);
                                return;

                            case CalculatorStandardView.ButtonType.Sqrt:
                                if (base.DisplayValue < 0M)
                                {
                                    this.Error(EditorLocalizer.GetString(EditorStringId.CalculatorInvalidInputError));
                                    return;
                                }
                                base.Status = CalcStatus.Ok;
                                this.SetDisplayText(this.Sqrt(base.DisplayValue), null, true);
                                if (!this.LastOperationButtonID.Equals(CalculatorStandardView.ButtonType.Equal))
                                {
                                    break;
                                }
                                base.Result = base.DisplayValue;
                                return;

                            case CalculatorStandardView.ButtonType.Equal:
                                if (this.LastOperationButtonID is CalculatorStandardView.ButtonType)
                                {
                                    string operationString = this.GetOperationString((CalculatorStandardView.ButtonType) this.LastOperationButtonID);
                                    string str2 = null;
                                    if (!string.IsNullOrEmpty(operationString) && ((operationString != "/") || (base.DisplayValue != 0M)))
                                    {
                                        str2 = $"{(base.HistoryString == "") ? this.FormatForHistory(base.Result) : base.HistoryString} {operationString} {this.FormatForHistory(base.DisplayValue)}";
                                    }
                                    switch (((CalculatorStandardView.ButtonType) this.LastOperationButtonID))
                                    {
                                        case CalculatorStandardView.ButtonType.Add:
                                            base.Result += base.DisplayValue;
                                            break;

                                        case CalculatorStandardView.ButtonType.Sub:
                                            base.Result -= base.DisplayValue;
                                            break;

                                        case CalculatorStandardView.ButtonType.Mul:
                                            base.Result *= base.DisplayValue;
                                            break;

                                        case CalculatorStandardView.ButtonType.Div:
                                            if (!(base.DisplayValue == 0M))
                                            {
                                                base.Result /= base.DisplayValue;
                                                break;
                                            }
                                            this.Error(EditorLocalizer.GetString(EditorStringId.CalculatorDivisionByZeroError));
                                            return;

                                        case CalculatorStandardView.ButtonType.Equal:
                                            base.Result = base.DisplayValue;
                                            break;

                                        default:
                                            break;
                                    }
                                    if (str2 != null)
                                    {
                                        base.AddToHistory($"{str2} = {this.CheckFractionalPartLength(base.Result).ToString(CalculatorStandardView.DisplayFormat, base.Culture)}");
                                        base.HistoryString = "";
                                    }
                                }
                                this.LastOperationButtonID = CalculatorStandardView.ButtonType.Equal;
                                base.Status = CalcStatus.Ok;
                                this.SetDisplayText(base.Result, null, true);
                                return;

                            default:
                                return;
                        }
                    }
                }
            }
        }

        public override void SetDisplayText(string text)
        {
            decimal num;
            if (decimal.TryParse(text, NumberStyles.Number, base.Culture, out num))
            {
                this.SetDisplayText(num, null, true);
            }
            else
            {
                this.Error(EditorLocalizer.GetString(EditorStringId.CalculatorInvalidInputError));
            }
        }

        protected virtual void SetDisplayText(decimal v, string displayText, bool convert)
        {
            base.DisplayValue = v;
            v = this.CheckFractionalPartLength(v);
            this.DisplayText = convert ? v.ToString(CalculatorStandardView.DisplayFormat, base.Culture) : displayText;
        }

        private decimal Sqrt(decimal value) => 
            Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(value)));

        private bool StartEnteringDigits()
        {
            if (base.Status != CalcStatus.Ok)
            {
                return false;
            }
            base.Status = CalcStatus.OkEnteringDigits;
            this.View.ResetDisplayValue();
            return true;
        }

        private decimal ToDecimal(string value)
        {
            decimal num;
            if (string.IsNullOrEmpty(value))
            {
                return 0M;
            }
            if (decimal.TryParse(value, NumberStyles.Number, base.Culture, out num))
            {
                return num;
            }
            value = this.ExtractDecimalNumber(value);
            if (value.IndexOf(this.DecimalSeparator) == (value.Length - this.DecimalSeparator.Length))
            {
                value = value.Remove(value.Length - this.DecimalSeparator.Length, this.DecimalSeparator.Length);
            }
            return Convert.ToDecimal(value);
        }

        private string UpdateDecimalSeparator(string s)
        {
            int length = -1;
            int num2 = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (!char.IsDigit(s[i]) && (s[i] != '-'))
                {
                    if (length != -1)
                    {
                        num2++;
                    }
                    else
                    {
                        length = i;
                        num2 = 1;
                    }
                }
            }
            return (s.Substring(0, length) + this.DecimalSeparator + s.Substring(length + num2));
        }

        public override void UpdateFormatting()
        {
            base.UpdateFormatting();
            if (base.Status == CalcStatus.OkEnteringDigits)
            {
                string displayText = base.DisplayText;
                displayText = this.UpdateDecimalSeparator(displayText);
                displayText = this.CheckFractionalPartLength(displayText);
                this.SetDisplayText(this.ToDecimal(displayText), displayText, false);
            }
            else
            {
                this.SetDisplayText(base.DisplayValue, null, true);
                if (base.IsModified)
                {
                    base.SynchronizeValue();
                }
            }
        }

        protected object LastOperationButtonID
        {
            get => 
                this.View.LastOperationButtonID;
            set => 
                this.View.LastOperationButtonID = value;
        }

        protected CalculatorStandardView View =>
            (CalculatorStandardView) base.View;

        private string DecimalSeparator =>
            base.Culture.NumberFormat.NumberDecimalSeparator;
    }
}

