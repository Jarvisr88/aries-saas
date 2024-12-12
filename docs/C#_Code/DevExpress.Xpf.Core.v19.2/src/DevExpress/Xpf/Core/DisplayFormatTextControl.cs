namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Localization;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class DisplayFormatTextControl : Control, INotifyPropertyChanged
    {
        public static readonly DependencyProperty PrefixProperty = DependencyPropertyManager.RegisterAttached("Prefix", typeof(string), typeof(DisplayFormatTextControl));
        public static readonly DependencyProperty SuffixProperty = DependencyPropertyManager.RegisterAttached("Suffix", typeof(string), typeof(DisplayFormatTextControl));
        public static readonly DependencyProperty DisplayFormatSourceCollectionProperty = DependencyPropertyManager.RegisterAttached("DisplayFormatSourceCollection", typeof(ICollectionView), typeof(DisplayFormatTextControl));
        public static readonly DependencyProperty ExampleTextProperty = DependencyPropertyManager.RegisterAttached("ExampleText", typeof(string), typeof(DisplayFormatTextControl));
        private static DisplayFormatItem emptyDisplayFormat = new DisplayFormatItem(string.Empty, DisplayFormatGroupType.Default);
        private static IEnumerable<DisplayFormatItem> displayFormatNumber;
        private static IEnumerable<DisplayFormatItem> displayFormatPercent;
        private static IEnumerable<DisplayFormatItem> displayFormatCurrency;
        private static IEnumerable<DisplayFormatItem> displayFormatSpecial;
        private static IEnumerable<DisplayFormatItem> displayFormatDateTime;
        private static Dictionary<TypeCategory, DisplayFormatItem> displayFormatCustomItems;
        private ObservableCollection<DisplayFormatItem> displayFormatSource = new ObservableCollection<DisplayFormatItem>();
        private string currentDisplayFormat;
        private Type sourceValueType = typeof(int);
        public ComboBoxEdit PART_DisplayFormatComboBox;
        private TextEdit PART_PrefixValue;
        private TextEdit PART_SuffixValue;
        private TextBlock PART_PrefixCaption;
        private TextBlock PART_SuffixCaption;
        private TextBlock PART_DisplayTextFormat;
        private TextBlock PART_ExampleCaption;
        private TextBlock PART_ExampleValue;
        private DisplayFormatItem notValidItem;

        public event EditValueChangedEventHandler CurrentDisplayFormatChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        static DisplayFormatTextControl()
        {
            List<DisplayFormatItem> list1 = new List<DisplayFormatItem>();
            list1.Add(new DisplayFormatItem("#.00", DisplayFormatGroupType.Number));
            list1.Add(new DisplayFormatItem("#,#", DisplayFormatGroupType.Number));
            list1.Add(new DisplayFormatItem("E2", DisplayFormatGroupType.Number));
            list1.Add(new DisplayFormatItem("n", DisplayFormatGroupType.Number));
            list1.Add(new DisplayFormatItem("n1", DisplayFormatGroupType.Number));
            list1.Add(new DisplayFormatItem("n2", DisplayFormatGroupType.Number));
            list1.Add(new DisplayFormatItem("e", DisplayFormatGroupType.Number));
            list1.Add(new DisplayFormatItem("e1", DisplayFormatGroupType.Number));
            list1.Add(new DisplayFormatItem("f", DisplayFormatGroupType.Number));
            list1.Add(new DisplayFormatItem("f1", DisplayFormatGroupType.Number));
            displayFormatNumber = list1;
            List<DisplayFormatItem> list2 = new List<DisplayFormatItem>();
            list2.Add(new DisplayFormatItem("0.00", DisplayFormatGroupType.Percent));
            list2.Add(new DisplayFormatItem("0%", DisplayFormatGroupType.Percent));
            displayFormatPercent = list2;
            List<DisplayFormatItem> list3 = new List<DisplayFormatItem>();
            list3.Add(new DisplayFormatItem("$0.00", DisplayFormatGroupType.Currency));
            list3.Add(new DisplayFormatItem("$0", DisplayFormatGroupType.Currency));
            list3.Add(new DisplayFormatItem("c", DisplayFormatGroupType.Currency));
            list3.Add(new DisplayFormatItem("c1", DisplayFormatGroupType.Currency));
            list3.Add(new DisplayFormatItem("c2", DisplayFormatGroupType.Currency));
            displayFormatCurrency = list3;
            List<DisplayFormatItem> list4 = new List<DisplayFormatItem>();
            list4.Add(new DisplayFormatItem("(###) ###-####", DisplayFormatGroupType.Special));
            list4.Add(new DisplayFormatItem("###-##-####", DisplayFormatGroupType.Special));
            displayFormatSpecial = list4;
            List<DisplayFormatItem> list5 = new List<DisplayFormatItem>();
            list5.Add(new DisplayFormatItem("yy/MM/dd", DisplayFormatGroupType.Datetime));
            list5.Add(new DisplayFormatItem("ddMMyy", DisplayFormatGroupType.Datetime));
            list5.Add(new DisplayFormatItem("yy/MM/dd hh:mm:ss", DisplayFormatGroupType.Datetime));
            list5.Add(new DisplayFormatItem("M/d/yyyy", DisplayFormatGroupType.Datetime));
            list5.Add(new DisplayFormatItem("M/d/yy", DisplayFormatGroupType.Datetime));
            list5.Add(new DisplayFormatItem("MM/dd/yy", DisplayFormatGroupType.Datetime));
            list5.Add(new DisplayFormatItem("MM/dd/yyyy", DisplayFormatGroupType.Datetime));
            list5.Add(new DisplayFormatItem("yyyy-MM-dd", DisplayFormatGroupType.Datetime));
            list5.Add(new DisplayFormatItem("dd-MMM-yy", DisplayFormatGroupType.Datetime));
            list5.Add(new DisplayFormatItem("dddd, MMMM dd, yyyy", DisplayFormatGroupType.Datetime));
            displayFormatDateTime = list5;
            displayFormatCustomItems = new Dictionary<TypeCategory, DisplayFormatItem>();
        }

        public DisplayFormatTextControl()
        {
            base.DefaultStyleKey = typeof(DisplayFormatTextControl);
            CollectionViewSource source1 = new CollectionViewSource();
            source1.Source = this.DisplayFormatSource;
            this.DisplayFormatSourceCollection = source1.View;
            this.DisplayFormatSourceCollection.CollectionChanged += new NotifyCollectionChangedEventHandler(this.DisplayFormatSourceCollection_CollectionChanged);
        }

        private void AddCustomElement(IList destination, bool generateExample = true)
        {
            DisplayFormatItem customElement = this.GetCustomElement(generateExample);
            if (customElement != null)
            {
                destination.Add(customElement);
            }
        }

        private void AddDateTimeFormatElements()
        {
            this.AddElementsToSource(displayFormatDateTime);
        }

        public void AddDisplayFormatToList(string newDisplayFormat)
        {
            this.AddNewDisplayFormatIfNeed(newDisplayFormat);
        }

        private void AddElementsToList(IList destination, IEnumerable<DisplayFormatItem> elements, bool generateExample = true)
        {
            foreach (DisplayFormatItem item in elements)
            {
                if (generateExample)
                {
                    item.Example = this.GetExample(item.Value);
                }
                destination.Add(item);
            }
        }

        private void AddElementsToSource(IEnumerable<DisplayFormatItem> elements)
        {
            this.AddElementsToList(this.DisplayFormatSource, elements, true);
        }

        private void AddNewDisplayFormat(string displayFormat)
        {
            DisplayFormatItem customElement = this.GetCustomElement(true);
            if (customElement != null)
            {
                customElement.Example = this.GetExample(displayFormat);
                customElement.Value = displayFormat;
            }
            else
            {
                customElement = new DisplayFormatItem(displayFormat, DisplayFormatGroupType.Custom);
                customElement.Example = this.GetExample(customElement.Value);
                displayFormatCustomItems.Add(DisplayFormatExamleHelper.GetTypeCategory(this.SourceValueType), customElement);
            }
            this.SetFiltrerByType();
        }

        private void AddNewDisplayFormatIfNeed(string displayFormat)
        {
            if (!this.IsSourceContainsDisplayFormat(displayFormat))
            {
                this.AddNewDisplayFormat(displayFormat);
            }
        }

        private void AddNumericFormatElements()
        {
            this.AddElementsToSource(displayFormatNumber);
            this.AddElementsToSource(displayFormatPercent);
            this.AddElementsToSource(displayFormatCurrency);
            this.AddElementsToSource(displayFormatSpecial);
        }

        private void BorderTextChanged(bool suffix, string text)
        {
            if (this.PART_DisplayFormatComboBox.SelectedItem != null)
            {
                if (suffix)
                {
                    this.Suffix = text;
                }
                else
                {
                    this.Prefix = text;
                }
                this.OnCurrentDisplayFormatChanged();
                this.UpdateExample(this.currentDisplayFormat);
                this.UpdateDisplayFormatSourceExamples();
            }
        }

        protected bool ChangeProperty<T>(ref T property, T value, string propertyName)
        {
            if (Equals((T) property, value))
            {
                return false;
            }
            property = value;
            this.RaisePropertyChanged(propertyName);
            return true;
        }

        private void CleanNotValidDisplayFormat()
        {
            if ((this.notValidItem != null) && this.DisplayFormatSource.Contains(this.notValidItem))
            {
                this.DisplayFormatSource.Remove(this.notValidItem);
            }
        }

        public static void ClearCustomDisplayFormat()
        {
            displayFormatCustomItems.Clear();
        }

        private void ComboBoxValueChanged(string actualDisplayFormat)
        {
            this.SetCurrentDisplayFormat(actualDisplayFormat, false);
            this.OnCurrentDisplayFormatChanged();
        }

        private void DisplayFormatComboBox_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if ((e.NewValue != null) && (e.OldValue != e.NewValue))
            {
                this.CleanNotValidDisplayFormat();
                this.ComboBoxValueChanged((string) e.NewValue);
                e.Handled = true;
            }
        }

        private void DisplayFormatComboBox_ProcessNewValue(DependencyObject sender, ProcessNewValueEventArgs e)
        {
            this.CleanNotValidDisplayFormat();
            ErrorParameters parameters = DisplayFormatValidationHelper.IsDisplayFormatStringValid(ref e.DisplayText);
            if ((parameters != null) && (parameters.ErrorType == ErrorType.None))
            {
                this.ComboBoxValueChanged(e.DisplayText);
                e.Handled = true;
            }
            else
            {
                this.SelectNotValidComboBoxValue(e.DisplayText);
                e.Handled = true;
            }
        }

        private void DisplayFormatComboBox_Validate(object sender, ValidationEventArgs e)
        {
            this.DisplayFormatValidate(e, '0');
        }

        private void DisplayFormatSourceCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

        private void DisplayFormatValidate(ValidationEventArgs e, char inplaceChar)
        {
            string suffix = (string) e.Value;
            ErrorParameters nullErrorParameters = (inplaceChar != '0') ? DisplayFormatValidationHelper.IsSuffixValid(suffix) : DisplayFormatValidationHelper.IsDisplayFormatStringValid(ref suffix);
            nullErrorParameters ??= DisplayFormatValidationHelper.GetNullErrorParameters();
            e.ErrorType = nullErrorParameters.ErrorType;
            e.ErrorContent = nullErrorParameters.ErrorContent;
            e.IsValid = nullErrorParameters.ErrorType == ErrorType.None;
            e.Handled = true;
        }

        private IEnumerable<DisplayFormatItem> GetAllDisplayFormats()
        {
            List<DisplayFormatItem> destination = new List<DisplayFormatItem> {
                emptyDisplayFormat
            };
            TypeCategory typeCategory = DisplayFormatExamleHelper.GetTypeCategory(this.SourceValueType);
            if (typeCategory != TypeCategory.Numeric)
            {
                if (typeCategory == TypeCategory.DateTime)
                {
                    this.AddElementsToList(destination, displayFormatDateTime, false);
                }
            }
            else
            {
                this.AddElementsToList(destination, displayFormatNumber, false);
                this.AddElementsToList(destination, displayFormatPercent, false);
                this.AddElementsToList(destination, displayFormatCurrency, false);
                this.AddElementsToList(destination, displayFormatSpecial, false);
            }
            this.AddCustomElement(destination, false);
            return destination;
        }

        private DisplayFormatItem GetCustomElement(bool generateExample = true)
        {
            DisplayFormatItem item = null;
            if (displayFormatCustomItems.TryGetValue(DisplayFormatExamleHelper.GetTypeCategory(this.SourceValueType), out item) && generateExample)
            {
                item.Example = this.GetExample(item.Value);
            }
            return item;
        }

        private string GetDisplayFormatAndUpdateParts(string value)
        {
            string str;
            string str2;
            string str3;
            this.Prefix = string.Empty;
            this.Suffix = string.Empty;
            if ((value == null) || (this.SourceValueType == null))
            {
                return null;
            }
            this.ExampleText = null;
            DisplayFormatHelper.GetDisplayFormatParts(value, out str, out str3, out str2, this.NullValueDisplayFormat);
            this.Prefix = str;
            this.Suffix = str2;
            this.AddNewDisplayFormatIfNeed(str3);
            this.UpdateExample(str3);
            return str3;
        }

        private string GetExample(string displayFormat) => 
            this.GetExample(displayFormat, CultureInfo.CurrentCulture);

        private string GetExample(string displayFormat, CultureInfo culture)
        {
            List<object> list1 = new List<object>();
            list1.Add(DisplayFormatExamleHelper.GetExample(this.sourceValueType));
            List<object> list = list1;
            if (string.IsNullOrEmpty(displayFormat))
            {
                return string.Format(culture, "{0}", list.ToArray());
            }
            if (!displayFormat.Contains("{0"))
            {
                return DisplayFormatHelper.GetDisplayTextFromDisplayFormat(culture, "{0:" + displayFormat + "}", list.ToArray());
            }
            if (displayFormat.Contains("{1"))
            {
                if (!string.IsNullOrEmpty(this.SecondParameterName))
                {
                    list.Add(this.SecondParameterName);
                }
                else
                {
                    list.Add(string.Empty);
                }
            }
            return DisplayFormatHelper.GetDisplayTextFromDisplayFormat(culture, displayFormat, list.ToArray());
        }

        private DisplayFormatItem GetItemFromDisplayFormatSource(string value)
        {
            DisplayFormatItem item2;
            using (IEnumerator enumerator = this.DisplayFormatSource.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        DisplayFormatItem current = (DisplayFormatItem) enumerator.Current;
                        if (current.Value != value)
                        {
                            continue;
                        }
                        item2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return item2;
        }

        private void InitializeControlsTexts()
        {
            XtraLocalizer<EditorStringId> active = EditorLocalizer.Active;
            if (this.PART_DisplayTextFormat != null)
            {
                this.PART_DisplayTextFormat.Text = active.GetLocalizedString(EditorStringId.DisplayFormatTextControlDisplayFormatText);
            }
            if (this.PART_ExampleCaption != null)
            {
                this.PART_ExampleCaption.Text = active.GetLocalizedString(EditorStringId.DisplayFormatTextControlExample);
            }
            if (this.PART_PrefixCaption != null)
            {
                this.PART_PrefixCaption.Text = active.GetLocalizedString(EditorStringId.DisplayFormatTextControlPrefix);
            }
            if (this.PART_SuffixCaption != null)
            {
                this.PART_SuffixCaption.Text = active.GetLocalizedString(EditorStringId.DisplayFormatTextControlSuffix);
            }
            if (this.PART_DisplayFormatComboBox != null)
            {
                this.PART_DisplayFormatComboBox.NullText = active.GetLocalizedString(EditorStringId.DisplayFormatNullValue);
            }
        }

        internal bool IsSourceContainsDisplayFormat(string displayFormat)
        {
            bool flag;
            using (IEnumerator<DisplayFormatItem> enumerator = this.GetAllDisplayFormats().GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        DisplayFormatItem current = enumerator.Current;
                        if (current.Value != displayFormat)
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        private void LoadControls()
        {
            this.PART_DisplayFormatComboBox = base.GetTemplateChild("PART_DisplayFormatComboBox") as ComboBoxEdit;
            this.PART_PrefixValue = base.GetTemplateChild("PART_PrefixValue") as TextEdit;
            this.PART_SuffixValue = base.GetTemplateChild("PART_SuffixValue") as TextEdit;
            this.PART_PrefixCaption = base.GetTemplateChild("PART_PrefixCaption") as TextBlock;
            this.PART_SuffixCaption = base.GetTemplateChild("PART_SuffixCaption") as TextBlock;
            this.PART_DisplayTextFormat = base.GetTemplateChild("PART_DisplayTextFormat") as TextBlock;
            this.PART_ExampleCaption = base.GetTemplateChild("PART_ExampleCaption") as TextBlock;
            this.PART_ExampleValue = base.GetTemplateChild("PART_ExampleValue") as TextBlock;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.LoadControls();
            this.SubscribeEvents();
            this.InitializeControlsTexts();
        }

        private void OnCurrentDisplayFormatChanged()
        {
            if (this.CurrentDisplayFormatChanged != null)
            {
                this.CurrentDisplayFormatChanged(this, new EditValueChangedEventArgs(null, this.CurrentDisplayFormat));
            }
        }

        private void PART_DisplayFormatComboBox_Spin(object sender, SpinEventArgs e)
        {
            base.Dispatcher.BeginInvoke(() => this.PART_DisplayFormatComboBox.DoValidate(), new object[0]);
        }

        private void Prefix_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            this.BorderTextChanged(false, (string) e.NewValue);
        }

        private void RaisePropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void SelectNotValidComboBoxValue(string notValidDisplayFormat)
        {
            this.notValidItem = new DisplayFormatItem(notValidDisplayFormat, DisplayFormatGroupType.Custom, string.Empty);
            this.DisplayFormatSource.Add(this.notValidItem);
        }

        private void SetComboBoxValue()
        {
            if (this.currentDisplayFormat != null)
            {
                this.PART_DisplayFormatComboBox.SelectedItem = this.GetItemFromDisplayFormatSource(this.currentDisplayFormat);
            }
        }

        private void SetCurrentDisplayFormat(string actualDisplayFormat, bool newItemSelected)
        {
            if ((actualDisplayFormat != null) || (this.currentDisplayFormat != null))
            {
                this.PART_DisplayFormatComboBox.SelectedItem = null;
                this.currentDisplayFormat = newItemSelected ? this.GetDisplayFormatAndUpdateParts(actualDisplayFormat) : this.GetDisplayFormatAndUpdateParts(DisplayFormatHelper.GetDisplayFormatFromParts(this.Prefix, actualDisplayFormat, this.Suffix));
                this.SetComboBoxValue();
                this.UpdateControlState(actualDisplayFormat);
            }
        }

        private void SetFiltrerByType()
        {
            this.DisplayFormatSource.Clear();
            emptyDisplayFormat.Example = this.GetExample(emptyDisplayFormat.Value);
            this.DisplayFormatSource.Add(emptyDisplayFormat);
            TypeCategory typeCategory = DisplayFormatExamleHelper.GetTypeCategory(this.SourceValueType);
            if (typeCategory == TypeCategory.Numeric)
            {
                this.AddNumericFormatElements();
            }
            else if (typeCategory == TypeCategory.DateTime)
            {
                this.AddDateTimeFormatElements();
            }
            this.AddCustomElement(this.DisplayFormatSource, true);
            this.DisplayFormatSourceCollection.GroupDescriptions.Clear();
            this.DisplayFormatSourceCollection.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
        }

        private void SubscribeEvents()
        {
            if (this.PART_DisplayFormatComboBox != null)
            {
                this.PART_DisplayFormatComboBox.Validate += new ValidateEventHandler(this.DisplayFormatComboBox_Validate);
                this.PART_DisplayFormatComboBox.EditValueChanged += new EditValueChangedEventHandler(this.DisplayFormatComboBox_EditValueChanged);
                this.PART_DisplayFormatComboBox.ProcessNewValue += new ProcessNewValueEventHandler(this.DisplayFormatComboBox_ProcessNewValue);
                this.PART_DisplayFormatComboBox.Spin += new SpinEventHandler(this.PART_DisplayFormatComboBox_Spin);
            }
            if (this.PART_PrefixValue != null)
            {
                this.PART_PrefixValue.Validate += new ValidateEventHandler(this.Suffix_Validate);
                this.PART_PrefixValue.EditValueChanged += new EditValueChangedEventHandler(this.Prefix_EditValueChanged);
            }
            if (this.PART_SuffixValue != null)
            {
                this.PART_SuffixValue.Validate += new ValidateEventHandler(this.Suffix_Validate);
                this.PART_SuffixValue.EditValueChanged += new EditValueChangedEventHandler(this.Suffix_EditValueChanged);
            }
        }

        private void Suffix_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            this.BorderTextChanged(true, (string) e.NewValue);
        }

        private void Suffix_Validate(object sender, ValidationEventArgs e)
        {
            this.DisplayFormatValidate(e, '1');
        }

        private void UpdateControlState(string value)
        {
            base.IsEnabled = value != null;
        }

        private void UpdateDisplayFormatSourceExamples()
        {
            if (this.DisplayFormatSource != null)
            {
                foreach (DisplayFormatItem item in this.DisplayFormatSource)
                {
                    item.Example = this.GetExample(item.Value);
                }
            }
        }

        private void UpdateExample(string clearDisplayFormat)
        {
            this.ExampleText = this.GetExample(DisplayFormatHelper.GetDisplayFormatFromParts(this.Prefix, clearDisplayFormat, this.Suffix));
        }

        public static IEnumerable<DisplayFormatItem> DisplayFormatNumber =>
            displayFormatNumber;

        public static IEnumerable<DisplayFormatItem> DisplayFormatPercent =>
            displayFormatPercent;

        public static IEnumerable<DisplayFormatItem> DisplayFormatCurrency =>
            displayFormatCurrency;

        public static IEnumerable<DisplayFormatItem> DisplayFormatSpecial =>
            displayFormatSpecial;

        public static IEnumerable<DisplayFormatItem> DisplayFormatDateTime =>
            displayFormatDateTime;

        public IList DisplayFormatSource =>
            this.displayFormatSource;

        public ICollectionView DisplayFormatSourceCollection
        {
            get => 
                (ICollectionView) base.GetValue(DisplayFormatSourceCollectionProperty);
            set => 
                base.SetValue(DisplayFormatSourceCollectionProperty, value);
        }

        public string CurrentDisplayFormat
        {
            get
            {
                string str = DisplayFormatHelper.GetDisplayFormatFromParts(this.Prefix, this.currentDisplayFormat, this.Suffix);
                return ((str == this.NullValueDisplayFormat) ? string.Empty : str);
            }
            set => 
                this.SetCurrentDisplayFormat(value, true);
        }

        public string NullValueDisplayFormat { get; set; }

        public string SecondParameterName { get; set; }

        public string Prefix
        {
            get => 
                (string) base.GetValue(PrefixProperty);
            set => 
                base.SetValue(PrefixProperty, value);
        }

        public string Suffix
        {
            get => 
                (string) base.GetValue(SuffixProperty);
            set => 
                base.SetValue(SuffixProperty, value);
        }

        public string ExampleText
        {
            get => 
                (string) base.GetValue(ExampleTextProperty);
            set => 
                base.SetValue(ExampleTextProperty, value);
        }

        public Type SourceValueType
        {
            get => 
                this.sourceValueType;
            set
            {
                this.ChangeProperty<Type>(ref this.sourceValueType, value, "SourceValueType");
                this.SetFiltrerByType();
            }
        }
    }
}

