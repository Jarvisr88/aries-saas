namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class ColorEditStrategy : EditStrategyBase
    {
        public ColorEditStrategy(ColorEdit editor) : base(editor)
        {
        }

        public virtual object CoerceColor(Color color) => 
            this.CoerceValue(ColorEdit.ColorProperty, color);

        public virtual void OnColorChanged(Color oldValue, Color newValue)
        {
            if (!base.ShouldLockUpdate)
            {
                base.SyncWithValue(ColorEdit.ColorProperty, oldValue, newValue);
            }
        }

        public virtual void OnGalleryColorChanged(Color color)
        {
            base.ValueContainer.SetEditValue(color, UpdateEditorSource.ValueChanging);
        }

        public virtual void OnNoColorButtonClick()
        {
            base.ValueContainer.SetEditValue(ColorEdit.EmptyColor, UpdateEditorSource.ValueChanging);
        }

        public virtual void OnResetButtonClick()
        {
            base.ValueContainer.SetEditValue(this.Editor.DefaultColor, UpdateEditorSource.ValueChanging);
        }

        public override void RaiseValueChangedEvents(object oldValue, object newValue)
        {
            base.RaiseValueChangedEvents(oldValue, newValue);
            if (!base.ShouldLockRaiseEvents && (oldValue != newValue))
            {
                this.Editor.RaiseColorChanged();
            }
        }

        protected override void RegisterUpdateCallbacks()
        {
            base.RegisterUpdateCallbacks();
            PropertyCoercionHandler getBaseValueHandler = <>c.<>9__11_0;
            if (<>c.<>9__11_0 == null)
            {
                PropertyCoercionHandler local1 = <>c.<>9__11_0;
                getBaseValueHandler = <>c.<>9__11_0 = baseValue => baseValue;
            }
            base.PropertyUpdater.Register(ColorEdit.ColorProperty, getBaseValueHandler, <>c.<>9__11_1 ??= ((PropertyCoercionHandler) (baseValue => ColorEditHelper.GetColorFromValue(baseValue))));
        }

        protected override void SyncEditCorePropertiesInternal()
        {
            base.SyncEditCorePropertiesInternal();
            this.UpdateItemsState();
        }

        protected override void SyncWithValueInternal()
        {
            this.UpdateItemsState();
        }

        public virtual void UpdateItemsState()
        {
            Gallery gallery = this.Editor.Gallery;
            if (gallery != null)
            {
                Color color = this.Editor.Color;
                foreach (GalleryItemGroup group in gallery.Groups)
                {
                    foreach (ColorGalleryItem item in group.Items)
                    {
                        item.IsChecked = color == item.Color;
                        item.Hint = this.Editor.GetColorNameCore(item.Color);
                    }
                }
            }
        }

        protected ColorEdit Editor =>
            (ColorEdit) base.Editor;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ColorEditStrategy.<>c <>9 = new ColorEditStrategy.<>c();
            public static PropertyCoercionHandler <>9__11_0;
            public static PropertyCoercionHandler <>9__11_1;

            internal object <RegisterUpdateCallbacks>b__11_0(object baseValue) => 
                baseValue;

            internal object <RegisterUpdateCallbacks>b__11_1(object baseValue) => 
                ColorEditHelper.GetColorFromValue(baseValue);
        }
    }
}

