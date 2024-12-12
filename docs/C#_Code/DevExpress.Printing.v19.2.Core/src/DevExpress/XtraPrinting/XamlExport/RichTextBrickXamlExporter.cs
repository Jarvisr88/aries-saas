namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.XtraPrinting;
    using System;

    internal class RichTextBrickXamlExporter : PanelBrickXamlExporter
    {
        private unsafe float[] GetCorrectPadding(BrickStyle brickStyle)
        {
            PaddingInfo info = brickStyle.Padding.Scale(96f / brickStyle.Padding.Dpi);
            float[] thicknessValues = GetThicknessValues(brickStyle.Sides, brickStyle.BorderWidth);
            float* singlePtr1 = thicknessValues;
            singlePtr1[0] += info.Left;
            float* singlePtr2 = &(thicknessValues[1]);
            singlePtr2[0] += info.Top;
            float* singlePtr3 = &(thicknessValues[2]);
            singlePtr3[0] += info.Right;
            float* singlePtr4 = &(thicknessValues[3]);
            singlePtr4[0] += info.Bottom;
            return thicknessValues;
        }

        protected override void WriteBrickToXamlCore(XamlWriter writer, VisualBrick brick, XamlExportContext exportContext)
        {
            writer.WriteAttribute(XamlAttribute.Padding, this.GetCorrectPadding(brick.Style));
            if (!brick.NoClip)
            {
                writer.WriteAttribute(XamlNsPrefix.dxpn, XamlAttribute.VisualHelperClipToBounds, true.ToString());
            }
        }
    }
}

