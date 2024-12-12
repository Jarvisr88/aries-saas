namespace DevExpress.Office.Model
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class NativeImageId : IUniqueImageId
    {
        private readonly int imageHashCode;
        private readonly WeakReference imageRef;

        public NativeImageId(Image img)
        {
            Guard.ArgumentNotNull(img, "img");
            this.imageHashCode = img.GetHashCode();
            this.imageRef = new WeakReference(img);
        }

        public override bool Equals(object obj)
        {
            NativeImageId b = obj as NativeImageId;
            return ((obj != null) ? EqualsCore(this, b) : false);
        }

        private static bool EqualsCore(NativeImageId a, NativeImageId b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }
            Image target = (Image) a.imageRef.Target;
            Image objB = (Image) b.imageRef.Target;
            return (((target != null) || (objB != null)) ? ReferenceEquals(target, objB) : false);
        }

        public override int GetHashCode() => 
            this.imageHashCode;
    }
}

