namespace DevExpress.Export.Xl
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;

    public class XlCustomFilterCriteria
    {
        private static Dictionary<char, string> replacementTable = CreateReplacementTable();
        private Regex wildcardRegex;
        private XlVariantValue criteriaValue;

        public XlCustomFilterCriteria(XlFilterOperator filterOperator, XlVariantValue value)
        {
            this.FilterOperator = filterOperator;
            this.Value = value;
        }

        private int CalculateSubtractionSign(IXlCell cell)
        {
            if (cell == null)
            {
                return -2147483648;
            }
            XlVariantValue value2 = cell.Value;
            return ((!this.Value.IsError || !value2.IsError) ? ((!this.Value.IsNumeric || !value2.IsNumeric) ? -2147483648 : Math.Sign((double) (value2.NumericValue - this.Value.NumericValue))) : Math.Sign((int) (value2.ErrorValue.Type - this.Value.ErrorValue.Type)));
        }

        private int CalculateSubtractionSignWithStringComparison(int sign, IXlCell cell, IXlCellFormatter cellFormatter)
        {
            if (((sign == -2147483648) && (cell != null)) && (cell.Value.IsText && this.Value.IsText))
            {
                sign = this.StringCompare(cell, cellFormatter);
            }
            return sign;
        }

        private static string CreatePattern(string wildcardString)
        {
            StringBuilder builder = new StringBuilder();
            int length = wildcardString.Length;
            for (int i = 0; i < length; i++)
            {
                string str;
                char key = wildcardString[i];
                if (!replacementTable.TryGetValue(key, out str))
                {
                    builder.Append(key);
                }
                else if ((i <= 0) || ((wildcardString[i - 1] != '~') || ((key != '*') && (key != '?'))))
                {
                    builder.Append(str);
                }
                else
                {
                    builder[builder.Length - 1] = '\\';
                    builder.Append(key);
                }
            }
            return builder.ToString();
        }

        private static Dictionary<char, string> CreateReplacementTable() => 
            new Dictionary<char, string> { 
                { 
                    '*',
                    ".*"
                },
                { 
                    '?',
                    "."
                },
                { 
                    '\\',
                    @"\\"
                },
                { 
                    '[',
                    @"\["
                },
                { 
                    ']',
                    @"\]"
                },
                { 
                    '(',
                    @"\("
                },
                { 
                    ')',
                    @"\)"
                },
                { 
                    '{',
                    @"\{"
                },
                { 
                    '}',
                    @"\}"
                },
                { 
                    '.',
                    @"\."
                },
                { 
                    '+',
                    @"\+"
                },
                { 
                    '|',
                    @"\|"
                },
                { 
                    '$',
                    @"\$"
                },
                { 
                    '^',
                    @"\^"
                }
            };

        private static bool Match(Regex regex, string compare)
        {
            System.Text.RegularExpressions.Match match = regex.Match(compare);
            if (match == null)
            {
                return false;
            }
            int length = match.Length;
            return ((length != compare.Length) ? ((length < compare.Length) && (((compare[length] == '\r') || (compare[length] == '\n')) ? match.Success : ((match.Index > 0) && (((compare[match.Index - 1] == '\r') || (compare[match.Index - 1] == '\n')) && match.Success)))) : match.Success);
        }

        internal bool MeetCriteria(IXlCell cell, IXlCellFormatter cellFormatter)
        {
            int sign = this.CalculateSubtractionSign(cell);
            switch (this.FilterOperator)
            {
                case XlFilterOperator.GreaterThan:
                    return (this.CalculateSubtractionSignWithStringComparison(sign, cell, cellFormatter) == 1);

                case XlFilterOperator.GreaterThanOrEqual:
                    return (this.CalculateSubtractionSignWithStringComparison(sign, cell, cellFormatter) >= 0);

                case XlFilterOperator.LessThan:
                    return (this.CalculateSubtractionSignWithStringComparison(sign, cell, cellFormatter) == -1);

                case XlFilterOperator.LessThanOrEqual:
                    sign = this.CalculateSubtractionSignWithStringComparison(sign, cell, cellFormatter);
                    return ((sign == 0) || (sign == -1));

                case XlFilterOperator.NotEqual:
                    return ((sign == -2147483648) ? !this.StringEquals(cell, cellFormatter) : (sign != 0));
            }
            return ((sign == -2147483648) ? this.StringEquals(cell, cellFormatter) : (sign == 0));
        }

        private int StringCompare(IXlCell cell, IXlCellFormatter cellFormatter)
        {
            string str = string.Empty;
            if (cell != null)
            {
                str = (cellFormatter != null) ? cellFormatter.GetFormattedValue(cell) : cell.Value.ToText().TextValue;
            }
            return StringExtensions.CompareInvariantCultureIgnoreCase(str, this.Value.ToText().TextValue);
        }

        private bool StringEquals(IXlCell cell, IXlCellFormatter cellFormatter)
        {
            bool flag = this.wildcardRegex != null;
            string str = string.Empty;
            if (cell != null)
            {
                if (cell.Value.IsNumeric & flag)
                {
                    return false;
                }
                str = (cellFormatter != null) ? cellFormatter.GetFormattedValue(cell) : cell.Value.ToText().TextValue;
            }
            return (!flag ? (string.IsNullOrEmpty(str) ? this.IsBlank : (str == this.Value.ToText().TextValue)) : Match(this.wildcardRegex, str));
        }

        private void UpdateWildcard()
        {
            this.wildcardRegex = null;
            if (this.Value.IsText)
            {
                string textValue = this.Value.TextValue;
                if (!string.IsNullOrEmpty(textValue))
                {
                    char[] anyOf = new char[] { '*', '?' };
                    if (textValue.IndexOfAny(anyOf) != -1)
                    {
                        string pattern = CreatePattern(textValue);
                        this.wildcardRegex = new Regex(pattern, RegexOptions.IgnoreCase);
                    }
                }
            }
        }

        public XlFilterOperator FilterOperator { get; set; }

        public XlVariantValue Value
        {
            get => 
                this.criteriaValue;
            set
            {
                this.criteriaValue = value;
                this.UpdateWildcard();
            }
        }

        private bool IsBlank
        {
            get
            {
                string textValue = this.Value.ToText().TextValue;
                return (string.IsNullOrEmpty(textValue) || (textValue == " "));
            }
        }
    }
}

