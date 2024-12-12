namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class FormatStringConverterExtension : MarkupExtension
    {
        private bool allowSimpleFormatString = true;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            FormatStringConverter converter1 = new FormatStringConverter();
            converter1.FormatString = this.FormatString;
            converter1.AllowSimpleFormatString = this.AllowSimpleFormatString;
            converter1.SplitPascalCase = this.SplitPascalCase;
            converter1.OutStringCaseFormat = this.OutStringCaseFormat;
            return converter1;
        }

        public string FormatString { get; set; }

        public bool AllowSimpleFormatString
        {
            get => 
                this.allowSimpleFormatString;
            set => 
                this.allowSimpleFormatString = value;
        }

        public bool SplitPascalCase { get; set; }

        public FormatStringConverter.TextCaseFormat OutStringCaseFormat { get; set; }
    }
}

