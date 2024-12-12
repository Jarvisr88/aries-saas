namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Themes;
    using DevExpress.Xpf.Editors.Validation;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;

    public class InplaceBaseEdit : Chrome, IInplaceBaseEdit, IBaseEdit, IInputElement, IChrome
    {
        private const string BorderName = "PART_Border";
        private const string GlowName = "PART_Glow";
        private const string ContentRootName = "PART_Root";
        private const string EditorName = "PART_Editor";
        private const string ErrorProviderName = "PART_ErrorProvider";
        private const string LeftButtonsName = "PART_LeftButtons";
        private const string RightButtonsName = "PART_RightButtons";
        public static readonly DependencyProperty EditValueProperty;
        public static readonly DependencyProperty EditCoreStyleProperty;
        private bool initialized;
        private bool showBorder;
        private bool showText;
        private HorizontalAlignment hca;
        private VerticalAlignment vca;
        private bool allowDefaultButton;
        private bool showEditorButtons;
        private BaseValidationError validationError;
        private DataTemplate validationErrorTemplate;
        private DataTemplate errorToolTipContentTemplate;
        private BaseEditSettings editSettings;
        private DevExpress.Xpf.Editors.EditMode editMode;
        private readonly Locker supportInitializeLocker;
        private TextTrimming textTrimming;
        private TextWrapping textWrapping;
        private RenderTextBlockContext textBlock;
        private EditorRenderCheckBoxContext checkBox;
        private RenderBaseEditContext activeEditor;
        private RenderEditorControlContext editorControl;
        private RenderItemsControlContext leftButtonsItemsControl;
        private RenderItemsControlContext rightButtonsItemsControl;
        private RenderContentControlContext errorProvider;
        private RenderControlContext borderControl;
        private RenderControlContext glowControl;
        private FrameworkRenderElementContext contentRoot;
        private IDisplayTextProvider displayTextProvider;
        private string displayText;
        private bool isNullTextVisible;
        private bool isReadOnly;
        private bool hasTextDecorations;
        private bool applyItemTemplateToSelectedItem;
        private ControlTemplate displayTemplate;
        private ControlTemplate editTemplate;
        private object dataContext;
        private HighlightedTextCriteria highlightedTextCriteria;
        private string highlightedText;
        private TextDecorationCollection textDecorations;
        private Style editCoreStyle;
        private string nullText;
        private object nullValue;
        private bool showNullText;
        private bool showNullTextForEmptyValue;
        private InvalidValueBehavior invalidValueBehavior;
        private IInplaceEditingProvider provider;
        private ImageSource checkedGlyph;
        private ImageSource uncheckedGlyph;
        private ImageSource indeterminateGlyph;
        private DataTemplate glyphTemplate;
        private DevExpress.Xpf.Editors.CheckEditDisplayMode displayMode;
        private TimeSpan? dayDuration;
        private bool? hasDisplayTextProviderText;
        private bool isValueChanged;

        event RoutedEventHandler IBaseEdit.Loaded
        {
            add
            {
                base.Loaded += value;
            }
            remove
            {
                base.Loaded -= value;
            }
        }

        event RoutedEventHandler IBaseEdit.Unloaded
        {
            add
            {
                base.Unloaded += value;
            }
            remove
            {
                base.Unloaded -= value;
            }
        }

        public event RoutedEventHandler EditorActivated;

        public event EditValueChangedEventHandler EditValueChanged;

        static InplaceBaseEdit()
        {
            Type ownerType = typeof(InplaceBaseEdit);
            FrameworkPropertyMetadata typeMetadata = new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, (o, args) => ((InplaceBaseEdit) o).OnEditValueChanged(args.OldValue, args.NewValue), null, false, UpdateSourceTrigger.PropertyChanged);
            typeMetadata.BindsTwoWayByDefault = true;
            EditValueProperty = DependencyProperty.Register("EditValue", typeof(object), ownerType, typeMetadata);
            EditCoreStyleProperty = DependencyProperty.Register("EditCoreStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (o, args) => ((InplaceBaseEdit) o).EditCoreStyleChanged((Style) args.NewValue)));
            ToolTipService.InitialShowDelayProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(500));
            UIElement.FocusableProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(true));
            NumberSubstitution.SubstitutionProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(NumberSubstitutionMethod.European));
            ContentPresenter.RecognizesAccessKeyProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(false));
        }

        public InplaceBaseEdit() : this(false)
        {
            base.Content = null;
            base.ContentTemplate = null;
            base.ContentTemplateSelector = null;
        }

        protected internal InplaceBaseEdit(bool fake)
        {
            this.showText = true;
            this.allowDefaultButton = true;
            this.editMode = DevExpress.Xpf.Editors.EditMode.InplaceInactive;
            this.supportInitializeLocker = new Locker();
            this.textTrimming = TextTrimming.CharacterEllipsis;
            this.textWrapping = TextWrapping.NoWrap;
            Action<InplaceBaseEdit, object, EventArgs> onEventAction = <>c.<>9__235_0;
            if (<>c.<>9__235_0 == null)
            {
                Action<InplaceBaseEdit, object, EventArgs> local1 = <>c.<>9__235_0;
                onEventAction = <>c.<>9__235_0 = (owner, o, e) => owner.OnEditSettingsChanged();
            }
            this.EditSettingsChangedEventHandler = new EditSettingsChangedEventHandler<InplaceBaseEdit>(this, onEventAction);
            Action<InplaceBaseEdit, object, ItemsProviderChangedEventArgs> action2 = <>c.<>9__235_1;
            if (<>c.<>9__235_1 == null)
            {
                Action<InplaceBaseEdit, object, ItemsProviderChangedEventArgs> local2 = <>c.<>9__235_1;
                action2 = <>c.<>9__235_1 = (owner, o, e) => owner.ItemsProviderChanged(e);
            }
            this.ItemsProviderChangedEventHandler = new ItemsProviderChangedEventHandler<InplaceBaseEdit>(this, action2);
            base.FocusVisualStyle = null;
            ((IBaseEdit) this).CanAcceptFocus = true;
            if (fake)
            {
                this.SetBypassLayoutPolicies(true);
            }
        }

        private void ActiveEditorEditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (!this.supportInitializeLocker.IsLocked)
            {
                this.EditValue = e.NewValue;
            }
        }

        protected virtual void AfterSetSettings()
        {
            this.ResetDisplayText();
            this.ProcessInnerContext();
        }

        private void AssignActiveEditorValues()
        {
            if (!this.IsInSupportInitialize)
            {
                IInplaceBaseEdit edit = this;
                if (this.activeEditor != null)
                {
                    this.activeEditor.EditMode = this.EditMode;
                    this.activeEditor.EditValue = this.EditValueCache;
                    this.activeEditor.HorizontalContentAlignment = edit.HorizontalContentAlignment;
                    this.activeEditor.VerticalContentAlignment = edit.VerticalContentAlignment;
                    this.activeEditor.ValidationError = this.validationError;
                    this.activeEditor.InvalidValueBehavior = this.invalidValueBehavior;
                    this.activeEditor.IsReadOnly = this.isReadOnly;
                    object dataContext = this.dataContext;
                    if (this.dataContext == null)
                    {
                        object local1 = this.dataContext;
                        dataContext = base.DataContext;
                    }
                    this.activeEditor.RealDataContext = dataContext;
                    this.activeEditor.ShowBorderInInplaceMode = this.showBorder;
                    this.activeEditor.FlowDirection = base.FlowDirection;
                    if (this.editTemplate != null)
                    {
                        this.activeEditor.EditTemplate = this.editTemplate;
                    }
                    if (this.displayTemplate != null)
                    {
                        this.activeEditor.DisplayTemplate = this.displayTemplate;
                    }
                    if (this.editCoreStyle != null)
                    {
                        this.activeEditor.Style = this.editCoreStyle;
                    }
                }
            }
        }

        private void AssignBorderValues()
        {
            if (!this.IsInSupportInitialize && ((this.borderControl != null) && ((this.contentRoot != null) && (this.glowControl != null))))
            {
                RenderTemplate textEditBorderTemplate = null;
                RenderTemplate hoverBorderTemplate = null;
                Thickness textEditBorderThickness = new Thickness();
                InplaceResourceProvider resourceProvider = ThemeHelper.GetResourceProvider(this);
                if (this.showBorder)
                {
                    hoverBorderTemplate = resourceProvider.GetHoverBorderTemplate(this);
                    if (!this.Settings.UsesFlatBorderTemplate())
                    {
                        textEditBorderTemplate = resourceProvider.GetTextEditBorderTemplate(this);
                        textEditBorderThickness = resourceProvider.GetTextEditBorderThickness(this);
                    }
                    else
                    {
                        textEditBorderTemplate = resourceProvider.GetCommonBorderTemplate(this);
                        textEditBorderThickness = resourceProvider.GetCommonBorderThickness(this);
                    }
                }
                this.borderControl.Visibility = new Visibility?(this.showBorder ? Visibility.Visible : Visibility.Collapsed);
                this.borderControl.RenderTemplate = textEditBorderTemplate;
                this.glowControl.Visibility = new Visibility?(this.showBorder ? Visibility.Visible : Visibility.Collapsed);
                this.glowControl.RenderTemplate = hoverBorderTemplate;
                this.contentRoot.Margin = new Thickness?(textEditBorderThickness);
            }
        }

        private void AssignCheckBoxValues()
        {
            if (!this.IsInSupportInitialize)
            {
                IInplaceBaseEdit edit = this;
                if (this.checkBox != null)
                {
                    this.checkBox.IsChecked = CheckEditSettings.GetBooleanFromEditValue(this.EditValueCache, ((CheckEditSettings) this.Settings).IsThreeState);
                    this.checkBox.HorizontalContentAlignment = new HorizontalAlignment?(edit.HorizontalContentAlignment);
                    this.checkBox.DisplayMode = edit.CheckEditDisplayMode;
                    this.checkBox.Glyph = this.CalcActualGlyph(this.EditValueCache);
                    this.checkBox.GlyphTemplate = this.GlyphTemplate;
                }
            }
        }

        private void AssignEditorControlValues()
        {
            if (!this.IsInSupportInitialize)
            {
                IInplaceBaseEdit edit = this;
                if (this.editorControl != null)
                {
                    ControlTemplate displayTemplate = this.displayTemplate;
                    if (this.displayTemplate == null)
                    {
                        ControlTemplate local1 = this.displayTemplate;
                        displayTemplate = this.CalcDisplayTemplate();
                    }
                    this.editorControl.Template = displayTemplate;
                    object dataContext = this.dataContext;
                    if (this.dataContext == null)
                    {
                        object local2 = this.dataContext;
                        dataContext = this;
                    }
                    this.editorControl.DataContext = dataContext;
                    object obj2 = this.dataContext;
                    if (this.dataContext == null)
                    {
                        object local3 = this.dataContext;
                        obj2 = this;
                    }
                    this.editorControl.RealDataContext = obj2;
                    this.editorControl.EditValue = this.CalcEditorControlValue();
                    this.editorControl.HighlightedTextCriteria = edit.HighlightedTextCriteria;
                    this.editorControl.HighlightedText = edit.HighlightedText;
                    this.editorControl.DisplayText = this.DisplayText;
                    this.editorControl.IsChecked = this.CalcIsChecked();
                    this.editorControl.SelectedItem = this.CalcSelectedItem();
                    this.editorControl.SelectedIndex = this.CalcSelectedIndex();
                    this.editorControl.ItemTemplate = this.CalcItemTemplate();
                    this.editorControl.ItemTemplateSelector = this.CalcItemTemplateSelector();
                    this.editorControl.HorizontalContentAlignment = edit.HorizontalContentAlignment;
                    this.editorControl.VerticalContentAlignment = edit.VerticalContentAlignment;
                    this.editorControl.IsReadOnly = edit.IsReadOnly;
                    this.editorControl.IsTextEditable = this.CalcIsTextEditable();
                    this.editorControl.StyleSettings = this.editSettings.StyleSettings;
                }
            }
        }

        private void AssignErrorProviderValues()
        {
            if (!this.IsInSupportInitialize && (this.errorProvider != null))
            {
                this.errorProvider.Visibility = new Visibility?((this.validationError != null) ? Visibility.Visible : Visibility.Collapsed);
                this.errorProvider.Content = this.validationError;
                if (this.showBorder)
                {
                    this.errorProvider.SvgPalette = ThemeHelper.GetResourceProvider(this).GetSvgPalette(this);
                }
                else
                {
                    this.errorProvider.SvgPalette = WpfSvgPalette.GetPalette(this);
                }
            }
        }

        private void AssignTextBlockValues()
        {
            if (!this.IsInSupportInitialize)
            {
                IInplaceBaseEdit edit = this;
                if (this.textBlock != null)
                {
                    string displayText = this.DisplayText;
                    string str2 = string.IsNullOrEmpty(displayText) ? " " : displayText;
                    this.textBlock.Text = str2;
                    this.textBlock.HighlightedText = edit.HighlightedText;
                    this.textBlock.TextTrimming = new TextTrimming?(this.textTrimming);
                    this.textBlock.TextWrapping = new TextWrapping?(this.textWrapping);
                    this.textBlock.TextAlignment = new TextAlignment?(edit.HorizontalContentAlignment.ToTextAlignment());
                    this.textBlock.VerticalAlignment = new VerticalAlignment?(edit.VerticalContentAlignment);
                    this.textBlock.HighlightedTextCriteria = edit.HighlightedTextCriteria;
                    this.textBlock.ForceUseRealTextBlock = edit.HasTextDecorations || str2.Contains("\n");
                    this.textBlock.TextDecorations = edit.TextDecorations;
                    this.textBlock.Opacity = new double?(edit.IsNullTextVisible ? 0.35 : 1.0);
                    this.textBlock.Visibility = new Visibility?(this.showText ? Visibility.Visible : Visibility.Collapsed);
                }
            }
        }

        private void AssignValues()
        {
            this.AssignCheckBoxValues();
            this.AssignTextBlockValues();
            this.AssignBorderValues();
            this.AssignEditorControlValues();
            this.AssignActiveEditorValues();
            this.AssignErrorProviderValues();
        }

        public override void BeginInit()
        {
            base.BeginInit();
            this.initialized = false;
            ((IBaseEdit) this).BeginInit(false);
        }

        private ImageSource CalcActualGlyph(object editValue)
        {
            bool? booleanFromEditValue = CheckEditSettings.GetBooleanFromEditValue(editValue, true);
            if (booleanFromEditValue != null)
            {
                bool valueOrDefault = booleanFromEditValue.GetValueOrDefault();
                if (!valueOrDefault)
                {
                    return this.UncheckedGlyph;
                }
                if (valueOrDefault)
                {
                    return this.CheckedGlyph;
                }
            }
            return this.IndeterminateGlyph;
        }

        private ControlTemplate CalcDisplayTemplate()
        {
            InplaceResourceProvider resourceProvider = ThemeHelper.GetResourceProvider(this);
            if (this.Settings is CheckEditSettings)
            {
                return null;
            }
            LookUpEditSettingsBase settings = this.Settings as LookUpEditSettingsBase;
            if (settings == null)
            {
                return null;
            }
            if (this.CalcIsTokenMode())
            {
                return resourceProvider.GetTokenEditorDisplayTemplate(this);
            }
            if (!settings.ApplyItemTemplateToSelectedItem || settings.IsTextEditable.GetValueOrDefault(true))
            {
                return null;
            }
            ComboBoxEditSettings settings2 = this.Settings as ComboBoxEditSettings;
            return (((settings2 == null) || !settings2.GetApplyImageTemplateToSelectedItem()) ? resourceProvider.GetSelectedItemTemplate(this) : resourceProvider.GetSelectedItemImageTemplate(this));
        }

        private string CalcDisplayText(object value, string valueText = null)
        {
            this.hasDisplayTextProviderText = false;
            if (this.isNullTextVisible && (this.displayTextProvider == null))
            {
                return this.nullText;
            }
            this.displayText ??= this.Settings.GetDisplayTextFromEditor(value);
            valueText ??= this.displayText;
            if (this.displayTextProvider != null)
            {
                string str;
                this.hasDisplayTextProviderText = this.displayTextProvider.GetDisplayText(valueText, value, out str);
                if (this.hasDisplayTextProviderText.GetValueOrDefault(true))
                {
                    return str;
                }
            }
            return valueText;
        }

        private object CalcEditorControlValue() => 
            this.CalcIsTokenMode() ? this.CalcTokenEditValue(this.EditValueCache) : this.EditValueCache;

        private object CalcIsChecked()
        {
            CheckEditSettings settings = this.Settings as CheckEditSettings;
            return ((settings != null) ? CheckEditSettings.GetBooleanFromEditValue(this.EditValueCache, settings.IsThreeState) : null);
        }

        private bool CalcIsTextEditable()
        {
            ButtonEditSettings editSettings = this.editSettings as ButtonEditSettings;
            return ((editSettings != null) && ((editSettings.IsTextEditable != null) && editSettings.IsTextEditable.Value));
        }

        private bool CalcIsTokenMode()
        {
            if (this.Settings == null)
            {
                return false;
            }
            ITokenStyleSettings styleSettings = this.Settings.StyleSettings as ITokenStyleSettings;
            return ((styleSettings != null) ? styleSettings.IsTokenStyleSettings() : false);
        }

        private DataTemplate CalcItemTemplate()
        {
            LookUpEditSettingsBase settings = this.Settings as LookUpEditSettingsBase;
            return settings?.ItemTemplate;
        }

        private DataTemplateSelector CalcItemTemplateSelector()
        {
            LookUpEditSettingsBase settings = this.Settings as LookUpEditSettingsBase;
            return settings?.ItemTemplateSelector;
        }

        private object CalcSelectedIndex()
        {
            LookUpEditSettingsBase settings = this.Settings as LookUpEditSettingsBase;
            return ((settings != null) ? ((object) settings.ItemsProvider.IndexOfValue(this.EditValueCache, settings.ItemsProvider.CurrentDataViewHandle)) : null);
        }

        private object CalcSelectedItem()
        {
            LookUpEditSettingsBase settings = this.Settings as LookUpEditSettingsBase;
            return settings?.ItemsProvider.GetItem(this.EditValueCache, settings?.ItemsProvider.CurrentDataViewHandle);
        }

        private object CalcTokenEditValue(object editValue)
        {
            if (editValue == null)
            {
                return null;
            }
            LookUpEditSettingsBase settings = this.Settings as LookUpEditSettingsBase;
            if (settings == null)
            {
                return null;
            }
            IItemsProvider2 provider = settings.ItemsProvider;
            IList source = editValue as IList;
            TokenEditorCustomItem item = null;
            if (source == null)
            {
                item = new TokenEditorCustomItem();
                object displayValueByEditValue = provider.GetDisplayValueByEditValue(editValue, provider.CurrentDataViewHandle);
                CustomItem item1 = new CustomItem();
                item1.EditValue = editValue;
                List<CustomItem> list1 = new List<CustomItem>();
                List<CustomItem> list3 = new List<CustomItem>();
                item1.DisplayText = (displayValueByEditValue != null) ? displayValueByEditValue.ToString() : string.Empty;
                list3.Add(item1);
                item.EditValue = list3;
            }
            else
            {
                Func<CustomItem, bool> predicate = <>c.<>9__260_1;
                if (<>c.<>9__260_1 == null)
                {
                    Func<CustomItem, bool> local1 = <>c.<>9__260_1;
                    predicate = <>c.<>9__260_1 = item => item != null;
                }
                List<CustomItem> list2 = (from x in source.Cast<object>() select this.GetCustomItemFromValue(provider, x)).Where<CustomItem>(predicate).ToList<CustomItem>();
                if (list2.Count > 0)
                {
                    item = new TokenEditorCustomItem {
                        EditValue = list2
                    };
                }
            }
            return item;
        }

        private object CalcToolTip(bool isTextTrimmed, BaseValidationError error)
        {
            if (error != null)
            {
                DataTemplate errorToolTipContentTemplate = this.Settings.ErrorToolTipContentTemplate;
                DataTemplate template3 = errorToolTipContentTemplate;
                if (errorToolTipContentTemplate == null)
                {
                    DataTemplate local1 = errorToolTipContentTemplate;
                    InplaceBaseEditThemeKeyExtension resourceKey = new InplaceBaseEditThemeKeyExtension();
                    resourceKey.ResourceKey = InplaceBaseEditThemeKeys.ValidationErrorToolTipTemplate;
                    resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(this);
                    template3 = (DataTemplate) base.FindResource(resourceKey);
                }
                DataTemplate template = template3;
                ContentControl control1 = new ContentControl();
                control1.Content = error;
                control1.ContentTemplate = template;
                return control1;
            }
            if (!isTextTrimmed)
            {
                return null;
            }
            TextEditSettings settings = this.Settings as TextEditSettings;
            DataTemplate trimmedTextToolTipContentTemplate = settings?.TrimmedTextToolTipContentTemplate;
            DataTemplate template4 = trimmedTextToolTipContentTemplate;
            if (trimmedTextToolTipContentTemplate == null)
            {
                DataTemplate local3 = trimmedTextToolTipContentTemplate;
                InplaceBaseEditThemeKeyExtension resourceKey = new InplaceBaseEditThemeKeyExtension();
                resourceKey.ResourceKey = InplaceBaseEditThemeKeys.TrimmedTextToolTipTemplate;
                resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(this);
                template4 = (DataTemplate) base.FindResource(resourceKey);
            }
            DataTemplate template2 = template4;
            ContentControl control2 = new ContentControl();
            control2.Content = new ToolTipContent(this.DisplayText);
            control2.ContentTemplate = template2;
            return control2;
        }

        public void ClearError()
        {
        }

        protected virtual BaseEditSettings CreateDefaultEditSettings()
        {
            BaseEditSettings settings = this.CreateDefaultEditSettingsInternal();
            settings.ApplyToEdit(this, true);
            return settings;
        }

        protected virtual BaseEditSettings CreateDefaultEditSettingsInternal() => 
            new TextEditSettings();

        private EditorContent CreateEditorContent()
        {
            EditorContent content1 = new EditorContent();
            content1.EditMode = this.EditMode;
            content1.Error = this.validationError;
            content1.Settings = this.Settings;
            EditorContent content2 = content1;
            ControlTemplate displayTemplate = this.displayTemplate;
            if (this.displayTemplate == null)
            {
                ControlTemplate local1 = this.displayTemplate;
                displayTemplate = this.CalcDisplayTemplate();
            }
            content2.DisplayTemplate = displayTemplate;
            EditorContent local2 = content2;
            local2.ShowBorder = this.showBorder;
            local2.ShowEditorButtons = this.showEditorButtons;
            local2.ShowText = this.showText;
            return local2;
        }

        void IChrome.InvalidateVisual()
        {
            base.InvalidateVisual();
        }

        void IBaseEdit.BeginInit(bool callBase)
        {
            this.supportInitializeLocker.Lock();
        }

        void IBaseEdit.ClearValue(DependencyProperty dp)
        {
            base.ClearValue(dp);
        }

        void IBaseEdit.EndInit(bool callBase, bool shouldSync)
        {
            this.supportInitializeLocker.Unlock();
            if (!this.IsInSupportInitialize && !this.initialized)
            {
                this.InternalInitialize();
            }
        }

        object IBaseEdit.GetValue(DependencyProperty d) => 
            base.GetValue(d);

        bool IBaseEdit.IsChildElement(IInputElement element, DependencyObject root)
        {
            if (LayoutHelper.IsChildElementEx(root ?? this, (DependencyObject) element, true))
            {
                return true;
            }
            Func<RenderBaseEditContext, IBaseEdit> evaluator = <>c.<>9__285_0;
            if (<>c.<>9__285_0 == null)
            {
                Func<RenderBaseEditContext, IBaseEdit> local2 = <>c.<>9__285_0;
                evaluator = <>c.<>9__285_0 = x => x.Control as IBaseEdit;
            }
            return this.activeEditor.With<RenderBaseEditContext, IBaseEdit>(evaluator).If<IBaseEdit>(delegate (IBaseEdit x) {
                DependencyObject obj1 = root;
                if (root == null)
                {
                    DependencyObject local1 = root;
                    obj1 = this;
                }
                return x.IsChildElement(element, obj1);
            }).ReturnSuccess<IBaseEdit>();
        }

        BindingExpressionBase IBaseEdit.SetBinding(DependencyProperty dp, BindingBase binding) => 
            base.SetBinding(dp, binding);

        void IBaseEdit.SetInplaceEditingProvider(IInplaceEditingProvider provider)
        {
            this.provider = provider;
            this.activeEditor.SetInplaceEditingProvider(provider);
        }

        void IBaseEdit.UpdateDisplayText()
        {
            this.ResetDisplayText();
            this.AssignValues();
        }

        void IBaseEdit.UpdateErrorTooltip()
        {
        }

        IEnumerable<ButtonInfoBase> IInplaceBaseEdit.GetSortedButtons()
        {
            ButtonEditSettings settings = this.Settings as ButtonEditSettings;
            if (settings == null)
            {
                return new List<ButtonInfoBase>();
            }
            List<ButtonInfoBase> source = new List<ButtonInfoBase>();
            if (this.GetShowEditorButtons())
            {
                List<ButtonInfoBase> collection = new List<ButtonInfoBase>();
                foreach (ButtonInfoBase base2 in settings.GetActualButtons())
                {
                    if (base2.IsLeft)
                    {
                        source.Add(base2);
                        continue;
                    }
                    collection.Add(base2);
                }
                if (this.allowDefaultButton)
                {
                    source.Add(settings.CreateDefaultButtonInfo());
                }
                source.AddRange(collection);
                Func<ButtonInfoBase, int> keySelector = <>c.<>9__309_0;
                if (<>c.<>9__309_0 == null)
                {
                    Func<ButtonInfoBase, int> local1 = <>c.<>9__309_0;
                    keySelector = <>c.<>9__309_0 = x => x.Index;
                }
                source = source.OrderBy<ButtonInfoBase, int>(keySelector).ToList<ButtonInfoBase>();
                string editorThemeName = ThemeHelper.GetEditorThemeName(this);
                foreach (ButtonInfoBase base4 in source)
                {
                    ThemeManager.SetThemeName(base4, editorThemeName);
                }
            }
            return source;
        }

        void IInplaceBaseEdit.RaiseEditValueChanged(object oldValue, object newValue)
        {
            this.EditValue = newValue;
        }

        public bool DoValidate()
        {
            Func<RenderBaseEditContext, bool> evaluator = <>c.<>9__203_0;
            if (<>c.<>9__203_0 == null)
            {
                Func<RenderBaseEditContext, bool> local1 = <>c.<>9__203_0;
                evaluator = <>c.<>9__203_0 = x => x.DoValidate();
            }
            return this.activeEditor.Return<RenderBaseEditContext, bool>(evaluator, (<>c.<>9__203_1 ??= () => true));
        }

        protected virtual void EditCoreStyleChanged(Style newValue)
        {
            this.editCoreStyle = newValue;
        }

        public override void EndInit()
        {
            base.EndInit();
            ((IBaseEdit) this).EndInit(false, true);
        }

        public void FlushPendingEditActions()
        {
            Action<RenderBaseEditContext> action = <>c.<>9__211_0;
            if (<>c.<>9__211_0 == null)
            {
                Action<RenderBaseEditContext> local1 = <>c.<>9__211_0;
                action = <>c.<>9__211_0 = x => x.FlushPendingEditActions();
            }
            this.activeEditor.Do<RenderBaseEditContext>(action);
        }

        public void ForceInitialize(bool callBase)
        {
        }

        private CustomItem GetCustomItemFromValue(IItemsProvider2 provider, object editValue)
        {
            CustomItem item = new CustomItem();
            this.GetDisplayText(editValue, true);
            object obj2 = this.CalcDisplayText(editValue, this.Settings.GetDisplayTextFromEditor(editValue));
            if (editValue != null)
            {
                item.EditValue = editValue;
                item.DisplayText = obj2?.ToString();
            }
            return item;
        }

        public string GetDisplayText(object editValue, bool applyFormatting) => 
            this.DisplayText;

        public bool GetShowEditorButtons() => 
            ((IBaseEdit) this).ShowEditorButtons;

        protected override FrameworkRenderElementContext InitializeContext()
        {
            FrameworkRenderElementContext context;
            try
            {
                context = base.InitializeContext();
            }
            finally
            {
                this.ProcessInnerContext();
            }
            return context;
        }

        private void InternalInitialize()
        {
            base.RenderTemplateSelector = ThemeHelper.GetResourceProvider(this).GetTextEditTemplateSelector(this);
            this.AssignValues();
        }

        public virtual bool IsActivatingKey(Key key, ModifierKeys modifiers)
        {
            GetIsActivatingKeyEventArgs e = new GetIsActivatingKeyEventArgs(key, modifiers, this, this.IsActivatingKeyCore(key, modifiers));
            this.Settings.RaiseGetIsActivatingKey(this, e);
            return e.IsActivatingKey;
        }

        private bool IsActivatingKeyCore(Key key, ModifierKeys modifiers) => 
            !((IBaseEdit) this).IsReadOnly && (base.IsEnabled && this.Settings.IsActivatingKey(key, modifiers));

        private bool IsNullValue(object value) => 
            BaseEditSettings.IsNativeNullValue(value) || (this.IsStringEmptyIsNullValue(value) || Equals(value, this.nullValue));

        private bool IsStringEmptyIsNullValue(object value) => 
            this.showNullTextForEmptyValue && BaseEditSettings.IsStringEmpty(value);

        private void ItemsProviderChanged(ItemsProviderChangedEventArgs e)
        {
            if (this.EditMode != DevExpress.Xpf.Editors.EditMode.InplaceActive)
            {
                this.ResetDisplayText();
                this.AssignValues();
            }
        }

        public bool NeedsKey(Key key, ModifierKeys modifiers) => 
            this.activeEditor.Return<RenderBaseEditContext, bool>(x => x.NeedsKey(key, modifiers), delegate {
                Func<bool> needsEnterFunc = <>c.<>9__207_2;
                if (<>c.<>9__207_2 == null)
                {
                    Func<bool> local1 = <>c.<>9__207_2;
                    needsEnterFunc = <>c.<>9__207_2 = () => false;
                }
                bool? nullable = BaseEdit.NeedsBasicKey(key, needsEnterFunc);
                return (nullable != null) ? nullable.GetValueOrDefault() : true;
            });

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new UIElementAutomationPeer(this);

        private void OnEditModeChanged()
        {
            base.RenderContent = this.CreateEditorContent();
            this.EditValue = this.EditValueCache;
            this.RefreshToolTip();
        }

        private void OnEditSettingsChanged()
        {
            this.Settings.AssignToEdit(this);
            this.ResetDisplayText();
            this.AssignValues();
        }

        protected virtual void OnEditValueChanged(object oldValue, object newValue)
        {
            this.EditValueCache = newValue;
            this.ResetDisplayText();
            this.AssignValues();
            this.RaiseEditValueChanged(oldValue, newValue);
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            if (!this.initialized)
            {
                this.InternalInitialize();
                this.initialized = true;
            }
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            this.RefreshToolTip();
        }

        private void OnShowBorderChanged()
        {
            base.ClipToBounds = !this.showBorder;
            this.UpdateEditorContent();
        }

        private void OnValidationErrorChanged(BaseValidationError error)
        {
            if (this.EditMode == DevExpress.Xpf.Editors.EditMode.InplaceInactive)
            {
                this.UpdateEditorContent();
            }
            else
            {
                this.activeEditor.Do<RenderBaseEditContext>(x => x.ValidationError = error);
            }
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);
            this.UpdateEditorContent();
            if (this.EditMode != DevExpress.Xpf.Editors.EditMode.InplaceInactive)
            {
                this.EditValue = this.EditValueCache;
            }
        }

        public void ProcessActivatingKey(Key key, ModifierKeys modifiers)
        {
            this.activeEditor.Do<RenderBaseEditContext>(x => x.ProcessActivatingKey(key, modifiers));
        }

        private void ProcessActiveEditor()
        {
            Func<RenderBaseEditContext, bool> evaluator = <>c.<>9__272_0;
            if (<>c.<>9__272_0 == null)
            {
                Func<RenderBaseEditContext, bool> local1 = <>c.<>9__272_0;
                evaluator = <>c.<>9__272_0 = x => x.IsValueChanged;
            }
            this.isValueChanged = this.activeEditor.Return<RenderBaseEditContext, bool>(evaluator, () => this.isValueChanged);
            this.activeEditor.Do<RenderBaseEditContext>(delegate (RenderBaseEditContext x) {
                x.EditValueChanged -= new EventHandler<EditValueChangedEventArgs>(this.ActiveEditorEditValueChanged);
            });
            this.activeEditor = base.Namescope.GetElement("PART_Editor") as RenderBaseEditContext;
            this.activeEditor.Do<RenderBaseEditContext>(x => x.SetDisplayTextProvider(this.displayTextProvider));
            this.AssignActiveEditorValues();
            this.SetEditIsValueChanged(this.isValueChanged);
            this.activeEditor.Do<RenderBaseEditContext>(delegate (RenderBaseEditContext x) {
                x.EditValueChanged += new EventHandler<EditValueChangedEventArgs>(this.ActiveEditorEditValueChanged);
            });
            this.RaiseEditorActivated();
        }

        private void ProcessBorder()
        {
            this.borderControl = base.Namescope.GetElement("PART_Border") as RenderControlContext;
            this.glowControl = base.Namescope.GetElement("PART_Glow") as RenderControlContext;
            this.contentRoot = base.Namescope.GetElement("PART_Root");
            this.AssignBorderValues();
        }

        private void ProcessEditorControl()
        {
            this.editorControl = base.Namescope.GetElement("PART_Editor") as RenderEditorControlContext;
            this.AssignEditorControlValues();
        }

        private void ProcessInnerContext()
        {
            this.ProcessTextBlock();
            this.ProcessEditorControl();
            this.ProcessBorder();
            this.ProcessActiveEditor();
            this.ProcessInplaceButtonEdit();
            this.ProcessInplaceCheckBox();
            this.ProcessValidationProvider();
        }

        private void ProcessInplaceButtonEdit()
        {
            if (this.Settings is ButtonEditSettings)
            {
                this.leftButtonsItemsControl = base.Namescope.GetElement("PART_LeftButtons") as RenderItemsControlContext;
                this.rightButtonsItemsControl = base.Namescope.GetElement("PART_RightButtons") as RenderItemsControlContext;
                IInplaceBaseEdit edit = this;
                IEnumerable<ButtonInfoBase> sortedButtons = edit.GetSortedButtons();
                if (!edit.ShowText)
                {
                    if (this.leftButtonsItemsControl != null)
                    {
                        this.leftButtonsItemsControl.Visibility = new Visibility?(sortedButtons.Any<ButtonInfoBase>() ? Visibility.Visible : Visibility.Collapsed);
                    }
                    if (this.rightButtonsItemsControl != null)
                    {
                        this.rightButtonsItemsControl.Visibility = new Visibility?(sortedButtons.Any<ButtonInfoBase>() ? Visibility.Visible : Visibility.Collapsed);
                        this.rightButtonsItemsControl.ItemsSource = sortedButtons.ToList<ButtonInfoBase>();
                    }
                }
                else
                {
                    Func<ButtonInfoBase, bool> predicate = <>c.<>9__269_0;
                    if (<>c.<>9__269_0 == null)
                    {
                        Func<ButtonInfoBase, bool> local1 = <>c.<>9__269_0;
                        predicate = <>c.<>9__269_0 = btn => btn.IsLeft;
                    }
                    List<ButtonInfoBase> source = sortedButtons.Where<ButtonInfoBase>(predicate).ToList<ButtonInfoBase>();
                    if (this.leftButtonsItemsControl != null)
                    {
                        this.leftButtonsItemsControl.Visibility = new Visibility?(source.Any<ButtonInfoBase>() ? Visibility.Visible : Visibility.Collapsed);
                        this.leftButtonsItemsControl.ItemsSource = source;
                    }
                    Func<ButtonInfoBase, bool> func2 = <>c.<>9__269_1;
                    if (<>c.<>9__269_1 == null)
                    {
                        Func<ButtonInfoBase, bool> local2 = <>c.<>9__269_1;
                        func2 = <>c.<>9__269_1 = btn => !btn.IsLeft;
                    }
                    List<ButtonInfoBase> list2 = sortedButtons.Where<ButtonInfoBase>(func2).ToList<ButtonInfoBase>();
                    if (this.rightButtonsItemsControl != null)
                    {
                        this.rightButtonsItemsControl.Visibility = new Visibility?(list2.Any<ButtonInfoBase>() ? Visibility.Visible : Visibility.Collapsed);
                        this.rightButtonsItemsControl.ItemsSource = list2;
                    }
                }
            }
        }

        private void ProcessInplaceCheckBox()
        {
            this.checkBox = base.Namescope.GetElement("PART_Editor") as EditorRenderCheckBoxContext;
            this.AssignCheckBoxValues();
        }

        private void ProcessTextBlock()
        {
            this.textBlock = base.Namescope.GetElement("PART_Editor") as RenderTextBlockContext;
            this.AssignTextBlockValues();
        }

        private void ProcessValidationProvider()
        {
            this.errorProvider = (RenderContentControlContext) base.Namescope.GetElement("PART_ErrorProvider");
            this.AssignErrorProviderValues();
        }

        private void RaiseEditorActivated()
        {
            if ((this.activeEditor != null) && (this.EditorActivated != null))
            {
                this.EditorActivated(this, new RoutedEventArgs());
            }
        }

        private void RaiseEditValueChanged(object oldValue, object newValue)
        {
            if (this.EditValueChanged != null)
            {
                this.EditValueChanged(this, new EditValueChangedEventArgs(oldValue, newValue));
            }
        }

        private void RefreshToolTip()
        {
            Func<bool> fallback = <>c.<>9__282_2;
            if (<>c.<>9__282_2 == null)
            {
                Func<bool> local1 = <>c.<>9__282_2;
                fallback = <>c.<>9__282_2 = () => false;
            }
            bool isTextTrimmed = this.textBlock.If<RenderTextBlockContext>(x => ((IInplaceBaseEdit) this).ShowToolTipForTrimmedText).Return<RenderTextBlockContext, bool>(x => TextBlockService.CalcIsTextTrimmed(this.textBlock), fallback);
            ToolTipService.SetToolTip(this, this.CalcToolTip(isTextTrimmed, this.validationError));
        }

        protected override void ReleaseContext(FrameworkRenderElementContext context)
        {
            using (this.supportInitializeLocker.Lock())
            {
                base.ReleaseContext(context);
            }
            this.ProcessInnerContext();
        }

        protected override void RenderContentChanged(object oldContent, object newContent)
        {
        }

        private void ResetDisplayText()
        {
            this.displayText = null;
            this.isNullTextVisible = this.showNullText && this.IsNullValue(this.EditValueCache);
        }

        public void SelectAll()
        {
            Action<RenderBaseEditContext> action = <>c.<>9__204_0;
            if (<>c.<>9__204_0 == null)
            {
                Action<RenderBaseEditContext> local1 = <>c.<>9__204_0;
                action = <>c.<>9__204_0 = x => x.SelectAll();
            }
            this.activeEditor.Do<RenderBaseEditContext>(action);
        }

        internal void SetDisplayTextProvider(IDisplayTextProvider displayTextProvider)
        {
            this.displayTextProvider = displayTextProvider;
            this.ResetDisplayText();
        }

        private void SetEditIsValueChanged(bool value)
        {
            this.activeEditor.Do<RenderBaseEditContext>(x => x.IsValueChanged = value);
        }

        public void SetSettings(BaseEditSettings settings)
        {
            this.UnsubscribeFromSettings(this.editSettings);
            this.editSettings = settings;
            this.UpdateEditorContent();
            this.SubscribeToSettings(settings);
            this.AfterSetSettings();
        }

        public void SetShowEditorButtons(bool show)
        {
            ((IBaseEdit) this).ShowEditorButtons = show;
        }

        private void SubscribeToSettings(BaseEditSettings s)
        {
            if (s != null)
            {
                s.EditSettingsChanged += this.EditSettingsChangedEventHandler.Handler;
            }
            LookUpEditSettingsBase base2 = s as LookUpEditSettingsBase;
            if (base2 != null)
            {
                base2.ItemsProvider.ItemsProviderChanged += this.ItemsProviderChangedEventHandler.Handler;
            }
        }

        private void UnsubscribeFromSettings(BaseEditSettings s)
        {
            if (s != null)
            {
                s.EditSettingsChanged -= this.EditSettingsChangedEventHandler.Handler;
            }
            LookUpEditSettingsBase base2 = s as LookUpEditSettingsBase;
            if (base2 != null)
            {
                base2.ItemsProvider.ItemsProviderChanged -= this.ItemsProviderChangedEventHandler.Handler;
            }
        }

        private void UpdateEditorContent()
        {
            base.RenderContent = this.CreateEditorContent();
        }

        private EditSettingsChangedEventHandler<InplaceBaseEdit> EditSettingsChangedEventHandler { get; set; }

        private ItemsProviderChangedEventHandler<InplaceBaseEdit> ItemsProviderChangedEventHandler { get; set; }

        public BaseEditSettings Settings =>
            this.editSettings ?? this.CreateDefaultEditSettings();

        public object EditValueCache { get; private set; }

        public Style EditCoreStyle
        {
            get => 
                (Style) base.GetValue(EditCoreStyleProperty);
            set => 
                base.SetValue(EditCoreStyleProperty, value);
        }

        public string DisplayText =>
            this.CalcDisplayText(this.EditValueCache, null);

        public bool HasTextDecorations
        {
            get => 
                this.hasTextDecorations;
            set
            {
                if (this.hasTextDecorations != value)
                {
                    this.hasTextDecorations = value;
                    this.AssignTextBlockValues();
                }
            }
        }

        public bool ApplyItemTemplateToSelectedItem
        {
            get => 
                this.applyItemTemplateToSelectedItem;
            set
            {
                if (this.applyItemTemplateToSelectedItem != value)
                {
                    this.applyItemTemplateToSelectedItem = value;
                    this.UpdateEditorContent();
                }
            }
        }

        private bool IsInSupportInitialize =>
            (bool) this.supportInitializeLocker;

        public DevExpress.Xpf.Editors.EditMode EditMode
        {
            get => 
                this.editMode;
            set
            {
                if (this.editMode != value)
                {
                    this.editMode = value;
                    this.OnEditModeChanged();
                }
            }
        }

        public DevExpress.Xpf.Editors.CheckEditDisplayMode CheckEditDisplayMode
        {
            get => 
                this.displayMode;
            set
            {
                if (this.displayMode != value)
                {
                    this.displayMode = value;
                    this.AssignCheckBoxValues();
                }
            }
        }

        public ImageSource CheckedGlyph
        {
            get => 
                this.checkedGlyph;
            set
            {
                if (!ReferenceEquals(this.checkedGlyph, value))
                {
                    this.checkedGlyph = value;
                    this.AssignCheckBoxValues();
                }
            }
        }

        public ImageSource UncheckedGlyph
        {
            get => 
                this.uncheckedGlyph;
            set
            {
                if (!ReferenceEquals(this.uncheckedGlyph, value))
                {
                    this.uncheckedGlyph = value;
                    this.AssignCheckBoxValues();
                }
            }
        }

        public ImageSource IndeterminateGlyph
        {
            get => 
                this.indeterminateGlyph;
            set
            {
                if (!ReferenceEquals(this.indeterminateGlyph, value))
                {
                    this.indeterminateGlyph = value;
                    this.AssignCheckBoxValues();
                }
            }
        }

        public DataTemplate GlyphTemplate
        {
            get => 
                this.glyphTemplate;
            set
            {
                if (!ReferenceEquals(this.glyphTemplate, value))
                {
                    this.glyphTemplate = value;
                    this.AssignCheckBoxValues();
                }
            }
        }

        public TimeSpan? DayDuration
        {
            get => 
                this.dayDuration;
            set
            {
                TimeSpan? dayDuration = this.dayDuration;
                TimeSpan? nullable2 = value;
                if (!(((dayDuration != null) == (nullable2 != null)) ? ((dayDuration != null) ? (dayDuration.GetValueOrDefault() == nullable2.GetValueOrDefault()) : true) : false))
                {
                    this.dayDuration = value;
                    this.ResetDisplayText();
                }
            }
        }

        bool IBaseEdit.ShouldDisableExcessiveUpdatesInInplaceInactiveMode
        {
            get => 
                false;
            set
            {
            }
        }

        bool? IBaseEdit.DisableExcessiveUpdatesInInplaceInactiveMode
        {
            get => 
                null;
            set
            {
            }
        }

        bool IBaseEdit.IsReadOnly
        {
            get => 
                this.isReadOnly;
            set
            {
                if (this.isReadOnly != value)
                {
                    this.isReadOnly = value;
                    if (this.activeEditor != null)
                    {
                        this.activeEditor.IsReadOnly = value;
                    }
                    if (this.editorControl != null)
                    {
                        this.editorControl.IsReadOnly = value;
                    }
                }
            }
        }

        object IBaseEdit.DataContext
        {
            get => 
                this.dataContext;
            set
            {
                if (this.dataContext != value)
                {
                    this.dataContext = value;
                    if (this.editorControl != null)
                    {
                        this.editorControl.DataContext = value;
                        this.editorControl.RealDataContext = value;
                    }
                    if (this.activeEditor != null)
                    {
                        this.activeEditor.RealDataContext = value;
                    }
                }
            }
        }

        bool IBaseEdit.IsEditorActive
        {
            get
            {
                Func<RenderBaseEditContext, bool> evaluator = <>c.<>9__131_0;
                if (<>c.<>9__131_0 == null)
                {
                    Func<RenderBaseEditContext, bool> local1 = <>c.<>9__131_0;
                    evaluator = <>c.<>9__131_0 = x => x.IsEditorActive;
                }
                return this.activeEditor.Return<RenderBaseEditContext, bool>(evaluator, null);
            }
        }

        IValueConverter IBaseEdit.DisplayTextConverter { get; set; }

        InvalidValueBehavior IBaseEdit.InvalidValueBehavior
        {
            get => 
                this.invalidValueBehavior;
            set
            {
                if (this.invalidValueBehavior != value)
                {
                    this.invalidValueBehavior = value;
                    if (this.activeEditor != null)
                    {
                        this.activeEditor.InvalidValueBehavior = value;
                    }
                }
            }
        }

        string IBaseEdit.DisplayFormatString { get; set; }

        HorizontalAlignment IBaseEdit.HorizontalContentAlignment
        {
            get => 
                this.hca;
            set
            {
                if (this.hca != value)
                {
                    this.hca = value;
                    if (this.activeEditor != null)
                    {
                        this.activeEditor.HorizontalContentAlignment = value;
                    }
                    if (this.textBlock != null)
                    {
                        this.textBlock.TextAlignment = new TextAlignment?(value.ToTextAlignment());
                    }
                }
            }
        }

        VerticalAlignment IBaseEdit.VerticalContentAlignment
        {
            get => 
                this.vca;
            set
            {
                if (this.vca != value)
                {
                    this.vca = value;
                    if (this.activeEditor != null)
                    {
                        this.activeEditor.VerticalContentAlignment = value;
                    }
                    if (this.textBlock != null)
                    {
                        this.textBlock.VerticalAlignment = new VerticalAlignment?(value);
                    }
                }
            }
        }

        bool IBaseEdit.ShowEditorButtons
        {
            get => 
                this.showEditorButtons;
            set
            {
                if (this.showEditorButtons != value)
                {
                    this.showEditorButtons = value;
                    this.UpdateEditorContent();
                }
            }
        }

        bool IInplaceBaseEdit.ShowBorder
        {
            get => 
                this.showBorder;
            set
            {
                if (this.showBorder != value)
                {
                    this.showBorder = value;
                    this.OnShowBorderChanged();
                }
            }
        }

        bool IInplaceBaseEdit.ShowText
        {
            get => 
                this.showText;
            set
            {
                if (this.showText != value)
                {
                    this.showText = value;
                    this.UpdateEditorContent();
                }
            }
        }

        bool IInplaceBaseEdit.IsNullTextVisible =>
            !this.isNullTextVisible ? false : !this.hasDisplayTextProviderText.GetValueOrDefault(false);

        bool IInplaceBaseEdit.ShowToolTipForTrimmedText { get; set; }

        public ControlTemplate EditTemplate
        {
            get => 
                this.editTemplate;
            set
            {
                if (!ReferenceEquals(this.editTemplate, value))
                {
                    this.editTemplate = value;
                    if (this.activeEditor != null)
                    {
                        object dataContext = this.dataContext;
                        if (this.dataContext == null)
                        {
                            object local1 = this.dataContext;
                            dataContext = base.DataContext;
                        }
                        this.activeEditor.RealDataContext = dataContext;
                        this.activeEditor.EditTemplate = value;
                    }
                }
            }
        }

        ControlTemplate IBaseEdit.DisplayTemplate
        {
            get => 
                this.displayTemplate;
            set
            {
                if (!ReferenceEquals(this.displayTemplate, value))
                {
                    this.displayTemplate = value;
                    this.UpdateEditorContent();
                    if (this.editorControl != null)
                    {
                        ControlTemplate template1 = value;
                        if (value == null)
                        {
                            ControlTemplate local1 = value;
                            template1 = this.CalcDisplayTemplate();
                        }
                        this.editorControl.Template = template1;
                    }
                }
            }
        }

        bool IBaseEdit.CanAcceptFocus { get; set; }

        bool IBaseEdit.HasValidationError =>
            this.validationError != null;

        BaseValidationError IBaseEdit.ValidationError
        {
            get => 
                this.validationError;
            set
            {
                if (!ReferenceEquals(this.validationError, value))
                {
                    this.validationError = value;
                    this.OnValidationErrorChanged(value);
                    this.AssignActiveEditorValues();
                }
            }
        }

        DataTemplate IBaseEdit.ValidationErrorTemplate
        {
            get => 
                this.validationErrorTemplate;
            set
            {
                if (!ReferenceEquals(this.validationErrorTemplate, value))
                {
                    this.validationErrorTemplate = value;
                }
            }
        }

        DataTemplate IBaseEdit.ErrorToolTipContentTemplate
        {
            get => 
                this.errorToolTipContentTemplate;
            set
            {
                if (!ReferenceEquals(this.errorToolTipContentTemplate, value))
                {
                    this.errorToolTipContentTemplate = value;
                    this.RefreshToolTip();
                }
            }
        }

        bool IBaseEdit.IsPrintingMode { get; set; }

        bool IBaseEdit.IsValueChanged
        {
            get
            {
                Func<RenderBaseEditContext, bool> evaluator = <>c.<>9__200_0;
                if (<>c.<>9__200_0 == null)
                {
                    Func<RenderBaseEditContext, bool> local1 = <>c.<>9__200_0;
                    evaluator = <>c.<>9__200_0 = x => x.IsValueChanged;
                }
                return this.activeEditor.Return<RenderBaseEditContext, bool>(evaluator, () => this.isValueChanged);
            }
            set
            {
                this.isValueChanged = value;
                this.SetEditIsValueChanged(value);
            }
        }

        public FrameworkElement EditCore =>
            null;

        string IBaseEdit.NullText
        {
            get => 
                this.nullText;
            set
            {
                if (this.nullText != value)
                {
                    this.nullText = value;
                    this.ResetDisplayText();
                    this.AssignTextBlockValues();
                }
            }
        }

        bool IBaseEdit.ShowNullText
        {
            get => 
                this.showNullText;
            set
            {
                if (this.showNullText != value)
                {
                    this.showNullText = value;
                    this.ResetDisplayText();
                    this.AssignTextBlockValues();
                }
            }
        }

        bool IBaseEdit.ShowNullTextForEmptyValue
        {
            get => 
                this.showNullTextForEmptyValue;
            set
            {
                if (this.showNullTextForEmptyValue != value)
                {
                    this.showNullTextForEmptyValue = value;
                    this.ResetDisplayText();
                    this.AssignTextBlockValues();
                }
            }
        }

        object IBaseEdit.NullValue
        {
            get => 
                this.nullValue;
            set
            {
                if (this.nullValue != value)
                {
                    this.nullValue = value;
                    this.ResetDisplayText();
                    this.AssignTextBlockValues();
                }
            }
        }

        public object EditValue
        {
            get => 
                base.GetValue(EditValueProperty);
            set => 
                base.SetValue(EditValueProperty, value);
        }

        IBaseEdit IInplaceBaseEdit.BaseEdit
        {
            get
            {
                Func<RenderBaseEditContext, FrameworkElement> evaluator = <>c.<>9__289_0;
                if (<>c.<>9__289_0 == null)
                {
                    Func<RenderBaseEditContext, FrameworkElement> local1 = <>c.<>9__289_0;
                    evaluator = <>c.<>9__289_0 = x => x.Control;
                }
                return (BaseEdit) this.activeEditor.With<RenderBaseEditContext, FrameworkElement>(evaluator);
            }
        }

        TextTrimming IInplaceBaseEdit.TextTrimming
        {
            get => 
                this.textTrimming;
            set
            {
                if (this.textTrimming != value)
                {
                    this.textTrimming = value;
                    (this.Settings as TextEditSettings).Do<TextEditSettings>(x => x.TextTrimming = value);
                    this.AssignTextBlockValues();
                }
            }
        }

        TextWrapping IInplaceBaseEdit.TextWrapping
        {
            get => 
                this.textWrapping;
            set
            {
                if (this.textWrapping != value)
                {
                    this.textWrapping = value;
                    TextEditSettings settings = this.Settings as TextEditSettings;
                    if ((settings != null) && (settings.TextWrapping != value))
                    {
                        settings.SetCurrentValue(TextEditSettings.TextWrappingProperty, value);
                    }
                    this.AssignTextBlockValues();
                }
            }
        }

        bool IInplaceBaseEdit.AllowDefaultButton
        {
            get => 
                this.allowDefaultButton;
            set
            {
                if (this.allowDefaultButton != value)
                {
                    this.allowDefaultButton = value;
                    this.ProcessInplaceButtonEdit();
                }
            }
        }

        HighlightedTextCriteria IInplaceBaseEdit.HighlightedTextCriteria
        {
            get => 
                this.highlightedTextCriteria;
            set
            {
                if (this.highlightedTextCriteria != value)
                {
                    this.highlightedTextCriteria = value;
                    this.AssignTextBlockValues();
                }
            }
        }

        TextDecorationCollection IInplaceBaseEdit.TextDecorations
        {
            get => 
                this.textDecorations;
            set
            {
                if (!ReferenceEquals(this.textDecorations, value))
                {
                    this.textDecorations = value;
                    this.AssignTextBlockValues();
                }
            }
        }

        string IInplaceBaseEdit.HighlightedText
        {
            get => 
                this.highlightedText;
            set
            {
                if (this.highlightedText != value)
                {
                    this.highlightedText = value;
                    this.AssignTextBlockValues();
                }
            }
        }

        object IBaseEdit.EditValue
        {
            get => 
                this.EditValueCache;
            set
            {
                if (this.EditMode == DevExpress.Xpf.Editors.EditMode.InplaceInactive)
                {
                    this.OnEditValueChanged(this.EditValueCache, value);
                }
                else
                {
                    this.EditValue = value;
                }
            }
        }

        Style IInplaceBaseEdit.ActiveEditorStyle
        {
            get => 
                this.editCoreStyle;
            set
            {
                if (!ReferenceEquals(this.editCoreStyle, value))
                {
                    this.editCoreStyle = value;
                    if (this.activeEditor != null)
                    {
                        this.activeEditor.Style = this.editCoreStyle;
                    }
                }
            }
        }

        double IBaseEdit.MaxWidth
        {
            get => 
                base.MaxWidth;
            set => 
                base.MaxWidth = value;
        }

        bool IChrome.IsLoaded =>
            base.IsLoaded;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly InplaceBaseEdit.<>c <>9 = new InplaceBaseEdit.<>c();
            public static Func<RenderBaseEditContext, bool> <>9__131_0;
            public static Func<RenderBaseEditContext, bool> <>9__200_0;
            public static Func<RenderBaseEditContext, bool> <>9__203_0;
            public static Func<bool> <>9__203_1;
            public static Action<RenderBaseEditContext> <>9__204_0;
            public static Func<bool> <>9__207_2;
            public static Action<RenderBaseEditContext> <>9__211_0;
            public static Action<InplaceBaseEdit, object, EventArgs> <>9__235_0;
            public static Action<InplaceBaseEdit, object, ItemsProviderChangedEventArgs> <>9__235_1;
            public static Func<CustomItem, bool> <>9__260_1;
            public static Func<ButtonInfoBase, bool> <>9__269_0;
            public static Func<ButtonInfoBase, bool> <>9__269_1;
            public static Func<RenderBaseEditContext, bool> <>9__272_0;
            public static Func<bool> <>9__282_2;
            public static Func<RenderBaseEditContext, IBaseEdit> <>9__285_0;
            public static Func<RenderBaseEditContext, FrameworkElement> <>9__289_0;
            public static Func<ButtonInfoBase, int> <>9__309_0;

            internal void <.cctor>b__9_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((InplaceBaseEdit) o).OnEditValueChanged(args.OldValue, args.NewValue);
            }

            internal void <.cctor>b__9_1(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((InplaceBaseEdit) o).EditCoreStyleChanged((Style) args.NewValue);
            }

            internal void <.ctor>b__235_0(InplaceBaseEdit owner, object o, EventArgs e)
            {
                owner.OnEditSettingsChanged();
            }

            internal void <.ctor>b__235_1(InplaceBaseEdit owner, object o, ItemsProviderChangedEventArgs e)
            {
                owner.ItemsProviderChanged(e);
            }

            internal bool <CalcTokenEditValue>b__260_1(CustomItem item) => 
                item != null;

            internal bool <DevExpress.Xpf.Editors.IBaseEdit.get_IsEditorActive>b__131_0(RenderBaseEditContext x) => 
                x.IsEditorActive;

            internal bool <DevExpress.Xpf.Editors.IBaseEdit.get_IsValueChanged>b__200_0(RenderBaseEditContext x) => 
                x.IsValueChanged;

            internal IBaseEdit <DevExpress.Xpf.Editors.IBaseEdit.IsChildElement>b__285_0(RenderBaseEditContext x) => 
                x.Control as IBaseEdit;

            internal FrameworkElement <DevExpress.Xpf.Editors.IInplaceBaseEdit.get_BaseEdit>b__289_0(RenderBaseEditContext x) => 
                x.Control;

            internal int <DevExpress.Xpf.Editors.IInplaceBaseEdit.GetSortedButtons>b__309_0(ButtonInfoBase x) => 
                x.Index;

            internal bool <DoValidate>b__203_0(RenderBaseEditContext x) => 
                x.DoValidate();

            internal bool <DoValidate>b__203_1() => 
                true;

            internal void <FlushPendingEditActions>b__211_0(RenderBaseEditContext x)
            {
                x.FlushPendingEditActions();
            }

            internal bool <NeedsKey>b__207_2() => 
                false;

            internal bool <ProcessActiveEditor>b__272_0(RenderBaseEditContext x) => 
                x.IsValueChanged;

            internal bool <ProcessInplaceButtonEdit>b__269_0(ButtonInfoBase btn) => 
                btn.IsLeft;

            internal bool <ProcessInplaceButtonEdit>b__269_1(ButtonInfoBase btn) => 
                !btn.IsLeft;

            internal bool <RefreshToolTip>b__282_2() => 
                false;

            internal void <SelectAll>b__204_0(RenderBaseEditContext x)
            {
                x.SelectAll();
            }
        }
    }
}

