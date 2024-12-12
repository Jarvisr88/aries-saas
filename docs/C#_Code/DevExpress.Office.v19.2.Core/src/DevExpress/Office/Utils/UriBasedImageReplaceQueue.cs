namespace DevExpress.Office.Utils
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;

    public class UriBasedImageReplaceQueue
    {
        private List<UriBasedImageStreamPair> list = new List<UriBasedImageStreamPair>();
        private IBatchUpdateable owner;

        public UriBasedImageReplaceQueue(IBatchUpdateable owner)
        {
            this.owner = owner;
        }

        public void Add(UriBasedOfficeImage image, Stream stream)
        {
            List<UriBasedImageStreamPair> list = this.list;
            lock (list)
            {
                this.list.Add(new UriBasedImageStreamPair(image, stream));
            }
        }

        public bool ForceImageProcess(UriBasedOfficeImage image)
        {
            Stream imageStream = null;
            List<UriBasedImageStreamPair> list = this.list;
            lock (list)
            {
                int index = this.IndexOf(image);
                if (index >= 0)
                {
                    imageStream = this.list[index].Stream;
                    this.list.RemoveAt(index);
                }
                else
                {
                    return false;
                }
            }
            this.owner.BeginUpdate();
            try
            {
                image.ReplaceInnerImage(imageStream);
            }
            finally
            {
                this.owner.EndUpdate();
            }
            return true;
        }

        private int IndexOf(UriBasedOfficeImage image)
        {
            for (int i = 0; i < this.list.Count; i++)
            {
                UriBasedImageStreamPair pair = this.list[i];
                if (ReferenceEquals(pair.Image, image))
                {
                    return i;
                }
            }
            return -1;
        }

        public void ProcessRegisteredImages()
        {
            List<UriBasedImageStreamPair> list;
            List<UriBasedImageStreamPair> list2 = this.list;
            lock (list2)
            {
                if (this.list.Count != 0)
                {
                    list = new List<UriBasedImageStreamPair>(this.list);
                    this.list.Clear();
                }
                else
                {
                    return;
                }
            }
            try
            {
                this.owner.BeginUpdate();
                for (int i = 0; i < list.Count; i++)
                {
                    UriBasedImageStreamPair pair = list[i];
                    pair.Image.ReplaceInnerImage(pair.Stream);
                }
            }
            finally
            {
                this.owner.EndUpdate();
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct UriBasedImageStreamPair
        {
            private UriBasedOfficeImage image;
            private System.IO.Stream stream;
            public UriBasedImageStreamPair(UriBasedOfficeImage image, System.IO.Stream stream)
            {
                this.image = image;
                this.stream = stream;
            }

            public UriBasedOfficeImage Image =>
                this.image;
            public System.IO.Stream Stream =>
                this.stream;
        }
    }
}

