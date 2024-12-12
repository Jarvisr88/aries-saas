namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public class CheckEditSettings : BaseEditSettings
    {
        public static readonly DependencyProperty ClickModeProperty = CheckEdit.ClickModeProperty.AddOwner(typeof(CheckEditSettings));
        public static readonly DependencyProperty IsThreeStateProperty = CheckEdit.IsThreeStateProperty.AddOwner(typeof(CheckEditSettings));
        public static readonly DependencyProperty ContentProperty = CheckEdit.ContentProperty.AddOwner(typeof(CheckEditSettings));
        public static readonly DependencyProperty ContentTemplateProperty = CheckEdit.ContentTemplateProperty.AddOwner(typeof(CheckEditSettings));
        public static readonly DependencyProperty ContentTemplateSelectorProperty = CheckEdit.ContentTemplateSelectorProperty.AddOwner(typeof(CheckEditSettings));
        public static readonly DependencyProperty CheckedGlyphProperty = CheckEdit.CheckedGlyphProperty.AddOwner(typeof(CheckEditSettings));
        public static readonly DependencyProperty UncheckedGlyphProperty = CheckEdit.UncheckedGlyphProperty.AddOwner(typeof(CheckEditSettings));
        public static readonly DependencyProperty IndeterminateGlyphProperty = CheckEdit.IndeterminateGlyphProperty.AddOwner(typeof(CheckEditSettings));
        public static readonly DependencyProperty GlyphTemplateProperty = CheckEdit.GlyphTemplateProperty.AddOwner(typeof(CheckEditSettings));

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            IInplaceBaseEdit edit2 = edit as IInplaceBaseEdit;
            if (edit2 != null)
            {
                edit2.CheckedGlyph = this.CheckedGlyph;
                edit2.UncheckedGlyph = this.UncheckedGlyph;
                edit2.IndeterminateGlyph = this.IndeterminateGlyph;
                edit2.GlyphTemplate = this.GlyphTemplate;
                CheckEditStyleSettingsBase styleSettings = base.StyleSettings as CheckEditStyleSettingsBase;
                edit2.CheckEditDisplayMode = (styleSettings != null) ? styleSettings.GetDisplayMode() : CheckEditDisplayMode.Check;
            }
            else
            {
                CheckEdit ce = edit as CheckEdit;
                if (ce != null)
                {
                    base.SetValueFromSettings(ClickModeProperty, () => ce.ClickMode = this.ClickMode);
                    base.SetValueFromSettings(IsThreeStateProperty, () => ce.IsThreeState = this.IsThreeState);
                    base.SetValueFromSettings(ContentProperty, () => ce.Content = this.Content);
                    base.SetValueFromSettings(ContentTemplateProperty, () => ce.ContentTemplate = this.ContentTemplate);
                    base.SetValueFromSettings(ContentTemplateSelectorProperty, () => ce.ContentTemplateSelector = this.ContentTemplateSelector);
                    base.SetValueFromSettings(CheckedGlyphProperty, () => ce.CheckedGlyph = this.CheckedGlyph);
                    base.SetValueFromSettings(UncheckedGlyphProperty, () => ce.UncheckedGlyph = this.UncheckedGlyph);
                    base.SetValueFromSettings(IndeterminateGlyphProperty, () => ce.IndeterminateGlyph = this.IndeterminateGlyph);
                    base.SetValueFromSettings(GlyphTemplateProperty, () => ce.GlyphTemplate = this.GlyphTemplate, () => this.ClearEditorPropertyIfNeeded(ce, CheckEdit.GlyphTemplateProperty, GlyphTemplateProperty));
                }
            }
        }

        protected override EditSettingsHorizontalAlignment GetActualHorizontalContentAlignment() => 
            EditSettingsHorizontalAlignment.Center;

        protected internal static bool? GetBooleanFromEditValue(object editValue, bool isThreeState)
        {
            if (editValue as bool)
            {
                return new bool?((bool) editValue);
            }
            if (IsNativeNullValue(editValue))
            {
                return null;
            }
            try
            {
                bool flag;
                return (!(editValue is string) ? new bool?(Convert.ToBoolean(editValue)) : (!bool.TryParse(editValue as string, out flag) ? GetDefaultBooleanValue(isThreeState) : new bool?(flag)));
            }
            catch
            {
                return GetDefaultBooleanValue(isThreeState);
            }
        }

        private static bool? GetDefaultBooleanValue(bool isThreeState)
        {
            if (!isThreeState)
            {
                return false;
            }
            return null;
        }

        protected internal override bool IsActivatingKey(Key key, ModifierKeys modifiers) => 
            !this.IsToggleCheckGesture(key) ? base.IsActivatingKey(key, modifiers) : true;

        public bool IsToggleCheckGesture(Key key) => 
            key == Key.Space;

        public DataTemplate GlyphTemplate
        {
            get => 
                (DataTemplate) base.GetValue(GlyphTemplateProperty);
            set => 
                base.SetValue(GlyphTemplateProperty, value);
        }

        public ImageSource CheckedGlyph
        {
            get => 
                (ImageSource) base.GetValue(CheckedGlyphProperty);
            set => 
                base.SetValue(CheckedGlyphProperty, value);
        }

        public ImageSource UncheckedGlyph
        {
            get => 
                (ImageSource) base.GetValue(UncheckedGlyphProperty);
            set => 
                base.SetValue(UncheckedGlyphProperty, value);
        }

        public ImageSource IndeterminateGlyph
        {
            get => 
                (ImageSource) base.GetValue(IndeterminateGlyphProperty);
            set => 
                base.SetValue(IndeterminateGlyphProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets a value that specifies when the editor's state changes. This is a dependency property.")]
        public System.Windows.Controls.ClickMode ClickMode
        {
            get => 
                (System.Windows.Controls.ClickMode) base.GetValue(ClickModeProperty);
            set => 
                base.SetValue(ClickModeProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets whether the check editor supports three check states. This is a dependency property.")]
        public bool IsThreeState
        {
            get => 
                (bool) base.GetValue(IsThreeStateProperty);
            set => 
                base.SetValue(IsThreeStateProperty, value);
        }

        [TypeConverter(typeof(ObjectConverter)), Category("Data"), Description("Gets or sets the editor's content. This is a dependency property.")]
        public object Content
        {
            get => 
                base.GetValue(ContentProperty);
            set => 
                base.SetValue(ContentProperty, value);
        }

        [Category("Appearance "), Description("Gets or sets the template that defines the presentation of the editor's content, represented by the CheckEditSettings.Content property. This is a dependency property.")]
        public DataTemplate ContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ContentTemplateProperty);
            set => 
                base.SetValue(ContentTemplateProperty, value);
        }

        [Description("Gets or sets an object that chooses the editor's content template based on custom logic.This is a dependency property."), Category("Appearance ")]
        public DataTemplateSelector ContentTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ContentTemplateSelectorProperty);
            set => 
                base.SetValue(ContentTemplateSelectorProperty, value);
        }
    }
}

