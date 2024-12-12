namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Themes;
    using DevExpress.Xpf.Utils.Themes;
    using System;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;

    public class InplaceResourceProvider
    {
        private RenderTemplate textEditInplaceInactiveTemplate;
        private RenderTemplate textEditInplaceActiveTemplate;
        private RenderTemplateSelector textEditTemplateSelector;
        private Size? checkBoxRenderSize;
        private RenderTemplate applyGlyphKindTemplate;
        private RenderTemplate cancelGlyphKindTemplate;
        private RenderTemplate dropDownGlyphTemplate;
        private RenderTemplate regularGlyphTemplate;
        private RenderTemplate criticalErrorTemplate;
        private RenderTemplate validationErrorTemplate;
        private RenderTemplate informationErrorTemplate;
        private RenderTemplate warningErrorTemplate;
        private RenderTemplate renderCheckBoxTemplate;
        private RenderTemplate renderImageCheckBoxTemplate;
        private RenderTemplate renderImageWithCheckBoxCheckBoxTemplate;
        private RenderTemplate checkEditInplaceInactiveTemplate;
        private RenderTemplate textEditBorderTemplate;
        private RenderTemplate commonBorderTemplate;
        private RenderTemplate hoverBorderTemplate;
        private ControlTemplate commonBorderDecorationTemplate;
        private ControlTemplate textEditBorderDecorationTemplate;
        private ControlTemplate hoverBorderDecorationTemplate;
        private Thickness? textEditBorderThickness;
        private Thickness? commonBorderThickness;
        private RenderTemplate rightGlyphTemplate;
        private RenderTemplate leftGlyphTemplate;
        private RenderTemplate upGlyphTemplate;
        private RenderTemplate downGlyphTemplate;
        private RenderTemplate rightSpinGlyphTemplate;
        private RenderTemplate leftSpinGlyphTemplate;
        private RenderTemplate upSpinGlyphTemplate;
        private RenderTemplate downSpinGlyphTemplate;
        private RenderTemplate plusGlyphTemplate;
        private RenderTemplate minusGlyphTemplate;
        private RenderTemplate redoGlyphTemplate;
        private RenderTemplate undoGlyphTemplate;
        private RenderTemplate refreshGlyphTemplate;
        private RenderTemplate noneGlyphTemplate;
        private RenderTemplate searchGlyphTemplate;
        private RenderTemplate nextPageGlyphTemplate;
        private RenderTemplate prevPageGlyphTemplate;
        private RenderTemplate lastGlyphTemplate;
        private RenderTemplate firstGlyphTemplate;
        private RenderTemplate editGlyphTemplate;
        private RenderTemplate userGlyphTemplate;
        private RenderTemplate customGlyphTemplate;
        private RenderTemplate commonBaseEditInplaceInactiveTemplate;
        private ControlTemplate selectedItemTemplate;
        private ControlTemplate selectedItemImageTemplate;
        private ControlTemplate tokenEditorDisplayTemplate;
        private RenderTemplate commonBaseEditInplaceInactiveTemplateWithDisplayTemplate;
        private RenderTemplate renderButtonContainerTemplate;
        private RenderTemplate renderSpinButtonContainerTemplate;
        private RenderTemplate renderButtonTemplate;
        private RenderTemplate renderSpinButtonTemplate;
        private RenderTemplate renderSpinUpButtonTemplate;
        private RenderTemplate renderSpinLeftButtonTemplate;
        private RenderTemplate renderSpinRightButtonTemplate;
        private RenderTemplate renderSpinDownButtonTemplate;
        private RenderTemplate realContentPresenterTemplate;
        private RenderTemplate contentPresenterTemplate;
        private RenderTemplate textContentPresenterTemplate;
        private Thickness? rightButtonMargin;
        private WpfSvgPalette svgPalette;
        private WpfSvgPalette validatePalette;
        private string themeName;
        private bool? useLightweightTemplates;
        private static readonly Func<FrameworkElement, FrameworkContentElement, object, object> FindResourceInternal;

        static InplaceResourceProvider()
        {
            int? parametersCount = null;
            Type[] typeParameters = new Type[] { typeof(FrameworkElement), typeof(FrameworkContentElement), typeof(object) };
            FindResourceInternal = ReflectionHelper.CreateInstanceMethodHandler<FrameworkElement, Func<FrameworkElement, FrameworkContentElement, object, object>>(null, "FindResourceInternal", BindingFlags.NonPublic | BindingFlags.Static, parametersCount, typeParameters, true);
        }

        public InplaceResourceProvider(string themeName)
        {
            this.themeName = themeName;
        }

        public RenderTemplate GetButtonInfoApplyGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.applyGlyphKindTemplate, InplaceBaseEditThemeKeys.ApplyGlyph);

        public RenderTemplate GetButtonInfoCancelGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.cancelGlyphKindTemplate, InplaceBaseEditThemeKeys.CancelGlyph);

        internal RenderTemplate GetButtonInfoCustomGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.customGlyphTemplate, InplaceBaseEditThemeKeys.CustomGlyph);

        internal RenderTemplate GetButtonInfoDownGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.downGlyphTemplate, InplaceBaseEditThemeKeys.DownGlyph);

        public RenderTemplate GetButtonInfoDropDownGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.dropDownGlyphTemplate, InplaceBaseEditThemeKeys.DropDownGlyph);

        internal RenderTemplate GetButtonInfoEditGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.editGlyphTemplate, InplaceBaseEditThemeKeys.EditGlyph);

        internal RenderTemplate GetButtonInfoFirstGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.firstGlyphTemplate, InplaceBaseEditThemeKeys.FirstGlyph);

        internal RenderTemplate GetButtonInfoLastGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.lastGlyphTemplate, InplaceBaseEditThemeKeys.LastGlyph);

        internal RenderTemplate GetButtonInfoLeftGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.leftGlyphTemplate, InplaceBaseEditThemeKeys.LeftGlyph);

        internal RenderTemplate GetButtonInfoMinusGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.minusGlyphTemplate, InplaceBaseEditThemeKeys.MinusGlyph);

        internal RenderTemplate GetButtonInfoNextPageGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.nextPageGlyphTemplate, InplaceBaseEditThemeKeys.NextPageGlyph);

        internal RenderTemplate GetButtonInfoNoneGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.noneGlyphTemplate, InplaceBaseEditThemeKeys.NoneGlyph);

        internal RenderTemplate GetButtonInfoPlusGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.plusGlyphTemplate, InplaceBaseEditThemeKeys.PlusGlyph);

        internal RenderTemplate GetButtonInfoPrevPageGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.prevPageGlyphTemplate, InplaceBaseEditThemeKeys.PrevPageGlyph);

        internal RenderTemplate GetButtonInfoRedoGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.redoGlyphTemplate, InplaceBaseEditThemeKeys.RedoGlyph);

        internal RenderTemplate GetButtonInfoRefreshGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.refreshGlyphTemplate, InplaceBaseEditThemeKeys.RefreshGlyph);

        public RenderTemplate GetButtonInfoRegularGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.regularGlyphTemplate, InplaceBaseEditThemeKeys.RegularGlyph);

        internal RenderTemplate GetButtonInfoRightGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.rightGlyphTemplate, InplaceBaseEditThemeKeys.RightGlyph);

        internal RenderTemplate GetButtonInfoSearchGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.searchGlyphTemplate, InplaceBaseEditThemeKeys.SearchGlyph);

        internal RenderTemplate GetButtonInfoSpinDownGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.downSpinGlyphTemplate, InplaceBaseEditThemeKeys.SpinDownGlyph);

        internal RenderTemplate GetButtonInfoSpinLeftGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.leftSpinGlyphTemplate, InplaceBaseEditThemeKeys.SpinLeftGlyph);

        internal RenderTemplate GetButtonInfoSpinRightGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.rightSpinGlyphTemplate, InplaceBaseEditThemeKeys.SpinRightGlyph);

        internal RenderTemplate GetButtonInfoSpinUpGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.upSpinGlyphTemplate, InplaceBaseEditThemeKeys.SpinUpGlyph);

        internal RenderTemplate GetButtonInfoUndoGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.undoGlyphTemplate, InplaceBaseEditThemeKeys.UndoGlyph);

        internal RenderTemplate GetButtonInfoUpGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.upGlyphTemplate, InplaceBaseEditThemeKeys.UpGlyph);

        internal RenderTemplate GetButtonInfoUserGlyphKindTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.userGlyphTemplate, InplaceBaseEditThemeKeys.UserGlyph);

        public Size GetCheckBoxRenderSize(FrameworkElement element)
        {
            if (this.checkBoxRenderSize == null)
            {
                CheckEditThemeKeyExtension resourceKey = new CheckEditThemeKeyExtension();
                resourceKey.ResourceKey = CheckEditThemeKeys.CheckSize;
                resourceKey.ThemeName = this.themeName;
                this.checkBoxRenderSize = new Size?((Size) element.FindResource(resourceKey));
            }
            return this.checkBoxRenderSize.Value;
        }

        public RenderTemplate GetCheckEditInplaceInactiveTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.checkEditInplaceInactiveTemplate, InplaceBaseEditThemeKeys.CheckEditInplaceInactiveTemplate);

        public RenderTemplate GetCommonBaseEditInplaceInactiveTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.commonBaseEditInplaceInactiveTemplate, InplaceBaseEditThemeKeys.CommonBaseEditInplaceInactiveTemplate);

        public RenderTemplate GetCommonBaseEditInplaceInactiveTemplateWithDisplayTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.commonBaseEditInplaceInactiveTemplateWithDisplayTemplate, InplaceBaseEditThemeKeys.CommonBaseEditInplaceInactiveTemplateWithDisplayTemplate);

        public ControlTemplate GetCommonBorderDecorationTemplate(FrameworkElement element)
        {
            ControlTemplate commonBorderDecorationTemplate = this.commonBorderDecorationTemplate;
            if (this.commonBorderDecorationTemplate == null)
            {
                ControlTemplate local1 = this.commonBorderDecorationTemplate;
                BaseEditThemeKeyExtension resourceKey = new BaseEditThemeKeyExtension();
                resourceKey.ResourceKey = BaseEditThemeKeys.CommonBorderDecorationTemplate;
                resourceKey.ThemeName = this.themeName;
                commonBorderDecorationTemplate = this.commonBorderDecorationTemplate = (ControlTemplate) element.FindResource(resourceKey);
            }
            return commonBorderDecorationTemplate;
        }

        public RenderTemplate GetCommonBorderTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.commonBorderTemplate, InplaceBaseEditThemeKeys.CommonBorderTemplate);

        public Thickness GetCommonBorderThickness(FrameworkElement element) => 
            this.GetResource<Thickness>(element, ref this.commonBorderThickness, InplaceBaseEditThemeKeys.CommonBorderThickness);

        public RenderTemplate GetContentPresenterTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.contentPresenterTemplate, InplaceBaseEditThemeKeys.ContentPresenterTemplate);

        public RenderTemplate GetCriticalErrorTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.criticalErrorTemplate, InplaceBaseEditThemeKeys.CriticalErrorTemplate);

        public ControlTemplate GetHoverBorderDecorationTemplate(FrameworkElement element)
        {
            ControlTemplate hoverBorderDecorationTemplate = this.hoverBorderDecorationTemplate;
            if (this.hoverBorderDecorationTemplate == null)
            {
                ControlTemplate local1 = this.hoverBorderDecorationTemplate;
                BaseEditThemeKeyExtension resourceKey = new BaseEditThemeKeyExtension();
                resourceKey.ResourceKey = BaseEditThemeKeys.HoverBorderDecorationTemplate;
                resourceKey.ThemeName = this.themeName;
                hoverBorderDecorationTemplate = this.hoverBorderDecorationTemplate = (ControlTemplate) element.FindResource(resourceKey);
            }
            return hoverBorderDecorationTemplate;
        }

        public RenderTemplate GetHoverBorderTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.hoverBorderTemplate, InplaceBaseEditThemeKeys.HoverBorderTemplate);

        public RenderTemplate GetInformationErrorTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.informationErrorTemplate, InplaceBaseEditThemeKeys.InformationErrorTemplate);

        public RenderTemplate GetRealContentPresenterTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.realContentPresenterTemplate, InplaceBaseEditThemeKeys.RealContentPresenterTemplate);

        public RenderTemplate GetRenderButtonContainerTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.renderButtonContainerTemplate, InplaceBaseEditThemeKeys.RenderButtonContainerTemplate);

        public RenderTemplate GetRenderButtonTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.renderButtonTemplate, InplaceBaseEditThemeKeys.RenderButtonTemplate);

        public RenderTemplate GetRenderCheckBoxTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.renderCheckBoxTemplate, InplaceBaseEditThemeKeys.RenderCheckBoxTemplate);

        public RenderTemplate GetRenderImageCheckBoxTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.renderImageCheckBoxTemplate, InplaceBaseEditThemeKeys.RenderImageCheckBoxTemplate);

        public RenderTemplate GetRenderImageWithTemplateCheckBoxTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.renderImageWithCheckBoxCheckBoxTemplate, InplaceBaseEditThemeKeys.RenderImageWithTemplateCheckBoxTemplate);

        public RenderTemplate GetRenderRealButtonContainerTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.renderButtonContainerTemplate, InplaceBaseEditThemeKeys.RenderRealButtonContainerTemplate);

        public RenderTemplate GetRenderSpinButtonContainerTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.renderSpinButtonContainerTemplate, InplaceBaseEditThemeKeys.RenderSpinButtonContainerTemplate);

        public RenderTemplate GetRenderSpinButtonTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.renderSpinButtonTemplate, InplaceBaseEditThemeKeys.RenderSpinButtonTemplate);

        public RenderTemplate GetRenderSpinDownButtonTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.renderSpinDownButtonTemplate, InplaceBaseEditThemeKeys.RenderSpinDownButtonTemplate);

        public RenderTemplate GetRenderSpinLeftButtonTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.renderSpinLeftButtonTemplate, InplaceBaseEditThemeKeys.RenderSpinLeftButtonTemplate);

        public RenderTemplate GetRenderSpinRightButtonTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.renderSpinRightButtonTemplate, InplaceBaseEditThemeKeys.RenderSpinRightButtonTemplate);

        public RenderTemplate GetRenderSpinUpButtonTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.renderSpinUpButtonTemplate, InplaceBaseEditThemeKeys.RenderSpinUpButtonTemplate);

        private T GetResource<T>(FrameworkElement element, ref T field, InplaceBaseEditThemeKeys key) where T: class
        {
            ref T localRef1 = field;
            ref T localRef2 = localRef1;
            if (((T) localRef1) == null)
            {
                ref T local1 = localRef1;
                InplaceBaseEditThemeKeyExtension resourceKey = new InplaceBaseEditThemeKeyExtension();
                resourceKey.ResourceKey = key;
                resourceKey.ThemeName = this.themeName;
                T local2 = field = (T) element.FindResource(resourceKey);
                localRef2 = local2;
            }
            return localRef2;
        }

        private TValue GetResource<TValue>(FrameworkElement element, ref TValue? field, InplaceBaseEditThemeKeys key) where TValue: struct
        {
            TValue? nullable1;
            TValue? nullable2 = field;
            if (nullable2 != null)
            {
                nullable1 = nullable2;
            }
            else
            {
                InplaceBaseEditThemeKeyExtension resourceKey = new InplaceBaseEditThemeKeyExtension();
                resourceKey.ResourceKey = key;
                resourceKey.ThemeName = this.themeName;
                nullable1 = field = new TValue?((TValue) element.FindResource(resourceKey));
            }
            TValue? nullable = nullable1;
            if (nullable != null)
            {
                return nullable.Value;
            }
            return default(TValue);
        }

        public Thickness GetRightButtonMargin(FrameworkElement element)
        {
            if (this.rightButtonMargin == null)
            {
                ButtonsThemeKeyExtension resourceKey = new ButtonsThemeKeyExtension();
                resourceKey.ResourceKey = ButtonsThemeKeys.RightButtonMargin;
                resourceKey.ThemeName = this.themeName;
                this.rightButtonMargin = new Thickness?((Thickness) element.FindResource(resourceKey));
            }
            return this.rightButtonMargin.Value;
        }

        public ControlTemplate GetSelectedItemImageTemplate(FrameworkElement element) => 
            this.GetResource<ControlTemplate>(element, ref this.selectedItemImageTemplate, InplaceBaseEditThemeKeys.SelectedItemImageTemplate);

        public ControlTemplate GetSelectedItemTemplate(FrameworkElement element) => 
            this.GetResource<ControlTemplate>(element, ref this.selectedItemTemplate, InplaceBaseEditThemeKeys.SelectedItemTemplate);

        public WpfSvgPalette GetSvgPalette(object element)
        {
            if (this.svgPalette == null)
            {
                BrushesThemeKeyExtension resourceKey = new BrushesThemeKeyExtension();
                resourceKey.ResourceKey = BrushesThemeKeys.SvgPalette;
                resourceKey.ThemeName = this.themeName;
                this.svgPalette = (WpfSvgPalette) this.TryFindResource(element, resourceKey);
            }
            return this.svgPalette;
        }

        public RenderTemplate GetTextContentPresenterTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.textContentPresenterTemplate, InplaceBaseEditThemeKeys.TextContentPresenterTemplate);

        public ControlTemplate GetTextEditBorderDecorationTemplate(FrameworkElement element)
        {
            ControlTemplate textEditBorderDecorationTemplate = this.textEditBorderDecorationTemplate;
            if (this.textEditBorderDecorationTemplate == null)
            {
                ControlTemplate local1 = this.textEditBorderDecorationTemplate;
                BaseEditThemeKeyExtension resourceKey = new BaseEditThemeKeyExtension();
                resourceKey.ResourceKey = BaseEditThemeKeys.TextEditBorderDecorationTemplate;
                resourceKey.ThemeName = this.themeName;
                textEditBorderDecorationTemplate = this.textEditBorderDecorationTemplate = (ControlTemplate) element.FindResource(resourceKey);
            }
            return textEditBorderDecorationTemplate;
        }

        public RenderTemplate GetTextEditBorderTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.textEditBorderTemplate, InplaceBaseEditThemeKeys.TextEditBorderTemplate);

        public Thickness GetTextEditBorderThickness(FrameworkElement element) => 
            this.GetResource<Thickness>(element, ref this.textEditBorderThickness, InplaceBaseEditThemeKeys.TextEditBorderThickness);

        public RenderTemplate GetTextEditInplaceActiveTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.textEditInplaceActiveTemplate, InplaceBaseEditThemeKeys.TextEditInplaceActiveTemplate);

        public RenderTemplate GetTextEditInplaceInactiveTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.textEditInplaceInactiveTemplate, InplaceBaseEditThemeKeys.TextEditInplaceInactiveTemplate);

        public RenderTemplateSelector GetTextEditTemplateSelector(FrameworkElement element) => 
            this.GetResource<RenderTemplateSelector>(element, ref this.textEditTemplateSelector, InplaceBaseEditThemeKeys.TextEditInplaceTemplateSelector);

        public ControlTemplate GetTokenEditorDisplayTemplate(FrameworkElement element) => 
            this.GetResource<ControlTemplate>(element, ref this.tokenEditorDisplayTemplate, InplaceBaseEditThemeKeys.AutoCompleteBoxTemplate);

        public bool GetUseLightweightTemplates(FrameworkElement element)
        {
            if (this.useLightweightTemplates == null)
            {
                TextEditThemeKeyExtension resourceKey = new TextEditThemeKeyExtension();
                resourceKey.ResourceKey = TextEditThemeKeys.UseLightweightTemplates;
                resourceKey.ThemeName = this.themeName;
                this.useLightweightTemplates = new bool?((bool) this.TryFindResource(element, resourceKey));
            }
            return this.useLightweightTemplates.Value;
        }

        public WpfSvgPalette GetValidationErrorPalette(FrameworkElement element) => 
            this.GetResource<WpfSvgPalette>(element, ref this.validatePalette, InplaceBaseEditThemeKeys.ValidationErrorSvgPalette);

        public RenderTemplate GetValidationErrorTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.validationErrorTemplate, InplaceBaseEditThemeKeys.ValidationErrorTemplate);

        public RenderTemplate GetWarningErrorTemplate(FrameworkElement element) => 
            this.GetResource<RenderTemplate>(element, ref this.warningErrorTemplate, InplaceBaseEditThemeKeys.WarningErrorTemplate);

        private object TryFindResource(object element, object resourceKey)
        {
            object obj2 = FindResourceInternal(element as FrameworkElement, element as FrameworkContentElement, resourceKey);
            if (obj2 == DependencyProperty.UnsetValue)
            {
                obj2 = null;
            }
            return obj2;
        }
    }
}

