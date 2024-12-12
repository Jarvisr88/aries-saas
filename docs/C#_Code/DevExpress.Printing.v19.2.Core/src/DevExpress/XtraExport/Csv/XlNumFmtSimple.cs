namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    internal abstract class XlNumFmtSimple : List<IXlNumFmtElement>, IXlNumFmt
    {
        protected XlNumFmtSimple()
        {
        }

        protected XlNumFmtSimple(IEnumerable<IXlNumFmtElement> elements)
        {
            base.AddRange(elements);
        }

        public bool EnclosedInParenthesesForPositive()
        {
            bool flag = false;
            bool flag2 = false;
            for (int i = 0; i < base.Count; i++)
            {
                IXlNumFmtElement element = base[i];
                XlNumFmtQuotedText text = element as XlNumFmtQuotedText;
                if (text == null)
                {
                    XlNumFmtBackslashedText text2 = element as XlNumFmtBackslashedText;
                    if (text2 != null)
                    {
                        if (text2.Text == '(')
                        {
                            if (flag2)
                            {
                                return true;
                            }
                            flag = true;
                        }
                        if (text2.Text == ')')
                        {
                            if (flag)
                            {
                                return true;
                            }
                            flag2 = true;
                        }
                    }
                }
                else
                {
                    foreach (char ch in text.Text)
                    {
                        if (ch == '(')
                        {
                            if (flag2)
                            {
                                return true;
                            }
                            flag = true;
                        }
                        if (ch == ')')
                        {
                            if (flag)
                            {
                                return true;
                            }
                            flag2 = true;
                        }
                    }
                }
            }
            return false;
        }

        public virtual XlNumFmtResult Format(XlVariantValue value, CultureInfo culture)
        {
            XlNumFmtResult result;
            if (value.IsEmpty)
            {
                return new XlNumFmtResult { Text = string.Empty };
            }
            if (value.IsError)
            {
                return new XlNumFmtResult { Text = value.ErrorValue.Name };
            }
            try
            {
                result = this.FormatCore(value, culture);
                if (result.IsError)
                {
                    result.Text = "#";
                }
            }
            catch
            {
                result = new XlNumFmtResult {
                    IsError = true,
                    Text = "#"
                };
            }
            return result;
        }

        public abstract XlNumFmtResult FormatCore(XlVariantValue value, CultureInfo culture);
        public virtual XlVariantValue Round(XlVariantValue value, CultureInfo culture) => 
            value;

        public abstract XlNumFmtType Type { get; }
    }
}

