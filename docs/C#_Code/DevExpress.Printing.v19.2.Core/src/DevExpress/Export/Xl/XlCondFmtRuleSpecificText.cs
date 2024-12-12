namespace DevExpress.Export.Xl
{
    using System;

    public class XlCondFmtRuleSpecificText : XlCondFmtRuleWithFormatting
    {
        private string text;

        public XlCondFmtRuleSpecificText(XlCondFmtSpecificTextType ruleType, string text) : base((XlCondFmtType) ruleType)
        {
            this.text = string.Empty;
            this.Text = text;
        }

        public string Text
        {
            get => 
                this.text;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.text = string.Empty;
                }
                else
                {
                    this.text = value;
                }
            }
        }
    }
}

