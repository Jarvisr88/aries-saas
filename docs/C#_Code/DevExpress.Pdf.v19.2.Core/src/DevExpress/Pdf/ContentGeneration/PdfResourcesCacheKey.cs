namespace DevExpress.Pdf.ContentGeneration
{
    using System;

    public class PdfResourcesCacheKey
    {
        private WeakReference weakReference;
        private int keyHashCode;

        public PdfResourcesCacheKey(object key)
        {
            this.weakReference = new WeakReference(key);
            this.keyHashCode = key.GetHashCode();
        }

        public override bool Equals(object obj) => 
            (obj != null) && (!(obj.GetType() != base.GetType()) && (this.weakReference.Target == ((PdfResourcesCacheKey) obj).weakReference.Target));

        public override int GetHashCode() => 
            this.keyHashCode;

        public bool IsAlive =>
            this.weakReference.IsAlive;
    }
}

