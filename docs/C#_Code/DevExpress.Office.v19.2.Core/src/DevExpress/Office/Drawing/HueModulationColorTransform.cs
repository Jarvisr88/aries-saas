﻿namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Model;
    using System;
    using System.Drawing;

    public class HueModulationColorTransform : ColorTransformValueBase
    {
        public HueModulationColorTransform(int value) : base(value)
        {
        }

        public override Color ApplyTransform(Color color) => 
            ColorHSL.FromColorRGB(color).ApplyHueMod(base.Value).ToRgb();

        public override ColorTransformBase Clone() => 
            new HueModulationColorTransform(base.Value);

        public override bool Equals(object obj)
        {
            HueModulationColorTransform transform = obj as HueModulationColorTransform;
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

