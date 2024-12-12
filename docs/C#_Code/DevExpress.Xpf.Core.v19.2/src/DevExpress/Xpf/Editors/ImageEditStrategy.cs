namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class ImageEditStrategy : EditStrategyBase
    {
        public ImageEditStrategy(ImageEdit edit) : base(edit)
        {
        }

        public virtual object CoerceSource(ImageSource value) => 
            this.CoerceValue(ImageEdit.SourceProperty, value);

        private static object GetDataFromImageSource(object baseValue)
        {
            ImageSource source = baseValue as ImageSource;
            if (source != null)
            {
                byte[] buffer = ImageLoader.ImageToByteArray(source);
                if (buffer != null)
                {
                    return buffer;
                }
            }
            return (source ?? baseValue);
        }

        public static ImageSource GetImageFromData(object data)
        {
            if (data is ImageSource)
            {
                return (ImageSource) data;
            }
            try
            {
                return GetImageFromDataCore(data);
            }
            catch
            {
                return null;
            }
        }

        private static ImageSource GetImageFromDataCore(object data) => 
            ((data is byte[]) || ((data is Uri) || (data is string))) ? (new ImageSourceConverter().ConvertFrom(data) as ImageSource) : null;

        public virtual void OnSourceChanged(ImageSource oldValue, ImageSource newValue)
        {
            if (!base.ShouldLockUpdate)
            {
                base.SyncWithValue(ImageEdit.SourceProperty, oldValue, newValue);
            }
        }

        protected override void RegisterUpdateCallbacks()
        {
            // Unresolved stack state at '0000008A'
        }

        public virtual void SetImage(ImageSource imageSource)
        {
            this.Editor.SetCurrentValue(ImageEdit.SourceProperty, imageSource);
        }

        protected ImageEdit Editor =>
            (ImageEdit) base.Editor;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ImageEditStrategy.<>c <>9 = new ImageEditStrategy.<>c();
            public static PropertyCoercionHandler <>9__8_0;
            public static PropertyCoercionHandler <>9__8_1;
            public static PropertyCoercionHandler <>9__8_3;

            internal object <RegisterUpdateCallbacks>b__8_0(object baseValue) => 
                baseValue;

            internal object <RegisterUpdateCallbacks>b__8_1(object baseValue) => 
                ImageEditStrategy.GetDataFromImageSource(baseValue);

            internal object <RegisterUpdateCallbacks>b__8_3(object baseValue) => 
                ImageEditStrategy.GetImageFromData(baseValue);
        }
    }
}

