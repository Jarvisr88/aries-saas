namespace DevExpress.Xpf.Office.UI
{
    using DevExpress.Office.Services;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Office.Internal;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;

    public class CharacterMapControl : UserControl, IComponentConnector
    {
        private const string AdornerName = "SelectionAdorner";
        private readonly int[] CodePages = new int[] { 
            0x4e2, 0x4e3, 0x4e4, 0x4e5, 0x4e6, 0x4e7, 0x4e8, 0x4e9, 0x4ea, 0x36a, 0x1b5, 720, 0x2e1, 0x307, 850, 0x354,
            0x357, 0x359, 0x35a, 0x35e, 0x362, 0x36a
        };
        private readonly char[] CommonCharacters = new char[] { '\x00a3', '\x00a5', '\x00a9', '\x00ae', '\x00b1', '≠', '≤', '≥', '\x00f7', '\x00d7', '∞' };
        private int charsPerLine = 12;
        private int linesPerView = 8;
        private Dictionary<char, List<string>> characterNames;
        private List<char> chars;
        private FrameworkElement selectedElement;
        private Brush adornerBrush;
        private bool suppressRaiseSearchBoxTextChangedEvent;
        internal ToggleButton btnCommonChars;
        internal ToggleButton btnSpecialChars;
        internal Grid SpecialGrid;
        internal ListBox SpecialList;
        internal Grid CommonGrid;
        internal TextBox tbSearch;
        internal Grid CharactersGrid;
        internal ScrollBar sbCharactersScrollbar;
        internal FontEdit cbFontFamily;
        internal ComboBoxEdit cbCharacterSet;
        internal ComboBoxEdit cbFilter;
        internal TextBlock tbSearchResult;
        internal StackPanel pnlCommonlyUsed;
        internal TextBlock CharacterDescription;
        private bool _contentLoaded;

        public event EventHandler CharDoubleClick;

        public CharacterMapControl()
        {
            this.InitializeComponent();
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            this.OnCharactersGridSizeChanged();
            this.InitFontFamilyComboBox();
            this.InitCharacterSetBox();
            this.InitFilter();
            this.InitSearch();
            this.InitStateButtons();
            this.InitSpecialList();
            this.RenewCommonlyUsed();
            this.RenewCharactersScrollbar();
            this.RenewCharacters();
            this.CharactersScrollBar.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.CharactersScrollBar_ValueChanged);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        private void AddAdorner(FrameworkElement element)
        {
            AdornerLayer layer = AdornerHelper.FindAdornerLayer(element);
            if (layer != null)
            {
                SelectionAdorner adorner = new SelectionAdorner(element, this.adornerBrush);
                adorner.Name = "SelectionAdorner";
                layer.Add(adorner);
            }
        }

        private IEnumerable<char> BuildChars(Encoding code, int range)
        {
            List<char> list = new List<char>();
            if (this.ServiceProvider != null)
            {
                IFontCharacterSetService service = this.ServiceProvider.GetService(typeof(IFontCharacterSetService)) as IFontCharacterSetService;
                if (service == null)
                {
                    return list;
                }
                service.BeginProcessing(this.FontName);
                try
                {
                    int num = 0;
                    while (true)
                    {
                        while (true)
                        {
                            if (num < 0xffff)
                            {
                                char c = (char) num;
                                bool flag = char.IsControl(c);
                                bool flag2 = char.GetUnicodeCategory(c) == UnicodeCategory.PrivateUse;
                                if (!flag && (!flag2 && service.ContainsChar(c)))
                                {
                                    if (this.FilterCategory != null)
                                    {
                                        UnicodeCategory? filterCategory = this.FilterCategory;
                                        if (!((char.GetUnicodeCategory(c) == ((UnicodeCategory) filterCategory.GetValueOrDefault())) ? (filterCategory != null) : false))
                                        {
                                            break;
                                        }
                                    }
                                    list.Add(c);
                                }
                            }
                            else
                            {
                                return list;
                            }
                            break;
                        }
                        num++;
                    }
                }
                finally
                {
                    service.EndProcessing();
                }
            }
            return list;
        }

        private void CharactersScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.RenewCharactersScrollbar();
            this.RenewCharacters();
        }

        private void CommonButton_Click(object sender, RoutedEventArgs e)
        {
            this.SpecialButton.IsChecked = false;
            this.CommonButton.IsChecked = true;
            this.SpecialGrid.Visibility = Visibility.Collapsed;
            this.CommonGrid.Visibility = Visibility.Visible;
        }

        private void CreateCharacterNames()
        {
            string str;
            this.characterNames = new Dictionary<char, List<string>>();
            CultureInfo provider = new CultureInfo("en-US");
            TextReader reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("DevExpress.Xpf.Core.Editors.Office.CharacterNames.txt"));
            while (!string.IsNullOrEmpty(str = reader.ReadLine()))
            {
                int num;
                int.TryParse(str.Substring(str.Length - 5), NumberStyles.HexNumber, provider, out num);
                if (this.characterNames.ContainsKey((char) ((ushort) num)))
                {
                    this.characterNames[(char) ((ushort) num)].Add(this.ExtractCharacterName(str));
                    continue;
                }
                List<string> list = new List<string> {
                    this.ExtractCharacterName(str)
                };
                this.characterNames.Add((char) ((ushort) num), list);
            }
        }

        private TextBlock CreateTextBlock()
        {
            TextBlock block = new TextBlock {
                FontSize = 15.0,
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                MinHeight = 10.0,
                MinWidth = 15.0
            };
            block.MouseLeftButtonUp += new MouseButtonEventHandler(this.tb_MouseLeftButtonUp);
            block.MouseLeftButtonDown += new MouseButtonEventHandler(this.tb_MouseLeftButtonDown);
            return block;
        }

        internal string ExtractCharacterName(string line) => 
            line.Substring(0, line.Length - 6);

        internal string GetAlternativeName(char c)
        {
            List<string> list;
            string str = string.Empty;
            this.CharacterNames.TryGetValue(c, out list);
            if (list == null)
            {
                return string.Empty;
            }
            foreach (string str2 in list)
            {
                if (str2.ToLower() == str2)
                {
                    str = str + str2 + "; ";
                }
            }
            return str;
        }

        private Brush GetBorderBrush()
        {
            Brush brush = this.TryGetBrushByState("FocusedState");
            if (brush != null)
            {
                return brush;
            }
            brush = this.TryGetBrushByState("HoverState");
            if (brush != null)
            {
                return brush;
            }
            brush = this.TryGetBrushByState("TextBoxFocusedState");
            if (brush != null)
            {
                return brush;
            }
            DXBorder border = LayoutHelper.FindElementByName(this.SearchBox, "Focused") as DXBorder;
            if ((border != null) && (border.Background != null))
            {
                return border.Background;
            }
            Predicate<FrameworkElement> predicate = <>c.<>9__61_0;
            if (<>c.<>9__61_0 == null)
            {
                Predicate<FrameworkElement> local1 = <>c.<>9__61_0;
                predicate = <>c.<>9__61_0 = elem => (elem is Border) && (elem.Name != "focus");
            }
            Border border2 = LayoutHelper.FindElement(this.SearchBox, predicate) as Border;
            return (((border2 == null) || (border2.Background == null)) ? new SolidColorBrush(Colors.Black) : border2.Background);
        }

        internal virtual Encoding GetCurrentEncoding()
        {
            EncodingComboBoxItem editValue = this.CharacterSetBox.EditValue as EncodingComboBoxItem;
            return editValue?.Encoding;
        }

        private FontFamily GetFontFamily() => 
            new FontFamily(this.FontName);

        internal string GetGroupName(char c)
        {
            List<string> list;
            string str = string.Empty;
            this.CharacterNames.TryGetValue(c, out list);
            if (list == null)
            {
                return string.Empty;
            }
            foreach (string str2 in list)
            {
                if ((str2.ToLower() != str2) && (str2.ToUpper() != str2))
                {
                    str = str2;
                }
            }
            return str;
        }

        internal string GetOriginName(char c)
        {
            List<string> list;
            string alternativeName = string.Empty;
            this.CharacterNames.TryGetValue(c, out list);
            if (list == null)
            {
                return string.Empty;
            }
            foreach (string str2 in list)
            {
                if (str2.ToUpper() == str2)
                {
                    alternativeName = str2;
                }
            }
            if (string.IsNullOrEmpty(alternativeName))
            {
                alternativeName = this.GetAlternativeName(c);
            }
            if (string.IsNullOrEmpty(alternativeName))
            {
                alternativeName = this.GetGroupName(c);
            }
            return (string.IsNullOrEmpty(alternativeName) ? string.Empty : (";" + alternativeName));
        }

        private SelectionAdorner GetSelectionAdorner(FrameworkElement element)
        {
            AdornerLayer layer = AdornerHelper.FindAdornerLayer(element);
            if (layer == null)
            {
                return null;
            }
            Adorner[] adorners = layer.GetAdorners(element);
            if (adorners == null)
            {
                return null;
            }
            Func<Adorner, bool> predicate = <>c.<>9__65_0;
            if (<>c.<>9__65_0 == null)
            {
                Func<Adorner, bool> local1 = <>c.<>9__65_0;
                predicate = <>c.<>9__65_0 = adorner => adorner.Name == "SelectionAdorner";
            }
            return (adorners.First<Adorner>(predicate) as SelectionAdorner);
        }

        private void InitCharacterSetBox()
        {
            this.CharacterSetBox.Items.Add(new EncodingComboBoxItem(Encoding.Unicode));
            foreach (int num2 in this.CodePages.ToArray<int>())
            {
                Encoding encoding = DXEncoding.GetEncoding(num2);
                if (encoding != null)
                {
                    this.CharacterSetBox.Items.Add(new EncodingComboBoxItem(encoding));
                }
            }
            if (this.CharacterSetBox.Items.Count > 0)
            {
                this.CharacterSetBox.EditValue = this.CharacterSetBox.Items[0];
            }
            this.CharacterSetBox.EditValueChanged += new EditValueChangedEventHandler(this.OnCharacterSetBoxEditValueChanged);
        }

        private void InitFilter()
        {
            DevExpress.Xpf.Editors.ListItemCollection items = this.FilterBox.Items;
            items.BeginUpdate();
            try
            {
                UnicodeCategory? category = null;
                items.Add(new UnicodeCategoryComboBoxItem(category, EditorStringId.Caption_UnicodeCategoryAllSymbols));
                items.Add(new UnicodeCategoryComboBoxItem(0, EditorStringId.Caption_UnicodeCategoryUppercaseLetter));
                items.Add(new UnicodeCategoryComboBoxItem(1, EditorStringId.Caption_UnicodeCategoryLowercaseLetter));
                items.Add(new UnicodeCategoryComboBoxItem(2, EditorStringId.Caption_UnicodeCategoryTitlecaseLetter));
                items.Add(new UnicodeCategoryComboBoxItem(3, EditorStringId.Caption_UnicodeCategoryModifierLetter));
                items.Add(new UnicodeCategoryComboBoxItem(4, EditorStringId.Caption_UnicodeCategoryOtherLetter));
                items.Add(new UnicodeCategoryComboBoxItem(5, EditorStringId.Caption_UnicodeCategoryNonSpacingMark));
                items.Add(new UnicodeCategoryComboBoxItem(6, EditorStringId.Caption_UnicodeCategorySpacingCombiningMark));
                items.Add(new UnicodeCategoryComboBoxItem(7, EditorStringId.Caption_UnicodeCategoryEnclosingMark));
                items.Add(new UnicodeCategoryComboBoxItem(8, EditorStringId.Caption_UnicodeCategoryDecimalDigitNumber));
                items.Add(new UnicodeCategoryComboBoxItem(9, EditorStringId.Caption_UnicodeCategoryLetterNumber));
                items.Add(new UnicodeCategoryComboBoxItem(10, EditorStringId.Caption_UnicodeCategoryOtherNumber));
                items.Add(new UnicodeCategoryComboBoxItem(11, EditorStringId.Caption_UnicodeCategorySpaceSeparator));
                items.Add(new UnicodeCategoryComboBoxItem(12, EditorStringId.Caption_UnicodeCategoryLineSeparator));
                items.Add(new UnicodeCategoryComboBoxItem(13, EditorStringId.Caption_UnicodeCategoryParagraphSeparator));
                items.Add(new UnicodeCategoryComboBoxItem(14, EditorStringId.Caption_UnicodeCategoryControl));
                items.Add(new UnicodeCategoryComboBoxItem(15, EditorStringId.Caption_UnicodeCategoryFormat));
                items.Add(new UnicodeCategoryComboBoxItem(0x10, EditorStringId.Caption_UnicodeCategorySurrogate));
                items.Add(new UnicodeCategoryComboBoxItem(0x11, EditorStringId.Caption_UnicodeCategoryPrivateUse));
                items.Add(new UnicodeCategoryComboBoxItem(0x12, EditorStringId.Caption_UnicodeCategoryConnectorPunctuation));
                items.Add(new UnicodeCategoryComboBoxItem(0x13, EditorStringId.Caption_UnicodeCategoryDashPunctuation));
                items.Add(new UnicodeCategoryComboBoxItem(20, EditorStringId.Caption_UnicodeCategoryOpenPunctuation));
                items.Add(new UnicodeCategoryComboBoxItem(0x15, EditorStringId.Caption_UnicodeCategoryClosePunctuation));
                items.Add(new UnicodeCategoryComboBoxItem(0x16, EditorStringId.Caption_UnicodeCategoryInitialQuotePunctuation));
                items.Add(new UnicodeCategoryComboBoxItem(0x17, EditorStringId.Caption_UnicodeCategoryFinalQuotePunctuation));
                items.Add(new UnicodeCategoryComboBoxItem(0x18, EditorStringId.Caption_UnicodeCategoryOtherPunctuation));
                items.Add(new UnicodeCategoryComboBoxItem(0x19, EditorStringId.Caption_UnicodeCategoryMathSymbol));
                items.Add(new UnicodeCategoryComboBoxItem(0x1a, EditorStringId.Caption_UnicodeCategoryCurrencySymbol));
                items.Add(new UnicodeCategoryComboBoxItem(0x1b, EditorStringId.Caption_UnicodeCategoryModifierSymbol));
                items.Add(new UnicodeCategoryComboBoxItem(0x1c, EditorStringId.Caption_UnicodeCategoryOtherSymbol));
                items.Add(new UnicodeCategoryComboBoxItem(0x1d, EditorStringId.Caption_UnicodeCategoryOtherNotAssigned));
            }
            finally
            {
                items.EndUpdate();
            }
            this.FilterBox.EditValue = items[0];
            this.FilterBox.EditValueChanged += new EditValueChangedEventHandler(this.OnFilterBoxEditValueChanged);
        }

        private void InitFontFamilyComboBox()
        {
            if (this.FontNameBox.Items.Count > 0)
            {
                this.FontNameBox.SelectedIndex = 0;
            }
            this.FontNameBox.EditValueChanged += new EditValueChangedEventHandler(this.OnFontNameBoxEditValueChanged);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Core.v19.2;component/editors/office/charactermap.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void InitSearch()
        {
            this.SearchBox.TextChanged += new TextChangedEventHandler(this.SearchBox_TextChanged);
            this.SearchResult.Foreground = Brushes.Black;
        }

        private void InitSpecialList()
        {
            foreach (MemberInfo info in typeof(Characters).GetMembers())
            {
                if (info.MemberType == MemberTypes.Field)
                {
                    FieldInfo field = typeof(Characters).GetField(info.Name);
                    if (field.IsStatic || field.IsLiteral)
                    {
                        char ch = (char) field.GetValue(null);
                        TextBlock newItem = new TextBlock();
                        newItem.Text = ch.ToString() + " " + info.Name;
                        newItem.Tag = ch;
                        this.SpecialList.Items.Add(newItem);
                    }
                }
            }
            this.SpecialList.MouseLeftButtonUp += new MouseButtonEventHandler(this.SpecialList_MouseLeftButtonDown);
        }

        private void InitStateButtons()
        {
            this.CommonButton.Click += new RoutedEventHandler(this.CommonButton_Click);
            this.SpecialButton.Click += new RoutedEventHandler(this.SpecialButton_Click);
        }

        private void OnCharacterSetBoxEditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            this.UpdateAll();
        }

        private void OnCharactersGridSizeChanged()
        {
            this.CharactersGrid.ColumnDefinitions.Clear();
            this.CharactersGrid.RowDefinitions.Clear();
            this.CharactersGrid.Children.Clear();
            for (int i = 0; i < this.CharsPerLine; i++)
            {
                this.CharactersGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int j = 0; j < this.LinesPerView; j++)
            {
                this.CharactersGrid.RowDefinitions.Add(new RowDefinition());
            }
            int num3 = 0;
            while (num3 < this.LinesPerView)
            {
                int num4 = 0;
                while (true)
                {
                    if (num4 >= this.CharsPerLine)
                    {
                        num3++;
                        break;
                    }
                    TextBlock element = this.CreateTextBlock();
                    element.SetValue(Grid.RowProperty, num3);
                    element.SetValue(Grid.ColumnProperty, num4);
                    this.CharactersGrid.Children.Add(element);
                    num4++;
                }
            }
        }

        private void OnFilterBoxEditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            this.UpdateAll();
        }

        private void OnFontNameBoxEditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            this.UpdateAll();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.adornerBrush = this.GetBorderBrush();
            FloatingContainer dialogOwner = FloatingContainer.GetDialogOwner(this) as FloatingContainer;
            if (dialogOwner != null)
            {
                DialogControl content = dialogOwner.Content as DialogControl;
                if (content != null)
                {
                    content.OkButton.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void OnSelectedElementChanged(FrameworkElement oldElement, FrameworkElement newElement)
        {
            if (oldElement != null)
            {
                this.RemoveSelectionAdorner(oldElement);
            }
            TextBlock block = newElement as TextBlock;
            if ((block != null) && (block.Text.Length == 1))
            {
                this.Selection = block.Text[0];
                char c = block.Text[0];
                string[] textArray1 = new string[] { "U+", ((int) c).ToString("X"), this.GetOriginName(c), "; ", char.GetUnicodeCategory(c).ToString() };
                this.CharacterDescription.Text = string.Concat(textArray1);
                this.suppressRaiseSearchBoxTextChangedEvent = true;
                this.SearchBox.Text = ((int) c).ToString();
                this.suppressRaiseSearchBoxTextChangedEvent = false;
                this.SetSearchSymbol();
                this.AddAdorner(newElement);
            }
        }

        private void RaiseCharDoubleClick()
        {
            if (this.CharDoubleClick != null)
            {
                this.CharDoubleClick(this, EventArgs.Empty);
            }
        }

        private void RemoveSelectionAdorner(FrameworkElement element)
        {
            AdornerLayer layer = AdornerHelper.FindAdornerLayer(element);
            if (layer != null)
            {
                Adorner selectionAdorner = this.GetSelectionAdorner(element);
                if (selectionAdorner != null)
                {
                    layer.Remove(selectionAdorner);
                }
            }
        }

        private void RenewCharacters()
        {
            if (this.FontName != null)
            {
                FontFamily fontFamily = this.GetFontFamily();
                if ((this.SelectedElement != null) && !this.CommonlyUsedPanel.Children.Contains(this.SelectedElement))
                {
                    this.RemoveSelectionAdorner(this.SelectedElement);
                }
                int num = ((int) this.CharactersScrollBar.Value) * this.CharsPerLine;
                foreach (TextBlock block in this.CharactersGrid.Children)
                {
                    if (num >= this.Chars.Count)
                    {
                        block.Text = "";
                        continue;
                    }
                    char ch = this.Chars[num];
                    block.FontFamily = fontFamily;
                    block.Text = ch.ToString();
                    if (ch == this.Selection)
                    {
                        if (!ReferenceEquals(this.SelectedElement, block))
                        {
                            this.SelectedElement = block;
                        }
                        else
                        {
                            this.AddAdorner(block);
                        }
                    }
                    num++;
                }
            }
        }

        private void RenewCharactersScrollbar()
        {
            this.CharactersScrollBar.Minimum = 0.0;
            this.CharactersScrollBar.SmallChange = 1.0;
            this.CharactersScrollBar.LargeChange = this.LinesPerView;
            this.CharactersScrollBar.ViewportSize = this.LinesPerView;
            double num = (this.FontName != null) ? (((this.Chars.Count<char>() / this.charsPerLine) - this.CharactersScrollBar.ViewportSize) + 1.0) : 0.0;
            this.CharactersScrollBar.Maximum = (num > 0.0) ? num : 1.0;
        }

        private void RenewCommonlyUsed()
        {
            this.CommonlyUsedPanel.Children.Clear();
            if (this.FontName != null)
            {
                FontFamily fontFamily = this.GetFontFamily();
                foreach (char ch in this.CommonCharacters.ToArray<char>())
                {
                    TextBlock element = this.CreateTextBlock();
                    element.Text = ch.ToString();
                    element.FontWeight = FontWeights.Bold;
                    element.FontSize = 32.0;
                    element.Width = 33.0;
                    element.Height = 40.0;
                    element.Margin = new Thickness(1.0);
                    element.FontFamily = fontFamily;
                    this.CommonlyUsedPanel.Children.Add(element);
                }
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.suppressRaiseSearchBoxTextChangedEvent)
            {
                int result = -1;
                if (int.TryParse(this.SearchBox.Text, out result))
                {
                    string str = ((char) result).ToString();
                    using (IEnumerator enumerator = this.CommonlyUsedPanel.Children.GetEnumerator())
                    {
                        while (true)
                        {
                            if (!enumerator.MoveNext())
                            {
                                break;
                            }
                            TextBlock current = (TextBlock) enumerator.Current;
                            if (current.Text == str)
                            {
                                this.SelectedElement = current;
                                return;
                            }
                        }
                    }
                    using (IEnumerator enumerator2 = this.CharactersGrid.Children.GetEnumerator())
                    {
                        while (true)
                        {
                            if (!enumerator2.MoveNext())
                            {
                                break;
                            }
                            TextBlock current = (TextBlock) enumerator2.Current;
                            if (current.Text == str)
                            {
                                this.SelectedElement = current;
                                return;
                            }
                        }
                    }
                    int index = this.Chars.IndexOf(str[0]);
                    if (index > 0)
                    {
                        this.CharactersScrollBar.Value = index / this.CharsPerLine;
                        this.SearchBox_TextChanged(sender, e);
                    }
                    else
                    {
                        this.SelectedElement = null;
                        this.SetSearchSymbol();
                    }
                }
            }
        }

        private void SetSearchSymbol()
        {
            if (this.SearchBox != null)
            {
                int result = -1;
                if (!int.TryParse(this.SearchBox.Text, out result))
                {
                    result = 0x7fffffff;
                }
                if (result > 0xffff)
                {
                    result = -1;
                }
                this.SearchResult.FontFamily = this.GetFontFamily();
                this.SearchResult.Text = (result < 0) ? "Err" : ((char) result).ToString();
            }
        }

        private void SpecialButton_Click(object sender, RoutedEventArgs e)
        {
            this.SpecialButton.IsChecked = true;
            this.CommonButton.IsChecked = false;
            this.SpecialGrid.Visibility = Visibility.Visible;
            this.CommonGrid.Visibility = Visibility.Collapsed;
        }

        private void SpecialList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DevExpress.Xpf.Office.Internal.MouseHelper.IsDoubleClick(e))
            {
                this.RaiseCharDoubleClick();
            }
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.btnCommonChars = (ToggleButton) target;
                    return;

                case 2:
                    this.btnSpecialChars = (ToggleButton) target;
                    return;

                case 3:
                    this.SpecialGrid = (Grid) target;
                    return;

                case 4:
                    this.SpecialList = (ListBox) target;
                    return;

                case 5:
                    this.CommonGrid = (Grid) target;
                    return;

                case 6:
                    this.tbSearch = (TextBox) target;
                    return;

                case 7:
                    this.CharactersGrid = (Grid) target;
                    return;

                case 8:
                    this.sbCharactersScrollbar = (ScrollBar) target;
                    return;

                case 9:
                    this.cbFontFamily = (FontEdit) target;
                    return;

                case 10:
                    this.cbCharacterSet = (ComboBoxEdit) target;
                    return;

                case 11:
                    this.cbFilter = (ComboBoxEdit) target;
                    return;

                case 12:
                    this.tbSearchResult = (TextBlock) target;
                    return;

                case 13:
                    this.pnlCommonlyUsed = (StackPanel) target;
                    return;

                case 14:
                    this.CharacterDescription = (TextBlock) target;
                    return;
            }
            this._contentLoaded = true;
        }

        private void tb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                this.RaiseCharDoubleClick();
            }
        }

        private void tb_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBlock block = (TextBlock) sender;
            if (block.Text.Length == 1)
            {
                this.SelectedElement = block;
            }
        }

        private Brush TryGetBrushByState(string stateName)
        {
            FrameworkElement element = LayoutHelper.FindElementByName(this.SearchBox, stateName);
            Grid treeRoot = element as Grid;
            if (treeRoot != null)
            {
                element = LayoutHelper.FindElementByType<DXBorder>(treeRoot);
            }
            DXBorder border = element as DXBorder;
            return border?.BorderBrush;
        }

        private void UpdateAll()
        {
            this.chars = null;
            this.RenewCharacters();
            this.RenewCharactersScrollbar();
            this.RenewCommonlyUsed();
            this.SetSearchSymbol();
        }

        public int CharsPerLine
        {
            get => 
                this.charsPerLine;
            set
            {
                if (value != this.CharsPerLine)
                {
                    this.charsPerLine = value;
                    this.OnCharactersGridSizeChanged();
                }
            }
        }

        public int LinesPerView
        {
            get => 
                this.linesPerView;
            set
            {
                if (value != this.LinesPerView)
                {
                    this.linesPerView = value;
                    this.OnCharactersGridSizeChanged();
                }
            }
        }

        private ToggleButton CommonButton =>
            this.btnCommonChars;

        private ToggleButton SpecialButton =>
            this.btnSpecialChars;

        public Panel CommonlyUsedPanel =>
            this.pnlCommonlyUsed;

        public ComboBoxEdit FilterBox =>
            this.cbFilter;

        public TextBox SearchBox =>
            this.tbSearch;

        public TextBlock SearchResult =>
            this.tbSearchResult;

        public FontEdit FontNameBox =>
            this.cbFontFamily;

        public ScrollBar CharactersScrollBar =>
            this.sbCharactersScrollbar;

        public virtual string FontName
        {
            get
            {
                string editValue = this.FontNameBox.EditValue as string;
                if (editValue != null)
                {
                    return editValue;
                }
                FontFamily family = this.FontNameBox.EditValue as FontFamily;
                return ((family == null) ? string.Empty : family.Source);
            }
            set => 
                this.FontNameBox.EditValue = value;
        }

        public ComboBoxEdit CharacterSetBox =>
            this.cbCharacterSet;

        public virtual UnicodeCategory? FilterCategory
        {
            get
            {
                UnicodeCategoryComboBoxItem editValue = this.FilterBox.EditValue as UnicodeCategoryComboBoxItem;
                if (editValue != null)
                {
                    return editValue.UnicodeCategory;
                }
                return null;
            }
        }

        public Dictionary<char, List<string>> CharacterNames
        {
            get
            {
                if (this.characterNames == null)
                {
                    this.CreateCharacterNames();
                }
                return this.characterNames;
            }
        }

        public List<char> Chars
        {
            get
            {
                if (this.chars == null)
                {
                    Encoding currentEncoding = this.GetCurrentEncoding();
                    this.chars = ((currentEncoding == null) || ReferenceEquals(currentEncoding, Encoding.Unicode)) ? this.BuildChars(null, 0xffff).ToList<char>() : this.BuildChars(currentEncoding, 0xff).ToList<char>();
                }
                return this.chars;
            }
        }

        public char Selection { get; private set; }

        public IServiceProvider ServiceProvider { get; set; }

        private FrameworkElement SelectedElement
        {
            get => 
                this.selectedElement;
            set
            {
                if (!ReferenceEquals(this.selectedElement, value))
                {
                    FrameworkElement selectedElement = this.SelectedElement;
                    this.selectedElement = value;
                    this.OnSelectedElementChanged(selectedElement, value);
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CharacterMapControl.<>c <>9 = new CharacterMapControl.<>c();
            public static Predicate<FrameworkElement> <>9__61_0;
            public static Func<Adorner, bool> <>9__65_0;

            internal bool <GetBorderBrush>b__61_0(FrameworkElement elem) => 
                (elem is Border) && (elem.Name != "focus");

            internal bool <GetSelectionAdorner>b__65_0(Adorner adorner) => 
                adorner.Name == "SelectionAdorner";
        }

        private static class Characters
        {
            public const char Bullet = '•';
            public const char ClosingDoubleQuotationMark = '”';
            public const char ClosingSingleQuotationMark = '’';
            public const char Colon = ':';
            public const char ColumnBreak = '\x000e';
            public const char CopyrightSymbol = '\x00a9';
            public const char CurrencySign = '\x00a4';
            public const char Dash = '-';
            public const char Dot = '.';
            public const char Ellipsis = '…';
            public const char EmDash = '—';
            public const char EmSpace = ' ';
            public const char EnDash = '–';
            public const char EnSpace = ' ';
            public const char EqualSign = '=';
            public const char FloatingObjectMark = '\b';
            public const char Hyphen = '\x001f';
            public const char LeftDoubleQuote = '“';
            public const char LeftSingleQuote = '‘';
            public const char LineBreak = '\v';
            public const char MiddleDot = '\x00b7';
            public const char NonBreakingSpace = '\x00a0';
            public const char ObjectMark = '￼';
            public const char OpeningDoubleQuotationMark = '“';
            public const char OpeningSingleQuotationMark = '‘';
            public const char OptionalHyphen = '\x00ad';
            public const char PageBreak = '\f';
            public const char ParagraphMark = '\r';
            public const char PilcrowSign = '\x00b6';
            public const char QmSpace = ' ';
            public const char RegisteredTrademarkSymbol = '\x00ae';
            public const char RightDoubleQuote = '”';
            public const char RightSingleQuote = '’';
            public const char SectionMark = '\x001d';
            public const char SeparatorMark = '|';
            public const char Space = ' ';
            public const char TabMark = '\t';
            public const char TrademarkSymbol = '™';
            public const char Underscore = '_';
        }

        private class SelectionAdorner : Adorner
        {
            private Brush brush;
            private System.Windows.Media.TranslateTransform translateTransform;

            public SelectionAdorner(UIElement element, Brush brush) : base(element)
            {
                this.brush = brush;
                this.translateTransform = new System.Windows.Media.TranslateTransform(0.0, 0.0);
            }

            protected override void OnRender(DrawingContext drawingContext)
            {
                base.OnRender(drawingContext);
                drawingContext.PushTransform(this.TranslateTransform);
                Pen pen = new Pen(this.brush, 1.0);
                drawingContext.DrawRectangle(null, pen, new Rect(new Point(0.0, 0.0), base.AdornedElement.RenderSize));
            }

            public System.Windows.Media.TranslateTransform TranslateTransform =>
                this.translateTransform;
        }
    }
}

