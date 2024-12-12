namespace DevExpress.Xpf.Utils.Native
{
    using System;
    using System.IO;

    public class StreamProvider
    {
        public virtual Stream GetStream(object source)
        {
            Stream stream = source as Stream;
            if (source is Uri)
            {
                stream = this.GetStream(source as Uri);
            }
            else if (source is string)
            {
                stream = this.GetStream(source as string);
            }
            return stream;
        }

        protected virtual Stream GetStream(string path) => 
            new FileStream(path, FileMode.Open, FileAccess.Read);

        protected virtual Stream GetStream(Uri source) => 
            new ResourceLocator(source).LoadStreamSync();
    }
}

