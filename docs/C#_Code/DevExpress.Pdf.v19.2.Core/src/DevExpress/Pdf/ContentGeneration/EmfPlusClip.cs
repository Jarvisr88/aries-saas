namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;
    using System.Drawing;

    public class EmfPlusClip
    {
        private readonly EmfPlusRegion clipRegion;

        public EmfPlusClip(EmfPlusRegion clipRegion)
        {
            this.clipRegion = clipRegion;
        }

        public void ApplyClip(PdfGraphicsCommandConstructor commandConstructor)
        {
            EmfPlusRegionNode regionData = this.clipRegion.RegionData;
            if (EmfPlusRegionComplexityAnalyzer.IsSimple(regionData))
            {
                regionData.Accept(new EmfPlusRegionClipBuilder(commandConstructor));
            }
            else
            {
                using (Region region = EmfPlusNativeRegionBuilder.CreateRegion(regionData))
                {
                    using (Bitmap bitmap = new Bitmap(1, 1))
                    {
                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            commandConstructor.IntersectClipWithoutWorldTransform(region.GetBounds(graphics));
                        }
                    }
                }
            }
        }

        public EmfPlusClip Combine(EmfPlusCombineMode mode, EmfPlusClip newClip) => 
            (mode != EmfPlusCombineMode.CombineModeReplace) ? new EmfPlusClip(this.clipRegion.Combine(mode, newClip.clipRegion)) : newClip;
    }
}

