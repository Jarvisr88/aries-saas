namespace DevExpress.Office.Design.Internal
{
    using DevExpress.Office;
    using DevExpress.Office.Localization;
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    public class UIUnit
    {
        private static readonly DevExpress.Office.Design.Internal.UnitAbbreviationDictionary unitAbbreviationDictionary = new DevExpress.Office.Design.Internal.UnitAbbreviationDictionary();
        private readonly DevExpress.Office.Design.Internal.UnitPrecisionDictionary unitPrecisionDictionary;
        private static readonly DevExpress.Office.Design.Internal.UnitCaptionDictionary unitCaptionDictionary = new DevExpress.Office.Design.Internal.UnitCaptionDictionary();
        private DocumentUnit unitType;
        private string stringValue;
        private bool isNegative;
        private bool isValueInPercent;

        public UIUnit(float value, DocumentUnit type, DevExpress.Office.Design.Internal.UnitPrecisionDictionary unitPrecisionDictionary)
        {
            this.unitPrecisionDictionary = unitPrecisionDictionary;
            this.unitType = this.GetValidType(type);
            float num = (float) Math.Round((double) value, (int) (this.UnitPrecisionDictionary[this.UnitType] + 1));
            this.stringValue = Math.Abs(num).ToString();
            this.isNegative = value < 0f;
            this.isValueInPercent = false;
        }

        public UIUnit(string stringValue, DocumentUnit type, DevExpress.Office.Design.Internal.UnitPrecisionDictionary unitPrecisionDictionary)
        {
            this.unitPrecisionDictionary = unitPrecisionDictionary;
            this.unitType = this.GetValidType(type);
            TryParseFloatValue(stringValue, out this.stringValue, out this.isNegative);
            this.stringValue = this.GetValidValueString(this.stringValue, this.UnitPrecisionDictionary[this.UnitType] + 1);
            this.stringValue = this.TrimInsignificantZeros(this.StringValue);
            this.isValueInPercent = false;
        }

        protected UIUnit(string value, DocumentUnit type, bool isNegative)
        {
            this.stringValue = value;
            this.unitType = this.GetValidType(type);
            this.isNegative = isNegative;
            this.isValueInPercent = false;
        }

        public static UIUnit Create(string text, DocumentUnit defaultUnitType, DevExpress.Office.Design.Internal.UnitPrecisionDictionary unitPrecisionDictionary) => 
            Create(text, defaultUnitType, unitPrecisionDictionary, false);

        public static UIUnit Create(string text, DocumentUnit defaultUnitType, DevExpress.Office.Design.Internal.UnitPrecisionDictionary unitPrecisionDictionary, bool isValueInPercent)
        {
            DocumentUnit type = defaultUnitType;
            return new UIUnit(GetUnitTypeAndStringValue(text, defaultUnitType, out type, isValueInPercent), type, unitPrecisionDictionary) { IsValueInPercent = isValueInPercent };
        }

        private string CreateZeroStringValue(int maxDigitsAfterDecimalSeparator)
        {
            string str = "0";
            if (maxDigitsAfterDecimalSeparator > 0)
            {
                str = str + DecimalSeparator.ToString() + new string('0', maxDigitsAfterDecimalSeparator);
            }
            return str;
        }

        private static string Decrement(string number)
        {
            string str = string.Empty;
            for (int i = number.Length - 1; i >= 0; i--)
            {
                char ch = number[i];
                if (ch == '0')
                {
                    str = "9" + str;
                }
                else
                {
                    if ((ch >= '0') && (ch <= '9'))
                    {
                        char ch2 = (char) (ch - '\x0001');
                        return (number.Substring(0, i) + ch2.ToString() + str);
                    }
                    str = ch.ToString() + str;
                }
            }
            return null;
        }

        private string GetRightPart(string[] stringValueParts, int maxDigitsAfterDecimalSeparator) => 
            (stringValueParts.Length == 2) ? stringValueParts[1] : string.Empty;

        protected internal static string GetTextAbbreviation(DocumentUnit unitType) => 
            GetTextAbbreviation(unitType, false);

        protected internal static string GetTextAbbreviation(DocumentUnit unitType, bool isValueInPercent) => 
            !isValueInPercent ? OfficeLocalizer.GetString(UnitAbbreviationDictionary[unitType]) : OfficeLocalizer.GetString(OfficeStringId.UnitAbbreviation_Percent);

        protected internal static string GetTextCaption(DocumentUnit unitType) => 
            OfficeLocalizer.GetString(UnitCaptionDictionary[unitType]);

        private static string GetUnitTypeAndStringValue(string text, DocumentUnit defaultUnitType, out DocumentUnit type, bool isValueInPercent)
        {
            text ??= string.Empty;
            type = defaultUnitType;
            string str = text;
            foreach (DocumentUnit unit in UnitAbbreviationDictionary.Keys)
            {
                string str2 = GetTextAbbreviation(unit, isValueInPercent).Trim();
                int length = text.LastIndexOf(str2);
                if ((length != -1) && (length == (text.Length - str2.Length)))
                {
                    str = text.Substring(0, length);
                    type = unit;
                    break;
                }
            }
            return str;
        }

        private DocumentUnit GetValidType(DocumentUnit type) => 
            (type != DocumentUnit.Document) ? type : DocumentUnit.Inch;

        protected internal virtual string GetValidValueString(string stringValue, int maxDigitsAfterDecimalSeparator)
        {
            if (string.IsNullOrEmpty(stringValue))
            {
                return this.CreateZeroStringValue(maxDigitsAfterDecimalSeparator);
            }
            char[] separator = new char[] { DecimalSeparator };
            string[] stringValueParts = stringValue.Split(separator);
            if (stringValueParts.Length > 2)
            {
                return this.CreateZeroStringValue(maxDigitsAfterDecimalSeparator);
            }
            string str = string.IsNullOrEmpty(stringValueParts[0]) ? "0" : stringValueParts[0];
            string rightPart = this.GetRightPart(stringValueParts, maxDigitsAfterDecimalSeparator);
            if (!this.IsIntegerValue(str) || (!string.IsNullOrEmpty(rightPart) && !this.IsIntegerValue(rightPart)))
            {
                return this.CreateZeroStringValue(maxDigitsAfterDecimalSeparator);
            }
            rightPart = this.GetValueOfDesiredLength(rightPart, maxDigitsAfterDecimalSeparator);
            return ((maxDigitsAfterDecimalSeparator >= 1) ? $"{str}{DecimalSeparator}{rightPart}" : str);
        }

        private string GetValueOfDesiredLength(string value, int desiredLength)
        {
            if (value.Length > desiredLength)
            {
                return value.Substring(0, desiredLength);
            }
            int count = desiredLength - value.Length;
            return (value + new string('0', count));
        }

        private static string Increment(string number)
        {
            string str = string.Empty;
            for (int i = number.Length - 1; i >= 0; i--)
            {
                char ch = number[i];
                if (ch == '9')
                {
                    str = "0" + str;
                }
                else
                {
                    if ((ch >= '0') && (ch <= '9'))
                    {
                        char ch2 = (char) (ch + '\x0001');
                        return (number.Substring(0, i) + ch2.ToString() + str);
                    }
                    str = ch.ToString() + str;
                }
            }
            return ("1" + str);
        }

        private bool IsIntegerValue(string value)
        {
            int length = value.Length;
            for (int i = 0; i < length; i++)
            {
                char ch = value[i];
                if ((ch < '0') || (ch > '9'))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsZeroValue(string resultValue)
        {
            char[] trimChars = new char[] { '0' };
            string str = resultValue.Trim(trimChars);
            return ((str.Length == 0) || (str == DecimalSeparator.ToString()));
        }

        public static UIUnit operator --(UIUnit unit)
        {
            string operationValidStringValue = unit.OperationValidStringValue;
            if (IsZeroValue(operationValidStringValue))
            {
                unit.SetIsNegative(true);
            }
            if (!unit.IsNegative && (unit.RoundedStringValue != unit.StringValue))
            {
                unit.TruncValue();
                return unit;
            }
            operationValidStringValue = !unit.isNegative ? Decrement(operationValidStringValue) : Increment(operationValidStringValue);
            unit.SetStringValue(operationValidStringValue);
            return unit;
        }

        public static UIUnit operator ++(UIUnit unit)
        {
            string operationValidStringValue = unit.OperationValidStringValue;
            if (unit.IsNegative && (unit.RoundedStringValue != unit.StringValue))
            {
                unit.TruncValue();
                return unit;
            }
            if (IsZeroValue(operationValidStringValue))
            {
                unit.SetIsNegative(false);
            }
            if (!unit.isNegative)
            {
                operationValidStringValue = Increment(operationValidStringValue);
            }
            else
            {
                operationValidStringValue = Decrement(operationValidStringValue);
                if (IsZeroValue(operationValidStringValue))
                {
                    unit.SetIsNegative(false);
                }
            }
            unit.SetStringValue(operationValidStringValue);
            return unit;
        }

        protected internal virtual void SetIsNegative(bool isNegative)
        {
            this.isNegative = isNegative;
        }

        protected internal virtual void SetStringValue(string stringValue)
        {
            this.stringValue = stringValue;
        }

        public override string ToString()
        {
            string str = this.IsNegative ? "-" : string.Empty;
            return $"{str}{this.TrimInsignificantZeros(this.StringValue)}{GetTextAbbreviation(this.UnitType, this.IsValueInPercent)}";
        }

        private string TrimBeginInsignificantZeros(string stringValue)
        {
            int length = stringValue.Length;
            int startIndex = 0;
            startIndex = 0;
            while ((startIndex < length) && (stringValue[startIndex] == '0'))
            {
                startIndex++;
            }
            if ((startIndex == length) || (startIndex == 0))
            {
                return stringValue;
            }
            if (!char.IsDigit(stringValue[startIndex]))
            {
                startIndex--;
            }
            return stringValue.Substring(startIndex, length - startIndex);
        }

        private string TrimEndInsignificantZeros(string stringValue)
        {
            if (stringValue.IndexOf(DecimalSeparator) == -1)
            {
                return stringValue;
            }
            char[] trimChars = new char[] { '0' };
            string str = stringValue.TrimEnd(trimChars);
            int length = str.Length;
            return ((str[length - 1] != DecimalSeparator) ? str : str.Substring(0, length - 1));
        }

        private string TrimInsignificantZeros(string stringValue)
        {
            string str = this.TrimEndInsignificantZeros(stringValue);
            return this.TrimBeginInsignificantZeros(str);
        }

        internal void TruncValue()
        {
            this.stringValue = this.OperationValidStringValue;
        }

        internal static bool TryParse(string text, DocumentUnit defaultUnitType, out UIUnit result) => 
            TryParse(text, defaultUnitType, out result, false);

        internal static bool TryParse(string text, DocumentUnit defaultUnitType, out UIUnit result, bool isValueInPercent)
        {
            result = null;
            DocumentUnit type = defaultUnitType;
            bool isNegative = false;
            string resultStringValue = string.Empty;
            if (!TryParseFloatValue(GetUnitTypeAndStringValue(text, defaultUnitType, out type, isValueInPercent), out resultStringValue, out isNegative))
            {
                return false;
            }
            result = new UIUnit(resultStringValue, type, isNegative);
            result.IsValueInPercent = isValueInPercent;
            return true;
        }

        protected static bool TryParseFloatValue(string sourceStringValue, out string resultStringValue, out bool isNegative)
        {
            isNegative = false;
            resultStringValue = "0";
            string str = DecimalSeparator.ToString();
            str.Replace(".", @"\.");
            string pattern = $"^(?<sign>\+|-)?(?<value>\d*{str}?(\d+)?)$";
            Match match = Regex.Match(sourceStringValue.Trim(), pattern);
            if (!match.Success)
            {
                return false;
            }
            GroupCollection groups = match.Groups;
            Group group = groups["sign"];
            Group group2 = groups["value"];
            if (group.Success && (group.Value == "-"))
            {
                isNegative = true;
            }
            resultStringValue = group2.Value.Trim();
            if ((resultStringValue.Length == 0) || ((resultStringValue.Length == 1) && (resultStringValue[0] == DecimalSeparator)))
            {
                isNegative = false;
            }
            return true;
        }

        public static DevExpress.Office.Design.Internal.UnitAbbreviationDictionary UnitAbbreviationDictionary =>
            unitAbbreviationDictionary;

        public DevExpress.Office.Design.Internal.UnitPrecisionDictionary UnitPrecisionDictionary =>
            this.unitPrecisionDictionary;

        public static DevExpress.Office.Design.Internal.UnitCaptionDictionary UnitCaptionDictionary =>
            unitCaptionDictionary;

        public static char DecimalSeparator =>
            CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];

        public DocumentUnit UnitType =>
            this.unitType;

        protected internal bool IsValueInPercent
        {
            get => 
                this.isValueInPercent;
            set => 
                this.isValueInPercent = value;
        }

        internal string OperationValidStringValue =>
            this.GetValidValueString(this.stringValue, this.UnitPrecisionDictionary[this.UnitType]);

        internal string StringValue =>
            this.TrimInsignificantZeros(this.stringValue);

        internal string RoundedStringValue =>
            this.TrimInsignificantZeros(this.OperationValidStringValue);

        public bool IsNegative =>
            this.isNegative;

        public float Value
        {
            get
            {
                float result = 0f;
                return (float.TryParse(this.StringValue, out result) ? (!this.IsNegative ? result : (-1f * result)) : 0f);
            }
        }

        public virtual bool IsValidValue
        {
            get
            {
                float result = 0f;
                return (float.TryParse(this.StringValue, out result) && (!float.IsInfinity(result) && !float.IsNaN(result)));
            }
        }
    }
}

