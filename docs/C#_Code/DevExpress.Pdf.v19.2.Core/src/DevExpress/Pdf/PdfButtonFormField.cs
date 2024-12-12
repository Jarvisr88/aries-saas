namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfButtonFormField : PdfInteractiveFormField
    {
        internal const string Type = "Btn";
        internal const string OffStateName = "Off";
        private readonly PdfButtonFormField valuesProvider;
        private readonly string defaultState;
        private readonly string[] kidsState;
        private string state;

        internal PdfButtonFormField(PdfAcroFormRadioGroupField radioGroup, PdfDocument document, PdfInteractiveFormField parent) : base(parent, document, radioGroup, false)
        {
            this.valuesProvider = this;
        }

        internal PdfButtonFormField(PdfAcroFormCheckBoxField checkBox, IPdfExportFontProvider fontSearch, PdfDocument document, PdfInteractiveFormField parent) : base(parent, fontSearch, document, checkBox)
        {
            this.valuesProvider = this;
            PdfWidgetAnnotation widget = base.Widget;
            string exportValue = checkBox.ExportValue;
            new PdfButtonFormFieldAppearanceBuilder(widget, this, checkBox, true).RebuildAppearance(widget.CreateAppearanceForm(PdfAnnotationAppearanceState.Normal, exportValue));
            new PdfButtonFormFieldAppearanceBuilder(widget, this, checkBox, false).RebuildAppearance(widget.CreateAppearanceForm(PdfAnnotationAppearanceState.Normal, "Off"));
            if (checkBox.ShouldGeneratePressedAppearance && (document.DocumentCatalog.CreationOptions.Compatibility == PdfCompatibility.Pdf))
            {
                new PdfButtonFormFieldFadedAppearanceBuilder(widget, this, checkBox, true).RebuildAppearance(widget.CreateAppearanceForm(PdfAnnotationAppearanceState.Down, exportValue));
                new PdfButtonFormFieldFadedAppearanceBuilder(widget, this, checkBox, false).RebuildAppearance(widget.CreateAppearanceForm(PdfAnnotationAppearanceState.Down, "Off"));
            }
            this.SetValue(checkBox.IsChecked ? this.OnState : "Off", null);
        }

        internal PdfButtonFormField(PdfInteractiveForm form, PdfInteractiveFormField parent, PdfReaderDictionary dictionary, PdfObjectReference valueReference) : base(form, parent, dictionary, valueReference)
        {
            PdfButtonFormField valuesProvider = base.ValuesProvider as PdfButtonFormField;
            PdfButtonFormField field2 = valuesProvider;
            if (valuesProvider == null)
            {
                PdfButtonFormField local1 = valuesProvider;
                field2 = this;
            }
            this.valuesProvider = field2;
            this.state = ReadState(dictionary, "V");
            this.defaultState = ReadState(dictionary, "DV");
            IList<PdfInteractiveFormField> kids = base.Kids;
            IList<object> array = dictionary.GetArray("Opt");
            if (array != null)
            {
                if (base.Flags.HasFlag(PdfInteractiveFormFieldFlags.PushButton))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                if (kids == null)
                {
                    if (array.Count < 1)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    PdfWidgetAnnotation widget = base.Widget;
                    this.kidsState = new string[] { ConvertToKidState(array[0], base.Widget) };
                }
                else
                {
                    bool flag = false;
                    int count = kids.Count;
                    if (array.Count < count)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.kidsState = new string[count];
                    int index = 0;
                    while (true)
                    {
                        if (index >= count)
                        {
                            if (!flag)
                            {
                                foreach (PdfInteractiveFormField field in kids)
                                {
                                    string appearanceName = field.Widget.AppearanceName;
                                    if (!string.IsNullOrEmpty(appearanceName) && (appearanceName != "Off"))
                                    {
                                        this.state = appearanceName;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                        PdfWidgetAnnotation widget = kids[index].Widget;
                        this.kidsState[index] = ConvertToKidState(array[index], widget);
                        if (((this.state == null) || (this.state == widget.AppearanceName)) && ((this.defaultState == null) || widget.Appearance.Names.Contains(this.defaultState)))
                        {
                            flag = true;
                        }
                        index++;
                    }
                }
            }
        }

        internal PdfButtonFormField(PdfDocument document, PdfButtonFormField radioGroupRootField, PdfRadioGroupFieldAppearance radioGroup, string buttonName, PdfWidgetAnnotation radioButtonWidget) : base(radioGroupRootField, radioButtonWidget)
        {
            this.valuesProvider = radioGroupRootField;
            new PdfButtonFormFieldAppearanceBuilder(radioButtonWidget, this, radioGroup, true).RebuildAppearance(radioButtonWidget.CreateAppearanceForm(PdfAnnotationAppearanceState.Normal, buttonName));
            new PdfButtonFormFieldAppearanceBuilder(radioButtonWidget, this, radioGroup, false).RebuildAppearance(radioButtonWidget.CreateAppearanceForm(PdfAnnotationAppearanceState.Normal, "Off"));
            if (radioGroup.ShouldGeneratePressedAppearance && (document.DocumentCatalog.CreationOptions.Compatibility == PdfCompatibility.Pdf))
            {
                new PdfButtonFormFieldFadedAppearanceBuilder(radioButtonWidget, this, radioGroup, true).RebuildAppearance(radioButtonWidget.CreateAppearanceForm(PdfAnnotationAppearanceState.Down, buttonName));
                new PdfButtonFormFieldFadedAppearanceBuilder(radioButtonWidget, this, radioGroup, false).RebuildAppearance(radioButtonWidget.CreateAppearanceForm(PdfAnnotationAppearanceState.Down, "Off"));
            }
        }

        protected internal override void Accept(IPdfInteractiveFormFieldVisitor visitor)
        {
            visitor.Visit(this);
        }

        private static string ConvertToKidState(object option, PdfWidgetAnnotation widget)
        {
            string str = option as string;
            if (str == null)
            {
                byte[] buffer = option as byte[];
                if (buffer != null)
                {
                    str = PdfDocumentReader.ConvertToTextString(buffer);
                }
            }
            if ((str == null) || (widget == null))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return str;
        }

        protected internal override void FillDictionary(PdfWriterDictionary dictionary)
        {
            base.FillDictionary(dictionary);
            dictionary.AddName("V", this.state);
            dictionary.AddName("DV", this.defaultState);
            if ((this.kidsState != null) && !dictionary.Objects.IsCloning)
            {
                dictionary.Add("Opt", this.kidsState);
            }
        }

        private string FindOption(string onAppearanceName)
        {
            IList<PdfInteractiveFormField> kids = this.valuesProvider.Kids;
            string[] kidsState = this.valuesProvider.kidsState;
            if ((kids != null) && (kidsState != null))
            {
                int count = kids.Count;
                for (int i = 0; i < count; i++)
                {
                    PdfWidgetAnnotation widget = kids[i].Widget;
                    if ((widget != null) && (onAppearanceName == widget.OnAppearanceName))
                    {
                        return kidsState[i];
                    }
                }
            }
            return string.Empty;
        }

        private bool HasAppearance(string value)
        {
            PdfWidgetAnnotation widget = base.Widget;
            if (widget == null)
            {
                return false;
            }
            PdfAnnotationAppearances appearances = widget.Appearance;
            if (appearances == null)
            {
                return false;
            }
            PdfAnnotationAppearance normal = appearances.Normal;
            return ((normal != null) && normal.GetNames(null).Contains(value));
        }

        private static string ReadState(PdfReaderDictionary dictionary, string key)
        {
            object obj2;
            string str;
            if (!dictionary.TryGetValue(key, out obj2))
            {
                return null;
            }
            obj2 = dictionary.Objects.TryResolve(obj2, null);
            PdfName name = obj2 as PdfName;
            if (name != null)
            {
                str = name.Name;
            }
            else
            {
                byte[] buffer = obj2 as byte[];
                if (buffer == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                str = PdfDocumentReader.ConvertToTextString(buffer);
            }
            return (string.IsNullOrEmpty(str) ? null : str);
        }

        protected internal override void SetExportValue(object value, IPdfExportFontProvider fontSearch)
        {
            if (!this.AcceptValue(value))
            {
                throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectButtonFormFieldValue));
            }
            string item = value as string;
            IList<PdfInteractiveFormField> kids = this.valuesProvider.Kids;
            IList<string> kidsState = this.valuesProvider.kidsState;
            if ((kids != null) && (kidsState != null))
            {
                string newValue = null;
                string str3 = kidsState.Contains(item) ? item : this.FindOption(item);
                if (!string.IsNullOrEmpty(str3))
                {
                    bool flag = false;
                    int count = kidsState.Count;
                    for (int i = 0; i < count; i++)
                    {
                        PdfWidgetAnnotation widget = kids[i].Widget;
                        if (widget != null)
                        {
                            if (str3 != kidsState[i])
                            {
                                widget.AppearanceName = "Off";
                            }
                            else
                            {
                                string onAppearanceName = widget.OnAppearanceName;
                                if ((onAppearanceName != this.valuesProvider.state) && string.IsNullOrEmpty(newValue))
                                {
                                    flag = true;
                                    PdfInteractiveFormFieldValueChangingEventArgs args = new PdfInteractiveFormFieldValueChangingEventArgs(base.FullName, this.valuesProvider.state, onAppearanceName);
                                    if (!base.RaiseFieldChanging(args))
                                    {
                                        return;
                                    }
                                    newValue = args.NewValue as string;
                                }
                                widget.AppearanceName = onAppearanceName;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(newValue))
                    {
                        string state = this.valuesProvider.state;
                        this.valuesProvider.state = newValue;
                        base.RaiseFieldChanged(state, newValue);
                    }
                    if (flag)
                    {
                        this.valuesProvider.SetFormModifiedState(fontSearch);
                    }
                    return;
                }
            }
            base.SetExportValue(value, fontSearch);
        }

        protected internal override void SetValue(object value, IPdfExportFontProvider fontSearch)
        {
            base.SetValue(value, fontSearch);
            if (!this.AcceptValue(value))
            {
                throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectButtonFormFieldValue));
            }
            string str = value as string;
            IList<PdfInteractiveFormField> kids = this.valuesProvider.Kids;
            if (str != this.state)
            {
                string state = this.valuesProvider.state;
                string newValue = str ?? "Off";
                bool flag = newValue != state;
                PdfInteractiveFormFieldValueChangingEventArgs args = new PdfInteractiveFormFieldValueChangingEventArgs(base.FullName, state, newValue);
                if (base.RaiseFieldChanging(args))
                {
                    this.valuesProvider.state = args.NewValue as string;
                    PdfWidgetAnnotation widget = base.Widget;
                    if (widget != null)
                    {
                        widget.AppearanceName = str;
                    }
                    if (kids != null)
                    {
                        if ((str != null) && (str != "Off"))
                        {
                            bool flag2 = false;
                            foreach (PdfButtonFormField field in kids)
                            {
                                if (field.HasAppearance(str))
                                {
                                    flag2 = true;
                                    break;
                                }
                            }
                            if (!flag2)
                            {
                                this.SetValue("Off", fontSearch);
                                return;
                            }
                        }
                        foreach (PdfButtonFormField field2 in kids)
                        {
                            string str4 = field2.HasAppearance(str) ? str : "Off";
                            PdfWidgetAnnotation annotation2 = field2.Widget;
                            if (annotation2 != null)
                            {
                                annotation2.AppearanceName = str4;
                            }
                            field2.state = str;
                        }
                    }
                    if (flag)
                    {
                        this.valuesProvider.SetFormModifiedState(fontSearch);
                        base.RaiseFieldChanged(state, newValue);
                    }
                }
            }
        }

        public string DefaultState =>
            this.valuesProvider.defaultState;

        public IList<string> KidsState =>
            this.valuesProvider.kidsState;

        public string State =>
            this.valuesProvider.state;

        internal string OnState
        {
            get
            {
                PdfWidgetAnnotation widget = base.Widget;
                return ((widget == null) ? "Off" : widget.OnAppearanceName);
            }
        }

        protected internal override bool ShouldHighlight =>
            base.ShouldHighlight && !base.Flags.HasFlag(PdfInteractiveFormFieldFlags.PushButton);

        protected internal override object Value
        {
            get
            {
                string state = this.valuesProvider.state;
                string str2 = this.FindOption(state);
                return (string.IsNullOrEmpty(str2) ? (state ?? "Off") : str2);
            }
        }

        protected internal override object DefaultValue =>
            this.valuesProvider.defaultState ?? "Off";

        protected internal override bool ShouldUseDefaultAppearanceForm =>
            base.Flags.HasFlag(PdfInteractiveFormFieldFlags.PushButton);

        protected override string FieldType =>
            "Btn";
    }
}

