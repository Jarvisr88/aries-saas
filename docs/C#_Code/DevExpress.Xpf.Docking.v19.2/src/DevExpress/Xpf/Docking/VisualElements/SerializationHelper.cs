namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.IO;

    internal class SerializationHelper
    {
        private DockLayoutManager Container;
        private Stream memoryStream;

        public SerializationHelper(DockLayoutManager container)
        {
            this.Container = container;
        }

        public void RestoreLayout()
        {
            if (this.Container != null)
            {
                if (WindowHelper.IsXBAP)
                {
                    this.RestoreLayoutFromStream(this.memoryStream);
                }
                else
                {
                    this.RestoreLayoutFromFile();
                }
            }
        }

        private void RestoreLayoutFromFile()
        {
            OpenFileDialogService service1 = new OpenFileDialogService();
            service1.DefaultFileName = "Layout";
            service1.Filter = "*.xml|*.xml";
            service1.Window = WindowServiceHelper.GetWindow(this.Container);
            IOpenFileDialogService service = service1;
            if (service.ShowDialog())
            {
                this.Container.RestoreLayoutFromXml(service.GetFullFileName());
            }
        }

        private void RestoreLayoutFromStream(Stream stream)
        {
            if (stream != null)
            {
                stream.Seek(0L, SeekOrigin.Begin);
                this.Container.RestoreLayoutFromStream(stream);
            }
        }

        public void SaveLayout()
        {
            if (this.Container != null)
            {
                if (WindowHelper.IsXBAP)
                {
                    this.SaveLayoutToStream();
                }
                else
                {
                    this.SaveLayoutToFile();
                }
            }
        }

        private void SaveLayoutToFile()
        {
            SaveFileDialogService service1 = new SaveFileDialogService();
            service1.DefaultFileName = "Layout";
            service1.DefaultExt = ".xml";
            service1.Filter = "*.xml|*.xml";
            service1.Window = WindowServiceHelper.GetWindow(this.Container);
            ISaveFileDialogService service = service1;
            if (service.ShowDialog(null, null))
            {
                this.Container.SaveLayoutToXml(service.GetFullFileName());
            }
        }

        private void SaveLayoutToStream()
        {
            Ref.Dispose<Stream>(ref this.memoryStream);
            this.memoryStream = new MemoryStream();
            this.Container.SaveLayoutToStream(this.memoryStream);
        }
    }
}

