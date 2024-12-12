namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Media;

    public class EnumMemberInfo
    {
        private Lazy<ImageSource> image;

        public EnumMemberInfo(string value, string description, object id, ImageSource image) : this(value, description, id, image, true, true, nullable)
        {
        }

        public EnumMemberInfo(string value, string description, object id, bool showImage, bool showName, Func<ImageSource> getImage, int? order = new int?())
        {
            this.Name = value;
            this.Description = description;
            this.Id = id;
            this.image = new Lazy<ImageSource>(getImage);
            this.ShowImage = showImage;
            this.ShowName = showName;
            this.Order = order;
        }

        public EnumMemberInfo(string value, string description, object id, ImageSource image, bool showImage, bool showName, int? order = new int?()) : this(value, description, id, showImage, showName, () => image, order)
        {
        }

        public override bool Equals(object obj)
        {
            Func<EnumMemberInfo, object> evaluator = <>c.<>9__31_0;
            if (<>c.<>9__31_0 == null)
            {
                Func<EnumMemberInfo, object> local1 = <>c.<>9__31_0;
                evaluator = <>c.<>9__31_0 = o => o.Id;
            }
            return Equals(this.Id, (obj as EnumMemberInfo).Return<EnumMemberInfo, object>(evaluator, <>c.<>9__31_1 ??= () => null));
        }

        public override int GetHashCode() => 
            this.Id.GetHashCode();

        public override string ToString() => 
            this.Name.ToString();

        public string Name { get; private set; }

        public bool ShowName { get; private set; }

        public object Id { get; private set; }

        public string Description { get; private set; }

        public ImageSource Image =>
            this.image.Value;

        public bool ShowImage { get; private set; }

        public int? Order { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EnumMemberInfo.<>c <>9 = new EnumMemberInfo.<>c();
            public static Func<EnumMemberInfo, object> <>9__31_0;
            public static Func<object> <>9__31_1;

            internal object <Equals>b__31_0(EnumMemberInfo o) => 
                o.Id;

            internal object <Equals>b__31_1() => 
                null;
        }
    }
}

