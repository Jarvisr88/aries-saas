namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfChoiceFormField : PdfInteractiveFormField
    {
        internal const string Type = "Ch";
        private const string topIndexDictionaryKey = "TI";
        private const string selectedIndicesDictionaryKey = "I";
        private readonly PdfChoiceFormField valuesProvider;
        private readonly IList<string> defaultValues;
        private readonly IList<PdfOptionsFormFieldOption> options;
        private int topIndex;
        private IList<int> selectedIndices;
        private IList<string> selectedValues;

        internal PdfChoiceFormField(PdfAcroFormComboBoxField comboBox, IPdfExportFontProvider fontSearch, PdfDocument document, PdfInteractiveFormField parent) : this(parent, fontSearch, document, comboBox)
        {
            IList<string> selectedValues = comboBox.SelectedValues;
            if ((selectedValues != null) && (selectedValues.Count > 0))
            {
                this.SetValue(selectedValues[0], fontSearch);
            }
            else
            {
                this.valuesProvider.SetFormModifiedState(fontSearch);
            }
        }

        internal PdfChoiceFormField(PdfInteractiveForm form, PdfInteractiveFormField parent, PdfReaderDictionary dictionary, PdfObjectReference valueReference) : base(form, parent, dictionary, valueReference)
        {
            int? integer = dictionary.GetInteger("TI");
            this.topIndex = (integer != null) ? integer.GetValueOrDefault() : 0;
            bool flag = false;
            List<string> values = new List<string>();
            IList<object> array = dictionary.GetArray("Opt");
            if (array == null)
            {
                if (this.topIndex != 0)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            else
            {
                PdfObjectCollection objects = dictionary.Objects;
                this.options = new List<PdfOptionsFormFieldOption>(array.Count);
                List<string> list4 = new List<string>();
                foreach (object obj2 in array)
                {
                    PdfOptionsFormFieldOption item = new PdfOptionsFormFieldOption(objects.TryResolve(obj2, null));
                    values.Add(item.Text);
                    string exportText = item.ExportText;
                    if (list4.Contains(exportText))
                    {
                        flag = true;
                    }
                    else
                    {
                        list4.Add(exportText);
                    }
                    this.options.Add(item);
                }
                if ((this.topIndex < 0) || ((this.topIndex > 0) && (this.topIndex >= this.options.Count)))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            bool flag2 = (base.Flags & PdfInteractiveFormFieldFlags.MultiSelect) == PdfInteractiveFormFieldFlags.MultiSelect;
            this.selectedValues = this.GetValues(dictionary, "V", values);
            IList<object> list3 = dictionary.GetArray("I");
            if (list3 == null)
            {
                if (((this.selectedValues != null) & flag) & flag2)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            else
            {
                this.selectedIndices = new List<int>(list3.Count);
                foreach (object obj3 in list3)
                {
                    if (!(obj3 is int))
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    int item = (int) obj3;
                    if (item < 0)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.selectedIndices.Add(item);
                }
            }
            if (flag2)
            {
                int num2 = (this.selectedIndices == null) ? 0 : this.selectedIndices.Count;
                int num3 = (this.selectedValues == null) ? 0 : this.selectedValues.Count;
                if (num2 != num3)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                for (int i = 0; i < num3; i++)
                {
                    if (values[this.selectedIndices[i]] != this.selectedValues[i])
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                }
            }
            this.defaultValues = this.GetValues(dictionary, "DV", values);
            PdfChoiceFormField valuesProvider = base.ValuesProvider as PdfChoiceFormField;
            PdfChoiceFormField field2 = valuesProvider;
            if (valuesProvider == null)
            {
                PdfChoiceFormField local1 = valuesProvider;
                field2 = this;
            }
            this.valuesProvider = field2;
        }

        private PdfChoiceFormField(PdfInteractiveFormField parent, IPdfExportFontProvider fontSearch, PdfDocument document, PdfAcroFormChoiceField choiceField) : base(parent, fontSearch, document, choiceField)
        {
            this.options = choiceField.Values;
            if (choiceField.IsMultiSelect)
            {
                this.selectedIndices = new List<int>();
            }
            this.valuesProvider = this;
        }

        internal PdfChoiceFormField(PdfInteractiveFormField parent, IPdfExportFontProvider fontSearch, PdfDocument document, PdfAcroFormListBoxField listBox) : this(parent, fontSearch, document, (PdfAcroFormChoiceField) listBox)
        {
            this.topIndex = listBox.TopIndex;
            if (listBox.MultiSelect)
            {
                this.selectedIndices = new List<int>();
            }
            this.SetValue(listBox.SelectedValues, fontSearch);
        }

        protected internal override void Accept(IPdfInteractiveFormFieldVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override bool AcceptValue(object value) => 
            base.AcceptValue(value) || (value is IList<string>);

        private string ConvertToString(object value, IList<string> values)
        {
            string item = value as string;
            if (item == null)
            {
                byte[] buffer = value as byte[];
                if (buffer == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                item = PdfDocumentReader.ConvertToTextString(buffer);
            }
            if (((base.Flags & PdfInteractiveFormFieldFlags.Combo) == PdfInteractiveFormFieldFlags.None) && !values.Contains(item))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return item;
        }

        protected internal override void FillDictionary(PdfWriterDictionary dictionary)
        {
            base.FillDictionary(dictionary);
            dictionary.Add("TI", this.topIndex, 0);
            Func<PdfOptionsFormFieldOption, object> converter = <>c.<>9__37_0;
            if (<>c.<>9__37_0 == null)
            {
                Func<PdfOptionsFormFieldOption, object> local1 = <>c.<>9__37_0;
                converter = <>c.<>9__37_0 = value => value.Write();
            }
            dictionary.AddEnumerable<PdfOptionsFormFieldOption>("Opt", this.options, converter);
            Func<int, object> func2 = <>c.<>9__37_1;
            if (<>c.<>9__37_1 == null)
            {
                Func<int, object> local2 = <>c.<>9__37_1;
                func2 = <>c.<>9__37_1 = value => value;
            }
            dictionary.AddList<int>("I", this.selectedIndices, func2);
            this.WriteValues(dictionary, "V", this.selectedValues);
            this.WriteValues(dictionary, "DV", this.defaultValues);
        }

        private IList<string> GetValues(PdfReaderDictionary dictionary, string key, IList<string> values)
        {
            object obj2;
            if (!dictionary.TryGetValue(key, out obj2))
            {
                return null;
            }
            obj2 = dictionary.Objects.TryResolve(obj2, null);
            if (obj2 == null)
            {
                return null;
            }
            List<string> list = new List<string>();
            IList<object> list2 = obj2 as IList<object>;
            if (list2 == null)
            {
                list.Add(this.ConvertToString(obj2, values));
            }
            else
            {
                foreach (object obj3 in list2)
                {
                    list.Add(this.ConvertToString(obj3, values));
                }
            }
            return list;
        }

        internal void SetTopIndex(int topIndex, IPdfExportFontProvider fontSearch)
        {
            int num = topIndex;
            IList<string> selectedValues = this.valuesProvider.selectedValues;
            IList<PdfOptionsFormFieldOption> options = this.valuesProvider.options;
            if ((selectedValues != null) && (options != null))
            {
                int count = options.Count;
                for (int i = 0; i < count; i++)
                {
                    if (selectedValues.Contains(options[i].Text))
                    {
                        int num8;
                        double num4 = this.valuesProvider.TextState.FontSize + 1.0;
                        int num5 = PdfPageTreeObject.NormalizeRotate((base.Widget.AppearanceCharacteristics == null) ? 0 : base.Widget.AppearanceCharacteristics.RotationAngle);
                        int num7 = ((int) ((((num5 == 90) || (num5 == 270)) ? base.Widget.Rect.Width : base.Widget.Rect.Height) / num4)) - 1;
                        if ((i - num7) < 0)
                        {
                            num8 = 0;
                        }
                        int num9 = (i <= num7) ? i : (i + num7);
                        if (num9 >= count)
                        {
                            num9 = count - 1;
                        }
                        int num10 = topIndex;
                        if (num10 < num8)
                        {
                            num = num8;
                        }
                        else if (num10 > num9)
                        {
                            num = num9;
                        }
                        break;
                    }
                }
            }
            if (this.valuesProvider.topIndex != num)
            {
                this.valuesProvider.topIndex = num;
                this.valuesProvider.SetFormModifiedState(fontSearch);
            }
        }

        protected internal override void SetValue(object value, IPdfExportFontProvider fontSearch)
        {
            base.SetValue(value, fontSearch);
            if (!this.AcceptValue(value))
            {
                throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectChoiceFormFieldValue));
            }
            string item = value as string;
            if (!base.Flags.HasFlag(PdfInteractiveFormFieldFlags.Combo) || ((item == null) || ((item.Length != 0) || (this.valuesProvider.selectedValues != null))))
            {
                bool flag;
                IList<string> list1;
                if (item == null)
                {
                    list1 = value as IList<string>;
                }
                else
                {
                    string[] textArray1 = new string[] { item };
                    list1 = textArray1;
                }
                IList<string> list = list1;
                IList<string> selectedValues = this.valuesProvider.selectedValues;
                if (selectedValues == null)
                {
                    flag = list != null;
                }
                else if (list == null)
                {
                    flag = true;
                }
                else
                {
                    int count = list.Count;
                    flag = count != selectedValues.Count;
                    if (!flag)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            if (!selectedValues.Contains(list[i]))
                            {
                                flag = true;
                            }
                        }
                    }
                }
                if (flag)
                {
                    selectedValues = new List<string>();
                    IList<int> list4 = new List<int>();
                    if (list != null)
                    {
                        IList<PdfOptionsFormFieldOption> options = this.valuesProvider.options;
                        int num3 = (options == null) ? 0 : options.Count;
                        if (base.Flags.HasFlag(PdfInteractiveFormFieldFlags.Combo) && base.Flags.HasFlag(PdfInteractiveFormFieldFlags.Edit))
                        {
                            if ((item == null) && (list.Count > 0))
                            {
                                item = list[0];
                            }
                            if (item != null)
                            {
                                selectedValues.Add(item);
                                for (int i = 0; i < num3; i++)
                                {
                                    if (options[i].Text == item)
                                    {
                                        list4.Add(i);
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            int count = list.Count;
                            int num6 = 0;
                            while (num6 < count)
                            {
                                string str2 = list[num6];
                                int num7 = 0;
                                while (true)
                                {
                                    if (num7 < num3)
                                    {
                                        if (options[num7].Text != str2)
                                        {
                                            num7++;
                                            continue;
                                        }
                                        selectedValues.Add(str2);
                                        list4.Add(num7);
                                    }
                                    num6++;
                                    break;
                                }
                            }
                        }
                    }
                    IList<string> oldValue = this.valuesProvider.selectedValues;
                    PdfInteractiveFormFieldValueChangingEventArgs args = new PdfInteractiveFormFieldValueChangingEventArgs(base.FullName, oldValue, selectedValues);
                    if (base.RaiseFieldChanging(args))
                    {
                        this.valuesProvider.selectedValues = args.NewValue as IList<string>;
                        this.valuesProvider.selectedIndices = list4;
                        this.valuesProvider.SetFormModifiedState(fontSearch);
                        base.RaiseFieldChanged(oldValue, args.NewValue);
                    }
                }
            }
        }

        private void WriteValues(PdfWriterDictionary dictionary, string key, IList<string> values)
        {
            if (values != null)
            {
                if ((values.Count == 1) && ((base.Flags & PdfInteractiveFormFieldFlags.MultiSelect) == PdfInteractiveFormFieldFlags.None))
                {
                    dictionary.Add(key, values[0]);
                }
                else
                {
                    Func<string, object> converter = <>c.<>9__34_0;
                    if (<>c.<>9__34_0 == null)
                    {
                        Func<string, object> local1 = <>c.<>9__34_0;
                        converter = <>c.<>9__34_0 = value => value;
                    }
                    dictionary.AddList<string>(key, values, converter);
                }
            }
        }

        public IList<string> DefaultValues =>
            this.valuesProvider.defaultValues;

        public IList<PdfOptionsFormFieldOption> Options =>
            this.valuesProvider.options;

        public int TopIndex =>
            this.valuesProvider.topIndex;

        public IList<int> SelectedIndices =>
            this.valuesProvider.selectedIndices;

        public IList<string> SelectedValues =>
            this.valuesProvider.selectedValues;

        internal bool IsCombo =>
            base.Flags.HasFlag(PdfInteractiveFormFieldFlags.Combo);

        protected internal override object Value
        {
            get
            {
                IList<string> selectedValues = this.valuesProvider.selectedValues;
                if (selectedValues == null)
                {
                    return null;
                }
                int count = selectedValues.Count;
                return ((count == 0) ? null : ((count == 1) ? ((object) selectedValues[0]) : ((object) selectedValues)));
            }
        }

        protected internal override object DefaultValue =>
            this.valuesProvider.defaultValues;

        protected override string FieldType =>
            "Ch";

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfChoiceFormField.<>c <>9 = new PdfChoiceFormField.<>c();
            public static Func<string, object> <>9__34_0;
            public static Func<PdfOptionsFormFieldOption, object> <>9__37_0;
            public static Func<int, object> <>9__37_1;

            internal object <FillDictionary>b__37_0(PdfOptionsFormFieldOption value) => 
                value.Write();

            internal object <FillDictionary>b__37_1(int value) => 
                value;

            internal object <WriteValues>b__34_0(string value) => 
                value;
        }
    }
}

