﻿namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Model;
    using System;
    using System.Drawing;

    public class LuminanceOffsetColorTransform : ColorTransformValueBase
    {
        public LuminanceOffsetColorTransform(int value) : base(value)
        {
        }

        public override Color ApplyTransform(Color color) => 
            ColorHSL.FromColorRGB(color).ApplyLuminanceOffset(base.Value).ToRgb();

        public override ColorTransformBase Clone() => 
            new LuminanceOffsetColorTransform(base.Value);

        public override bool Equals(object obj)
        {
            LuminanceOffsetColorTransform transform = obj as LuminanceOffsetColorTransform;
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

