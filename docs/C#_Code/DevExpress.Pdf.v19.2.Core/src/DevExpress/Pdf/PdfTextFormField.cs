namespace DevExpress.Pdf
{
    using DevExpress.Pdf.ContentGeneration;
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;

    public class PdfTextFormField : PdfInteractiveFormField
    {
        internal const string Type = "Tx";
        private const string maxLenDictionaryKey = "MaxLen";
        private const PdfInteractiveFormFieldFlags zeroLengthFlagsCheck = (PdfInteractiveFormFieldFlags.Comb | PdfInteractiveFormFieldFlags.FileSelect | PdfInteractiveFormFieldFlags.Password | PdfInteractiveFormFieldFlags.Multiline);
        private const int maxMultilineFieldAutoFontSize = 14;
        private const double multilineFieldAutoFontSizeStep = 0.5;
        private readonly PdfTextFormField valuesProvider;
        private readonly int? maxLen;
        private readonly string defaultText;
        private string text;

        internal PdfTextFormField(PdfInteractiveForm form, PdfInteractiveFormField parent, PdfReaderDictionary dictionary, PdfObjectReference valueReference) : base(form, parent, dictionary, valueReference)
        {
            string text = GetText(dictionary, "V");
            string text2 = text;
            if (text == null)
            {
                string local1 = text;
                text2 = string.Empty;
            }
            this.text = text2;
            this.defaultText = GetText(dictionary, "DV");
            this.maxLen = dictionary.GetInteger("MaxLen");
            if (this.maxLen != null)
            {
                int num = this.maxLen.Value;
                if ((num < 0) || ((num == 0) && ((base.Flags & (PdfInteractiveFormFieldFlags.Comb | PdfInteractiveFormFieldFlags.FileSelect | PdfInteractiveFormFieldFlags.Password | PdfInteractiveFormFieldFlags.Multiline)) == PdfInteractiveFormFieldFlags.Comb)))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            PdfTextFormField valuesProvider = base.ValuesProvider as PdfTextFormField;
            PdfTextFormField field2 = valuesProvider;
            if (valuesProvider == null)
            {
                PdfTextFormField local2 = valuesProvider;
                field2 = this;
            }
            this.valuesProvider = field2;
        }

        internal PdfTextFormField(PdfInteractiveFormField parent, IPdfExportFontProvider fontSearch, PdfDocument document, PdfAcroFormTextBoxField textBoxField) : base(parent, fontSearch, document, textBoxField)
        {
            this.text = string.Empty;
            PdfTextFormField valuesProvider = base.ValuesProvider as PdfTextFormField;
            PdfTextFormField field2 = valuesProvider;
            if (valuesProvider == null)
            {
                PdfTextFormField local1 = valuesProvider;
                field2 = this;
            }
            this.valuesProvider = field2;
            int maxLength = textBoxField.MaxLength;
            if (maxLength > 0)
            {
                this.maxLen = new int?(maxLength);
            }
            this.SetValue(textBoxField.Text, fontSearch, true);
        }

        protected internal override void Accept(IPdfInteractiveFormFieldVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override bool AcceptValue(object value) => 
            base.AcceptValue(value) || (value is IList<string>);

        protected internal override double CalcFontSize(string text, PdfRectangle layoutRect, PdfExportFont font)
        {
            if (!base.Flags.HasFlag(PdfInteractiveFormFieldFlags.Multiline))
            {
                return base.CalcFontSize(text, layoutRect, font);
            }
            double num = Math.Min((double) (layoutRect.Height * 0.75), (double) 14.0);
            PdfStringFormat format = new PdfStringFormat(PdfStringFormatFlags.NoClip | PdfStringFormatFlags.LineLimit) {
                LeadingMarginFactor = 0.0,
                TrailingMarginFactor = 0.0,
                Trimming = PdfStringTrimming.None
            };
            PdfFontMetrics metrics = CreateMultilineFormFieldMetrics(font, base.TextState);
            DXSizeF ef = new DXSizeF((float) layoutRect.Width, float.MaxValue);
            int num2 = 0;
            int num3 = 0x15;
            while (num2 < num3)
            {
                int v = num2 + ((num3 - num2) / 2);
                DXSizeF ef2 = TextWidthHelper.MeasureString(text, new DXSizeF?(ef), font, metrics, (float) GetAutoFontSize(v), base.TextState);
                if (ef2.Height <= layoutRect.Height)
                {
                    num2 = v + 1;
                    continue;
                }
                num3 = v;
            }
            return Math.Max(GetAutoFontSize(Math.Max(num2 - 1, 0)), 4.0);
        }

        internal static PdfFontMetrics CreateMultilineFormFieldMetrics(PdfExportFont font, PdfInteractiveFormFieldTextState textState)
        {
            PdfSetTextFontCommand fontCommand = textState.FontCommand;
            return (((fontCommand == null) || (fontCommand.Font == null)) ? new PdfFontMetrics(font.Metrics, font.Font.Metrics.Height / 1000.0) : new PdfFontMetrics(font.Metrics, fontCommand.Font.Metrics.Height / 1000.0));
        }

        protected internal override void FillDictionary(PdfWriterDictionary dictionary)
        {
            base.FillDictionary(dictionary);
            if (!string.IsNullOrEmpty(this.text))
            {
                dictionary.Add("V", this.text);
            }
            if (!string.IsNullOrEmpty(this.defaultText))
            {
                dictionary.Add("DV", this.defaultText);
            }
            dictionary.AddNullable<int>("MaxLen", this.maxLen);
        }

        private static double GetAutoFontSize(int v)
        {
            double num = 10.0;
            double num2 = num / 0.5;
            return (4.0 + ((num * v) / num2));
        }

        private static string GetText(PdfReaderDictionary dictionary, string key)
        {
            object obj2;
            if (!dictionary.TryGetValue(key, out obj2))
            {
                return null;
            }
            obj2 = dictionary.Objects.TryResolve(obj2, null);
            PdfName name = obj2 as PdfName;
            if (name != null)
            {
                return name.Name;
            }
            byte[] buffer = obj2 as byte[];
            return ((buffer != null) ? PdfDocumentReader.ConvertToString(buffer) : null);
        }

        protected internal override void SetValue(object value, IPdfExportFontProvider fontSearch)
        {
            base.SetValue(value, fontSearch);
            this.SetValue(value, fontSearch, false);
        }

        private void SetValue(object value, IPdfExportFontProvider fontSearch, bool forceSet)
        {
            string[] strArray;
            if (!this.AcceptValue(value))
            {
                throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectTextFormFieldValue));
            }
            string newValue = value as string;
            if (newValue != null)
            {
                string[] separator = new string[] { "\r\n", "\r", "\n" };
                strArray = newValue.Split(separator, StringSplitOptions.None);
            }
            else
            {
                IList<string> list = value as IList<string>;
                if (list == null)
                {
                    strArray = null;
                }
                else
                {
                    int count = list.Count;
                    strArray = new string[count];
                    for (int i = 0; i < count; i++)
                    {
                        strArray[i] = list[i];
                    }
                }
            }
            if (strArray != null)
            {
                if (base.Flags.HasFlag(PdfInteractiveFormFieldFlags.Password) && (strArray.Length != 0))
                {
                    int length = strArray.Length;
                    string[] strArray2 = new string[length];
                    int index = 0;
                    while (true)
                    {
                        if (index >= length)
                        {
                            strArray = strArray2;
                            break;
                        }
                        strArray2[index] = new string('*', strArray[index].Length);
                        index++;
                    }
                }
                newValue = string.Join("\r", strArray);
            }
            newValue ??= string.Empty;
            string text = this.valuesProvider.text;
            PdfInteractiveFormFieldValueChangingEventArgs args = new PdfInteractiveFormFieldValueChangingEventArgs(base.FullName, text, newValue);
            if (forceSet || ((text != newValue) && (!Equals(text, value) && base.RaiseFieldChanging(args))))
            {
                string str3 = args.NewValue as string;
                this.valuesProvider.text = str3;
                this.valuesProvider.SetFormModifiedState(fontSearch);
                base.RaiseFieldChanged(text, str3);
            }
        }

        public int? MaxLen =>
            this.valuesProvider.maxLen;

        public string DefaultText =>
            this.valuesProvider.defaultText;

        public string Text =>
            this.valuesProvider.text;

        protected internal override object Value =>
            this.valuesProvider.text;

        protected internal override object DefaultValue =>
            this.valuesProvider.defaultText;

        protected override string FieldType =>
            "Tx";
    }
}

