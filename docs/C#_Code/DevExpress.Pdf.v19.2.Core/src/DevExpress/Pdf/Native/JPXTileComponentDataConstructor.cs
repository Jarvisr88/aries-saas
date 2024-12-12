namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXTileComponentDataConstructor
    {
        private readonly JPXTileComponent[] tileComponents;
        private readonly JPXTileComponentData[] data;

        private JPXTileComponentDataConstructor(JPXTileComponent[] tileComponents)
        {
            this.tileComponents = tileComponents;
            this.data = new JPXTileComponentData[tileComponents.Length];
        }

        private void AddTileComponentData(int componentIndex)
        {
            JPXTileComponent component = this.tileComponents[componentIndex];
            this.data[componentIndex] = new JPXTileComponentData(component.BitsPerComponent, component.Transform());
        }

        private JPXTileComponentData[] Create()
        {
            PdfTask.Execute(new Action<int>(this.AddTileComponentData), this.tileComponents.Length);
            return this.data;
        }

        public static JPXTileComponentData[] Create(JPXTileComponent[] tileComponents) => 
            new JPXTileComponentDataConstructor(tileComponents).Create();
    }
}

