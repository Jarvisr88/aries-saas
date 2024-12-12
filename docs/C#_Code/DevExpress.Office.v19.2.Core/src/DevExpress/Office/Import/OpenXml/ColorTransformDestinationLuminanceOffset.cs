﻿namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class ColorTransformDestinationLuminanceOffset : ColorTransformDestinationBase
    {
        public ColorTransformDestinationLuminanceOffset(DestinationAndXmlBasedImporter importer, DrawingColor color) : base(importer, color)
        {
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            base.Color.Transforms.AddCore(new LuminanceOffsetColorTransform(base.GetIntegerValue(reader)));
        }
    }
}

