namespace DevExpress.Xpf.Printing
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class EditingFieldExtensions
    {
        private static DevExpress.Xpf.Printing.EditingFieldExtensions instance;
        private readonly ObservableCollection<InplaceEditorInfoBase> infoCollection = new ObservableCollection<InplaceEditorInfoBase>();

        static EditingFieldExtensions()
        {
            Instance.RegisterEditorInfo(EditingFieldEditorNames.Integer, PreviewStringId.EditingFieldEditorCategories_Numeric.GetString(), PreviewStringId.EditingFieldEditors_Integer.GetString());
            Instance.RegisterEditorInfo(EditingFieldEditorNames.IntegerPositive, PreviewStringId.EditingFieldEditorCategories_Numeric.GetString(), PreviewStringId.EditingFieldEditors_IntegerPositive.GetString());
            Instance.RegisterEditorInfo(EditingFieldEditorNames.FixedPoint, PreviewStringId.EditingFieldEditorCategories_Numeric.GetString(), PreviewStringId.EditingFieldEditors_FixedPoint.GetString());
            Instance.RegisterEditorInfo(EditingFieldEditorNames.FixedPointPositive, PreviewStringId.EditingFieldEditorCategories_Numeric.GetString(), PreviewStringId.EditingFieldEditors_FixedPointPositive.GetString());
            Instance.RegisterEditorInfo(EditingFieldEditorNames.Date, PreviewStringId.EditingFieldEditorCategories_DateTime.GetString(), PreviewStringId.EditingFieldEditors_Date.GetString());
            Instance.RegisterEditorInfo(EditingFieldEditorNames.OnlyLetters, PreviewStringId.EditingFieldEditorCategories_Letters.GetString(), PreviewStringId.EditingFieldEditors_OnlyLetters.GetString());
            Instance.RegisterEditorInfo(EditingFieldEditorNames.OnlyUppercaseLetters, PreviewStringId.EditingFieldEditorCategories_Letters.GetString(), PreviewStringId.EditingFieldEditors_OnlyUppercaseLetters.GetString());
            Instance.RegisterEditorInfo(EditingFieldEditorNames.OnlyLowercaseLetters, PreviewStringId.EditingFieldEditorCategories_Letters.GetString(), PreviewStringId.EditingFieldEditors_OnlyLowercaseLetters.GetString());
            Instance.RegisterEditorInfo(EditingFieldEditorNames.OnlyLatinLetters, PreviewStringId.EditingFieldEditorCategories_Letters.GetString(), PreviewStringId.EditingFieldEditors_OnlyLatinLetters.GetString());
            ImageEditorOptions options = new ImageEditorOptions();
            options.AllowLoadImage = true;
            options.AllowChangeSizeOptions = true;
            options.AllowDraw = false;
            Instance.RegisterImageEditorInfo(EditingFieldEditorNames.Image, options, PreviewStringId.EditingFieldEditors_Image.GetString());
            ImageEditorOptions options2 = new ImageEditorOptions();
            options2.AllowLoadImage = false;
            options2.AllowChangeSizeOptions = false;
            options2.AllowDraw = true;
            Instance.RegisterImageEditorInfo(EditingFieldEditorNames.Signature, options2, PreviewStringId.EditingFieldEditors_Signature.GetString());
            ImageEditorOptions options3 = new ImageEditorOptions();
            options3.AllowLoadImage = true;
            options3.AllowChangeSizeOptions = true;
            options3.AllowDraw = true;
            Instance.RegisterImageEditorInfo(EditingFieldEditorNames.ImageAndSignature, options3, PreviewStringId.EditingFieldEditors_ImageAndSignature.GetString());
        }

        private EditingFieldExtensions()
        {
        }

        public IEnumerable<InplaceEditorInfoBase> GetImageInplaceEditors()
        {
            Func<InplaceEditorInfoBase, bool> predicate = <>c.<>9__17_0;
            if (<>c.<>9__17_0 == null)
            {
                Func<InplaceEditorInfoBase, bool> local1 = <>c.<>9__17_0;
                predicate = <>c.<>9__17_0 = x => x.EditingFieldType == EditingFieldType.Image;
            }
            return this.infoCollection.Where<InplaceEditorInfoBase>(predicate).ToArray<InplaceEditorInfoBase>();
        }

        public IEnumerable<InplaceEditorInfoBase> GetTextInplaceEditors()
        {
            Func<InplaceEditorInfoBase, bool> predicate = <>c.<>9__16_0;
            if (<>c.<>9__16_0 == null)
            {
                Func<InplaceEditorInfoBase, bool> local1 = <>c.<>9__16_0;
                predicate = <>c.<>9__16_0 = x => x.EditingFieldType == EditingFieldType.Text;
            }
            return this.infoCollection.Where<InplaceEditorInfoBase>(predicate).ToArray<InplaceEditorInfoBase>();
        }

        public bool RegisterEditorInfo(InplaceEditorInfoBase editorInfo)
        {
            Guard.ArgumentNotNull(editorInfo, "editorInfo");
            if (this.EditorInfoCollection.Any<InplaceEditorInfoBase>(x => (x.EditorName == editorInfo.EditorName)) || !EditingFieldEditorNameExtensions.Instance.RegisterEditorName(editorInfo.EditorName, editorInfo.DisplayName))
            {
                return false;
            }
            this.infoCollection.Add(editorInfo);
            return true;
        }

        public bool RegisterEditorInfo(string editorName, string category, string editorDisplayName = null)
        {
            Guard.ArgumentIsNotNullOrEmpty(editorName, "editorName");
            Guard.ArgumentIsNotNullOrEmpty(category, "category");
            InplaceEditorInfo editorInfo = new InplaceEditorInfo(editorName, category, editorDisplayName);
            return this.RegisterEditorInfo(editorInfo);
        }

        public bool RegisterImageCollectionEditorInfo(string editorName, IEnumerable<Image> images, bool sizeOptionsEnabled = false, string displayName = null)
        {
            ImageCollectionEditorInfo editorInfo = new ImageCollectionEditorInfo(editorName, images, sizeOptionsEnabled, displayName);
            return this.RegisterEditorInfo(editorInfo);
        }

        public bool RegisterImageCollectionEditorInfo(string editorName, IDictionary<string, Image> images, bool searchEnabled = true, bool sizeOptionsEnabled = false, string displayName = null)
        {
            ImageCollectionEditorInfo editorInfo = new ImageCollectionEditorInfo(editorName, images, searchEnabled, sizeOptionsEnabled, displayName);
            return this.RegisterEditorInfo(editorInfo);
        }

        public bool RegisterImageEditorInfo(string editorName, ImageEditorOptions options, string displayName = null)
        {
            Guard.ArgumentIsNotNullOrEmpty(editorName, "editorName");
            return this.RegisterEditorInfo(new ImageInplaceEditorInfo(editorName, options, displayName));
        }

        public bool RegisterTextEditorInfo(string editorName, string category, string editorDisplayName = null)
        {
            Guard.ArgumentIsNotNullOrEmpty(editorName, "editorName");
            Guard.ArgumentIsNotNullOrEmpty(category, "category");
            InplaceEditorInfo editorInfo = new InplaceEditorInfo(editorName, category, editorDisplayName);
            return this.RegisterEditorInfo(editorInfo);
        }

        public bool UnregisterEditorInfo(InplaceEditorInfoBase editorInfo)
        {
            if ((editorInfo == null) || (!this.EditorInfoCollection.Contains<InplaceEditorInfoBase>(editorInfo) || !EditingFieldEditorNameExtensions.Instance.UnregisterEditorName(editorInfo.EditorName)))
            {
                return false;
            }
            this.infoCollection.Remove(editorInfo);
            return true;
        }

        public bool UnregisterEditorInfo(string editorName)
        {
            Guard.ArgumentIsNotNullOrEmpty(editorName, "editorName");
            InplaceEditorInfoBase editorInfo = this.EditorInfoCollection.SingleOrDefault<InplaceEditorInfoBase>(x => x.EditorName == editorName);
            return this.UnregisterEditorInfo(editorInfo);
        }

        public static DevExpress.Xpf.Printing.EditingFieldExtensions Instance =>
            instance ??= new DevExpress.Xpf.Printing.EditingFieldExtensions();

        public IEnumerable<InplaceEditorInfoBase> EditorInfoCollection =>
            this.infoCollection;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DevExpress.Xpf.Printing.EditingFieldExtensions.<>c <>9 = new DevExpress.Xpf.Printing.EditingFieldExtensions.<>c();
            public static Func<InplaceEditorInfoBase, bool> <>9__16_0;
            public static Func<InplaceEditorInfoBase, bool> <>9__17_0;

            internal bool <GetImageInplaceEditors>b__17_0(InplaceEditorInfoBase x) => 
                x.EditingFieldType == EditingFieldType.Image;

            internal bool <GetTextInplaceEditors>b__16_0(InplaceEditorInfoBase x) => 
                x.EditingFieldType == EditingFieldType.Text;
        }
    }
}

