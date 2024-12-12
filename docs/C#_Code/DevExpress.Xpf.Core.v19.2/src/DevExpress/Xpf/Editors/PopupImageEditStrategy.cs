namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class PopupImageEditStrategy : PopupBaseEditStrategy
    {
        public PopupImageEditStrategy(PopupImageEdit edit) : base(edit)
        {
            this.UpdateBaseUriOnLoadedAction = new PostponedAction(() => !this.Editor.IsLoaded);
        }

        public virtual void AcceptPopupValue()
        {
            if (this.ImageEditControl != null)
            {
                base.ValueContainer.SetEditValue(this.ImageEditControl.EditValue, UpdateEditorSource.ValueChanging);
                this.UpdateDisplayText();
            }
        }

        public virtual object CoerceSource(ImageSource value) => 
            this.CoerceValue(PopupImageEdit.SourceProperty, value);

        public override void OnLoaded()
        {
            base.OnLoaded();
            this.UpdateBaseUriOnLoadedAction.PerformForce();
        }

        protected override void RegisterUpdateCallbacks()
        {
            base.RegisterUpdateCallbacks();
            PropertyCoercionHandler getPropertyValueHandler = <>c.<>9__14_1;
            if (<>c.<>9__14_1 == null)
            {
                PropertyCoercionHandler local1 = <>c.<>9__14_1;
                getPropertyValueHandler = <>c.<>9__14_1 = (PropertyCoercionHandler) (baseValue => ImageEditStrategy.GetImageFromData(baseValue));
            }
            base.PropertyUpdater.Register(PopupImageEdit.SourceProperty, baseValue => ((IImageEdit) this.Editor).GetDataFromImage((ImageSource) baseValue), getPropertyValueHandler);
        }

        public virtual void SourceChanged(ImageSource oldSource, ImageSource newSource)
        {
            this.UpdateBaseUriOnLoadedAction.PerformPostpone(() => this.Editor.UpdateBaseUri());
            if (!base.ShouldLockUpdate)
            {
                base.SyncWithValue(PopupImageEdit.SourceProperty, oldSource, newSource);
            }
        }

        protected override void SyncWithValueInternal()
        {
            this.Editor.HasImage = this.Editor.Source != null;
            if (this.ImageEditControl != null)
            {
                this.ImageEditControl.ShowMenu = this.Editor.ShowMenu;
                this.ImageEditControl.ShowMenuMode = this.Editor.ShowMenuMode;
                if (this.Editor.EmptyContentTemplate != null)
                {
                    this.ImageEditControl.EmptyContentTemplate = this.Editor.EmptyContentTemplate;
                }
                if (this.Editor.MenuTemplate != null)
                {
                    this.ImageEditControl.MenuTemplate = this.Editor.MenuTemplate;
                }
                if (this.Editor.MenuContainerTemplate != null)
                {
                    this.ImageEditControl.MenuContainerTemplate = this.Editor.MenuContainerTemplate;
                }
                this.ImageEditControl.ShowLoadDialogOnClickMode = this.Editor.ShowLoadDialogOnClickMode;
                this.ImageEditControl.Source = this.Editor.Source;
                this.ImageEditControl.IsReadOnly = this.Editor.IsReadOnly;
            }
        }

        private PostponedAction UpdateBaseUriOnLoadedAction { get; set; }

        protected PopupImageEdit Editor =>
            (PopupImageEdit) base.Editor;

        protected ImageEdit ImageEditControl =>
            this.Editor.ImageEditControl;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PopupImageEditStrategy.<>c <>9 = new PopupImageEditStrategy.<>c();
            public static PropertyCoercionHandler <>9__14_1;

            internal object <RegisterUpdateCallbacks>b__14_1(object baseValue) => 
                ImageEditStrategy.GetImageFromData(baseValue);
        }
    }
}

