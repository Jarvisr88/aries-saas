namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;

    public class TextBlockService : DependencyObject
    {
        public const string SearchStringDelimiter = "\n";
        public static readonly DependencyProperty TextInfoProperty;
        public static readonly DependencyProperty AllowIsTextTrimmedProperty;
        protected static readonly DependencyPropertyKey IsTextTrimmedPropertyKey;
        public static readonly DependencyProperty HighlightedTextAppearanceProperty;
        public static readonly DependencyProperty HighlightedTextCriteriaProperty;
        public static readonly DependencyProperty IsTextTrimmedProperty;
        public static readonly DependencyProperty EnableTextHighlightingProperty;
        public static readonly DependencyProperty HighlightedTextProperty;
        public static readonly DependencyProperty ListenForegroundChangesProperty;
        public static readonly RoutedEvent IsTextTrimmedChangedEvent;
        public static readonly RoutedEvent HighlightedTextChangedEvent;
        internal const string DefaultTextValue = " ";
        private static Func<TextBlock, object> _firstLineFieldGetter = ReflectionHelper.CreateFieldGetter<TextBlock, object>(typeof(TextBlock), "_firstLine", BindingFlags.NonPublic | BindingFlags.Instance);
        private static Func<TextBlock, object> _subsequentLinesFieldGetter = ReflectionHelper.CreateFieldGetter<TextBlock, object>(typeof(TextBlock), "_subsequentLines", BindingFlags.NonPublic | BindingFlags.Instance);
        private static Func<object, double> _widthFieldGetter;
        private static Func<object, double> _heightFieldGetter;
        private static Func<object, double> _baselineFieldGetter;

        static TextBlockService()
        {
            Type ownerType = typeof(TextBlockService);
            TextInfoProperty = DependencyPropertyManager.RegisterAttached("TextInfo", typeof(TextBlockInfo), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(TextBlockService.TextInfoChanged)));
            HighlightedTextProperty = DependencyPropertyManager.RegisterAttached("HighlightedText", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(TextBlockService.HighlightedTextChanged)));
            EnableTextHighlightingProperty = DependencyPropertyManager.RegisterAttached("EnableTextHighlighting", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(TextBlockService.EnableTextHighlightingChanged)));
            AllowIsTextTrimmedProperty = DependencyPropertyManager.RegisterAttached("AllowIsTextTrimmed", typeof(bool), typeof(TextBlockService), new FrameworkPropertyMetadata(false));
            IsTextTrimmedPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("IsTextTrimmed", typeof(bool), typeof(TextBlockService), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(TextBlockService.OnIsTextTrimmedChanged)));
            HighlightedTextAppearanceProperty = DependencyPropertyManager.RegisterAttached("HighlightedTextAppearance", typeof(IHighlighterAppearance), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(TextBlockService.HighlightedTextChanged)));
            HighlightedTextCriteriaProperty = DependencyPropertyManager.RegisterAttached("HighlightedTextCriteria", typeof(HighlightedTextCriteria), ownerType, new FrameworkPropertyMetadata(HighlightedTextCriteria.StartsWith, FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(TextBlockService.HighlightedTextChanged)));
            IsTextTrimmedProperty = IsTextTrimmedPropertyKey.DependencyProperty;
            EventManager.RegisterClassHandler(typeof(TextBlock), UIElement.MouseEnterEvent, new MouseEventHandler(TextBlockService.OnTextBlockMouseEnter), true);
            EventManager.RegisterClassHandler(typeof(TextBlock), UIElement.MouseLeaveEvent, new MouseEventHandler(TextBlockService.OnTextBlockMouseLeave), true);
            IsTextTrimmedChangedEvent = EventManager.RegisterRoutedEvent("IsTextTrimmedChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(TextBlockService));
            HighlightedTextChangedEvent = EventManager.RegisterRoutedEvent("HighlightedTextChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(TextBlockService));
            ListenForegroundChangesProperty = DependencyPropertyManager.RegisterAttached("ListenForegroundChanges", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, new PropertyChangedCallback(TextBlockService.ListenForegroundChangesChanged)));
        }

        public static void AddHighlightedTextChangedHandler(DependencyObject dObj, RoutedEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(HighlightedTextChangedEvent, handler);
            }
        }

        public static void AddIsTextTrimmedChangedHandler(DependencyObject dObj, RoutedEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(IsTextTrimmedChangedEvent, handler);
            }
        }

        public static bool CalcIsTextTrimmed(RenderTextBlockContext textBlockContext)
        {
            if (textBlockContext.Child != null)
            {
                TextBlock control = textBlockContext.Child.Control as TextBlock;
                if (control != null)
                {
                    return CalcIsTextTrimmed(control);
                }
            }
            return ((textBlockContext.FormattedTextContainer != null) && textBlockContext.FormattedTextContainer.HasCollapsedLines);
        }

        public static bool CalcIsTextTrimmed(TextBlock textBlock)
        {
            bool flag2;
            if (!textBlock.IsArrangeValid)
            {
                return false;
            }
            bool? nullable = CalcIsTextTrimmed_NoTrimmingOWrapping(textBlock, textBlock.TextTrimming, textBlock.TextWrapping);
            if (nullable != null)
            {
                return nullable.Value;
            }
            object item = _firstLineFieldGetter(textBlock);
            if (item == null)
            {
                return false;
            }
            object obj3 = _subsequentLinesFieldGetter(textBlock);
            List<object> list = (obj3 != null) ? ((IEnumerable) obj3).Cast<object>().ToList<object>() : new List<object>();
            list.Insert(0, item);
            if ((_widthFieldGetter == null) || (_heightFieldGetter == null))
            {
                Type declaringType = item.GetType();
                _widthFieldGetter = ReflectionHelper.CreateFieldGetter<object, double>(declaringType, "_width", BindingFlags.NonPublic | BindingFlags.Instance);
                _heightFieldGetter = ReflectionHelper.CreateFieldGetter<object, double>(declaringType, "_height", BindingFlags.NonPublic | BindingFlags.Instance);
                _baselineFieldGetter = ReflectionHelper.CreateFieldGetter<object, double>(declaringType, "_baseline", BindingFlags.NonPublic | BindingFlags.Instance);
            }
            Geometry clip = VisualTreeHelper.GetClip(textBlock);
            Func<Geometry, double> evaluator = <>c.<>9__74_0;
            if (<>c.<>9__74_0 == null)
            {
                Func<Geometry, double> local1 = <>c.<>9__74_0;
                evaluator = <>c.<>9__74_0 = x => x.Bounds.Width;
            }
            Size size = new Size(clip.Return<Geometry, double>(evaluator, () => textBlock.RenderSize.Width), clip.Return<Geometry, double>(<>c.<>9__74_2 ??= x => x.Bounds.Bottom, () => textBlock.RenderSize.Height));
            bool round = textBlock.UseLayoutRounding || ((clip != null) && (clip.Bounds.Width == Math.Round(textBlock.RenderSize.Width)));
            double num = 0.0;
            using (List<object>.Enumerator enumerator = list.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        object current = enumerator.Current;
                        if (0.0 < ConditionalRound(_widthFieldGetter(current) - size.Width, round))
                        {
                            flag2 = true;
                        }
                        else
                        {
                            if (0.0 >= ((num + _baselineFieldGetter(current)) - size.Height))
                            {
                                num += _heightFieldGetter(current);
                                continue;
                            }
                            flag2 = true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag2;
        }

        private static bool? CalcIsTextTrimmed_NoTrimmingOWrapping(FrameworkElement element, TextTrimming trimming, TextWrapping wrapping)
        {
            if ((trimming == TextTrimming.None) || (wrapping != TextWrapping.NoWrap))
            {
                Geometry layoutClip = LayoutInformation.GetLayoutClip(element);
                if (layoutClip != null)
                {
                    return new bool?((layoutClip.Bounds.Width > 0.0) || (layoutClip.Bounds.Height > 0.0));
                }
            }
            return null;
        }

        private static void ClearHighlighting(TextBlock textBlock, string text)
        {
            textBlock.Text = string.IsNullOrEmpty(text) ? " " : text;
        }

        private static double ConditionalRound(double value, bool round) => 
            round ? Math.Round(value) : Math.Round(value, 2);

        private static Inline CreateDefaultHighlightedInline(string highlightedText)
        {
            System.Windows.Documents.Run run1 = new System.Windows.Documents.Run();
            run1.Text = highlightedText;
            System.Windows.Documents.Run run = run1;
            return new Bold { Inlines = { run } };
        }

        private static Inline CreateEditorHighlightedInline(FrameworkElement editor, IHighlighterAppearance settings, string highlightedText)
        {
            Inline inline = (settings == null) ? CreateDefaultHighlightedInline(highlightedText) : settings.CreateInlineForHighlighting(editor, highlightedText);
            inline.Tag = true;
            return inline;
        }

        private static Inline CreateHighlightedInline(TextBlock textBlock, IHighlighterAppearance editSettings, string highlightedText)
        {
            IHighlighterAppearance settings = null;
            BaseEdit ownerEdit = BaseEdit.GetOwnerEdit(textBlock);
            FrameworkElement editor = ownerEdit;
            if ((ownerEdit != null) && (editSettings == null))
            {
                settings = ownerEdit.PropertyProvider.StyleSettings;
            }
            if (settings == null)
            {
                if (editSettings != null)
                {
                    return CreateEditorHighlightedInline(editor ?? textBlock, editSettings ?? new TextEditStyleSettings(), highlightedText);
                }
                InplaceBaseEdit edit2 = LayoutHelper.FindParentObject<InplaceBaseEdit>(textBlock);
                if (edit2 != null)
                {
                    editor = edit2;
                    settings = edit2.Settings.StyleSettings ?? new TextEditStyleSettings();
                }
            }
            return CreateEditorHighlightedInline(editor, settings, highlightedText);
        }

        private static System.Windows.Documents.Run CreateRun(string text, TextBlock textBlock)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }
            System.Windows.Documents.Run run1 = new System.Windows.Documents.Run();
            run1.Text = text;
            run1.Foreground = textBlock.Foreground;
            run1.FontFamily = textBlock.FontFamily;
            run1.FontSize = textBlock.FontSize;
            run1.FontWeight = textBlock.FontWeight;
            run1.FontStyle = textBlock.FontStyle;
            return run1;
        }

        private static void EnableTextHighlightingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (Net45Detector.IsNet45())
            {
                TextBlock block = d as TextBlock;
                if (block != null)
                {
                    BehaviorCollection source = Interaction.GetBehaviors(block);
                    if ((bool) e.NewValue)
                    {
                        source.Add(new TextHighlightingBehavior());
                    }
                    else
                    {
                        Func<Behavior, bool> predicate = <>c.<>9__21_0;
                        if (<>c.<>9__21_0 == null)
                        {
                            Func<Behavior, bool> local1 = <>c.<>9__21_0;
                            predicate = <>c.<>9__21_0 = x => x is TextHighlightingBehavior;
                        }
                        Behavior behavior = source.FirstOrDefault<Behavior>(predicate);
                        if (behavior != null)
                        {
                            source.Remove(behavior);
                        }
                    }
                }
            }
        }

        public static bool GetAllowIsTextTrimmed(DependencyObject element) => 
            (bool) element.GetValue(AllowIsTextTrimmedProperty);

        public static bool GetEnableTextHighlighting(DependencyObject d) => 
            (bool) d.GetValue(EnableTextHighlightingProperty);

        public static string GetFirstLineFromText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }
            string[] separator = new string[] { Environment.NewLine };
            return text.Split(separator, StringSplitOptions.None).FirstOrDefault<string>();
        }

        public static string GetHighlightedText(DependencyObject d) => 
            (string) d.GetValue(HighlightedTextProperty);

        public static IHighlighterAppearance GetHighlightedTextAppearance(DependencyObject d) => 
            (IHighlighterAppearance) d.GetValue(HighlightedTextAppearanceProperty);

        public static HighlightedTextCriteria GetHighlightedTextCriteria(DependencyObject d) => 
            (HighlightedTextCriteria) d.GetValue(HighlightedTextCriteriaProperty);

        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        public static bool GetIsTextTrimmed(TextBlock target) => 
            (bool) target.GetValue(IsTextTrimmedProperty);

        public static bool GetListenForegroundChanges(DependencyObject d) => 
            (bool) d.GetValue(ListenForegroundChangesProperty);

        [IteratorStateMachine(typeof(<GetMultipleInlines>d__39))]
        private static IEnumerable<Inline> GetMultipleInlines(TextBlock textBlock, IHighlighterAppearance settings, string text, IEnumerable<string> searchString, HighlightedTextCriteria criteria)
        {
            IEnumerable<StringDescriptor> first = new List<StringDescriptor>();
            foreach (string str2 in searchString)
            {
                IEnumerable<StringDescriptor> second = Split(text, criteria, str2);
                first = Merge(first, second);
            }
            int startIndex = 0;
            Func<StringDescriptor, int> keySelector = <>c.<>9__39_0;
            if (<>c.<>9__39_0 == null)
            {
                Func<StringDescriptor, int> local1 = <>c.<>9__39_0;
                keySelector = <>c.<>9__39_0 = descr => descr.SelectionStart;
            }
            IEnumerator<StringDescriptor> enumerator = first.OrderBy<StringDescriptor, int>(keySelector).GetEnumerator();
            while (true)
            {
                StringDescriptor current;
                if (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    if (startIndex < current.SelectionStart)
                    {
                        yield return CreateRun(text.Substring(startIndex, current.SelectionStart - startIndex), textBlock);
                    }
                }
                else
                {
                    enumerator = null;
                    string str = text.Substring(startIndex, text.Length - startIndex);
                    if (!string.IsNullOrEmpty(str))
                    {
                        yield return CreateRun(str, textBlock);
                    }
                }
                yield return CreateHighlightedInline(textBlock, settings, text.Substring(current.SelectionStart, current.SelectionEnd - current.SelectionStart));
                startIndex = current.SelectionEnd;
                current = null;
            }
        }

        public static string GetStartWithPart(string originalString, string searchString) => 
            GetStartWithPart(originalString, searchString, 0);

        public static string GetStartWithPart(string originalString, string searchString, int startIndex)
        {
            string candidate = originalString.Substring(startIndex, Math.Min(searchString.Length, originalString.Length - startIndex));
            if (IsSame(searchString, candidate))
            {
                return candidate;
            }
            for (int i = startIndex + 1; i <= originalString.Length; i++)
            {
                string str2 = originalString.Substring(startIndex, i - startIndex);
                if (StartWith(searchString, str2))
                {
                    return str2;
                }
            }
            return string.Empty;
        }

        public static TextBlockInfo GetTextInfo(DependencyObject d) => 
            (TextBlockInfo) d.GetValue(TextInfoProperty);

        private static void HighlightedTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBlock block = d as TextBlock;
            if ((block != null) && ((bool) block.GetValue(EnableTextHighlightingProperty)))
            {
                block.RaiseEvent(new RoutedEventArgs(HighlightedTextChangedEvent));
            }
        }

        private static bool IsHighlightedInline(Inline inline) => 
            (inline.Tag != null) && inline.Tag.Equals(true);

        public static bool IsSame(string editText, string candidate) => 
            string.Compare(candidate, editText, true) == 0;

        private static void ListenForegroundChangesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBlock component = d as TextBlock;
            if (component != null)
            {
                DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromProperty(TextBlock.ForegroundProperty, typeof(TextBlock));
                DependencyPropertyDescriptor descriptor2 = DependencyPropertyDescriptor.FromProperty(TextBlock.FontWeightProperty, typeof(TextBlock));
                DependencyPropertyDescriptor descriptor3 = DependencyPropertyDescriptor.FromProperty(TextBlock.FontFamilyProperty, typeof(TextBlock));
                DependencyPropertyDescriptor descriptor4 = DependencyPropertyDescriptor.FromProperty(TextBlock.FontSizeProperty, typeof(TextBlock));
                if ((bool) e.NewValue)
                {
                    component.Unloaded += new RoutedEventHandler(TextBlockService.TextBlockUnloaded);
                    descriptor.AddValueChanged(component, new EventHandler(TextBlockService.TextBlockFontChanged));
                    descriptor2.AddValueChanged(component, new EventHandler(TextBlockService.TextBlockFontChanged));
                    descriptor3.AddValueChanged(component, new EventHandler(TextBlockService.TextBlockFontChanged));
                    descriptor4.AddValueChanged(component, new EventHandler(TextBlockService.TextBlockFontChanged));
                }
                else
                {
                    component.Unloaded -= new RoutedEventHandler(TextBlockService.TextBlockUnloaded);
                    descriptor.RemoveValueChanged(component, new EventHandler(TextBlockService.TextBlockFontChanged));
                    descriptor2.RemoveValueChanged(component, new EventHandler(TextBlockService.TextBlockFontChanged));
                    descriptor3.RemoveValueChanged(component, new EventHandler(TextBlockService.TextBlockFontChanged));
                    descriptor4.RemoveValueChanged(component, new EventHandler(TextBlockService.TextBlockFontChanged));
                }
            }
        }

        private static IEnumerable<StringDescriptor> Merge(IEnumerable<StringDescriptor> first, IEnumerable<StringDescriptor> second)
        {
            Stack<StringDescriptor> stack = new Stack<StringDescriptor>();
            Stack<StringDescriptor> stack2 = new Stack<StringDescriptor>();
            foreach (StringDescriptor descriptor in first.Append<StringDescriptor>(second))
            {
                StringDescriptor item = descriptor;
                if (stack.Count == 0)
                {
                    stack.Push(item);
                }
                while (true)
                {
                    if (stack.Count <= 0)
                    {
                        stack2.Push(item);
                        break;
                    }
                    StringDescriptor descriptor3 = stack.Pop();
                    if (!descriptor3.Intersect(descriptor))
                    {
                        stack2.Push(descriptor3);
                        continue;
                    }
                    item = descriptor3.Merge(item);
                    while (stack2.Count > 0)
                    {
                        stack.Push(stack2.Pop());
                    }
                }
            }
            return stack2;
        }

        private static void OnIsTextTrimmedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = d as UIElement;
            if (element != null)
            {
                element.RaiseEvent(new RoutedEventArgs(IsTextTrimmedChangedEvent));
            }
        }

        private static void OnTextBlockMouseEnter(object sender, MouseEventArgs e)
        {
            UpdateIsTextTrimmed(sender as TextBlock, true);
        }

        private static void OnTextBlockMouseLeave(object sender, MouseEventArgs e)
        {
            UpdateIsTextTrimmed(sender as TextBlock, false);
        }

        public static void RemoveHighlightedTextChangedHandler(DependencyObject dObj, RoutedEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(HighlightedTextChangedEvent, handler);
            }
        }

        public static void RemoveIsTextTrimmedChangedHandler(DependencyObject dObj, RoutedEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(IsTextTrimmedChangedEvent, handler);
            }
        }

        public static void SetAllowIsTextTrimmed(DependencyObject element, bool value)
        {
            element.SetValue(AllowIsTextTrimmedProperty, value);
        }

        public static void SetEnableTextHighlighting(DependencyObject d, bool value)
        {
            d.SetValue(EnableTextHighlightingProperty, value);
        }

        public static void SetHighlightedText(DependencyObject d, string value)
        {
            d.SetValue(HighlightedTextProperty, value);
        }

        public static void SetHighlightedTextAppearance(DependencyObject d, IHighlighterAppearance value)
        {
            d.SetValue(HighlightedTextAppearanceProperty, value);
        }

        public static void SetHighlightedTextCriteria(DependencyObject d, HighlightedTextCriteria value)
        {
            d.SetValue(HighlightedTextCriteriaProperty, value);
        }

        internal static void SetIsTextTrimmed(TextBlock target, bool value)
        {
            target.SetValue(IsTextTrimmedPropertyKey, value);
        }

        public static void SetListenForegroundChanges(DependencyObject d, bool value)
        {
            d.SetValue(ListenForegroundChangesProperty, value);
        }

        public static void SetTextInfo(DependencyObject d, TextBlockInfo value)
        {
            d.SetValue(TextInfoProperty, value);
        }

        private static IEnumerable<StringDescriptor> Split(string text, HighlightedTextCriteria criteria, string highlightedText) => 
            (criteria != HighlightedTextCriteria.Regex) ? ((criteria == HighlightedTextCriteria.Contains) ? SplitContains(text, highlightedText) : SplitStartWith(text, highlightedText)) : SplitRegex(text, highlightedText);

        public static IEnumerable<StringDescriptor> SplitContains(string text, string highlightedText)
        {
            List<StringDescriptor> list = new List<StringDescriptor>();
            if (string.IsNullOrEmpty(highlightedText))
            {
                return list;
            }
            CompareInfo compareInfo = CultureInfo.CurrentUICulture.CompareInfo;
            int startIndex = compareInfo.IndexOf(text, highlightedText, CompareOptions.IgnoreCase);
            if (startIndex < 0)
            {
                return list;
            }
            while (true)
            {
                if (startIndex > -1)
                {
                    StringDescriptor item = SplitStartWith(text, highlightedText, startIndex).LastOrDefault<StringDescriptor>();
                    if (item != null)
                    {
                        list.Add(item);
                        startIndex = compareInfo.IndexOf(text, highlightedText, item.SelectionEnd, CompareOptions.IgnoreCase);
                        continue;
                    }
                }
                return list;
            }
        }

        public static IEnumerable<StringDescriptor> SplitRegex(string text, string highlightedText)
        {
            List<StringDescriptor> list = new List<StringDescriptor>();
            if (!string.IsNullOrEmpty(highlightedText))
            {
                foreach (Group group in new Regex(highlightedText, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase).Match(text).Groups)
                {
                    if (group.Success)
                    {
                        StringDescriptor item = new StringDescriptor();
                        item.SelectionStart = group.Index;
                        item.SelectionEnd = group.Index + group.Length;
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public static IEnumerable<StringDescriptor> SplitStartWith(string text, string highlightedText) => 
            SplitStartWith(text, highlightedText, 0);

        public static IEnumerable<StringDescriptor> SplitStartWith(string text, string highlightedText, int startIndex)
        {
            List<StringDescriptor> list = new List<StringDescriptor>();
            if (!string.IsNullOrEmpty(highlightedText))
            {
                string str = GetStartWithPart(text, highlightedText, startIndex);
                if (!string.IsNullOrEmpty(str))
                {
                    StringDescriptor item = new StringDescriptor();
                    item.SelectionStart = startIndex;
                    item.SelectionEnd = startIndex + str.Length;
                    list.Add(item);
                }
            }
            return list;
        }

        public static bool StartWith(string searchString, string originalString) => 
            originalString.StartsWith(searchString, true, CultureInfo.CurrentCulture);

        private static void StopListenForegroundChanges(TextBlock textBlock)
        {
            textBlock.SetValue(ListenForegroundChangesProperty, false);
        }

        public static void SubscribeIsTextTrimmedChanged(TextBlock textBlock)
        {
            if (!FrameworkElementHelper.GetIsLoaded(textBlock))
            {
                FrameworkElementHelper.SetIsLoaded(textBlock, true);
                textBlock.MouseEnter += new MouseEventHandler(TextBlockService.OnTextBlockMouseEnter);
                textBlock.MouseLeave += new MouseEventHandler(TextBlockService.OnTextBlockMouseLeave);
            }
        }

        private static void TextBlockFontChanged(object sender, EventArgs e)
        {
            UpdateInlinesOnFontChanged((TextBlock) sender);
        }

        private static void TextBlockUnloaded(object sender, RoutedEventArgs e)
        {
            StopListenForegroundChanges((TextBlock) sender);
        }

        private static void TextInfoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBlock textBlock = d as TextBlock;
            if (textBlock != null)
            {
                TextBlockInfo newValue = e.NewValue as TextBlockInfo;
                if (newValue == null)
                {
                    ClearHighlighting(textBlock, null);
                }
                else
                {
                    UpdateTextBlock(textBlock, newValue.Appearance, newValue.Text, newValue.HighlightedText, newValue.HighlightedTextCriteria);
                }
            }
        }

        private static void UpdateInlines(TextBlock textBlock, string text, IEnumerable<string> searchStrings, Func<IEnumerable<string>, IEnumerable<Inline>> getInlines)
        {
            IEnumerable<Inline> range = getInlines(searchStrings);
            if (range == null)
            {
                ClearHighlighting(textBlock, text);
            }
            else
            {
                try
                {
                    textBlock.SetValue(TextHighlightingBehavior.IsTextBlockUpdatingProperty, true);
                    textBlock.Inlines.Clear();
                    textBlock.Inlines.AddRange(range);
                }
                finally
                {
                    textBlock.SetValue(TextHighlightingBehavior.IsTextBlockUpdatingProperty, false);
                }
            }
        }

        private static void UpdateInlinesOnFontChanged(TextBlock textBlock)
        {
            foreach (Inline inline in textBlock.Inlines)
            {
                if (!IsHighlightedInline(inline))
                {
                    inline.Foreground = textBlock.Foreground;
                    inline.FontWeight = textBlock.FontWeight;
                    inline.FontFamily = textBlock.FontFamily;
                    inline.FontSize = textBlock.FontSize;
                }
            }
        }

        public static void UpdateIsTextTrimmed(TextBlock textBlock, bool enabled)
        {
            if ((textBlock != null) && GetAllowIsTextTrimmed(textBlock))
            {
                if (enabled)
                {
                    SetIsTextTrimmed(textBlock, CalcIsTextTrimmed(textBlock));
                }
                else
                {
                    textBlock.ClearValue(IsTextTrimmedPropertyKey);
                }
            }
        }

        public static void UpdateTextBlock(TextBlock textBlock, string text, string highlightedText, HighlightedTextCriteria criteria)
        {
            UpdateTextBlock(textBlock, null, text, highlightedText, criteria);
        }

        public static void UpdateTextBlock(TextBlock textBlock, IHighlighterAppearance settings, string text, string highlightedText, HighlightedTextCriteria criteria)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(highlightedText))
            {
                ClearHighlighting(textBlock, text);
            }
            else
            {
                textBlock.SetValue(ListenForegroundChangesProperty, true);
                string[] separator = new string[] { "\n" };
                string[] searchStrings = highlightedText.Split(separator, StringSplitOptions.None);
                UpdateInlines(textBlock, text, searchStrings, textOperand => GetMultipleInlines(textBlock, settings, text, textOperand, criteria));
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TextBlockService.<>c <>9 = new TextBlockService.<>c();
            public static Func<Behavior, bool> <>9__21_0;
            public static Func<TextBlockService.StringDescriptor, int> <>9__39_0;
            public static Func<Geometry, double> <>9__74_0;
            public static Func<Geometry, double> <>9__74_2;

            internal double <CalcIsTextTrimmed>b__74_0(Geometry x) => 
                x.Bounds.Width;

            internal double <CalcIsTextTrimmed>b__74_2(Geometry x) => 
                x.Bounds.Bottom;

            internal bool <EnableTextHighlightingChanged>b__21_0(Behavior x) => 
                x is TextHighlightingBehavior;

            internal int <GetMultipleInlines>b__39_0(TextBlockService.StringDescriptor descr) => 
                descr.SelectionStart;
        }


        public class StringDescriptor
        {
            private bool Equals(TextBlockService.StringDescriptor other) => 
                (other != null) ? (!ReferenceEquals(this, other) ? ((other.SelectionStart == this.SelectionStart) && (other.SelectionEnd == this.SelectionEnd)) : true) : false;

            public override bool Equals(object obj) => 
                (obj != null) ? (!ReferenceEquals(this, obj) ? (!(obj.GetType() != typeof(TextBlockService.StringDescriptor)) ? this.Equals((TextBlockService.StringDescriptor) obj) : false) : true) : false;

            public override int GetHashCode() => 
                (this.SelectionStart * 0x18d) ^ this.SelectionEnd;

            public bool Intersect(TextBlockService.StringDescriptor stringDescriptor) => 
                Math.Max(this.SelectionStart, stringDescriptor.SelectionStart) <= Math.Min(this.SelectionEnd, stringDescriptor.SelectionEnd);

            public TextBlockService.StringDescriptor Merge(TextBlockService.StringDescriptor stringDescriptor)
            {
                if (!this.Intersect(stringDescriptor))
                {
                    throw new ArgumentException("intersect");
                }
                TextBlockService.StringDescriptor descriptor1 = new TextBlockService.StringDescriptor();
                descriptor1.SelectionStart = Math.Min(this.SelectionStart, stringDescriptor.SelectionStart);
                descriptor1.SelectionEnd = Math.Max(this.SelectionEnd, stringDescriptor.SelectionEnd);
                return descriptor1;
            }

            public int SelectionStart { get; set; }

            public int SelectionEnd { get; set; }
        }
    }
}

