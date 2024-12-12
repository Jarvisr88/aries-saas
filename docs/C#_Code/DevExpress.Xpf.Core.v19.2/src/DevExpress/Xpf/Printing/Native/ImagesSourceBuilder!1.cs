namespace DevExpress.Xpf.Printing.Native
{
    using System;

    public abstract class ImagesSourceBuilder<TSource> : ImagesSourceBuilder where TSource: ImageSource
    {
        private readonly TSource source;

        protected ImagesSourceBuilder(TSource source)
        {
            this.source = source;
        }

        protected TSource Source =>
            this.source;
    }
}

