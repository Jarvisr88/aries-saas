﻿namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Model;
    using System;
    using System.Drawing;

    public class SaturationModulationColorTransform : ColorTransformValueBase
    {
        public SaturationModulationColorTransform(int value) : base(value)
        {
        }

        public override Color ApplyTransform(Color color) => 
            ColorHSL.FromColorRGB(color).ApplySaturationMod(base.Value).ToRgb();

        public override ColorTransformBase Clone() => 
            new SaturationModulationColorTransform(base.Value);

        public override bool Equals(object obj)
        {
            SaturationModulationColorTransform transform = obj as SaturationModulationColorTransform;
            return ((transform != null) && (transform.Value == base.Value));
        }

        public override int GetHashCode() => 
            base.GetType().GetHashCode() ^ base.Value;

        public override void Visit(IColorTransformVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

